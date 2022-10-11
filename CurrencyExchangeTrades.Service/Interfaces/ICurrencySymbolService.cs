using CurrencyExchangeTrades.Domain.Entity;
using CurrencyExchangeTrades.Service.Dto;

namespace CurrencyExchangeTrades.Service.Interfaces
{
    public interface ICurrencySymbolService : IBaseService<CurrencySymbol, CurrencySymbolDto>
    {
        Task<List<CurrencySymbolDto>> GetSymbols();
        void SaveSymbols();
        int GetIdFromName(string Name);
        string GetNameFromId(int id);
    }
}
