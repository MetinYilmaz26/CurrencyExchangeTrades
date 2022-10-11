using CurrencyExchangeTrades.Service.Shared.Exchange.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace CurrencyExchangeTrades.Service.Shared.Exchange.Services
{
    public class RestService : IRestService
    {
        public void GetResponseContext<T>(string url, string api, Dictionary<string, string> headers, out T content)
        {
            RestClient client = new(url + api);
            RestRequest request = new();

            foreach (KeyValuePair<string, string> header in headers)
            {
                _ = request.AddHeader(header.Key, header.Value);
            }

            RestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.ErrorMessage, response.ErrorException);
            }
            content = JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}
