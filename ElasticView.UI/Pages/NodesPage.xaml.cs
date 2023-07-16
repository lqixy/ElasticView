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
    /// NodesPage.xaml 的交互逻辑
    /// </summary>
    public partial class NodesPage : Page
    {
        private readonly IElasticCatAppService _catAppService;
        private readonly string _url;
        private readonly IMapper _mapper;
        public NodesPage(string url, IServiceProvider serviceProvider)
        {
            _url = url;
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _catAppService = serviceProvider.GetRequiredService<IElasticCatAppService>();


            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var source = Task.Run(async () => await GetCatNodes()).Result;
            NodesGrid.ItemsSource = source;
        }

        private async Task<IEnumerable<CatNodesViewModel>> GetCatNodes()
        {
            var result = await _catAppService.GetNodes(_url);
            return _mapper.Map<IEnumerable<CatNodesViewModel>>(result);
        }

        private void Keyword_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
