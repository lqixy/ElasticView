using ElasticView.ApiDomain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticView.AppService.Contracts.ApiDto
{
    public class CreateIndexApiInput : ElasticSearchApiInput
    {
        public CreateIndexApiInput()
        {
        }

        public CreateIndexApiInput(CreateIndexApiSettingsInput settings)
        {
            Settings = settings;
        }

        [JsonProperty("settings")]
        public CreateIndexApiSettingsInput Settings { get; set; }
    }

    public class CreateIndexApiSettingsInput
    {
        public CreateIndexApiSettingsInput()
        {
        }

        public CreateIndexApiSettingsInput(int shardsCount, int replicasCount)
        {
            ShardsCount = shardsCount;
            ReplicasCount = replicasCount;
        }

        [JsonProperty("number_of_shards")]
        public int ShardsCount { get; set; } = 1;

        [JsonProperty("number_of_replicas")]
        public int ReplicasCount { get; set; } = 1;
    }
}
