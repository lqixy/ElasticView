using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticView.ApiDomain
{
    public interface IApiContext
    {
        Task<TResult> DeleteAsync<TResult>(string url);
        Task<TResult> GetAsync<TResult>(string url);
        Task<TResult> PostAsync<TResult>(string url, string input = "");
        Task<TResult> PutAsync<TResult>(string url, string input = "");
        Task<TResult> PutAsync<TResult, TInput>(string url, TInput? input = null) 
            where TInput : ElasticSearchApiInput;
    }
}
