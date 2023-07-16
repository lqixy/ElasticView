using AutoMapper;
using ElasticView.AppService;
using ElasticView.AppService.Contracts;
using ElasticView.UI.Models;
using ElasticView.UI.Windows;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IElasticAppService _elasticAppService;
        private readonly IElasticIndexAppService _indexAppService;
        private readonly IElasticCatAppService _catAppService;

        private readonly IMapper _mapper;

        private readonly string _url;
        public IndicesPage(string url,
                           IServiceProvider serviceProvider)
        {
            _url = url;
            _elasticAppService = serviceProvider.GetRequiredService<IElasticAppService>();
            _indexAppService = serviceProvider.GetRequiredService<IElasticIndexAppService>();
            _catAppService = serviceProvider.GetRequiredService<IElasticCatAppService>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();


            InitializeComponent();
            LoadDataAsync();
        }


        private void LoadDataAsync()
        {
            var data = Task.Run(async () => await GetIndexInfos()).Result;
            //this.DataContext = data;
            indexGrid.ItemsSource = data;
        }


        private async Task<IEnumerable<IndexInfoViewModel>> GetIndexInfos(string keyword = "", bool all = false)
        {
            if (string.IsNullOrWhiteSpace(_url)) return Enumerable.Empty<IndexInfoViewModel>();

            var data = await _elasticAppService.GetIndexInfos(_url, all);
            var source = data
                .WhereIf(x => x.Index.Contains(keyword), !string.IsNullOrWhiteSpace(keyword))
                .ToList();

            //var indexNames = source.Select(x => x.Index);
            var result = await _catAppService.GetAliases(_url);
            foreach (var item in source)
            {
                item.Aliases = result.FirstOrDefault(x => x.IndexName == item.Index)?
                    .Alias ?? new string[0];
            }

            //await _indexAppService.GetAlias(_url,);
            //await _indexAppService.GetAliases(_url, indexNames);
            //string.IsNullOrWhiteSpace(keyword)
            // ? data
            // : data.Where(x => x.Index.Contains(keyword));
            return _mapper.Map<IEnumerable<IndexInfoViewModel>>(source);
        }



        private async void Keyword_TextChanged(object sender, TextChangedEventArgs e)
        {
            await QueryIndexInfos();
        }

        private async Task QueryIndexInfos()
        {
            var all = ShowAll.IsChecked ?? false;
            var value = keyword.Text;
            var source = await GetIndexInfos(value, all);
            if (!string.IsNullOrWhiteSpace(value))
            {
                source = source.Where(x => x.Index.Contains(value));
            }

            indexGrid.ItemsSource = source;
        }

        private async void ShowAll_Checked(object sender, RoutedEventArgs e)
        {
            await QueryIndexInfos();

        }

        private async void ShowAll_Unchecked(object sender, RoutedEventArgs e)
        {
            await QueryIndexInfos();
        }

        private void CreateBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var indexDetailWindow = new IndexDetail();
            indexDetailWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            indexDetailWindow.Owner = Application.Current.MainWindow;
            var dialogResult = indexDetailWindow.ShowDialog();
            switch (dialogResult)
            {
                case true:
                    MessageBox.Show("true");
                    break;
                case false:
                    MessageBox.Show("false");
                    break;
                default:
                    break;
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

        private async void InfoBtn_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn != null)
            {
                var indexName = btn.Tag as string;
                var content = await _indexAppService.Get(_url, indexName);

                ShowIndexDialog("IndicesInfo", indexName, content);
            }
        }

        private async void StatsBtn_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn != null)
            {
                var indexName = btn.Tag as string;
                var content = await _indexAppService.StatsJson(_url, indexName);
                ShowIndexDialog("IndicesStats", indexName, content);
            }
        }

        private void ShowIndexDialog(string pageTitle, string indexName, string content)
        {
            var window = new UI.Windows.IndexInfo(pageTitle, indexName, content);
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.Owner = Application.Current.MainWindow;
            window.ShowDialog();
        }

        private async void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            var indexName = GetIndexName(sender);
            if (!string.IsNullOrWhiteSpace(indexName))
            {
                var result = await _indexAppService.CloseIndex(_url, indexName);
                if (result) await QueryIndexInfos();
            }
        }

        private string GetIndexName(object sender)
        {
            var indexName = string.Empty;
            var btn = sender as Button;
            if (btn != null)
            {
                indexName = btn.Tag as string;
            }
            return indexName;
        }

        private async void OpenBtn_Click(object sender, RoutedEventArgs e)
        {
            var indexName = GetIndexName(sender);
            if (!string.IsNullOrWhiteSpace(indexName))
            {
                var result = await _indexAppService.OpenIndex(_url, indexName);
                if (result) await QueryIndexInfos();
            }
        }

        private async void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var indexName = GetIndexName(sender);
            if (!string.IsNullOrWhiteSpace(indexName))
            {
                var result = await _indexAppService.DeleteIndex(_url, indexName);
                if (result) await QueryIndexInfos();
            }
        }


        //private void keyrowd_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    keywordTip.Visibility = string.IsNullOrWhiteSpace(keyword.Text)
        //        ? Visibility.Visible : Visibility.Hidden;
        //}
    }
}
