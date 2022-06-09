using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using JuniperTaxApp.Core.Exceptions;
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

            return await HandleResponse<TResponse>(response);
        }

        protected async Task<TResponse> PostAsync<TResponse>(string url, object requestObject)
        {
            var content = new StringContent(JsonConvert.SerializeObject(requestObject, JsonSerializerSettings));
            var response = await Client.PostAsync($"{BaseAddress}{url}", content);

            return await HandleResponse<TResponse>(response);
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

        protected async Task<T> HandleResponse<T>(HttpResponseMessage responseMessage)
        {
            if (responseMessage.IsSuccessStatusCode)
            {
                return await DeserializeContent<T>(responseMessage);
            }
            else
            {
                throw new BasicAPIClientException(responseMessage);
            }
        }
    }
}
