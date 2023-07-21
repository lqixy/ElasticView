using ElasticView.AppService.Contracts;
using ElasticView.UI.Models;
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
using System.Windows.Shapes;

namespace ElasticView.UI.Windows
{
    /// <summary>
    /// IndicesAliasWindow.xaml 的交互逻辑
    /// </summary>
    public partial class IndicesAliasWindow : Window
    {
        private readonly string _indexName;
        private readonly IElasticIndexAppService _indexAppService;
        private readonly string _url;
        public IndicesAliasWindow(string url, string indexName, IElasticIndexAppService indexAppService)
        {
            _url = url;
            this._indexName = indexName;
            _indexAppService = indexAppService;

            InitializeComponent();
            
            this.DataContext = new IndicesAliasWindowViewModel();
            this.Loaded += IndicesAliasWindow_Loaded;

            this.Closing += IndicesAliasWindow_Closing;
        }

        private void IndicesAliasWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            this.DialogResult = true;
        }

        private async void IndicesAliasWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is IndicesAliasWindowViewModel viewModel)
            {
                await viewModel.InitAsync(_url, _indexName, _indexAppService);
            }
        }
    }
}
