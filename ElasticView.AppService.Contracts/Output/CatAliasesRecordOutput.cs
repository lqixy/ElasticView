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
    public class CatAliasesRecordOutput
    {
        [JsonProperty("alias")]
        public string Alias { get; set; } 

        [JsonProperty("filter")]
        public string Filter { get; set; }

        [JsonProperty("index")]
        public string Index { get; set; }

        [JsonProperty("indexRouting")]
        public string IndexRouting { get; set; }

        [JsonProperty("searchRouting")]
        public string SearchRouting { get; set; }
    }
}
