using ElasticView.ApiDomain;
using ElasticView.AppService.Contracts;
using ElasticView.AppService.Contracts.Output;

namespace ElasticView.AppService
{
    public class ElasticAppService : IElasticAppService
    {

        private readonly IApiContext _context;

        public ElasticAppService(IApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IndexInfoDto>> GetIndexInfos(string url, bool all = false)
        {
            if (string.IsNullOrWhiteSpace(url)) return Enumerable.Empty<IndexInfoDto>();

            //var client = GetClient(url);

            url = $"{url}/_cat/indices?format=json";
            var result = await _context.GetAsync<IEnumerable<IndexInfoDto>>(url);
            return result;
            //return await GetIndexInfos(client, all);
        }
         

        private IndicesInfo GetIndices(ClusterIndicesStatsOutput clusterIndices)
        {
            var totalShardsCount = clusterIndices.Shards.Total;
            var successfulShardsCount = clusterIndices.Shards.Primaries;
            var indicesCount = clusterIndices.Count;
            var documentsCount = clusterIndices.Documents.Count;
            var size = clusterIndices.Store.SizeInBytes / 1024 / 1024;

            return new Contracts.IndicesInfo(totalShardsCount, successfulShardsCount, indicesCount,
                0, documentsCount, size);
        }

        private async Task<IEnumerable<JvmMemberInfo>> GetJvmMemberInfos(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return Enumerable.Empty<JvmMemberInfo>();

            var nodes = await GetNodeStatsAsync(url);
            return nodes.Nodes.Select(x =>
              {
                  var curMem = x.Value.Jvm.Memory;
                  return new JvmMemberInfo(
                  curMem.HeapMaxInBytes,
                  curMem.HeapMax,
                  curMem.HeapUsedInBytes,
                  curMem.HeapUsed,
                  curMem.HeapUsedPercent,
                  x.Value.Jvm.UptimeInMilliseconds
                  );
              });
        }

        private async Task<JvmInfo> GetJvm(string url, ClusterJvmOutput jvm)
        {

            var jvmDetails = await GetJvmMemberInfos(url);
            var jvmInfo = GetJvmEmptyJvmDetail(jvm);
            jvmInfo.UpdateJvmMemberInfos(jvmDetails);
            return jvmInfo;
        }
        private JvmInfo GetJvmEmptyJvmDetail(ClusterJvmOutput jvm)
        {
            var jvmVersion = string.Join(",", jvm.Versions.Select(x => x.Version).ToArray());
            var upTimeInMillis = jvm.MaxUptimeInMilliseconds;
            var threads = jvm.Threads;
            var maxInBytes = jvm.Memory.HeapMaxInBytes;
            var usedInBytes = jvm.Memory.HeapUsedInBytes;


            return new JvmInfo(jvmVersion, upTimeInMillis, threads, maxInBytes, usedInBytes);
        }
        


        public async Task<Summary> GetSummary(string url)
        {
            var clusterStats = await GetClusterStatsAsync(url);
            var indicesInfo = GetIndices(clusterStats.Indices);

            var clusterInfo = await GetClusterInfo(url, clusterStats);

            return new Summary(indicesInfo, clusterInfo);
        }

        private async Task<ClusterInfo> GetClusterInfo(string url, ClusterStatsOutput clusterStats)
        {
            var clusterHealth = await GetClusterHealth(url);

            var cpuInfo = new OSCpuInfo(clusterStats.Nodes.Process.Cpu.Percent);
            var memberInfo = GetOsMemberInfo(clusterStats.Nodes.OperatingSystem.Memory);
            var fileSystemInfo = new EsFileSystemInfo(clusterStats.Nodes.FileSystem.TotalInBytes,
                clusterStats.Nodes.FileSystem.FreeInBytes);
            var jvmInfo = await GetJvm(url, clusterStats.Nodes.Jvm);
            var clusterStatsInfo = GetClusterStatsInfo(clusterStats);
            return new ClusterInfo(clusterHealth, clusterStatsInfo, jvmInfo,
                memberInfo, fileSystemInfo, new FieldDataInfo(), new QueryCacheInfo(),
                cpuInfo);
        }
        private async Task<ClusterHealthInfo> GetClusterHealth(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return new ClusterHealthInfo();


            url = $"{url}/_cluster/health";
            var health = await _context.GetAsync<ClusterHealthOutput>(url);

            var healthInfo = new ClusterHealthInfo(health.Status.ToString(),
                health.TimedOut, health.NumberOfNodes,
                health.NumberOfDataNodes,
                health.ActivePrimaryShards, health.InitializingShards, health.UnassignedShards);
            return healthInfo;
        }
        private ClusterStatsInfo GetClusterStatsInfo(ClusterStatsOutput stats)
        {
            var uuid = stats.ClusterUUID;
            var clusterName = stats.ClusterName;
            var timestamp = stats.Timestamp;
            var version = string.Join(",", stats.Nodes.Versions.ToArray());
            var os = string.Join(",", stats.Nodes.OperatingSystem.Names.Select(x => x.Name).ToArray());
            return new ClusterStatsInfo(uuid, clusterName, timestamp, version, os);
        }
        private OsMemberInfo GetOsMemberInfo(OperatingSystemMemoryInfoOutput memory)
        {
            return new OsMemberInfo(memory.TotalBytes, memory.FreeBytes, memory.UsedPercent, memory.FreePercent);

        }

        private async Task<ClusterStatsOutput> GetClusterStatsAsync(string url)
        {

            url = $"{url}/_cluster/stats";
            var stats = await _context.GetAsync<ClusterStatsOutput>(url);
            return stats;
        }
         
        private async Task<NodeOutput> GetNodeStatsAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return new NodeOutput();

            url = $"{url}/_nodes/stats";
            var stats = await _context.GetAsync<NodeOutput>(url);
            return stats;
        }


        public async Task<StatusEnum> GetStatus(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) { return StatusEnum.Default; }


            var health = await GetClusterHealth(url);
            if (health is null) return StatusEnum.Default;

            var str = health.Status.ToString();
            var status = (StatusEnum)Enum.Parse(typeof(StatusEnum), str, true);

            //var f = Enum.TryParse(str, out StatusEnum status);
            return status;
        }
         

    }
}
