using CurrencyExchangeTrades.DataAccess.Repository.Interfaces;
using CurrencyExchangeTrades.Domain.Entity;

namespace CurrencyExchangeTrades.DataAccess.Repository.Repositories
{
    public class TradeRepository : BaseRepository<Trade>, ITradeRepository
    {
        public CurrencyExchangeTradesDBContext _currencyExchangeTradesDBContext { get; set; }
        public TradeRepository(CurrencyExchangeTradesDBContext currencyExchangeTradesDBContext) : base(currencyExchangeTradesDBContext)
        {
            _currencyExchangeTradesDBContext = currencyExchangeTradesDBContext;
        }

        public override async Task<Trade> Get(int id, string[]? navigationPropertyPaths = null)
        {
            return await base.Get(id, new string[] { "From", "To" });
        }
        public override async Task<List<Trade>> GetAll(string[]? navigationPropertyPaths = null)
        {
            return await base.GetAll(new string[] { "From", "To" });
        }

        public List<Trade> GetLastHourTrades(int clientId)
        {
            return _currencyExchangeTradesDBContext.Trades.Where(x => x.ClientId == clientId && x.TradeDate >= DateTime.Now.AddHours(-1)).ToList();
        }

    }
}
