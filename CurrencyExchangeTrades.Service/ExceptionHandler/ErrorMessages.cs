namespace CurrencyExchangeTrades.Service.ExceptionHandler
{
    internal class ErrorMessages
    {
        internal static string NoValue { get; } = "There is no value for this trade!";
        internal static string NoSuccess { get; } = "Trade didn't succeed!";
        internal static string NoValidValue { get; } = "There is no valid value for this trade!";
        internal static string ReachedMaximum { get; } = "This client reached the maximum trades per hour!";
        internal static string NoSymbol { get; } = "Symbol not found!";
        internal static string InvalidCurrency { get; } = "Currency value is invalid!";
    }
}
