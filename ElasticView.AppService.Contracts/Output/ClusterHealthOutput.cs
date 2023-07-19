using Newtonsoft.Json;

namespace ElasticView.AppService.Contracts.Output
{
    public class ClusterHealthOutput
    {
        [JsonProperty("active_primary_shards")]
        public int ActivePrimaryShards { get; internal set; }

        [JsonProperty("active_shards")]
        public int ActiveShards { get; internal set; }

        [JsonProperty("active_shards_percent_as_number")]
        public double ActiveShardsPercentAsNumber { get; internal set; }

        [JsonProperty("cluster_name")]
        public string ClusterName { get; internal set; }

        [JsonProperty("delayed_unassigned_shards")]
        public int DelayedUnassignedShards { get; internal set; }

        //[JsonProperty("indices")]
        //[JsonFormatter(typeof(ResolvableReadOnlyDictionaryFormatter<IndexName, IndexHealthStats>))]
        //public IReadOnlyDictionary<IndexName, IndexHealthStats> Indices { get; internal set; } =
        //    EmptyReadOnly<IndexName, IndexHealthStats>.Dictionary;

        [JsonProperty("initializing_shards")]
        public int InitializingShards { get; internal set; }

        [JsonProperty("number_of_data_nodes")]
        public int NumberOfDataNodes { get; internal set; }

        [JsonProperty("number_of_in_flight_fetch")]
        public int NumberOfInFlightFetch { get; internal set; }

        [JsonProperty("number_of_nodes")]
        public int NumberOfNodes { get; internal set; }

        [JsonProperty("number_of_pending_tasks")]
        public int NumberOfPendingTasks { get; internal set; }

        [JsonProperty("relocating_shards")]
        public int RelocatingShards { get; internal set; }

        [JsonProperty("status")]
        public string Status { get; internal set; }

        [JsonProperty("task_max_waiting_in_queue_millis")]
        public long TaskMaxWaitTimeInQueueInMilliseconds { get; internal set; }

        [JsonProperty("timed_out")]
        public bool TimedOut { get; internal set; }

        [JsonProperty("unassigned_shards")]
        public int UnassignedShards { get; internal set; }
    }
}
