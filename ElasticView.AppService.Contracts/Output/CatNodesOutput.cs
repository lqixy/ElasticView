using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticView.AppService.Contracts.Output
{
    public class CatNodesOutput
    {
        public string Cpu { get; set; } = string.Empty;

        [JsonProperty("heap.current")]
        public string HeapUsed { get; set; } = string.Empty;

        [JsonProperty("heap.max")]
        public string HeapMax { get; set; } = string.Empty;

        [JsonProperty("heap.percent")]
        public string HeapUsedPercent { get; set; } = string.Empty;

        public string Ip { get; set; } = string.Empty;

        public string Port { get; set; } = string.Empty;

        public string Master { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        [JsonProperty("node.role")]
        public string Role { get; set; } = string.Empty;

        [JsonProperty("ram.current")]
        public string RamUsed { get; set; } = string.Empty;

        [JsonProperty("ram.max")]
        public string RamMax { get; set; } = string.Empty;

        [JsonProperty("ram.percent")]
        public string RamUsedPercent { get; set; } = string.Empty;

        [JsonProperty("disk.used")]
        public string DiskUsed { get; set; } = string.Empty;

        [JsonProperty("disk.used_percent")]
        public string DiskUsedPercent { get; set; } = string.Empty;

        [JsonProperty("disk.total")]
        public string DiskTotal { get; set; } = string.Empty;
    }
}
