using Newtonsoft.Json;

namespace ElasticView.AppService.Contracts
{
    public class Summary
    {
        public Summary()
        {
        }

        public Summary(IndicesInfo indicesInfo, ClusterInfo clusterInfo
            //, IEnumerable<IndexInfoDto> indexInfos
            )
        {
            IndicesInfo = indicesInfo;
            ClusterInfo = clusterInfo;
            //IndexInfos = indexInfos;
        }

        public IndicesInfo IndicesInfo { get; set; } = new IndicesInfo();

        public ClusterInfo ClusterInfo { get; set; } = new ClusterInfo();

        //public IEnumerable<IndexInfoDto> IndexInfos { get; set; } = Enumerable.Empty<IndexInfoDto>();

    }


    public class IndicesInfo
    {

        public IndicesInfo()
        {
        }

        public IndicesInfo(double totalShards, double successfulShards, double indices, double templates, double documents, double totalSize)
        {
            TotalShards = totalShards;
            SuccessfulShards = successfulShards;
            Indices = indices;
            Templates = templates;
            Documents = documents;

            var unit = "mb";
            if (totalSize >= 1024)
            {
                totalSize /= 1024;
                unit = "G";
            }
            TotalSize = $"{totalSize:f1}{unit}";
        }

        public double TotalShards { get; set; }

        public double SuccessfulShards { get; set; }

        public double Indices { get; set; }

        public double Templates { get; set; }

        public double Documents { get; set; }

        public string TotalSize { get; set; }

        //public IndexInfo IndexInfo { get; set; } = new IndexInfo();
    }


    public class IndexInfoDto
    {
        public IndexInfoDto()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uUID"></param>
        /// <param name="primary">主分片数</param>
        /// <param name="primaryStoreSize">主分片大小</param>
        /// <param name="replica">副本数</param>
        /// <param name="index">索引名</param>
        /// <param name="docsCount"></param>
        /// <param name="docsDeleted"></param>
        /// <param name="storeSize"></param>
        /// <param name="status"></param>
        /// <param name="health"></param>
        public IndexInfoDto(string uUID,
                         string primary,
                         string primaryStoreSize,
                         string replica,
                         string index,
                         string docsCount,
                         string docsDeleted,
                         string storeSize,
                         string status,
                         string health)
        {
            UUID = uUID;
            Primary = primary;
            PrimaryStoreSize = primaryStoreSize;
            Replica = replica;
            Index = index;
            DocsCount = docsCount;
            DocsDeleted = docsDeleted;
            StoreSize = storeSize;
            Status = status;
            Health = health;
        }

        [JsonProperty("uuid")]
        public string UUID { get; set; } = string.Empty;

        [JsonProperty("pri")]
        public string Primary { get; set; } = string.Empty;

        [JsonProperty("pri.store.size")]
        public string PrimaryStoreSize { get; set; } = string.Empty;

        [JsonProperty("rep")]
        public string Replica { get; set; } = string.Empty;

        [JsonProperty("index")]
        public string Index { get; set; } = string.Empty;

        [JsonProperty("docs.count")]
        public string DocsCount { get; set; } = string.Empty;

        [JsonProperty("docs.deleted")]
        public string DocsDeleted { get; set; } = string.Empty;

        [JsonProperty("store.size")]
        public string StoreSize { get; set; } = string.Empty;

        [JsonProperty("health")]
        public string Health { get; set; } = string.Empty;

        [JsonProperty("status")]
        public string Status { get; set; } = string.Empty;

        public string[] Aliases { get; set; } = new string[0];


        public void UpdateAliases(string[] aliases)
        {
            this.Aliases = aliases;
        }

    }

    public class ClusterInfo
    {
        public ClusterInfo()
        {
        }

        public ClusterInfo(ClusterHealthInfo clusterHealthInfo,
            ClusterStatsInfo clusterStatsInfo,
                           JvmInfo jvm,
                           OsMemberInfo memberInfo,
                           EsFileSystemInfo fileSystemInfo,
                           FieldDataInfo fieldDataInfo,
                           QueryCacheInfo queryCacheInfo,
                           OSCpuInfo cpuInfo)
        {
            ClusterHealthInfo = clusterHealthInfo;
            ClusterStatsInfo = clusterStatsInfo;
            Jvm = jvm;
            MemberInfo = memberInfo;
            FileSystemInfo = fileSystemInfo;
            FieldDataInfo = fieldDataInfo;
            QueryCacheInfo = queryCacheInfo;
            CpuInfo = cpuInfo;
        }

        public ClusterHealthInfo ClusterHealthInfo { get; set; } = new ClusterHealthInfo();
        public ClusterStatsInfo ClusterStatsInfo { get; set; } = new ClusterStatsInfo();
        public JvmInfo Jvm { get; set; } = new JvmInfo();

        public OsMemberInfo MemberInfo { get; set; } = new OsMemberInfo();

        public EsFileSystemInfo FileSystemInfo { get; set; } = new EsFileSystemInfo();

        public FieldDataInfo FieldDataInfo { get; set; } = new FieldDataInfo();

        public QueryCacheInfo QueryCacheInfo { get; set; } = new QueryCacheInfo();

        public OSCpuInfo CpuInfo { get; set; } = new OSCpuInfo();
        //public double CpuPercent { get; set; }

        //public double CpuFree => 100 - CpuPercent;
    }

    public class ClusterStatsInfo
    {
        public ClusterStatsInfo()
        {
        }

        public ClusterStatsInfo(string uUID, string clusterName, long timestamp, string version, string oS)
        {
            UUID = uUID;
            ClusterName = clusterName;
            Timestamp = timestamp;
            Version = version;
            OS = oS;
        }

        public string UUID { get; set; }
        public string ClusterName { get; set; }
        public long Timestamp { get; set; }
        public string Version { get; set; }

        public string OS { get; set; }
    }

    public class ClusterHealthInfo
    {
        public ClusterHealthInfo()
        {
        }

        public ClusterHealthInfo(string status, bool timeOut, int nodes, int dataNodes, int activePrimaryShards, int initializingShards, int unassignedShards)
        {
            Status = status;
            TimeOut = timeOut;
            Nodes = nodes;
            DataNodes = dataNodes;
            ActivePrimaryShards = activePrimaryShards;
            InitializingShards = initializingShards;
            UnassignedShards = unassignedShards;
        }

        public string Status { get; set; }

        public bool TimeOut { get; set; }

        public int Nodes { get; set; }

        public int DataNodes { get; set; }

        public int ActivePrimaryShards { get; set; }

        public int InitializingShards { get; set; }

        public int UnassignedShards { get; set; }

    }

    public class QueryCacheInfo
    {
        public QueryCacheInfo()
        {
        }

        public QueryCacheInfo(long sizeInBytes, long totalCount, long cacheSize, long cacheCount)
        {
            SizeInBytes = sizeInBytes;
            TotalCount = totalCount;
            CacheSize = cacheSize;
            CacheCount = cacheCount;
        }

        public long SizeInBytes { get; set; }
        public long TotalCount { get; set; }
        public long CacheSize { get; set; }
        public long CacheCount { get; set; }
    }

    public class FieldDataInfo
    {

        public long SizeInBytes { get; set; }
    }

    public class EsFileSystemInfo
    {
        public EsFileSystemInfo()
        {
        }

        public EsFileSystemInfo(long totalInBytes, long freeInBytes)
        {
            TotalInBytes = totalInBytes;
            FreeInBytes = freeInBytes;
        }

        public long TotalInBytes { get; set; }

        public long UsedInBytes => TotalInBytes - FreeInBytes;

        public long FreeInBytes { get; set; }
    }

    //public class OSSystemInfo
    //{

    //}

    public class OSCpuInfo
    {
        public OSCpuInfo()
        {
        }

        public OSCpuInfo(float usedPercent)
        {
            UsedPercent = usedPercent;
        }

        public float UsedPercent { get; set; }

        public float FreePercent { get { return 100 - this.UsedPercent; } }
    }

    public class OsMemberInfo
    {
        public OsMemberInfo()
        {
        }

        public OsMemberInfo(long totalBytes, long freeBytes, int usedPercent, int freePercent)
        {
            TotalBytes = totalBytes;
            FreeBytes = freeBytes;
            UsedPercent = usedPercent;
            FreePercent = freePercent;
        }

        public long TotalBytes { get; set; }
        public long FreeBytes { get; set; }

        public int UsedPercent { get; set; }
        public int FreePercent { get; set; }
    }

    public class JvmInfo
    {
        public JvmInfo()
        {
        }



        public string Version { get; set; }

        public long UpTimeInMillis { get; set; }

        public long Threads { get; set; }

        public long HeapMaxInBytes { get; set; }

        public long HeapUsedInBytes { get; set; }

        public JvmInfo(string version, long upTimeInMillis, long threads, long heapMaxInBytes, long heapUsedInBytes, IEnumerable<JvmMemberInfo> jvmMemberInfos)
        {
            Version = version;
            UpTimeInMillis = upTimeInMillis;
            Threads = threads;
            HeapMaxInBytes = heapMaxInBytes;
            HeapUsedInBytes = heapUsedInBytes;
            JvmMemberInfos = jvmMemberInfos;
        }

        public JvmInfo(string version, long upTimeInMillis, long threads, long heapMaxInBytes, long heapUsedInBytes)
        {
            Version = version;
            UpTimeInMillis = upTimeInMillis;
            Threads = threads;
            HeapMaxInBytes = heapMaxInBytes;
            HeapUsedInBytes = heapUsedInBytes;
        }

        public void UpdateJvmMemberInfos(IEnumerable<JvmMemberInfo> infos)
        {
            this.JvmMemberInfos = infos;
        }

        public IEnumerable<JvmMemberInfo> JvmMemberInfos { get; set; } = Enumerable.Empty<JvmMemberInfo>();
    }

    public class JvmMemberInfo
    {
        public JvmMemberInfo()
        {
        }

        public JvmMemberInfo(long heapMaxInBytes,
                             string heapMax,
                             long heapUsedInBytes,
                             string heapUsed,
                             long heapUsedPercent,
                             long uptimeInMilliseconds)
        {
            HeapMaxInBytes = heapMaxInBytes;
            HeapMax = heapMax;
            HeapUsedInBytes = heapUsedInBytes;
            HeapUsed = heapUsed;
            HeapUsedPercent = heapUsedPercent;
            UptimeInMilliseconds = uptimeInMilliseconds;
        }

        public long HeapMaxInBytes { get; set; }
        public string HeapMax { get; set; }
        public long HeapUsedInBytes { get; set; }
        public string HeapUsed { get; set; }
        public long HeapUsedPercent { get; set; }
        public long UptimeInMilliseconds { get; set; }
    }

}
