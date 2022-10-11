namespace CurrencyExchangeTrades.Service.ExceptionHandler
{
    public class NoValueException : Exception
    {
        public NoValueException():base(ErrorMessages.NoValue)
        {
        }
    }
    public class NoValidValueException : Exception
    {
        public NoValidValueException():base(ErrorMessages.NoValidValue)
        {
        }
    }
    public class ReachedMaximumException : Exception
    {
        public ReachedMaximumException() : base(ErrorMessages.ReachedMaximum)
        {
        }
    }
    public class NoSymbolException : Exception
    {
        public NoSymbolException() : base(ErrorMessages.NoSymbol)
        {
        }
    }
    public class NoSuccessException : Exception
    {
        public NoSuccessException() : base(ErrorMessages.NoSuccess)
        {
        }
    }
    public class InvalidCurrencyException : Exception
    {
        public InvalidCurrencyException() : base(ErrorMessages.InvalidCurrency)
        {
        }
    }
}
