using AutoMapper;
using ElasticView.AppService.Contracts;
using ElasticView.UI.Models;
using ElasticView.UI.Pages;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace ElasticView.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IServiceProvider _serviceProvider;
        public MainWindow(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            InitializeComponent();

            InitData();

        }

        private void InitData()
        {
            this.DataContext = new MainWindowViewModel();
            this.Loaded += MainWindow_Loaded;
            MainFrame.Navigate(new DashPage(string.Empty, _serviceProvider));
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is MainWindowViewModel viewModel)
            {
                viewModel.Init(_serviceProvider, MainFrame);
            }
        }
         

        //private async void Connect(object sender, RoutedEventArgs e)
        //{
        //    var url = esUrl.Text;

        //    MainFrame.Navigate(new DashPage(url, _serviceProvider));
        //    IElasticAppService elasticAppService = _serviceProvider.GetRequiredService<IElasticAppService>();
        //    var status = await elasticAppService.GetStatus(url);
        //    //statusLabel.Content = status.ToString();
        //    //StatusLabel.Text = status.ToString();
        //    switch (status)
        //    {
        //        case StatusEnum.Green:
        //            StatusCard.Background = Brushes.Green;
        //            break;
        //        case StatusEnum.Yellow:
        //            StatusCard.Background = Brushes.Yellow;
        //            break;
        //        case StatusEnum.Red:
        //            StatusCard.Background = Brushes.Red;
        //            break;
        //        default:
        //            break;
        //    }
        //}
    }
}
