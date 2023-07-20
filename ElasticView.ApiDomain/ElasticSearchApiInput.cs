using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticView.ApiDomain
{
    public class ElasticSearchApiInput
    {

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
