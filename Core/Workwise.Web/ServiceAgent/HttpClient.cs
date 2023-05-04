using Newtonsoft.Json;
using System.Text;

namespace Workwise.ServiceAgent
{
    public class HttpClientWrapper : IHttpClient
    {
        public Uri BaseAddress { get; set; }
        public HttpClientWrapper()
        {
            this.BaseAddress = new Uri("https://localhost:44358/api/");
        }

        public T GetAsync<T>(string requestUri)
        {
            T result = default(T);

            using (var client = new HttpClient())
            {
                client.BaseAddress = this.BaseAddress;

                var response = client.GetAsync(requestUri).Result;
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    if (content != null)
                    {
                        result = JsonConvert.DeserializeObject<T>(content);
                    }
                }
            }
            return result;
        }

        public T PostDataAsync<T>(string requestUri, T value)
        {
            T result = default(T);

            using (var client = new HttpClient())
            {
                client.BaseAddress = this.BaseAddress;


                var response = client
                    .PostAsync(
                    requestUri,
                    new StringContent(JsonConvert.SerializeObject(value),
                    Encoding.UTF8,
                    "application/json")
                    ).Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    if (content != null)
                    {
                        result = JsonConvert.DeserializeObject<T>(content);
                    }
                }

            }
            return result;
        }

        public U PostDataAsync<T, U>(string requestUri, T value)
        {
            U result = default(U);

            using (var client = new HttpClient())
            {
                client.BaseAddress = this.BaseAddress;
                var response = client.PostAsJsonAsync(requestUri, value).Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    if (content != null)
                    {
                        result = JsonConvert.DeserializeObject<U>(content);
                    }
                }
            }
            return result;
        }

    }
}
