using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ElasticView.AppService;
using ElasticView.AppService.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ElasticView.UI.Models
{
    public partial class IndexPageViewModel : ObservableObject
    {
        private string _url;
        private IElasticAppService elasticAppService;
        private IElasticCatAppService catAppService;
        private IMapper mapper;


        [ObservableProperty]
        private int pageIndex = 1;


        [ObservableProperty]
        private int pageSize = 10;

        [ObservableProperty]
        private int totalPages = 1;

        [ObservableProperty]
        private string keyword = string.Empty;

        [ObservableProperty]
        private IEnumerable<IndexInfoViewModel> indexInfos = Enumerable.Empty<IndexInfoViewModel>();


        public async Task InitAsync(string url, IServiceProvider serviceProvider)
        {
            _url = url;

            elasticAppService = serviceProvider
              .GetRequiredService<IElasticAppService>();
            catAppService = serviceProvider
              .GetRequiredService<IElasticCatAppService>();
            mapper = serviceProvider
              .GetRequiredService<IMapper>();

            await GetIndInfosAsync();

        }

        private async Task GetIndInfosAsync()
        {

            var sources = await elasticAppService
                .GetIndexInfos(_url);
            var data = sources
                .WhereIf(x => x.Index.Contains(Keyword),
                !string.IsNullOrWhiteSpace(Keyword))
            .ToList();

            var aliases = await catAppService.GetAliases(_url);

            foreach (var item in data)
            {
                var curAliase = aliases
                      .FirstOrDefault(x => x.IndexName == item.Index)?
                      .Alias ?? new string[0];
                item.UpdateAliases(curAliase);
            }

            IndexInfos = mapper.Map<IEnumerable<IndexInfoViewModel>>(data);

        }


        public IAsyncRelayCommand SearchIndexInfosCommand => 
            new AsyncRelayCommand(SearchIndexInfos, IsKeywordNotNull);

        private async Task SearchIndexInfos()
        {
            await GetIndInfosAsync();
        }

        private bool IsKeywordNotNull()
        {
            return !string.IsNullOrWhiteSpace(Keyword);
        }


    }
}
