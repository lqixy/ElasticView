using ElasticView.AppService.Contracts;
using ElasticView.AppService.Contracts.InputDto;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticView.AppService
{
    public class ElasticOperationAppService : IElasticOperationAppService
    {
        


        private ElasticClient GetClient(string url)
        {
            return new ElasticClient(new Uri(url));

        }
    }
}
