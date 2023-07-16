using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticView.UI.Models
{
    public class CatNodesViewModel
    {
        public string Cpu { get; set; } = string.Empty;
        public string CpuPercent { get; set; } = string.Empty;

        public string HeapUsed { get; set; } = string.Empty;

        public string HeapMax { get; set; } = string.Empty;

        public string HeapUsedPercent { get; set; } = string.Empty;
        public string HeapUsedPercentDesc { get; set; } = string.Empty;
        public string HeapDesc { get; set; } = string.Empty;
        public string Ip { get; set; } = string.Empty;

        public string Port { get; set; } = string.Empty;

        public string Master { get; set; } = string.Empty;

        public string MasterDesc { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string RamUsed { get; set; } = string.Empty;

        public string RamMax { get; set; } = string.Empty;

        public string RamDesc { get; set; } = string.Empty;

        public string RamUsedPercent { get; set; } = string.Empty;

        public string RamUsedPercentDesc { get; set; } = string.Empty;


        public string DiskDesc { get; set; } = string.Empty;

        public string DiskUsed { get; set; } = string.Empty;

        
        public string DiskUsedPercent { get; set; } = string.Empty;
        public string DiskUsedPercentDesc { get; set; } = string.Empty;


        public string DiskTotal { get; set; } = string.Empty;
    }
}
