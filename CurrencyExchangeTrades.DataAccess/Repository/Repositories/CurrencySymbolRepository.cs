using CurrencyExchangeTrades.DataAccess.Repository.Interfaces;
using CurrencyExchangeTrades.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchangeTrades.DataAccess.Repository.Repositories
{
    public class CurrencySymbolRepository : BaseRepository<CurrencySymbol>, ICurrencySymbolRepository
    {
        public CurrencyExchangeTradesDBContext _currencyExchangeTradesDBContext { get; set; }
        public CurrencySymbolRepository(CurrencyExchangeTradesDBContext currencyExchangeTradesDBContext) : base(currencyExchangeTradesDBContext)
        {
            _currencyExchangeTradesDBContext = _context as CurrencyExchangeTradesDBContext;
        }

        public List<string> GetExistSymbols()
        {
           return _currencyExchangeTradesDBContext.CurrencySymbols.Select(x => x.Symbol).ToList();
        }

        public int GetIdFromName(string name)
        {
            CurrencySymbol? symbol = _currencyExchangeTradesDBContext.CurrencySymbols.FirstOrDefault(x => x.Symbol == name);
            return symbol == null ? -1 : symbol.Id;
        }
    }
}
