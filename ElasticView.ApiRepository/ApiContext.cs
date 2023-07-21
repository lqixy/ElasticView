using ElasticView.ApiDomain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using Serilog.Core;
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
        private readonly ILogger _logger;

        public ApiContext(HttpClient httpClient,
            ILogger logger)
        {
            _httpClient = httpClient;
            _logger = logger;
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
                    var content = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        _logger.Information($"请求成功.HttpMethod:GET \r\n url: {url}.\r\n 返回内容: {content}\r\n");
                    }
                    else
                    {
                        _logger.Error($"请示api错误,StatusCode:{response.StatusCode} \r\n ReasonPhrase:{response.ReasonPhrase}.\r\n content:{content}");
                        //throw new Exception($"请示api错误,StatusCode:{response.StatusCode} \r\n ReasonPhrase:{response.ReasonPhrase}");
                    }
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
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return default;
            }
        }

        public async Task<TResult> PostAsync<TResult>(string url, string input = "")
        {
            try
            {
                var stringContent = new StringContent(input);
                using (var response = await _httpClient.PostAsync(url, stringContent))
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        _logger.Information($"请求成功.HttpMethod:POST. \r\n  url: {url}. HttpContent: {stringContent}.\r\n 返回内容: {content}");
                    }
                    else
                    {
                        _logger.Error($"请示api错误,StatusCode:{response.StatusCode} \r\n ReasonPhrase:{response.ReasonPhrase}.\r\n content:{content}");
                        //throw new Exception($"请示api错误,StatusCode:{response.StatusCode} \r\n ReasonPhrase:{response.ReasonPhrase}");
                    }

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
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return default;
            }
        }

        public async Task<TResult> PutAsync<TResult, TInput>(string url, TInput? input = null)
            where TInput : ElasticSearchApiInput
        {
            try
            {
                var stringContent = input is null
                    ? null :
                    new StringContent(input.ToJson(), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.PutAsync(url, stringContent))
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        _logger.Information($"请求成功.HttpMethod:PUT \r\n  url: {url}.\r\n 返回内容: {content}");
                    }
                    else
                    {
                        _logger.Error($"请示api错误,StatusCode:{response.StatusCode} \r\n ReasonPhrase:{response.ReasonPhrase}.\r\n content:{content}");
                        //throw new Exception($"请示api错误,StatusCode:{response.StatusCode} \r\n ReasonPhrase:{response.ReasonPhrase}");
                    }

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
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return default;
            }
        }

        public async Task<TResult> PutAsync<TResult>(string url, string input = "")
        {
            try
            {
                var stringContent = string.IsNullOrWhiteSpace(input)
                    ? null : new StringContent($"{input}", Encoding.UTF8, "application/json");
                using (var response = await _httpClient.PutAsync(url, stringContent))
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        _logger.Information($"请求成功.HttpMethod:PUT \r\n  url: {url}. HttpContent: {stringContent}.\r\n 返回内容: {content}");
                    }
                    else
                    {
                        _logger.Error($"请示api错误,StatusCode:{response.StatusCode} \r\n ReasonPhrase:{response.ReasonPhrase}.\r\n content:{content}");
                        //throw new Exception($"请示api错误,StatusCode:{response.StatusCode} \r\n ReasonPhrase:{response.ReasonPhrase}");
                    }

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
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return default;
            }
        }

        public async Task<TResult> DeleteAsync<TResult>(string url)
        {
            try
            {
                using (var response = await _httpClient.DeleteAsync(url))
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        _logger.Information($"请求成功.HttpMethod:DELETE \r\n  url: {url}.\r\n 返回内容: {content}");
                    }
                    else
                    {
                        _logger.Error($"请示api错误,StatusCode:{response.StatusCode} \r\n ReasonPhrase:{response.ReasonPhrase}.\r\n content:{content}");
                        //throw new Exception($"请示api错误,StatusCode:{response.StatusCode} \r\n ReasonPhrase:{response.ReasonPhrase}");
                    }

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
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return default;
            }
        }


        //public async Task<TResult> GetAuthAsync<TResult>(string url)
        //{

        //}
    }
}
