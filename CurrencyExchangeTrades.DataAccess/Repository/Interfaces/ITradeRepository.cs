using CurrencyExchangeTrades.Domain.Entity;

namespace CurrencyExchangeTrades.DataAccess.Repository.Interfaces
{
    public interface ITradeRepository : IBaseRepository<Trade>
    {
        List<Trade> GetLastHourTrades(int clientId);
    }
}
