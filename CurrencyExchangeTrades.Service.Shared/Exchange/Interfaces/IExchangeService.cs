using CurrencyExchangeTrades.Service.Shared.Exchange.Dto;

namespace CurrencyExchangeTrades.Service.Shared.Exchange.Interfaces
{
    public interface IExchangeService
    {
        Symbol GetSymbols();
        ExchangeRate GetLatestExchangeRate(string fromCurrency);
    }
}
