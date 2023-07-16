using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticView.AppService.Contracts.InputDto
{
    public class CreateIndexInput
    {
        public CreateIndexInput()
        {
        }

        public CreateIndexInput(string name, int shardsCount, int replicasCount)
        {
            Name = name;
            ShardsCount = shardsCount;
            ReplicasCount = replicasCount;
        }

        public string Name { get; set; }

        public int ShardsCount { get; set; }

        public int ReplicasCount { get; set; }
    }
}
