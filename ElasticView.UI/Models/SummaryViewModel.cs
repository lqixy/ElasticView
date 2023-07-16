using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ElasticView.UI.Models
{


    public class SummaryViewModel : ObservableObject
    {
        public SummaryViewModel()
        {
        }


        public SummaryViewModel(IndicesInfoViewModel indicesInfo, ClusterInfoViewModel clusterInfo
            //, IEnumerable<IndexInfoViewModel> indexInfos
            )
        {
            IndicesInfoViewModel = indicesInfo;
            ClusterInfoViewModel = clusterInfo;
            //IndexInfos = indexInfos;
        }

        public IndicesInfoViewModel IndicesInfoViewModel { get; set; } = new IndicesInfoViewModel();

        public ClusterInfoViewModel ClusterInfoViewModel { get; set; } = new ClusterInfoViewModel();

        //public IEnumerable<IndexInfoViewModel> IndexInfos { get; set; } = Enumerable.Empty<IndexInfoViewModel>();

    }

    public class IndicesInfoViewModel
    {

        public IndicesInfoViewModel()
        {
        }

        public IndicesInfoViewModel(double totalShards,
                                    double successfulShards,
                                    double indices,
                                    double templates,
                                    double documents,
                                    string totalSize)
        {
            TotalShards = totalShards;
            SuccessfulShards = successfulShards;
            Indices = indices;
            Templates = templates;
            Documents = documents;

            TotalSize = totalSize;
        }

        public double TotalShards { get; set; }

        public double SuccessfulShards { get; set; }

        public double Indices { get; set; }

        public double Templates { get; set; }

        public double Documents { get; set; }

        public string TotalSize { get; set; }

        //public string TotalSizeDes { get; set; } = string.Empty;

        //public IndexInfoViewModel IndexInfoViewModel { get;  set; } = new IndexInfoViewModel();
    }


    public class IndexInfoViewModel
    {
        public IndexInfoViewModel()
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
        public IndexInfoViewModel(string uUID,
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

        public string UUID { get; set; } = string.Empty;
        public string Primary { get; set; } = string.Empty;
        public string PrimaryStoreSize { get; set; } = string.Empty;
        public string Replica { get; set; } = string.Empty;
        public string Index { get; set; } = string.Empty;
        public string DocsCount { get; set; } = string.Empty;
        public string DocsDeleted { get; set; } = string.Empty;
        public string StoreSize { get; set; } = string.Empty;
        public string Health { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Aliase { get; set; } = string.Empty;
        /// <summary>
        /// 分片副本 */*
        /// </summary>
        public string PriRep { get; set; }
    }

    public class ClusterInfoViewModel
    {

        public ClusterHealthInfoViewModel ClusterHealthInfoViewModel { get; set; } = new ClusterHealthInfoViewModel();

        public ClusterStatsInfoViewModel ClusterStatsInfo { get; set; } = new ClusterStatsInfoViewModel();

        public JvmInfoViewModel Jvm { get; set; } = new JvmInfoViewModel();

        public OsMemberInfoViewModel MemberInfo { get; set; } = new OsMemberInfoViewModel();

        public EsFileSystemInfoViewModel FileSystemInfo { get; set; } = new EsFileSystemInfoViewModel();

        public FieldDataInfoViewModel FieldDataInfoViewModel { get; set; } = new FieldDataInfoViewModel();

        public QueryCacheInfoViewModel QueryCacheInfoViewModel { get; set; } = new QueryCacheInfoViewModel();

        public OSCpuInfoViewModel CpuInfo { get; set; } = new OSCpuInfoViewModel();
    }

    public class ClusterStatsInfoViewModel
    {


        public string UUID { get; set; } = string.Empty;
        public string ClusterName { get; set; } = string.Empty;
        public long Timestamp { get; set; }
        public string Version { get; set; } = string.Empty;

        public string OS { get; set; } = string.Empty;
    }


    public class OSCpuInfoViewModel
    {
        public OSCpuInfoViewModel()
        {
        }

        public OSCpuInfoViewModel(float usedPercent)
        {
            UsedPercent = usedPercent;
        }

        public float UsedPercent { get; set; }

        public float FreePercent { get; set; }

        public string Used { get { return this.UsedPercent.ToString(); } }

        public string Free { get { return this.FreePercent.ToString(); } }

        public string Desc { get; set; } = string.Empty;

        //public float UsedPercentValue
        //{
        //    get { return this.UsedPercent; }
        //}

        //public float FreePercentValue
        //{
        //    get { return this.FreePercent; }
        //}
    }

    public class ClusterHealthInfoViewModel
    {
        public ClusterHealthInfoViewModel()
        {
        }

        //public ClusterHealthInfoViewModel(string status, bool timeOut, int nodes, int dataNodes, int activePrimaryShards, int initializingShards, int unassignedShards)
        //{
        //    Status = status;
        //    TimeOut = timeOut;
        //    Nodes = nodes;
        //    DataNodes = dataNodes;
        //    ActivePrimaryShards = activePrimaryShards;
        //    InitializingShards = initializingShards;
        //    UnassignedShards = unassignedShards;
        //}

        public string Status { get; set; } = string.Empty;

        public bool TimeOut { get; set; }

        public int Nodes { get; set; }

        public int DataNodes { get; set; }

        public int ActivePrimaryShards { get; set; }

        public int InitializingShards { get; set; }

        public int UnassignedShards { get; set; }

    }

    public class QueryCacheInfoViewModel
    {
        public QueryCacheInfoViewModel()
        {
        }

        public QueryCacheInfoViewModel(long sizeInBytes, long totalCount, long cacheSize, long cacheCount)
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

    public class FieldDataInfoViewModel
    {

        public long SizeInBytes { get; set; }
    }

    public class EsFileSystemInfoViewModel
    {
        public EsFileSystemInfoViewModel()
        {
        }

        public EsFileSystemInfoViewModel(long totalInBytes, long freeInBytes)
        {
            TotalInBytes = totalInBytes;
            FreeInBytes = freeInBytes;
        }

        public long TotalInBytes { get; set; }
        public long FreeInBytes { get; set; }

        public long UsedInBytes { get; set; }

        public string Free { get; set; }
        public string Used { get; set; }

        public string Desc { get; set; } =string.Empty;

        public double UsedPercentValue { get; set; }
        public double FreePercentValue { get; set; }
    }

    public class OsMemberInfoViewModel
    {
         

        public long TotalBytes { get; set; }
        public long FreeBytes { get; set; }

        public int UsedPercent { get; set; }
        public int FreePercent { get; set; }

        //public int UsedPercentValue
        //{
        //    get
        //    {
        //        return UsedPercent;
        //    }
        //}
        //public int FreePercentValue
        //{
        //    get
        //    {
        //        var percent = this.FreePercent > 0 ? FreePercent : 1;
        //        return percent;
        //    }
        //}

        public string Desc { get; set; } = string.Empty;

        public string Total { get; set; } = string.Empty;
        public string Used { get; set; } = string.Empty;
        public string Free { get; set; } = string.Empty;
    }

    public class JvmInfoViewModel
    {
        public JvmInfoViewModel()
        {
        }



        public string Version { get; set; } = string.Empty;

        public long UpTimeInMillis { get; set; }
        public string UpTime { get; set; } = string.Empty;
        public long Threads { get; set; }

        public long HeapMaxInBytes { get; set; }

        public long HeapFreeInBytes { get; set; }

        public string Free { get; set; } = string.Empty;

        public string HeapMax { get; set; } = string.Empty;

        public string Used { get; set; } = string.Empty;

        //public ChartValues<double> UsedPercentValue { get; set; }

        //public ChartValues<double> FreePercentValue { get; set; }
        public double UsedPercentValue { get; set; }

        public double FreePercentValue { get; set; }

        public string Desc { get; set; } = string.Empty;

        public long HeapUsedInBytes { get; set; }

        public JvmInfoViewModel(string version, long upTimeInMillis, long threads, long heapMaxInBytes, long heapUsedInBytes, IEnumerable<JvmMemberInfoViewModel> jvmMemberInfos)
        {
            Version = version;
            UpTimeInMillis = upTimeInMillis;
            Threads = threads;
            HeapMaxInBytes = heapMaxInBytes;
            HeapUsedInBytes = heapUsedInBytes;
            JvmMemberInfos = jvmMemberInfos;
        }

        public IEnumerable<JvmMemberInfoViewModel> JvmMemberInfos { get; set; } = Enumerable.Empty<JvmMemberInfoViewModel>();
    }

    public class JvmMemberInfoViewModel
    {
        public JvmMemberInfoViewModel()
        {
        }

        public JvmMemberInfoViewModel(long heapMaxInBytes,
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
        public string HeapMax { get; set; } = string.Empty;
        public long HeapUsedInBytes { get; set; }
        public string HeapUsed { get; set; } = string.Empty;
        public long HeapUsedPercent { get; set; }
        public long UptimeInMilliseconds { get; set; }
    }

}
