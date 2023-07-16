using ElasticView.ApiDomain;
using ElasticView.AppService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticView.AppService
{
    public class ElasticApiAppService : IElasticApiAppService
    {
        private readonly IApiContext _context;

        public ElasticApiAppService(IApiContext context)
        {
            _context = context;
        }

        
    }
}
