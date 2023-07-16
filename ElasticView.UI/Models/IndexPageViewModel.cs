using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticView.UI.Models
{
    public class IndexPageViewModel
    {
        public int Total { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; } = 10;

        public IEnumerable<IndexInfoViewModel> IndexInfos { get; set; } = Enumerable.Empty<IndexInfoViewModel>();
    }
}
