using ElasticView.ApiDomain;
using ElasticView.AppService.Contracts;
using ElasticView.AppService.Contracts.ApiDto;
using ElasticView.AppService.Contracts.InputDto;
using ElasticView.AppService.Contracts.Output;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ElasticView.AppService
{
    public class ElasticIndexAppService : IElasticIndexAppService
    {

        private readonly IApiContext _context;

        public ElasticIndexAppService(IApiContext context)
        {
            _context = context;
        }

        //public async Task CreateIndex(string url, CreateIndexInput indexInput)
        //{
        //    var client = GetClient(url);

        //    var response = await client.Indices.CreateAsync(indexInput.Name, req => req.Settings(s =>
        //         s.NumberOfShards(indexInput.ShardsCount)
        //         .NumberOfReplicas(indexInput.ReplicasCount)
        //         ));
        //}

        //public async Task<bool> CreateIndex(string url, CreateIndexInput indexInput)
        //{
        //    //var client = GetClient(url);
        //    url = $"{url}/{indexInput.IndexName}";

        //    var input = new CreateIndexApiInput(
        //        new CreateIndexApiSettingsInput(indexInput.ShardsCount, indexInput.ReplicasCount));

        //    var response = await _context
        //        .PutAsync<ElasticSearchApiOutput, CreateIndexApiInput>(url, input);
        //    return response.Success;
        //}

        public async Task<CreateIndexApiOutput> CreateIndex(string url, CreateIndexInput indexInput)
        {
            //var client = GetClient(url);
            url = $"{url}/{indexInput.IndexName}";

            var input = new CreateIndexApiInput(
                new CreateIndexApiSettingsInput(indexInput.ShardsCount, indexInput.ReplicasCount));

            var response = await _context
                .PutAsync<ElasticSearchApiOutput, CreateIndexApiInput>(url, input);

            var msg = response.Success ? "成功" : response.Error.Reason;
            return new CreateIndexApiOutput(response.Success, msg
                );
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
            url = $"{url}/{indexName}/_alias";
            var response = await _context.GetAsync<IndicesAliasOutput>(url);
        }
        public async Task<IEnumerable<IndexAliasOutput>> GetAliases(string url)
        {
            url = $"{url}/_cat/aliases?format=json";

            var records = await _context.GetAsync<IEnumerable<CatAliasesRecordOutput>>(url);

            if (records is null || !records.Any()) return Enumerable.Empty<IndexAliasOutput>();

            var result = records.GroupBy(x => x.Index)
                  .ToDictionary(k => k.Key, v => v.Select(vv => vv.Alias).ToArray())
                  .Select(x => new IndexAliasOutput(x.Key, x.Value))
                  ;
            return result;
        }

        /// <summary>
        /// 强制合并索引
        /// </summary>
        /// <param name="url"></param>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public async Task<bool> Forcemerge(string url, string indexName)
        {
            url = $"{url}/{indexName}_forcemerge";
            var response = await _context.GetAsync<CacheResponseOutput>(url);
            return response.Shards is not null;
        }

        public async Task<string> Get(string url, string indexName)
        {
            url = $"{url}/{indexName}";
            var response = await _context.GetAsync<JObject>(url);
            return response.ToString();
            
        }
        public async Task<string> GetStats(string url, string indexName)
        {
            url = $"{url}/{indexName}/_stats";
            var response = await _context.GetAsync<JObject>(url);
            return response.ToString();
        }

        public async Task<bool> AddAlias(string url, string indexName, string aliasName)
        {
            //var client = GetClient(url);
            //var response = await client.Indices.PutAliasAsync(indexName, aliasName);
            url = $"{url}/{indexName}/_alias/{aliasName}";
            var response = await _context.PutAsync<ElasticSearchApiOutput>(url);
            return response.Success;
        }

        public async Task<bool> DeleteAlias(string url, string indexName, string aliasName)
        {
            //var client = GetClient(url);
            //var response = await client.Indices.DeleteAliasAsync(indexName, aliasName);
            url = $"{url}/{indexName}/_alias/{aliasName}";
            var response = await _context.DeleteAsync<ElasticSearchApiOutput>(url);
            return response.Success;
        }

        public async Task<bool> CloseIndex(string url, string indexName)
        {
            url = $"{url}/{indexName}/_close";
            var response = await _context.PostAsync<ElasticSearchApiOutput>(url);
            return response.Success;
        }


        public async Task<bool> OpenIndex(string url, string indexName)
        {
            url = $"{url}/{indexName}/_open";
            var response = await _context.PostAsync<ElasticSearchApiOutput>(url);
            return response.Success;
        }

        public async Task<bool> DeleteIndex(string url, string indexName)
        {
            url = $"{url}/{indexName}";
            var response = await _context.DeleteAsync<ElasticSearchApiOutput>(url);
            return response.Success;
        }

        public async Task<bool> ClearCache(string url, string indexName)
        {
            url = $"{url}/{indexName}/_cache/clear";
            var response = await _context.PostAsync<CacheResponseOutput>(url);
            return response.Shards is not null;
        }

        public async Task<bool> Flush(string url, string indexName)
        {
            url = $"{url}/{indexName}/_flush";
            var response = await _context.PostAsync<CacheResponseOutput>(url);
            return response.Shards is not null;
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="url"></param>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public async Task<bool> Refresh(string url, string indexName)
        {
            url = $"{url}/{indexName}/_refresh";
            var response = await _context.PostAsync<CacheResponseOutput>(url);
            return response.Shards is not null;
        }

    }
}
