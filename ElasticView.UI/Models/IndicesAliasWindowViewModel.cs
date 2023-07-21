using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ElasticView.AppService.Contracts;
using ElasticView.UI.Windows;
using HandyControl.Controls;
using HandyControl.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ElasticView.UI.Models
{
    public partial class IndicesAliasWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string indexName = string.Empty;

        private IElasticIndexAppService elasticIndexAppService;
        private string url;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddNewAliasNameCommand))]
        private string aliasName = string.Empty;

        [ObservableProperty]
        private ObservableCollection<IndicesAliasDto> sourcesData = new ObservableCollection<IndicesAliasDto>();

        public IndicesAliasWindowViewModel()
        {
            AddNewAliasNameCommand = new AsyncRelayCommand(AddNewAliasName, CanClick);
        }

        public async Task InitAsync(string url, string indexName, IElasticIndexAppService elasticIndexAppService)
        {
            this.IndexName = indexName;
            this.url = url;
            this.elasticIndexAppService = elasticIndexAppService;

            await InitSourcesData();
        }

        private async Task InitSourcesData()
        {

            var aliases = await elasticIndexAppService.GetAliases(url);


            var list = aliases.Where(x => x.IndexName == IndexName)
                  .SelectMany(x => x.Alias)
                  .Select(x => new IndicesAliasDto(x));
            SourcesData = new ObservableCollection<IndicesAliasDto>(list);

        }

        private bool CanClick() => !string.IsNullOrWhiteSpace(AliasName);



        public IAsyncRelayCommand AddNewAliasNameCommand { get; }

        private async Task AddNewAliasName()
        {
            if (SourcesData.Any(x => x.Name == AliasName))
            {
                Growl.Error(new GrowlInfo()
                {
                    Message = "别名已存在",
                    WaitTime = 3
                });

                //await Task.Delay(3100);
                ClearAliasName();
                return;
            }

            var flag = await elasticIndexAppService.AddAlias(url, IndexName, AliasName);

            ClearAliasName();
            if (flag)
            {
                Growl.Success(new GrowlInfo()
                {
                    Message = "添加成功",
                    WaitTime = 3
                });

                await InitSourcesData();
            }
            else
            {
                Growl.Error(new GrowlInfo()
                {
                    Message = "添加成功",
                    WaitTime = 3
                });

            }
        }


        public IAsyncRelayCommand<string> DeleteAliasNameCommand =>
            new AsyncRelayCommand<string>(DeleteAliasName);

        private async Task DeleteAliasName(string name)
        {
            var result = HandyControl.Controls.MessageBox.Show(
                    $"删除索引 '{IndexName}' 的别名 '{name}'？", "提醒",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var flag = await elasticIndexAppService.DeleteAlias(url, IndexName, name);
                if (flag)
                {
                    Growl.Success(new GrowlInfo()
                    {
                        Message = "删除成功",
                        WaitTime = 3
                    });
                    await InitSourcesData();
                }
            }
        }

        private void ClearAliasName()
        {
            this.AliasName = string.Empty;
        }
        //private void CloseCurWindow()
        //{
        //    var curWindow = Application.Current.Windows
        //          .OfType<IndicesAli>()
        //          .SingleOrDefault();
        //    if (curWindow != null)
        //    {
        //        curWindow.DialogResult = true;
        //        curWindow.Close();
        //    }
        //}

    }
    public class IndicesAliasDto
    {
        public IndicesAliasDto()
        {
        }

        public IndicesAliasDto(string name)
        {
            Name = name;
        }

        public string Name { get; set; } = string.Empty;
    }
}
