using AutoMapper;
using ElasticView.AppService.Contracts;
using ElasticView.UI.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ElasticView.UI.Pages
{
    /// <summary>
    /// DashPage.xaml 的交互逻辑
    /// </summary>
    public partial class DashPage : Page
    {
        private readonly string url;
        private readonly IServiceProvider serviceProvider;

        public DashPage(string url, IServiceProvider serviceProvider)
        {

            this.url = url;
            this.serviceProvider = serviceProvider;

            InitializeComponent();

            InitData();
        }

        private void InitData()
        {
            this.DataContext = new DashPageViewModel();
            this.Loaded += DashPage_Loaded;
        }

        private async void DashPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is DashPageViewModel viewModel)
            {
                await viewModel.InitAsync(url, serviceProvider);
            }
        }
    }
}
