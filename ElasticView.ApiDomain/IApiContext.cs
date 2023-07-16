using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticView.ApiDomain
{
    public interface IApiContext
    {
        Task<TResult> GetAsync<TResult>(string url, string userName = "", string pwd = "");
    }
}
