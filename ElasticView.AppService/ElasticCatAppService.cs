using ElasticView.ApiDomain;
using ElasticView.AppService.Contracts;
using ElasticView.AppService.Contracts.InputDto;
using ElasticView.AppService.Contracts.Output;
using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticView.AppService
{
    public class ElasticCatAppService : IElasticCatAppService
    {

        private readonly IApiContext _apiContext;

        public ElasticCatAppService(IApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public async Task<IEnumerable<IndexAliasOutput>> GetAliases(string url)
        {
            url = $"{url}/_cat/aliases?format=json";

            var records = await _apiContext.GetAsync<IEnumerable<CatAliasesRecordOutput>>(url);

            if (records is null || !records.Any()) return Enumerable.Empty<IndexAliasOutput>();

            var result = records.GroupBy(x => x.Index)
                  .ToDictionary(k => k.Key, v => v.Select(vv => vv.Alias).ToArray())
                  .Select(x => new IndexAliasOutput(x.Key, x.Value))
                  ;
            return result;
        }

        //public async Task<IEnumerable<IndexAliasOutput>> GetAliases(string url)
        //{
        //    var client = GetClient(url);
        //    var response = await client.Cat.AliasesAsync();
        //    if (!response.IsValid) return Enumerable.Empty<IndexAliasOutput>();

        //    var result = response.Records.GroupBy(x => x.Index)
        //          .ToDictionary(k => k.Key, v => v.Select(vv => vv.Alias).ToArray())
        //          .Select(x => new IndexAliasOutput(x.Key, x.Value))
        //          ;
        //    return result;
        //}

        public async Task<IEnumerable<CatNodesOutput>> GetNodes(string url)
        {
            try
            {
                var queryString = "ip,name,heap.percent,heap.current,heap.max,ram.percent,ram.current,ram.max,node.role,master,cpu,load_1m,load_5m,load_15m,disk.used_percent,disk.used,disk.total,port";

                url = $"{url}/_cat/nodes?format=json&h={queryString}";
                var res = await _apiContext.GetAsync<IEnumerable<CatNodesOutput>>(url);
                return res;
            }
            catch (Exception e)
            {
                return Enumerable.Empty<CatNodesOutput>();
            }
            //var str = await res.Content.ReadAsStringAsync();
            //var client = GetClient(url);
            //var response = await client.Cat.NodesAsync(new CatNodesRequest
            //{
            //    Headers = new string[] { queryString }
            //});

            //if (!response.IsValid) return Enumerable.Empty<CatNodesOutput>();
            //var json = JsonConvert.SerializeObject(response.Records, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            //var result = JsonConvert.DeserializeObject<IEnumerable<CatNodesOutput>>(json);
            //return result;
        }


        //private ElasticClient GetClient(string url)
        //{
        //    return new ElasticClient(new Uri(url));

        //}
        //private ElasticClient GetClient(string url)
        //{
        //    var settings = new ConnectionSettings(new Uri(input.Url))
        //        .BasicAuthentication(input.UserName, input.Password)
        //        ;
        //    return new ElasticClient(settings);
        //}
    }
}
