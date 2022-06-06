using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JuniperTaxApp.Core.Clients
{
    public class BaseClient
    {
        protected readonly string BaseAddress;
        private const string JsonContentType = "application/json";
        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            DateTimeZoneHandling = DateTimeZoneHandling.Utc
        };

        public HttpClient Client { get; set; }

        public BaseClient(string baseAddress)
        {
            Client = new HttpClient();

            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonContentType));
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "5da2f821eee4035db4771edab942a4cc");

            BaseAddress = baseAddress;
        }

        protected async Task<TResponse> GetAsync<TResponse>(string url)
        {
            var response = await Client.GetAsync($"{BaseAddress}{url}");

            return await DeserializeContent<TResponse>(response);
        }

        protected async Task<TResponse> PostAsync<TResponse>(string url, object requestObject)
        {
            var content = new StringContent(JsonConvert.SerializeObject(requestObject, JsonSerializerSettings));
            var response = await Client.PostAsync($"{BaseAddress}{url}", content);

            // TODO: Handle error here
            // response.IsSuccessStatusCode == returns T/F
            // or
            // response.EnsureSuccessStatusCode

            return await DeserializeContent<TResponse>(response);
        }

        protected async Task<T> DeserializeContent<T>(HttpResponseMessage responseMessage)
        {
            var json = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json, JsonSerializerSettings);
        }

        protected string BuildRoute(params string[] routes)
        {
            return string.Join("/", routes);
        }
    }
}
