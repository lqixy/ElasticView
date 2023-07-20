using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ElasticView.AppService.Contracts;
using ElasticView.UI.Windows;
using HandyControl.Controls;
using HandyControl.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ElasticView.UI.Models
{
    public partial class IndicesDetailWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string indexName = string.Empty;

        [ObservableProperty]
        private int repCount = 1;

        [ObservableProperty]
        private int shardCount = 1;

        private IElasticIndexAppService indexAppService;
        private string url;
        public void Init(string url, IElasticIndexAppService indexAppService)
        {
            this.url = url;
            //this.indexAppService = serviceProvider.GetRequiredService<IElasticIndexAppService>();
            this.indexAppService = indexAppService;
        }

        public IAsyncRelayCommand CreateIndexCommand =>
            new AsyncRelayCommand(CreateIndex);

        private async Task CreateIndex()
        {
            var result = await indexAppService.CreateIndex(url,
                  new AppService.Contracts.InputDto.CreateIndexInput(this.IndexName,
                  this.ShardCount, this.RepCount));
            if (result.Success)
            {
                Growl.Success(new GrowlInfo
                {
                    Message = "成功",
                    WaitTime = 3
                });
            }
            else
            {
                Growl.Error(new GrowlInfo
                {
                    Message = result.Message,
                    WaitTime = 3
                });
            }

            await Task.Delay(3000);
            CloseCurWindow();
        }

        [RelayCommand]
        private void CloseWindow()
        {
            CloseCurWindow();
        }

        private void CloseCurWindow()
        {
            var curWindow = Application.Current.Windows
                  .OfType<IndicesDetailWindow>()
                  .SingleOrDefault();
            if (curWindow != null)
            {
                curWindow.DialogResult = true;
                curWindow.Close();
            }
        }
    }
}
