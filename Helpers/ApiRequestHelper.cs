using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace SurfTicket.Helpers
{
    public class ApiRequestHelper
    {
        private readonly HttpClient _httpClient;
        private static readonly JsonSerializerSettings settings = new()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.None,
            NullValueHandling = NullValueHandling.Ignore,
        };

        public ApiRequestHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromSeconds(60);
        }

        private HttpRequestException GetHttpException(string result, HttpResponseMessage response)
        {
            var exception = new HttpRequestException(response.ReasonPhrase);
            exception.Data["errorDetail"] = result;

            return exception;
        }

        private void AddHeaders(HttpRequestMessage request, Dictionary<string, string>? headers)
        {
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
        }

        public async Task<T?> GetAsync<T>(string url, Dictionary<string, string>? headers = null)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                AddHeaders(request, headers);
                using (var response = await _httpClient.SendAsync(request))
                {
                    var result = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<T>(result, settings);
                    }
                    else
                    {
                        throw GetHttpException(result, response);
                    }
                }
            }
        }

        public async Task<T?> PostAsync<T>(string url, object data, Dictionary<string, string>? headers = null)
        {
            var jsonContent = JsonConvert.SerializeObject(data, settings);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            using (var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = content })
            {
                AddHeaders(request, headers);
                using (var response = await _httpClient.SendAsync(request))
                {
                    var result = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<T>(result, settings);
                    }
                    else
                    {
                        throw GetHttpException(result, response);
                    }
                }
            }
        }

        public async Task<T?> PutAsync<T>(string url, object data, Dictionary<string, string>? headers = null)
        {
            var jsonContent = JsonConvert.SerializeObject(data, settings);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            using (var request = new HttpRequestMessage(HttpMethod.Put, url) { Content = content })
            {
                AddHeaders(request, headers);
                using (var response = await _httpClient.SendAsync(request))
                {
                    var result = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<T>(result, settings);
                    }
                    else
                    {
                        throw GetHttpException(result, response);
                    }
                }
            }
        }

        public async Task<T?> DeleteAsync<T>(string url, object data, Dictionary<string, string>? headers = null)
        {
            var jsonContent = JsonConvert.SerializeObject(data, settings);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            using (var request = new HttpRequestMessage(HttpMethod.Delete, url) { Content = content })
            {
                AddHeaders(request, headers);
                using (var response = await _httpClient.SendAsync(request))
                {
                    var result = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<T>(result, settings);
                    }
                    else
                    {
                        throw GetHttpException(result, response);
                    }
                }
            }
        }
    }
}
