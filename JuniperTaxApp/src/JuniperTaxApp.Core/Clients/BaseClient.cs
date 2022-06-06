using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JuniperTaxApp.Core.Clients
{
    public class BaseClient
    {
        protected readonly string _baseAddress;
        const string JsonContentType = "application/json";

        static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            DateTimeZoneHandling = DateTimeZoneHandling.Utc
        };

        public HttpClient Client { get; set; }

        public BaseClient(string baseAddress)
        {
            Client = new HttpClient();

            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonContentType));

            _baseAddress = baseAddress;
        }

        protected async Task<TResponse> GetAsync<TResponse>(string url)
        {
            var response = await Client.GetAsync($"{_baseAddress}{url}");

            return await DeserializeContent<TResponse>(response);
        }

        protected async Task<TResponse> PostAsync<TResponse>(string url, object requestObject)
        {
            var content = new StringContent(JsonConvert.SerializeObject(requestObject, _jsonSerializerSettings));
            var response = await Client.PostAsync($"{_baseAddress}{url}", content);

            return await DeserializeContent<TResponse>(response);
        }

        protected async Task<T> DeserializeContent<T>(HttpResponseMessage responseMessage)
        {
            var json = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json, _jsonSerializerSettings);
        }

        protected string BuildRoute(params string[] routes)
        {
            return string.Join("/", routes);
        }
    }
}
