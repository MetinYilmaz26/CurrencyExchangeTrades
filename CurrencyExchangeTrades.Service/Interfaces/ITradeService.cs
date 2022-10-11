using CurrencyExchangeTrades.Domain.Entity;
using CurrencyExchangeTrades.Service.Dto;

namespace CurrencyExchangeTrades.Service.Interfaces
{
    public interface ITradeService : IBaseService<Trade, TradeDto>
    {
        Task<string> SetTrade(TradeDto trade); 
        Task<string> SetTrade(TradeInputDto trade);
        List<Trade> GetLastHourTrades(int clientId);
    }
}
