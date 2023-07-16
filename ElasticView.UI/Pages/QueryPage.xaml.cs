using AutoMapper;
using ElasticView.AppService.Contracts;
using ElasticView.UI.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ElasticView.UI.Pages
{
    /// <summary>
    /// QueryPage.xaml 的交互逻辑
    /// </summary>
    public partial class QueryPage : Page
    {
        private readonly string _url;
        private readonly IServiceProvider serviceProvider;

        public QueryPage(string url, IServiceProvider serviceProvider)
        {
            _url = url;
            this.serviceProvider = serviceProvider;

            InitializeComponent();

            InitData();
        }

        private void InitData()
        {
            //this.DataContext = new DashPageViewModel();
            this.DataContext = new QueryPageViewModel();

            //this.Loaded += QueryPage_Loaded;
        }


        private async void QueryPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is IndicesPageViewModel viewModel)
            {
                //await viewModel.InitAsync(_url, serviceProvider);
                await viewModel.InitAsync();
            }
        }

    }
}
