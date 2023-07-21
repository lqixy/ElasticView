using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticView.AppService.Contracts.Output
{
    public partial class QueryIndicesInfoDto
    {
        public Articles Articles { get; set; }
    }

    public partial class Articles
    {
        public Aliases Aliases { get; set; }
        public Mappings Mappings { get; set; }
        public Settings Settings { get; set; }
    }

    public partial class Aliases
    {
        public Mappings Test1 { get; set; }
        public Mappings Test12 { get; set; }
        public Mappings Test3 { get; set; }
    }

    public partial class Mappings
    {
    }

    public partial class Settings
    {
        public Index Index { get; set; }
    }

    public partial class Index
    {
        public string CreationDate { get; set; }
        public long NumberOfShards { get; set; }
        public long NumberOfReplicas { get; set; }
        public string Uuid { get; set; }
        public Version Version { get; set; }
        public string ProvidedName { get; set; }
    }

    public partial class Version
    {
        public long Created { get; set; }
    }
}
