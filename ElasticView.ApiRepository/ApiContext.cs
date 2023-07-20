using ElasticView.ApiDomain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ElasticView.ApiRepository
{
    public class ApiContext : IApiContext
    {
        private readonly HttpClient _httpClient;

        public ApiContext(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<TResult> GetAsync<TResult>(string url, string userName = "", string pwd = "")
        {
            //_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue()
            if (!string.IsNullOrWhiteSpace(userName))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.UTF8.GetBytes($"{userName}:{pwd}")));
            }
            try
            {
                using (var response = await _httpClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {

                    }
                    else
                    {
                        throw new Exception($"请示api错误,StatusCode:{response.StatusCode} \r\n ReasonPhrase:{response.ReasonPhrase}");
                    }

                    var content = await response.Content.ReadAsStringAsync();
                    var jsonObject = JsonConvert.DeserializeObject<TResult>(content);
                    return jsonObject;
                }
            }
            catch (HttpRequestException e)
            {
                throw new UserFriendlyException(e.Message);
            }
            catch (JsonSerializationException e)
            {
                throw new UserFriendlyException($"json序列化错误: {e.Message}");
            }
        }

        public async Task<TResult> PostAsync<TResult>(string url, string input = "")
        {
            try
            {
                var stringContent = new StringContent(input);
                using (var response = await _httpClient.PostAsync(url, stringContent))
                {
                    if (response.IsSuccessStatusCode)
                    {

                    }
                    else
                    {
                        throw new UserFriendlyException($"请求api错误,StatusCode:{response.StatusCode} \r\n ReasonPhrase:{response.ReasonPhrase}");
                    }

                    var content = await response.Content.ReadAsStringAsync();
                    var jsonObject = JsonConvert.DeserializeObject<TResult>(content);
                    return jsonObject;
                }
            }
            catch (HttpRequestException e)
            {
                throw new UserFriendlyException(e.Message);
            }
            catch (JsonSerializationException e)
            {
                throw new UserFriendlyException($"json序列化错误: {e.Message}");
            }
        }


        public async Task<TResult> PutAsync<TResult>(string url, string input="")
        {
            var stringContent = string.IsNullOrWhiteSpace(input) 
                ? null : new StringContent($"{input}", Encoding.UTF8);
            using (var response = await _httpClient.PutAsync(url, stringContent))
            {
                if (response.IsSuccessStatusCode)
                {

                }
                else
                {
                    throw new UserFriendlyException($"请求api错误,StatusCode:{response.StatusCode} \r\n ReasonPhrase:{response.ReasonPhrase}");
                }

                var content = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<TResult>(content);
                return jsonObject;
            }
        }

        public async Task<TResult> DeleteAsync<TResult>(string url)
        {
            try
            {
                using (var response = await _httpClient.DeleteAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {

                    }
                    else
                    {
                        throw new UserFriendlyException($"请求api错误,StatusCode:{response.StatusCode} \r\n ReasonPhrase:{response.ReasonPhrase}");
                    }

                    var content = await response.Content.ReadAsStringAsync();
                    var jsonObject = JsonConvert.DeserializeObject<TResult>(content);
                    return jsonObject;
                }
            }
            catch (HttpRequestException e)
            {
                throw new UserFriendlyException(e.Message);
            }
            catch (JsonSerializationException e)
            {
                throw new UserFriendlyException($"json序列化错误: {e.Message}");
            }
        }


        //public async Task<TResult> GetAuthAsync<TResult>(string url)
        //{

        //}
    }
}
