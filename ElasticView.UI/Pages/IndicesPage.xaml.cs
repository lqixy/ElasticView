using ElasticView.AppService.Contracts;
using ElasticView.UI.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ElasticView.UI.Pages
{
    /// <summary>
    /// IndicesPage.xaml 的交互逻辑
    /// </summary>
    public partial class IndicesPage : System.Windows.Controls.Page
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IElasticIndexAppService _indexAppService;
        private readonly string _url;
        public IndicesPage(string url,
                           IServiceProvider serviceProvider)
        {
            _url = url;
            this.serviceProvider = serviceProvider;
            this._indexAppService = serviceProvider
                .GetRequiredService<IElasticIndexAppService>();

            InitializeComponent();

            InitData();
        }

        private void InitData()
        {
            this.DataContext = new IndicesPageViewModel();
            this.Loaded += IndicesPage_Loaded;
        }

        private async void IndicesPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is IndicesPageViewModel viewModel)
            {
                await viewModel.InitAsync(_url, serviceProvider);
            }
        }
         
        private void DataGridRow_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var dep = e.OriginalSource as DependencyObject;
            while ((dep != null) && dep is not DataGridRow && dep is not Button)
            {
                dep = VisualTreeHelper.GetParent(dep);
            }
            if (dep is Button)
            {
                return;
            }
        }

        //private async void InfoBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    var btn = sender as Button;
        //    if (btn != null)
        //    {
        //        var indexName = btn.Tag as string;
        //        var content = await _indexAppService.Get(_url, indexName);

        //        ShowIndexDialog("IndicesInfo", indexName, content);
        //    }
        //}

        //private async void StatsBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    var btn = sender as Button;
        //    if (btn != null)
        //    {
        //        var indexName = btn.Tag as string;
        //        var content = await _indexAppService.StatsJson(_url, indexName);
        //        ShowIndexDialog("IndicesStats", indexName, content);
        //    }
        //}

        //private void ShowIndexDialog(string pageTitle, string indexName, string content)
        //{
        //    var window = new UI.Windows.IndexInfo(pageTitle, indexName, content);
        //    window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        //    window.Owner = Application.Current.MainWindow;
        //    window.ShowDialog();
        //}

        //private async void CloseBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    var indexName = GetIndexName(sender);
        //    if (!string.IsNullOrWhiteSpace(indexName))
        //    {
        //        var result = await _indexAppService.CloseIndex(_url, indexName);
        //        //if (result) await QueryIndexInfos();
        //    }
        //}

        //private string GetIndexName(object sender)
        //{
        //    var indexName = string.Empty;
        //    var btn = sender as Button;
        //    if (btn != null)
        //    {
        //        indexName = btn.Tag as string;
        //    }
        //    return indexName;
        //}

        //private async void OpenBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    var indexName = GetIndexName(sender);
        //    if (!string.IsNullOrWhiteSpace(indexName))
        //    {
        //        var result = await _indexAppService.OpenIndex(_url, indexName);
        //        //if (result) await QueryIndexInfos(); 
                
        //    }
        //}

        //private async void DeleteBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    var indexName = GetIndexName(sender);
        //    if (!string.IsNullOrWhiteSpace(indexName))
        //    {
        //        var result = await _indexAppService.DeleteIndex(_url, indexName);
        //        //if (result) await QueryIndexInfos(); 
        //    }
        //}


        //private void keyrowd_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    keywordTip.Visibility = string.IsNullOrWhiteSpace(keyword.Text)
        //        ? Visibility.Visible : Visibility.Hidden;
        //}
    }
}
