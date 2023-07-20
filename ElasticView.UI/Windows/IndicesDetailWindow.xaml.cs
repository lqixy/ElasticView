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
    /// IndicesDetailWindow.xaml 的交互逻辑
    /// </summary>
    public partial class IndicesDetailWindow : Window
    {
        private readonly string _url;
        //private readonly IServiceProvider _serviceProvider;
        private readonly IElasticIndexAppService indexAppService;


        public IndicesDetailWindow(string url, IElasticIndexAppService indexAppService)
        {
            _url = url;
            //_serviceProvider = serviceProvider;
            this.indexAppService = indexAppService;


            InitializeComponent();

            this.DataContext = new IndicesDetailWindowViewModel();
            this.Loaded += IndicesDetailWindow_Loaded;
        }

        private void IndicesDetailWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is IndicesDetailWindowViewModel viewModel)
            {
                viewModel.Init(_url, indexAppService);
            }
        }
    }
}
