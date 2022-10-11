namespace CurrencyExchangeTrades.Domain.Logic
{
    public interface ITradeLogic
    {
        bool CheckClientRule(int countPerHour);
    }
}
