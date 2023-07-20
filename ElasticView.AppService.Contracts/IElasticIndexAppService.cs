using ElasticView.AppService.Contracts.ApiDto;
using ElasticView.AppService.Contracts.InputDto;
using ElasticView.AppService.Contracts.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticView.AppService.Contracts
{
    public interface IElasticIndexAppService
    {
        Task<bool> AddAlias(string url, string indexName, string aliasName);
        Task<bool> ClearCache(string url, string indexName);
        Task<bool> CloseIndex(string url, string indexName);
        Task<CreateIndexApiOutput> CreateIndex(string url, CreateIndexInput indexInput);
        Task<bool> DeleteAlias(string url, string indexName, string aliasName);
        Task<bool> DeleteIndex(string url, string indexName);
        Task<bool> Flush(string url, string indexName);
        Task<bool> Forcemerge(string url, string indexName);
        Task<string> Get(string url, string indexName);
        //Task GetAlias(string url, string indexName);
        //Task<IEnumerable<IndexAliasOutput>> GetAliases(string url, IEnumerable<string> aliases);
        Task<bool> OpenIndex(string url, string indexName);
        Task<bool> Refresh(string url, string indexName);
        //Task<string> StatsJson(string url, string indexName);
    }
}
