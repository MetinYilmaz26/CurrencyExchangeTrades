namespace CurrencyExchangeTrades.Service.Shared.Exchange.Dto
{
    public class Symbol
    {
        public bool Success { get; set; }
        public Dictionary<string, string>? Symbols { get; set; }
    }
}
