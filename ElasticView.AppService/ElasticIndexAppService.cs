using ElasticView.ApiDomain;
using ElasticView.AppService.Contracts;
using ElasticView.AppService.Contracts.ApiDto;
using ElasticView.AppService.Contracts.InputDto;
using ElasticView.AppService.Contracts.Output;
using Newtonsoft.Json;

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

        ///// <summary>
        ///// 获取别名
        ///// </summary>
        ///// <param name="url"></param>
        ///// <param name="indexName"></param>
        ///// <returns></returns>
        //public async Task GetAlias(string url, string indexName)
        //{
        //    var client = GetClient(url);
        //    client
        //    url = $"{url}/{indexName}/_alias";
        //    var response = await _context.GetAsync<>(url);
        //}


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
            var response = await _context.GetAsync<string>(url);
            return response;
            //return JsonConvert.SerializeObject(response.Indices.Values, Formatting.Indented);
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

        //public async Task<bool> CloseIndex(string url, string indexName)
        //{
        //    var client = GetClient(url);
        //    var response = await client.Indices.CloseAsync(indexName);
        //    return response.IsValid;
        //}

        public async Task<bool> OpenIndex(string url, string indexName)
        {
            url = $"{url}/{indexName}/_open";
            var response = await _context.PostAsync<ElasticSearchApiOutput>(url);
            return response.Success;
        }

        //public async Task<bool> OpenIndex(string url, string indexName)
        //{
        //    var client = GetClient(url);
        //    var response = await client.Indices.OpenAsync(indexName);
        //    return response.IsValid;
        //}

        //public async Task<bool> DeleteIndex(string url, string indexName)
        //{
        //    var client = GetClient(url);
        //    var response = await client.Indices.DeleteAsync(indexName);
        //    return response.IsValid;
        //}

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

        //public async Task<bool> ClearCache(string url, string indexName)
        //{
        //    var client = GetClient(url);
        //    var response = await client.Indices.ClearCacheAsync(indexName);
        //    return response.IsValid;
        //}

        //public async Task<string> StatsJson(string url, string indexName)
        //{
        //    var client = GetClient(url);
        //    var response = await client.Indices.StatsAsync(indexName);
        //    return JsonConvert.SerializeObject(response.Stats, Formatting.Indented);
        //}

        public async Task<bool> Flush(string url, string indexName)
        {
            url = $"{url}/{indexName}/_flush";
            var response = await _context.PostAsync<CacheResponseOutput>(url);
            return response.Shards is not null;
        }

        //public async Task<bool> Flush(string url, string indexName)
        //{
        //    var client = GetClient(url);
        //    var response = await client.Indices.FlushAsync(indexName);
        //    return response.IsValid;
        //}

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

        ///// <summary>
        ///// 刷新
        ///// </summary>
        ///// <param name="url"></param>
        ///// <param name="indexName"></param>
        ///// <returns></returns>
        //public async Task<bool> Refresh(string url, string indexName)
        //{
        //    var client = GetClient(url);
        //    var response = await client.Indices.RefreshAsync(indexName);
        //    return response.IsValid;
        //}

        ///// <summary>
        ///// 强制合并索引
        ///// </summary>
        ///// <param name="url"></param>
        ///// <param name="indexName"></param>
        ///// <returns></returns>
        //public async Task<bool> Forcemerge(string url, string indexName)
        //{
        //    var client = GetClient(url);
        //    var response = await client.Indices.ForceMergeAsync(indexName);
        //    return response.IsValid;
        //}

        //public async Task<string> Get(string url, string indexName)
        //{
        //    var client = GetClient(url);
        //    var response = await client.Indices.GetAsync(indexName);
        //    //return response.Indices.Values;
        //    return JsonConvert.SerializeObject(response.Indices.Values, Formatting.Indented);
        //}


        //private ElasticClient GetClient(string url)
        //{
        //    return new ElasticClient(new Uri(url));

        //}
        //private ElasticClient GetClient(string url)
        //{
        //    var settings = new ConnectionSettings(new Uri(url.Url))
        //        .BasicAuthentication(url.UserName, url.Password)
        //        ;
        //    return new ElasticClient(settings);
        //}
    }
}
