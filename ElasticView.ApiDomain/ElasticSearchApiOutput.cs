using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticView.ApiDomain
{
    public class ElasticSearchApiOutput
    {
        [JsonProperty("acknowledged")]
        public bool Success { get; set; }

        public string Index { get; set; }

        [JsonProperty("shards_acknowledged")]
        public bool ShardsAcknowledged { get; set; }

        [JsonProperty("error")]
        public ElasticSearchErrorApiOutput Error { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }
    }
    //public class ElasticSearchAcknowledgedApiOutput
    //{
    //    [JsonProperty("acknowledged")]
    //    public bool Success { get; set; }

    //    public string Index { get; set; }

    //    [JsonProperty("shards_acknowledged")]
    //    public bool ShardsAcknowledged { get; set; }
    //}

    public   class ElasticSearchErrorApiOutput
    {
        [JsonProperty("root_cause", NullValueHandling = NullValueHandling.Ignore)]
        public List<ElasticSearchErrorApiOutput> RootCause { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("index_uuid")]
        public string IndexUuid { get; set; }

        [JsonProperty("index")]
        public string Index { get; set; }
    }
     
}
