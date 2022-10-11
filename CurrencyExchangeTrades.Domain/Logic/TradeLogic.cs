namespace CurrencyExchangeTrades.Domain.Logic
{
    public class TradeLogic : ITradeLogic
    {
        public bool CheckClientRule(int countPerHour)
        {
            return countPerHour <= 10;
        }
    }
}
