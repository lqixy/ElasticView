using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticView.UI.Models
{
    public class StatusViewModel : ObservableObject
    {
        private string _status;

        public string Status { get { return _status; } set { SetProperty(ref _status, value); } }

    }
}
