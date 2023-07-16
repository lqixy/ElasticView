using ElasticView.AppService.Contracts.InputDto;
using ElasticView.AppService.Contracts.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticView.AppService.Contracts
{
    public interface IElasticCatAppService
    {
        Task<IEnumerable<IndexAliasOutput>> GetAliases(string url);
        Task<IEnumerable<CatNodesOutput>> GetNodes(string url);
    }
}
