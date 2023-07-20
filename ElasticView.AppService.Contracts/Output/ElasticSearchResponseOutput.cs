using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticView.AppService.Contracts.Output
{
    //public class ElasticSearchResponseOutput
    //{
    //    [JsonProperty("acknowledged")]
    //    public bool Success { get; set; }

    //    public string Index { get; set; }
        
    //    [JsonProperty("shards_acknowledged")]
    //    public bool ShardsAcknowledged { get; set; }
    //}

    public class CacheResponseOutput
    {
        [JsonProperty("_shards")]
        public CacheResponseShardsOutput Shards { get; set; }
    }

    public class CacheResponseShardsOutput
    {
        public int Total { get; set; }

        public int SuccessFul { get; set; }

        public int Failed { get; set; }
    }


}
