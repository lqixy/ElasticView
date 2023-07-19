using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ElasticView.AppService.Contracts.Output
{
    public class ClusterStatsOutput
    {
        [JsonProperty("cluster_name")]
        public string ClusterName { get; internal set; }

        [JsonProperty("cluster_uuid")]
        public string ClusterUUID { get; internal set; }

        [JsonProperty("indices")]
        public ClusterIndicesStatsOutput Indices { get; internal set; }

        [JsonProperty("nodes")]
        public ClusterNodesStatsOutput Nodes { get; internal set; }

        [JsonProperty("status")]
        public string Status { get; internal set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; internal set; }
    }

    public class ClusterNodesStatsOutput
    {
        [JsonProperty("count")]
        public ClusterNodeCountOutput Count { get; internal set; }

        [JsonProperty("discovery_types")]
        public IReadOnlyDictionary<string, int> DiscoveryTypes { get; internal set; }

        [JsonProperty("fs")]
        public ClusterFileSystemOutput FileSystem { get; internal set; }

        [JsonProperty("jvm")]
        public ClusterJvmOutput Jvm { get; internal set; }

        [JsonProperty("network_types")]
        public ClusterNetworkTypesOutput NetworkTypes { get; internal set; }

        [JsonProperty("os")]
        public ClusterOperatingSystemStatsOutput OperatingSystem { get; internal set; }

        //[JsonProperty("packaging_types")]
        //public IReadOnlyCollection<NodePackagingType> PackagingTypes { get; internal set; }

        //[JsonProperty("plugins")]
        //public IReadOnlyCollection<PluginStats> Plugins { get; internal set; }

        [JsonProperty("process")]
        public ClusterProcessOutput Process { get; internal set; }

        [JsonProperty("versions")]
        public IReadOnlyCollection<string> Versions { get; internal set; }

        //[JsonProperty("ingest")]
        //public ClusterIngestStats Ingest { get; internal set; }
    }
     
    public class ClusterProcessOutput
    {
        [JsonProperty("cpu")]
        public ClusterProcessCpuOutput Cpu { get; internal set; }

        [JsonProperty("open_file_descriptors")]
        public ClusterProcessOpenFileDescriptorsOutput OpenFileDescriptors { get; internal set; }
    }

    public class ClusterProcessOpenFileDescriptorsOutput
    {
        [JsonProperty("avg")]
        public long Avg { get; internal set; }

        [JsonProperty("max")]
        public long Max { get; internal set; }

        [JsonProperty("min")]
        public long Min { get; internal set; }
    }
    public class ClusterProcessCpuOutput
    {

        [JsonProperty("percent")]
        public int Percent { get; internal set; }
    }
    public class ClusterOperatingSystemStatsOutput
    {
        [JsonProperty("allocated_processors")]
        public int AllocatedProcessors { get; internal set; }

        [JsonProperty("available_processors")]
        public int AvailableProcessors { get; internal set; }

        [JsonProperty("mem")]
        public OperatingSystemMemoryInfoOutput Memory { get; internal set; }

        [JsonProperty("names")]
        public IReadOnlyCollection<ClusterOperatingSystemNameOutput> Names { get; internal set; }

        [JsonProperty("pretty_names")]
        public IReadOnlyCollection<ClusterOperatingSystemPrettyNaneOutput> PrettyNames { get; internal set; }

        [JsonProperty("architectures")]
        public IReadOnlyCollection<ArchitectureStatsOutput> Architectures { get; internal set; }
    }

    public class ArchitectureStatsOutput
    {
        [JsonProperty("arch")]
        public string Architecture { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }

    public class ClusterOperatingSystemPrettyNaneOutput
    {
        [JsonProperty("count")]
        public int Count { get; internal set; }

        [JsonProperty("pretty_name")]
        public string PrettyName { get; internal set; }
    }

    public class ClusterOperatingSystemNameOutput
    {
        [JsonProperty("count")]
        public int Count { get; internal set; }

        [JsonProperty("name")]
        public string Name { get; internal set; }
    }

    public class OperatingSystemMemoryInfoOutput
    {
        [JsonProperty("free_in_bytes")]
        public long FreeBytes { get; internal set; }

        [JsonProperty("free_percent")]
        public int FreePercent { get; internal set; }

        [JsonProperty("total_in_bytes")]
        public long TotalBytes { get; internal set; }

        [JsonProperty("used_in_bytes")]
        public long UsedBytes { get; internal set; }

        [JsonProperty("used_percent")]
        public int UsedPercent { get; internal set; }
    }

    public class ClusterNetworkTypesOutput
    {
        [JsonProperty("http_types")]
        public IReadOnlyDictionary<string, int> HttpTypes { get; internal set; }

        [JsonProperty("transport_types")]
        public IReadOnlyDictionary<string, int> TransportTypes { get; internal set; }
    }

    public class ClusterJvmOutput
    {
        [JsonProperty("max_uptime_in_millis")]
        public long MaxUptimeInMilliseconds { get; internal set; }

        [JsonProperty("mem")]
        public ClusterJvmMemoryOutput Memory { get; internal set; }

        [JsonProperty("threads")]
        public long Threads { get; internal set; }

        [JsonProperty("versions")]
        public IReadOnlyCollection<ClusterJvmVersionOutput> Versions { get; internal set; }
    }

    public class ClusterJvmVersionOutput
    {
        [JsonProperty("bundled_jdk")]
        public bool BundledJdk { get; internal set; }

        [JsonProperty("count")]
        public int Count { get; internal set; }

        [JsonProperty("using_bundled_jdk")]
        public bool? UsingBundledJdk { get; internal set; }

        [JsonProperty("version")]
        public string Version { get; internal set; }

        [JsonProperty("vm_name")]
        public string VmName { get; internal set; }

        [JsonProperty("vm_vendor")]
        public string VmVendor { get; internal set; }

        [JsonProperty("vm_version")]
        public string VmVersion { get; internal set; }
    }

    public class ClusterJvmMemoryOutput
    {
        [JsonProperty("heap_max_in_bytes")]
        public long HeapMaxInBytes { get; internal set; }

        [JsonProperty("heap_used_in_bytes")]
        public long HeapUsedInBytes { get; internal set; }
    }

    public class ClusterFileSystemOutput
    {
        [JsonProperty("available_in_bytes")]
        public long AvailableInBytes { get; internal set; }

        [JsonProperty("free_in_bytes")]
        public long FreeInBytes { get; internal set; }

        [JsonProperty("total_in_bytes")]
        public long TotalInBytes { get; internal set; }
    }

    public class ClusterNodeCountOutput
    {
        [JsonProperty("coordinating_only")]
        public int CoordinatingOnly { get; internal set; }

        [JsonProperty("data")]
        public int Data { get; internal set; }

        [JsonProperty("ingest")]
        public int Ingest { get; internal set; }

        [JsonProperty("master")]
        public int Master { get; internal set; }

        [JsonProperty("total")]
        public int Total { get; internal set; }

        [JsonProperty("voting_only")]
        public int VotingOnly { get; internal set; }
    }

    public class ClusterIndicesStatsOutput
    {
        [JsonProperty("completion")]
        public CompletionStatsOutput Completion { get;  set; }

        [JsonProperty("count")]
        public long Count { get;  set; }

        [JsonProperty("docs")]
        public DocStatsOutput Documents { get;  set; }

        [JsonProperty("fielddata")]
        public FielddataStatsOutput Fielddata { get;  set; }

        [JsonProperty("query_cache")]
        public QueryCacheStatsOutput QueryCache { get;  set; }

        [JsonProperty("segments")]
        public SegmentsStatsOutput Segments { get;  set; }

        [JsonProperty("shards")]
        public ClusterIndicesShardsStatsOutput Shards { get;  set; }

        [JsonProperty("store")]
        public StoreStatsOutput Store { get;  set; }
    }

    public class CompletionStatsOutput
    {

        [JsonProperty("size_in_bytes")]
        public long SizeInBytes { get; set; }
    }

    public class DocStatsOutput
    {

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("deleted")]
        public long Deleted { get; set; }
    }

    public class FielddataStatsOutput
    {

        [JsonProperty("evictions")]
        public long Evictions { get; set; }

        [JsonProperty("memory_size_in_bytes")]
        public long MemorySizeInBytes { get; set; }
    }

    public class QueryCacheStatsOutput
    {
        [JsonProperty("cache_count")]
        public long CacheCount { get; set; }

        [JsonProperty("cache_size")]
        public long CacheSize { get; set; }

        [JsonProperty("evictions")]
        public long Evictions { get; set; }

        [JsonProperty("hit_count")]
        public long HitCount { get; set; }

        [JsonProperty("memory_size_in_bytes")]
        public long MemorySizeInBytes { get; set; }

        [JsonProperty("miss_count")]
        public long MissCount { get; set; }

        [JsonProperty("total_count")]
        public long TotalCount { get; set; }
    }

    public class SegmentsStatsOutput
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("doc_values_memory_in_bytes")]
        public long DocValuesMemoryInBytes { get; set; }

        [JsonProperty("fixed_bit_set_memory_in_bytes")]
        public long FixedBitSetMemoryInBytes { get; set; }

        [JsonProperty("index_writer_max_memory_in_bytes")]
        public long IndexWriterMaxMemoryInBytes { get; set; }

        [JsonProperty("index_writer_memory_in_bytes")]
        public long IndexWriterMemoryInBytes { get; set; }

        [JsonProperty("max_unsafe_auto_id_timestamp")]
        public long MaximumUnsafeAutoIdTimestamp { get; set; }

        [JsonProperty("memory_in_bytes")]
        public long MemoryInBytes { get; set; }

        [JsonProperty("norms_memory_in_bytes")]
        public long NormsMemoryInBytes { get; set; }

        [JsonProperty("points_memory_in_bytes")]
        public long PointsMemoryInBytes { get; set; }

        [JsonProperty("stored_fields_memory_in_bytes")]
        public long StoredFieldsMemoryInBytes { get; set; }

        [JsonProperty("terms_memory_in_bytes")]
        public long TermsMemoryInBytes { get; set; }

        [JsonProperty("term_vectors_memory_in_bytes")]
        public long TermVectorsMemoryInBytes { get; set; }

        [JsonProperty("version_map_memory_in_bytes")]
        public long VersionMapMemoryInBytes { get; set; }

        //[JsonProperty("file_sizes")]
        //public IReadOnlyDictionary<string, ShardFileSizeInfo> FileSizes { get; internal set; }
    }

    public class StoreStatsOutput
    {
        /// <summary>
		/// Total size of all shards assigned to the node.
		/// </summary>
		[JsonProperty("size")]
        public string Size { get; set; }

        /// <summary>
        /// Total size, in bytes, of all shards assigned to the node.
        /// </summary>
        // TODO: should be long
        [JsonProperty("size_in_bytes")]
        public double SizeInBytes { get; set; }

        /// <summary>
        /// A prediction of how much larger the shard stores on this node will eventually grow due to ongoing peer recoveries, restoring snapshots,
        /// and similar activities. A value of -1b indicates that this is not available.
        /// <para />
        /// Valid in Elasticsearch 7.9.0+
        /// </summary>
        [JsonProperty("reserved")]
        public string Reserved { get; set; }

        /// <summary>
        /// A prediction, in bytes, of how much larger the shard stores on this node will eventually grow due to ongoing peer recoveries,
        /// restoring snapshots, and similar activities. A value of -1 indicates that this is not available.
        /// <para />
        /// Valid in Elasticsearch 7.9.0+
        /// </summary>
        [JsonProperty("reserved_in_bytes")]
        public long ReservedInBytes { get; set; }
    }

    public class ClusterIndicesShardsStatsOutput
    {

        [JsonProperty("index")]
        public ClusterIndicesShardsIndexStatsOutput Index { get; internal set; }

        [JsonProperty("primaries")]
        public double Primaries { get; internal set; }

        [JsonProperty("replication")]
        public double Replication { get; internal set; }

        [JsonProperty("total")]
        public double Total { get; internal set; }
    }

    public class ClusterIndicesShardsIndexStatsOutput
    {
        [JsonProperty("primaries")]
        public ClusterShardMetricsOutput Primaries { get; internal set; }

        [JsonProperty("replication")]
        public ClusterShardMetricsOutput Replication { get; internal set; }

        [JsonProperty("shards")]
        public ClusterShardMetricsOutput Shards { get; internal set; }
    }
     
    public class ClusterShardMetricsOutput
    {
        [JsonProperty("avg")]
        public double Avg { get; internal set; }

        [JsonProperty("max")]
        public double Max { get; internal set; }

        [JsonProperty("min")]
        public double Min { get; internal set; }
    }
}
