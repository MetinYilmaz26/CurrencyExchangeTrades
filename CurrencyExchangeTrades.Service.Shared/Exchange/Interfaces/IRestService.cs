namespace CurrencyExchangeTrades.Service.Shared.Exchange.Interfaces
{
    public interface IRestService
    {
        void GetResponseContext<T>(string url, string api, Dictionary<string, string> headers, out T content);
    }
}
