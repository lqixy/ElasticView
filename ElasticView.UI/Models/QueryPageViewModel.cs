using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ElasticView.AppService.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticView.UI.Models
{
    public partial class QueryPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<dynamic> sourcesData;

        [ObservableProperty]
        private IEnumerable<string> indexNames = Enumerable.Empty<string>();

        [ObservableProperty]
        private string curSelectedItem;

        private IElasticAppService elasticAppService;

        public QueryPageViewModel()
        {
            var list = Enumerable.Range(0, 100)
                .Select(x => new
                {
                    Index = x,
                    NameH = $"name:{x}",
                    AgeAD = x > 50 ? x - 10 : x + 10,
                    GenderSDF = x % 2 == 0,
                });
            SourcesData = new ObservableCollection<dynamic>(list);
        }

        public async Task InitAsync(string url, IServiceProvider serviceProvider)
        {
            elasticAppService = serviceProvider.GetRequiredService<IElasticAppService>();

            var indexes = await elasticAppService.GetIndexInfos(url);
            IndexNames = indexes.Select(x => x.Index);
        }

        [RelayCommand]
        private void SearchDocument()
        {
            var value = this.CurSelectedItem;
        }
    }
}
