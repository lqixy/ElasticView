using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ElasticView.AppService.Contracts.Output
{
    public class NodeOutput
    {
        [JsonProperty("cluster_name")]
        public string ClusterName { get; internal set; }

        [JsonProperty("nodes")]
        public IReadOnlyDictionary<string, NodeStatsOutput> Nodes { get; internal set; } =
            new ReadOnlyDictionary<string, NodeStatsOutput>(new Dictionary<string, NodeStatsOutput>(0));

    }
    public class NodeStatsOutput
    {
        //[JsonProperty("adaptive_selection")]
        //[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, AdaptiveSelectionStats>))]
        //public IReadOnlyDictionary<string, AdaptiveSelectionStats> AdaptiveSelection { get; internal set; }
        //    = EmptyReadOnly<string, AdaptiveSelectionStats>.Dictionary;

        //[JsonProperty("breakers")]
        //[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, BreakerStats>))]
        //public IReadOnlyDictionary<string, BreakerStats> Breakers { get; internal set; }

        //[JsonProperty("fs")]
        //public FileSystemStatsOutput FileSystem { get; internal set; }

        [JsonProperty("host")]
        public string Host { get; internal set; }

        //[JsonProperty("http")]
        //public HttpStats Http { get; internal set; }

        //[JsonProperty("indices")]
        //public IndexStats Indices { get; internal set; }

        //[JsonProperty("ingest")]
        //public NodeIngestStats Ingest { get; internal set; }

        [JsonProperty("ip")]
        public IEnumerable<string> Ip { get; internal set; }

        [JsonProperty("jvm")]
        public NodeJvmStatsOutput Jvm { get; internal set; }

        [JsonProperty("name")]
        public string Name { get; internal set; }

        //[JsonProperty("os")]
        //public OperatingSystemStats OperatingSystem { get; internal set; }

        //[JsonProperty("process")]
        //public ProcessStats Process { get; internal set; }

        ///// <summary>
        ///// All of the different roles that the node fulfills. An empty
        ///// collection means that the node is a coordinating only node.
        ///// </summary>
        //[JsonProperty("roles")]
        //public IEnumerable<NodeRole> Roles { get; internal set; }

        //[JsonProperty("script")]
        //public ScriptStats Script { get; internal set; }

        ///// <summary>
        ///// Available in Elasticsearch 7.8.0+
        ///// </summary>
        //[JsonProperty("script_cache")]
        //public ScriptCacheStats ScriptCache { get; internal set; }

        //[JsonProperty("thread_pool")]
        //[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, ThreadCountStats>))]
        //public IReadOnlyDictionary<string, ThreadCountStats> ThreadPool { get; internal set; }

        //[JsonProperty("timestamp")]
        //public long Timestamp { get; internal set; }

        //[JsonProperty("transport")]
        //public TransportStats Transport { get; internal set; }

        //[JsonProperty("transport_address")]
        //public string TransportAddress { get; internal set; }

        //[JsonProperty("indexing_pressure")]
        //public IndexingPressureStats IndexingPressure { get; internal set; }
    }

    public class NodeJvmStatsOutput
    {
        [JsonProperty("buffer_pools")]
        public IReadOnlyDictionary<string, NodeBufferPoolOutput> BufferPools { get; internal set; } =
            new ReadOnlyDictionary<string, NodeBufferPoolOutput>(new Dictionary<string, NodeBufferPoolOutput>(0));

        [JsonProperty("classes")]
        public JvmClassesStatsOutput Classes { get; internal set; }

        [JsonProperty("gc")]
        public GarbageCollectionStatsOutput GarbageCollection { get; internal set; }

        [JsonProperty("mem")]
        public MemoryStatsOutput Memory { get; internal set; }

        [JsonProperty("threads")]
        public ThreadStatsOutput Threads { get; internal set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; internal set; }

        [JsonProperty("uptime")]
        public string Uptime { get; internal set; }

        [JsonProperty("uptime_in_millis")]
        public long UptimeInMilliseconds { get; internal set; }

        
        public class JvmClassesStatsOutput
        {
            [JsonProperty("current_loaded_count")]
            public long CurrentLoadedCount { get; internal set; }

            [JsonProperty("total_loaded_count")]
            public long TotalLoadedCount { get; internal set; }

            [JsonProperty("total_unloaded_count")]
            public long TotalUnloadedCount { get; internal set; }
        }

        
        public class MemoryStatsOutput
        {
            [JsonProperty("heap_committed")]
            public string HeapCommitted { get; internal set; }

            [JsonProperty("heap_committed_in_bytes")]
            public long HeapCommittedInBytes { get; internal set; }

            [JsonProperty("heap_max")]
            public string HeapMax { get; internal set; }

            [JsonProperty("heap_max_in_bytes")]
            public long HeapMaxInBytes { get; internal set; }

            [JsonProperty("heap_used")]
            public string HeapUsed { get; internal set; }

            [JsonProperty("heap_used_in_bytes")]
            public long HeapUsedInBytes { get; internal set; }

            [JsonProperty("heap_used_percent")]
            public long HeapUsedPercent { get; internal set; }

            [JsonProperty("non_heap_committed")]
            public string NonHeapCommitted { get; internal set; }

            [JsonProperty("non_heap_committed_in_bytes")]
            public long NonHeapCommittedInBytes { get; internal set; }

            [JsonProperty("non_heap_used")]
            public string NonHeapUsed { get; internal set; }

            [JsonProperty("non_heap_used_in_bytes")]
            public long NonHeapUsedInBytes { get; internal set; }

            [JsonProperty("pools")]
            public IReadOnlyDictionary<string, JvmPoolOutput> Pools { get; internal set; } =
                new ReadOnlyDictionary<string, JvmPoolOutput>(new Dictionary<string, JvmPoolOutput>(0));

            
            public class JvmPoolOutput
            {
                [JsonProperty("max")]
                public string Max { get; internal set; }

                [JsonProperty("max_in_bytes")]
                public long MaxInBytes { get; internal set; }

                [JsonProperty("peak_max")]
                public string PeakMax { get; internal set; }

                [JsonProperty("peak_max_in_bytes")]
                public long PeakMaxInBytes { get; internal set; }

                [JsonProperty("peak_used")]
                public string PeakUsed { get; internal set; }

                [JsonProperty("peak_used_in_bytes")]
                public long PeakUsedInBytes { get; internal set; }

                [JsonProperty("used")]
                public string Used { get; internal set; }

                [JsonProperty("used_in_bytes")]
                public long UsedInBytes { get; internal set; }
            }
        }

        
        public class ThreadStatsOutput
        {
            [JsonProperty("count")]
            public long Count { get; internal set; }

            [JsonProperty("peak_count")]
            public long PeakCount { get; internal set; }
        }

        
        public class GarbageCollectionStatsOutput
        {
            [JsonProperty("collectors")] public IReadOnlyDictionary<string, GarbageCollectionGenerationStatsOutput> Collectors { get; internal set; } = new ReadOnlyDictionary<string, GarbageCollectionGenerationStatsOutput>(new Dictionary<string, GarbageCollectionGenerationStatsOutput>(0));
        }

        
        public class GarbageCollectionGenerationStatsOutput
        {
            [JsonProperty("collection_count")]
            public long CollectionCount { get; internal set; }

            [JsonProperty("collection_time")]
            public string CollectionTime { get; internal set; }

            [JsonProperty("collection_time_in_millis")]
            public long CollectionTimeInMilliseconds { get; internal set; }
        }

        
        public class NodeBufferPoolOutput
        {
            [JsonProperty("count")]
            public long Count { get; internal set; }

            [JsonProperty("total_capacity")]
            public string TotalCapacity { get; internal set; }

            [JsonProperty("total_capacity_in_bytes")]
            public long TotalCapacityInBytes { get; internal set; }

            [JsonProperty("used")]
            public string Used { get; internal set; }

            [JsonProperty("used_in_bytes")]
            public long UsedInBytes { get; internal set; }
        }
    }
}
