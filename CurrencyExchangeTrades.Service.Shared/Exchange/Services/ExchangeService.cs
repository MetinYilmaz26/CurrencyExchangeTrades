using CurrencyExchangeTrades.Service.Shared.Exchange.Dto;
using CurrencyExchangeTrades.Service.Shared.Exchange.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CurrencyExchangeTrades.Service.Shared.Exchange.Services
{
    public class ExchangeService : IExchangeService
    {
        private readonly IConfiguration _configuration;
        private readonly IRestService _restService;
        private readonly string _exchangeAPIUrl;
        private readonly string _exchangeAPIKey;
        public ExchangeService(IConfiguration configuration, IRestService restService)
        {
            _configuration = configuration;
            _exchangeAPIUrl = _configuration["ExchangeAPI:Url"];
            _exchangeAPIKey = _configuration["ExchangeAPI:Key"];
            _restService = restService;
        }

        public Symbol GetSymbols()
        {
            Dictionary<string, string> headers = new()
            {
                { "apikey", _exchangeAPIKey }
            };
            Symbol symbol;
            _restService.GetResponseContext(_exchangeAPIUrl, "/symbols", headers, out symbol);

            return symbol;
        }
        public ExchangeRate GetLatestExchangeRate(string fromCurrency)
        {
            Dictionary<string, string> headers = new()
            {
                { "apikey", _exchangeAPIKey }
            };
            ExchangeRate exchangeRate;
            _restService.GetResponseContext(_exchangeAPIUrl, $"/latest?base={fromCurrency}", headers, out exchangeRate);

            return exchangeRate;
        }
    }
}