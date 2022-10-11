namespace CurrencyExchangeTrades.Service.Shared.Exchange.Dto
{
    public class ExchangeRate
    {
        public bool Success { get; set; }
        public long TimeStamp { get; set; }
        public string? Base { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<string, string>? Rates { get; set; }

    }
}
