using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticView.AppService.Contracts.Output
{
    public class IndicesAliasOutput
    {
        //public IReadOnlyDictionary<string, IndicesAliasAliasesOutput> Aliases { get; set; } =
        //      new ReadOnlyDictionary<string, IndicesAliasAliasesOutput>(
        //          new Dictionary<string, IndicesAliasAliasesOutput>(0));
        public IndicesAliasAliasesOutput Data { get; set; }
    }

    public class IndicesAliasAliasesOutput
    {
        public IReadOnlyDictionary<string, IndicesAliasAliasesOutput> Aliases { get; set; } =
              new ReadOnlyDictionary<string, IndicesAliasAliasesOutput>(
                  new Dictionary<string, IndicesAliasAliasesOutput>(0));
    }
}
