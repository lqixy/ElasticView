using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticView.UI.Models
{
    public partial class QueryPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string pwd = string.Empty;


        [RelayCommand]
        private void UpdatePwd(string pwd)
        {
            this.pwd = pwd;
        }

    }
}
