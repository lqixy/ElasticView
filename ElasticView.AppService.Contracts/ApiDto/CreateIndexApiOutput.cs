using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticView.AppService.Contracts.ApiDto
{
    public class CreateIndexApiOutput
    {
        public CreateIndexApiOutput()
        {
        }

        public CreateIndexApiOutput(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }

        public string Message { get; set; }
    }
}
