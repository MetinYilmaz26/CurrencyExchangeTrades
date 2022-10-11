using CurrencyExchangeTrades.Domain.Entity;

namespace CurrencyExchangeTrades.DataAccess.Repository.Interfaces
{
    public interface ICurrencySymbolRepository : IBaseRepository<CurrencySymbol>
    {
        List<string> GetExistSymbols();
        int GetIdFromName(string name);
    }
}
