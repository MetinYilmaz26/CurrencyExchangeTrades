using AutoMapper;
using CurrencyExchangeTrades.DataAccess.Repository.Interfaces;
using CurrencyExchangeTrades.Domain.Entity;
using CurrencyExchangeTrades.Domain.Logic;
using CurrencyExchangeTrades.Service.Dto;
using CurrencyExchangeTrades.Service.ExceptionHandler;
using CurrencyExchangeTrades.Service.Interfaces;
using CurrencyExchangeTrades.Service.Shared.Exchange.Dto;
using CurrencyExchangeTrades.Service.Shared.Exchange.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Serilog;

namespace CurrencyExchangeTrades.Service.Services
{
    public class TradeService : BaseService<Trade, TradeDto>, ITradeService
    {
        private readonly ITradeRepository _tradeRepository;
        private readonly IExchangeService _exchangeService;
        private readonly IMemoryCache _memoryCache;
        private readonly ICurrencySymbolService _currencySymbolService;
        private readonly ITradeLogic _tradeLogic;
        private readonly IMapper _mapper;

        public TradeService(ITradeRepository repository, IMemoryCache memoryCache, IExchangeService exchangeService, IMapper mapper, ICurrencySymbolService currencySymbolService, ITradeLogic tradeLogic) : base(repository, mapper)
        {
            _tradeRepository = repository;
            _memoryCache = memoryCache;
            _exchangeService = exchangeService;
            _currencySymbolService = currencySymbolService;
            _tradeLogic = tradeLogic;
            _mapper = mapper;
        }

        public List<Trade> GetLastHourTrades(int clientId)
        {
            Log.Information("Get trades of last hour.");
            return _tradeRepository.GetLastHourTrades(clientId);
        }

        public async Task<string> SetTrade(TradeDto trade)
        {
            if (trade.From == null || trade.To == null || trade.From.Length != 3 || trade.To.Length != 3)
            {
                throw new InvalidCurrencyException();
            }
            trade.FromId = _currencySymbolService.GetIdFromName(trade.From);
            trade.ToId = _currencySymbolService.GetIdFromName(trade.To);
            return await SaveTrade(trade);
        }
        public async Task<string> SetTrade(TradeInputDto trade)
        {
            if (trade.From <= 0 || trade.To <= 0)
            {
                throw new InvalidCurrencyException();
            }
            trade.FromSymbol = _currencySymbolService.GetNameFromId(trade.From);
            trade.ToSymbol = _currencySymbolService.GetNameFromId(trade.To);
            return await SaveTrade(_mapper.Map<TradeDto>(trade));
        }

        private async Task<string> SaveTrade(TradeDto trade)
        {
            CheckLogic(trade.ClientId);
            double ChangeRate = SetRate(trade.To, trade.From);
            trade.Rate = ChangeRate;
            int result = await Add(trade);
            if (result == -1)
            {
                throw new NoSuccessException();
            }

            Log.Information("New trade saved.");
            return $"{trade.Amount} {trade.From} was exchanged for {trade.Amount * trade.Rate} {trade.To} at a rate of {trade.Rate}.";
        }


        private double SetRate(string to,string from)
        {
            double ChangeRate = 1;
            string? Rate = GetExchangeRate(from).Rates?.FirstOrDefault(x => x.Key == to).Value;//"EUR"
            if (Rate == null)
            {
                throw new NoValueException();
            }

            try
            {
                ChangeRate = double.Parse(Rate);
            }
            catch (FormatException)
            {
                throw new NoValidValueException();
            }

            return ChangeRate;
        }

        private void CheckLogic(int clientId)
        {
            if (!_tradeLogic.CheckClientRule(GetLastHourTrades(clientId).Count))
            {
                throw new ReachedMaximumException();
            }
        }

        private ExchangeRate GetExchangeRate(string fromCurrency)
        {
            if (fromCurrency == null)
            {
                throw new InvalidCurrencyException();
            }

            if (_memoryCache.TryGetValue(fromCurrency, out ExchangeRate _rate))
            {
                return _rate;
            }
            _rate = _exchangeService.GetLatestExchangeRate(fromCurrency);

            _ = _memoryCache.Set(fromCurrency, _rate, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            });
            Log.Information("New exchanges save to cache.");
            return _rate;

        }

    }
}
