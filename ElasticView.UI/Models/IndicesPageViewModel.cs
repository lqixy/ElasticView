using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ElasticView.AppService.Contracts;
using ElasticView.AppService;
using HandyControl.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using HandyControl.Controls;
using System.Windows;
using ElasticView.UI.Windows;
using ElasticView.ApiRepository;
using HandyControl.Tools;

namespace ElasticView.UI.Models
{
    public partial class IndicesPageViewModel : ObservableObject
    {

        private string _url;
        private IElasticAppService elasticAppService;
        private IElasticCatAppService catAppService;
        private IElasticIndexAppService indexAppService;
        private IMapper mapper;


        [ObservableProperty]
        private int pageIndex = 1;


        [ObservableProperty]
        private int pageSize = 10;

        [ObservableProperty]
        private int totalPages = 1;

        #region 属性
        private bool _showAll;

        public bool ShowAll
        {
            get { return _showAll; }
            set
            {
                _showAll = value;
                OnPropertyChanged(nameof(ShowAll));

                FilterDataGrid();
            }
        }



        private string _keyword;

        public string Keyword
        {
            get { return _keyword; }
            set
            {
                _keyword = value;

                OnPropertyChanged(nameof(Keyword));
                FilterDataGrid();
            }
        }

        private ObservableCollection<IndexInfoViewModel> _sourceData;

        public ObservableCollection<IndexInfoViewModel> SourceData
        {
            get { return _sourceData; }
            set
            {
                _sourceData = value;
                OnPropertyChanged(nameof(SourceData));
                FilterDataGrid();
            }
        }

        private ObservableCollection<IndexInfoViewModel> _filteredData;

        public ObservableCollection<IndexInfoViewModel> FilteredData
        {
            get { return _filteredData; }
            set
            {
                _filteredData = value;
                OnPropertyChanged(nameof(FilteredData));

            }
        }
        #endregion

        private void FilterDataGrid(int pageIndex = 1)
        {

            var sources = SourceData
                .WhereIf(x => x.Index.Contains(Keyword), !string.IsNullOrWhiteSpace(Keyword))
                .WhereIf(x => !x.Index.StartsWith("."), !ShowAll);
            this.TotalPages = (sources.Count() + PageSize - 1) / PageSize;

            var list = sources
                .Skip((pageIndex - 1) * PageSize)
                .Take(PageSize);

            FilteredData = new ObservableCollection<IndexInfoViewModel>(list);

        }

        public async Task InitAsync(string url, IServiceProvider serviceProvider)
        {
            _url = url;

            elasticAppService = serviceProvider
              .GetRequiredService<IElasticAppService>();
            catAppService = serviceProvider
              .GetRequiredService<IElasticCatAppService>();
            indexAppService = serviceProvider
                .GetRequiredService<IElasticIndexAppService>();

            mapper = serviceProvider
              .GetRequiredService<IMapper>();

            try
            {
                await InitSource();
            }
            catch (UserFriendlyException e)
            {
                HandyControl.Controls.MessageBox.Show(e.Message);
            }

        }

        private async Task InitSource()
        {

            var sources = (await elasticAppService
                 .GetIndexInfos(_url, true)).ToList();

            var aliases = await catAppService.GetAliases(_url);

            foreach (var item in sources)
            {
                var curAliase = aliases
                      .FirstOrDefault(x => x.IndexName == item.Index)?
                      .Alias ?? new string[0];
                item.UpdateAliases(curAliase);
            }
            var infos = mapper.Map<IEnumerable<IndexInfoViewModel>>(sources);
            SourceData = new ObservableCollection<IndexInfoViewModel>(infos);

            FilterDataGrid();
        }


        public IAsyncRelayCommand<FunctionEventArgs<int>> PageUpdatedCommand =>
            new AsyncRelayCommand<FunctionEventArgs<int>>(PageUpdated);

        private async Task PageUpdated(FunctionEventArgs<int> info)
        {
            FilterDataGrid(info.Info);
        }

        #region Command

        public IAsyncRelayCommand<string> QueryIndiceInfoCommand =>
            new AsyncRelayCommand<string>(QueryIndiceInfo);

        public IAsyncRelayCommand<string> QueryIndiceStatsCommand =>
           new AsyncRelayCommand<string>(QueryIndiceStats);


        public IAsyncRelayCommand<string> ForceMergeCommand =>
            new AsyncRelayCommand<string>(ForceMerge);

        public IAsyncRelayCommand<string> RefreshCommand =>
            new AsyncRelayCommand<string>(Refresh);

        public IAsyncRelayCommand<string> ClearCommand =>
            new AsyncRelayCommand<string>(Clear);

        public IAsyncRelayCommand<string> FlushCommand =>
            new AsyncRelayCommand<string>(Flush);

        public IAsyncRelayCommand<string> CloseCommand =>
            new AsyncRelayCommand<string>(Close);

        public IAsyncRelayCommand<string> OpenCommand =>
            new AsyncRelayCommand<string>(Open);

        public IAsyncRelayCommand<string> DeleteCommand =>
            new AsyncRelayCommand<string>(Delete);

        #endregion

        #region CommandFunction

        private async Task Delete(string indexName)
        {
            CheckIndexName(indexName);
            var flag = await indexAppService.DeleteIndex(_url, indexName);
            if (flag)
            {
                Growl.Success(GetSuccessGrowl());
                await InitSource();
            }
        }

        private async Task Open(string indexName)
        {
            CheckIndexName(indexName);
            var flag = await indexAppService.OpenIndex(_url, indexName);
            if (flag)
            {
                Growl.Success(GetSuccessGrowl());
                await InitSource();
            }
        }

        private async Task Close(string indexName)
        {
            CheckIndexName(indexName);
            var flag = await indexAppService.CloseIndex(_url, indexName);
            if (flag)
            {
                Growl.Success(GetSuccessGrowl());
                await InitSource();
            }
        }

        private async Task Flush(string indexName)
        {
            CheckIndexName(indexName);
            var flag = await indexAppService.Flush(_url, indexName);
            if (flag) Growl.Success(GetSuccessGrowl());
        }

        private async Task Clear(string indexName)
        {
            CheckIndexName(indexName);
            var flag = await indexAppService.ClearCache(_url, indexName);
            if (flag) Growl.Success(GetSuccessGrowl());
        }

        private async Task Refresh(string indexName)
        {
            CheckIndexName(indexName);
            var flag = await indexAppService.Refresh(_url, indexName);
            if (flag) Growl.Success(GetSuccessGrowl());
        }

        private async Task ForceMerge(string indexName)
        {
            CheckIndexName(indexName);
            var flag = await indexAppService.Forcemerge(_url, indexName);
            if (flag)
            {
                Growl.Success(GetSuccessGrowl());
            }
        }

        private async Task QueryIndiceInfo(string indexName)
        {
            CheckIndexName(indexName);

            var content = await indexAppService.Get(_url, indexName);

            ShowIndexDialog("IndicesInfo", indexName, content);
        }


        private async Task QueryIndiceStats(string indexName)
        {
            CheckIndexName(indexName);
            var content = await indexAppService.Get(_url, indexName);

            ShowIndexDialog("IndicesStats", indexName, content);
        }

        private void CheckIndexName(string indexName)
        {
            if (string.IsNullOrWhiteSpace(indexName))
            {
                Growl.Error("先选择内容");
                return;
            }
        }

        #endregion
        private void ShowIndexDialog(string pageTitle, string indexName, string content)
        {
            var window = new UI.Windows.IndexInfo(pageTitle, indexName, content);
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.Owner = Application.Current.MainWindow;
            window.ShowDialog();
        }
        private GrowlInfo GetSuccessGrowl(string msg = "成功")
        {
            return new GrowlInfo
            {
                Message = msg,
                WaitTime = 3
            };
        }


        //public RelayCommand CreateIndexWindowCommand => new RelayCommand(CreateIndexWindow);
        public IAsyncRelayCommand CreateIndexWindowCommand =>
            new AsyncRelayCommand(CreateIndexWindow);

        private async Task CreateIndexWindow()
        {
            var window = new IndicesDetailWindow(_url, indexAppService)
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow,
            };
            var dialog = window.ShowDialog();
            if (dialog.HasValue && dialog.Value)
            {
                await InitSource();
            }
        }
    }

}
