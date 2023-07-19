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

        public async Task<IndicesInfo> GetIndices(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return new IndicesInfo();
            //var client = GetClient(url);
            //var result = await GetIndices(client);
            //url = $"{url}/_cluster/stats";
            var stats = await GetClusterStatsAsync(url);

            var clusterIndices = stats.Indices;
            var totalShardsCount = clusterIndices.Shards.Total;
            var successfulShardsCount = clusterIndices.Shards.Primaries;
            var indicesCount = clusterIndices.Count;
            var documentsCount = clusterIndices.Documents.Count;
            var size = clusterIndices.Store.SizeInBytes / 1024 / 1024;

            return new Contracts.IndicesInfo(totalShardsCount, successfulShardsCount, indicesCount, 0, documentsCount, size);
        }

        private async Task<ClusterStatsOutput> GetClusterStatsAsync(string url)
        {

            url = $"{url}/_cluster/stats";
            var stats = await _context.GetAsync<ClusterStatsOutput>(url);
            return stats;
        }


        /// <summary>
        /// 获取cpu使用率
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public async Task<OSCpuInfo> GetCpuInfo(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return new OSCpuInfo();
            //url = $"{url}/_cluster/stats?human&pretty&filter_path=nodes.process";
            var stats = await GetClusterStatsAsync(url);
            //var result = await _context.GetAsync<ClusterNodesStatsOutput>(url);


            return new OSCpuInfo(stats.Nodes.Process.Cpu.Percent);
        }


        public async Task<EsFileSystemInfo> GetEsFileSystemInfo(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return new EsFileSystemInfo();
            var stats = await GetClusterStatsAsync(url);

            return new EsFileSystemInfo(stats.Nodes.FileSystem.TotalInBytes, stats.Nodes.FileSystem.FreeInBytes);

        }

        public async Task<OsMemberInfo> GetOsMemberInfo(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return new OsMemberInfo();
            var stats = await GetClusterStatsAsync(url);
            var os = stats.Nodes.OperatingSystem;

            return new OsMemberInfo(os.Memory.TotalBytes, os.Memory.FreeBytes, os.Memory.UsedPercent, os.Memory.FreePercent);
        }

        public async Task<ClusterStatsInfo> GetClusterStatsInfo(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return new ClusterStatsInfo();
            var stats = await GetClusterStatsAsync(url);
            var uuid = stats.ClusterUUID;
            var clusterName = stats.ClusterName;
            var timestamp = stats.Timestamp;
            var version = string.Join(",", stats.Nodes.Versions.ToArray());
            var os = string.Join(",", stats.Nodes.OperatingSystem.Names.Select(x => x.Name).ToArray());
            return new ClusterStatsInfo(uuid, clusterName, timestamp, version, os);
        }

        public async Task<ClusterHealthInfo> GetClusterHealth(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return new ClusterHealthInfo();
            //var stats = await GetClusterStatsAsync(url);

            url = $"{url}/_cluster/health";
            var health = await _context.GetAsync<ClusterHealthOutput>(url);

            var healthInfo = new ClusterHealthInfo(health.Status.ToString(),
                health.TimedOut, health.NumberOfNodes,
                health.NumberOfDataNodes,
                health.ActivePrimaryShards, health.InitializingShards, health.UnassignedShards);
            return healthInfo;
        }

        public async Task<JvmInfo> GetJvm(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return new JvmInfo();
            var stats = await GetClusterStatsAsync(url);

            var nodes = await GetNodeStatsAsync(url);


            var jvm = stats.Nodes.Jvm;
            var jvmVersion = string.Join(",", stats.Nodes.Jvm.Versions.Select(x => x.Version).ToArray());
            var upTimeInMillis = stats.Nodes.Jvm.MaxUptimeInMilliseconds;
            var threads = stats.Nodes.Jvm.Threads;
            var maxInBytes = jvm.Memory.HeapMaxInBytes;
            var usedInBytes = jvm.Memory.HeapUsedInBytes;

            return new JvmInfo(jvmVersion, upTimeInMillis, threads, maxInBytes, usedInBytes,
                nodes.Nodes.Select(x =>
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
                }));
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
            Enum.TryParse(str, out StatusEnum status);
            return status;
        }

        //public async Task<IEnumerable<IndexInfoDto>> GetIndexInfos(string url, bool all = false)
        //{
        //    if (string.IsNullOrWhiteSpace(url)) return Enumerable.Empty<IndexInfoDto>();

        //    var client = GetClient(url);
        //    return await GetIndexInfos(client, all);
        //}


        //private async Task<IEnumerable<IndexInfoDto>> GetIndexInfos(ElasticClient client, bool all = false)
        //{
        //    var indices = await client.Cat.IndicesAsync();
        //    var sources = all ? indices.Records : indices.Records.Where(x => !x.Index.StartsWith("."));
        //    return sources
        //        .Select(x => new IndexInfoDto(x.UUID,
        //                                   x.Primary,
        //                                   x.PrimaryStoreSize,
        //                                   x.Replica,
        //                                   x.Index,
        //                                   x.DocsCount,
        //                                   x.DocsDeleted,
        //                                   x.StoreSize,
        //                                   x.Status,
        //                                   x.Health
        //                                   ));
        //}

        //private async Task<IndicesInfo> GetIndices(ElasticClient client)
        //{
        //    var stats = await client.Cluster.StatsAsync();
        //    if (!stats.IsValid) return new IndicesInfo();

        //    var clusterIndices = stats.Indices;
        //    var totalShardsCount = clusterIndices.Shards.Total;
        //    var successfulShardsCount = clusterIndices.Shards.Primaries;
        //    var indicesCount = clusterIndices.Count;
        //    var documentsCount = clusterIndices.Documents.Count;
        //    var size = clusterIndices.Store.SizeInBytes / 1024 / 1024;

        //    return new Contracts.IndicesInfo(totalShardsCount, successfulShardsCount, indicesCount, 0, documentsCount, size);
        //}

        //public async Task<IndicesInfo> GetIndices(string url)
        //{
        //    if (string.IsNullOrWhiteSpace(url)) return new IndicesInfo();
        //    var client = GetClient(url);
        //    var result = await GetIndices(client);
        //    return result;
        //}

        //private async Task<ClusterInfo> GetClusterInfo(ElasticClient client)
        //{
        //    var healthInfo = await GetClusterHealth(client);

        //    var statsInfo = await GetClusterStatsInfo(client);

        //    var jvm = await GetJvm(client);

        //    var osMemberInfo = await GetOsMemberInfo(client);

        //    var fsInfo = await GetEsFileSystemInfo(client);

        //    var cpuInfo = await GetCpuInfo(client);

        //    var clusterInfo = new ClusterInfo(healthInfo, statsInfo, jvm,
        //        osMemberInfo, fsInfo,
        //        new FieldDataInfo(), new QueryCacheInfo(),
        //        cpuInfo
        //        );
        //    return clusterInfo;
        //}


        ///// <summary>
        ///// 获取cpu使用率
        ///// </summary>
        ///// <param name="client"></param>
        ///// <returns></returns>
        //public async Task<OSCpuInfo> GetCpuInfo(string url)
        //{
        //    if (string.IsNullOrWhiteSpace(url)) return new OSCpuInfo();
        //    var client = GetClient(url);
        //    return await GetCpuInfo(client);
        //}
        ///// <summary>
        ///// 获取cpu使用率
        ///// </summary>
        ///// <param name="client"></param>
        ///// <returns></returns>
        //private async Task<OSCpuInfo> GetCpuInfo(ElasticClient client)
        //{
        //    var stats = await client.Nodes.StatsAsync();
        //    if (!stats.IsValid) return new OSCpuInfo();

        //    var usedPercent = stats.Nodes.FirstOrDefault().Value.OperatingSystem.Cpu.Percent;

        //    return new OSCpuInfo(usedPercent);
        //}

        //public async Task<EsFileSystemInfo> GetEsFileSystemInfo(string url)
        //{
        //    if (string.IsNullOrWhiteSpace(url)) return new EsFileSystemInfo();
        //    var client = GetClient(url);
        //    return await GetEsFileSystemInfo(client);
        //}

        //private async Task<EsFileSystemInfo> GetEsFileSystemInfo(ElasticClient client)
        //{
        //    var stats = await client.Cluster.StatsAsync();
        //    if (!stats.IsValid) return new EsFileSystemInfo();

        //    var fs = stats.Nodes.FileSystem;
        //    return new EsFileSystemInfo(fs.TotalInBytes, fs.FreeInBytes);
        //}


        //public async Task<OsMemberInfo> GetOsMemberInfo(string url)
        //{
        //    if (string.IsNullOrWhiteSpace(url)) return new OsMemberInfo();
        //    var client = GetClient(url);
        //    return await GetOsMemberInfo(client);
        //}

        //private async Task<OsMemberInfo> GetOsMemberInfo(ElasticClient client)
        //{
        //    var stats = await client.Cluster.StatsAsync();
        //    if (!stats.IsValid) return new OsMemberInfo();

        //    var os = stats.Nodes.OperatingSystem;

        //    return new OsMemberInfo(os.Memory.TotalBytes, os.Memory.FreeBytes, os.Memory.UsedPercent, os.Memory.FreePercent);
        //}


        //public async Task<ClusterStatsInfo> GetClusterStatsInfo(string url)
        //{
        //    if (string.IsNullOrWhiteSpace(url)) return new ClusterStatsInfo();
        //    var client = GetClient(url);
        //    return await GetClusterStatsInfo(client);
        //}

        //private async Task<ClusterStatsInfo> GetClusterStatsInfo(ElasticClient client)
        //{
        //    var stats = await client.Cluster.StatsAsync();
        //    if (!stats.IsValid) return new ClusterStatsInfo();

        //    var uuid = stats.ClusterUUID;
        //    var clusterName = stats.ClusterName;
        //    var timestamp = stats.Timestamp;
        //    var version = string.Join(",", stats.Nodes.Versions.ToArray());
        //    var os = string.Join(",", stats.Nodes.OperatingSystem.Names.Select(x => x.Name).ToArray());
        //    return new ClusterStatsInfo(uuid, clusterName, timestamp, version, os);
        //}

        //public async Task<ClusterHealthInfo> GetClusterHealth(string url)
        //{
        //    if (string.IsNullOrWhiteSpace(url)) return new ClusterHealthInfo();
        //    var client = GetClient(url);
        //    return await GetClusterHealth(client);
        //}

        //private async Task<ClusterHealthInfo> GetClusterHealth(ElasticClient client)
        //{
        //    var health = await client.Cluster.HealthAsync();
        //    if (!health.IsValid) return new ClusterHealthInfo();

        //    var healthInfo = new ClusterHealthInfo(health.Status.ToString(),
        //        health.TimedOut, health.NumberOfNodes,
        //        health.NumberOfDataNodes,
        //        health.ActivePrimaryShards, health.InitializingShards, health.UnassignedShards);
        //    return healthInfo;
        //}

        //public async Task<JvmInfo> GetJvm(string url)
        //{
        //    if (string.IsNullOrWhiteSpace(url)) return new JvmInfo();
        //    var client = GetClient(url);
        //    return await GetJvm(client);
        //}
        //private async Task<JvmInfo> GetJvm(ElasticClient client)
        //{
        //    var stats = await client.Cluster.StatsAsync();
        //    if (!stats.IsValid) return new JvmInfo();

        //    var jvm = stats.Nodes.Jvm;
        //    var jvmVersion = string.Join(",", stats.Nodes.Jvm.Versions.Select(x => x.Version).ToArray());
        //    var upTimeInMillis = stats.Nodes.Jvm.MaxUptimeInMilliseconds;
        //    var threads = stats.Nodes.Jvm.Threads;
        //    var maxInBytes = jvm.Memory.HeapMaxInBytes;
        //    var usedInBytes = jvm.Memory.HeapUsedInBytes;

        //    return new JvmInfo(jvmVersion, upTimeInMillis, threads, maxInBytes, usedInBytes,
        //        client.Nodes.Stats().Nodes.Select(x =>
        //        {
        //            var curMem = x.Value.Jvm.Memory;
        //            return new JvmMemberInfo(
        //            curMem.HeapMaxInBytes,
        //            curMem.HeapMax,
        //            curMem.HeapUsedInBytes,
        //            curMem.HeapUsed,
        //            curMem.HeapUsedPercent,
        //            x.Value.Jvm.UptimeInMilliseconds
        //            );
        //        }));
        //}

        //private void QueryCpu(ElasticClient client)
        //{
        //    client.Nodes.Stats().Nodes.Values.Select(x => x.Roles)
        //}

        //private CatResponse<CatNodesRecord> GetNodes(ElasticClient client)
        //{
        //    var request = new CatNodesRequest
        //    {
        //        Headers = new string[] { "ip,name,heap.percent,heap.current,heap.max,ram.percent,ram.current,ram.max,node.role,master,cpu,load_1m,load_5m,load_15m,disk.used_percent,disk.used,disk.total,query_cache.memory_size,fielddata.memory_size" },

        //    };
        //    var result = client.Cat.Nodes(request);
        //    return result;
        //}

        //public async Task<StatusEnum> GetStatus(string url)
        //{
        //    if (string.IsNullOrWhiteSpace(url)) { return StatusEnum.Default; }

        //    var client = GetClient(url);
        //    var health = await client.Cluster.HealthAsync();
        //    if (!health.IsValid) return StatusEnum.Default;

        //    var str = health.Status.ToString();
        //    Enum.TryParse(str, out StatusEnum status);
        //    return status;
        //}

        //public async Task<StatusEnum> GetStatus(string url)
        //{
        //    if (string.IsNullOrWhiteSpace(url)) { return StatusEnum.Default; }

        //    var client = GetClient(url);
        //    var health = await client.Cluster.HealthAsync();
        //    if (!health.IsValid) return StatusEnum.Default;

        //    var str = health.Status.ToString();
        //    Enum.TryParse(str, out StatusEnum status);
        //    return status;
        //}

        //private ElasticClient GetClient(string url)
        //{
        //    var settings = new ConnectionSettings(new Uri(url))
        //        .BasicAuthentication(url.UserName, url.Password)
        //        ;
        //    return new ElasticClient(settings);
        //}

        //private ElasticClient GetClient(string url)
        //{
        //    return new ElasticClient(new Uri(url));

        //}

    }
}
