using ElasticView.AppService.Contracts.InputDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticView.AppService.Contracts
{
    public interface IElasticAppService
    {
        //Task<Summary> GetHealth(string url);
        Task<StatusEnum> GetStatus(string url);
        Task<IEnumerable<IndexInfoDto>> GetIndexInfos(string url, bool all = false);
        Task<Summary> GetSummary(string url);
        //Task<IndicesInfo> GetIndices(string url);
        //Task<ClusterHealthInfo> GetClusterHealth(string url);
        //Task<ClusterStatsInfo> GetClusterStatsInfo(string url);
        //Task<OsMemberInfo> GetOsMemberInfo(string url);
        //Task<EsFileSystemInfo> GetEsFileSystemInfo(string url);
        //Task<OSCpuInfo> GetCpuInfo(string url);
        //Task<JvmInfo> GetJvm(string url);
        //Task<StatusEnum> GetStatus(EsConnectInput input);
    }
}
