using ElasticView.UI.Models;
using System;
using System.Windows;
using System.Windows.Controls;

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
            this.DataContext = new QueryPageViewModel();

            this.Loaded += QueryPage_Loaded;
        }


        private async void QueryPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is QueryPageViewModel viewModel)
            {
                await viewModel.InitAsync(_url, serviceProvider);
            }
        }

    }
}
