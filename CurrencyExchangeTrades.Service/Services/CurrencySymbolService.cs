using AutoMapper;
using CurrencyExchangeTrades.DataAccess.Repository.Interfaces;
using CurrencyExchangeTrades.Domain.Entity;
using CurrencyExchangeTrades.Service.Dto;
using CurrencyExchangeTrades.Service.ExceptionHandler;
using CurrencyExchangeTrades.Service.Interfaces;
using CurrencyExchangeTrades.Service.Shared.Exchange.Dto;
using CurrencyExchangeTrades.Service.Shared.Exchange.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Serilog;

namespace CurrencyExchangeTrades.Service.Services
{
    public class CurrencySymbolService : BaseService<CurrencySymbol, CurrencySymbolDto>, ICurrencySymbolService
    {
        private readonly ICurrencySymbolRepository _currencySymbolRepository;
        private readonly IExchangeService _exchangeService;
        private readonly IMemoryCache _memoryCache;
        public CurrencySymbolService(ICurrencySymbolRepository repository, IExchangeService exchangeService, IMemoryCache memoryCache, IMapper mapper) : base(repository, mapper)
        {
            _exchangeService = exchangeService;
            _currencySymbolRepository = repository;
            _memoryCache = memoryCache;
        }

        public int GetIdFromName(string name)
        {
            Log.Information("Get symbol by id.");
            int returnId = _currencySymbolRepository.GetIdFromName(name);
            return returnId == -1 ? throw new NoSymbolException() : returnId;
        }
        public string GetNameFromId(int id)
        {
            Log.Information("Get symbol by name.");
            var returnSymbol = _currencySymbolRepository.Get(id).Result;
            return returnSymbol == null  ? throw new NoSymbolException() : returnSymbol.Symbol;
        }

        public async Task<List<CurrencySymbolDto>> GetSymbols()
        {
            if (_memoryCache.TryGetValue("Symbol", out List<CurrencySymbolDto> _storedSymbols))
            {
                return _storedSymbols;
            }
            _storedSymbols = await GetAll();
            _ = _memoryCache.Set("Symbol", _storedSymbols, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddDays(1),
                Priority = CacheItemPriority.Normal
            });
            return _storedSymbols;

        }

        public void SaveSymbols()
        {
            List<string> storedSymbols = _currencySymbolRepository.GetExistSymbols();
            Symbol exchangeSymbol = _exchangeService.GetSymbols();
            List<CurrencySymbol> symbols = new();
            if (exchangeSymbol.Symbols != null)
            {
                symbols.AddRange(exchangeSymbol.Symbols.Select(symbolItem => new CurrencySymbol() { Symbol = symbolItem.Key, Definition = symbolItem.Value, CreateTime = DateTime.Now }));
                CurrencySymbol[] newSymbols = symbols.Where(x => !storedSymbols.Contains(x.Symbol)).ToArray();

                if (newSymbols != null && newSymbols.Length > 0)
                {
                    _ = _currencySymbolRepository.AddRange(newSymbols);
                    Log.Information("Symbol saved.");
                }
            }
        }
    }
}
