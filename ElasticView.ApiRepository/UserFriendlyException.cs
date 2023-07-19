using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticView.ApiRepository
{
    public class UserFriendlyException : Exception
    {
        public UserFriendlyException(string? message) : base(message)
        {
        }
    }
}
