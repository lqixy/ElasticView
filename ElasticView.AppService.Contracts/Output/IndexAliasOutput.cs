using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticView.AppService.Contracts.Output
{
    public class IndexAliasOutput
    {
        public IndexAliasOutput()
        {
        }

        public IndexAliasOutput(string indexName, string[] alias)
        {
            IndexName = indexName;
            Alias = alias;
        }

        public string IndexName { get; set; } = string.Empty;

        public string[] Alias { get; set; } = new string[0];
    }
}
