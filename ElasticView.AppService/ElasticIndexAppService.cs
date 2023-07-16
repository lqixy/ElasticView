using ElasticView.AppService.Contracts;
using ElasticView.AppService.Contracts.InputDto;
using ElasticView.AppService.Contracts.Output;
using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ElasticView.AppService
{
    public class ElasticIndexAppService : IElasticIndexAppService
    {
        public async Task CreateIndex(string url, CreateIndexInput indexInput)
        {
            var client = GetClient(url);

            var response = await client.Indices.CreateAsync(indexInput.Name, req => req.Settings(s =>
                 s.NumberOfShards(indexInput.ShardsCount)
                 .NumberOfReplicas(indexInput.ReplicasCount)
                 ));
        }

        //public async Task Get(string url)
        //{
        //    var client = GetClient(url);
        //    var response = await client.Cat.IndicesAsync(new CatIndicesRequest
        //    {
        //        Headers = new string[] { "index,health,status,uuid,pri,rep,docs.count,store.size" }
        //    });

        //    response.Records.Select(x => new IndexInfoDto(x.UUID,x.Primary,x.PrimaryStoreSize,x.));
        //}

        /// <summary>
        /// 获取别名
        /// </summary>
        /// <param name="url"></param>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public async Task GetAlias(string url, string indexName)
        {
            var client = GetClient(url);
            var response = await client.Indices.GetAliasAsync(indexName);
            //response.Indices.Select(x=>x.Value.)
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="url"></param>
        ///// <param name="aliases"></param>
        ///// <returns></returns>
        //public async Task<IEnumerable<IndexAliasOutput>> GetAliases(string url, IEnumerable<string> aliases)
        //{
        //    if (!aliases.Any()) return Enumerable.Empty<IndexAliasOutput>();

        //    var client = GetClient(url);

        //    var part = Partitioner.Create(aliases);
        //    var bags = new ConcurrentBag<IndexAliasOutput>();
        //    Parallel.ForEach(part, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount },
        //        async x =>
        //        {
        //            //var response = client.Indices.GetAlias(x);
        //            var response = await client.Indices.GetAliasAsync(x);
        //            //response.Indices.Values.Select(x=>x.Aliases)

        //            bags.Add(new IndexAliasOutput());
        //        });

        //    return bags;
        //}

        public async Task AddAlias(string url, string indexName, string aliasName)
        {
            var client = GetClient(url);
            var response = await client.Indices.PutAliasAsync(indexName, aliasName);
        }

        public async Task DeleteAlias(string url, string indexName, string aliasName)
        {
            var client = GetClient(url);
            var response = await client.Indices.DeleteAliasAsync(indexName, aliasName);
        }


        public async Task<bool> CloseIndex(string url, string indexName)
        {
            var client = GetClient(url);
            var response = await client.Indices.CloseAsync(indexName);
            return response.IsValid;
        }

        public async Task<bool> OpenIndex(string url, string indexName)
        {
            var client = GetClient(url);
            var response = await client.Indices.OpenAsync(indexName);
            return response.IsValid;
        }

        public async Task<bool> DeleteIndex(string url, string indexName)
        {
            var client = GetClient(url);
            var response = await client.Indices.DeleteAsync(indexName);
            return response.IsValid;
        }

        public async Task ClearCache(string url, string indexName)
        {
            var client = GetClient(url);
            var response = await client.Indices.ClearCacheAsync(indexName);
        }

        public async Task<string> StatsJson(string url, string indexName)
        {
            var client = GetClient(url);
            var response = await client.Indices.StatsAsync(indexName);
            return JsonConvert.SerializeObject(response.Stats, Formatting.Indented);
        }

        public async Task Flush(string url, string indexName)
        {
            var client = GetClient(url);
            var response = await client.Indices.FlushAsync(indexName);
        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="url"></param>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public async Task Refresh(string url, string indexName)
        {
            var client = GetClient(url);
            var response = await client.Indices.RefreshAsync(indexName);
        }

        /// <summary>
        /// 强制合并索引
        /// </summary>
        /// <param name="url"></param>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public async Task Forcemerge(string url, string indexName)
        {
            var client = GetClient(url);
            var response = await client.Indices.ForceMergeAsync(indexName);
        }

        public async Task<string> Get(string url, string indexName)
        {
            var client = GetClient(url);
            var response = await client.Indices.GetAsync(indexName);
            //return response.Indices.Values;
            return JsonConvert.SerializeObject(response.Indices.Values, Formatting.Indented);
        }

        private ElasticClient GetClient(string url)
        {
            return new ElasticClient(new Uri(url));

        }
        //private ElasticClient GetClient(string url)
        //{
        //    var settings = new ConnectionSettings(new Uri(url.Url))
        //        .BasicAuthentication(url.UserName, url.Password)
        //        ;
        //    return new ElasticClient(settings);
        //}
    }
}
