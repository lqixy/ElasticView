using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ElasticView.AppService.Contracts;
using ElasticView.AppService.Contracts.InputDto;
using ElasticView.UI.Pages;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace ElasticView.UI.Models
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private IServiceProvider serviceProvider;
        private Frame frame;

        [ObservableProperty]
        private string status = "未连接";

        [ObservableProperty]
        private SolidColorBrush statusBackground = Brushes.White;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConnectCommand))]
        [NotifyCanExecuteChangedFor(nameof(NavToPageCommand))]
        private string esUrl = "http://127.0.0.1:9200";

        [ObservableProperty]
        private int currentPageIndex = 0;

        [ObservableProperty]
        private bool dashSelected = true;

        [ObservableProperty]
        private bool nodesSelected;

        [ObservableProperty]
        private bool indicesSelected;

        //[ObservableProperty]
        //private bool shardsSelected;

        //[ObservableProperty]
        //private bool searchSelected;

        //[RelayCommand(CanExecute = nameof(ButtonCanClick))]
        //private void ButtonClick()
        //{

        //}

        private bool ButtonCanClick() => !string.IsNullOrWhiteSpace(EsUrl);


        public void Init(IServiceProvider serviceProvider,
            Frame frame)
        {
            this.serviceProvider = serviceProvider;
            this.frame = frame;
        }

        //public IAsyncRelayCommand<string> ConnectCommand { get; }
        public IAsyncRelayCommand ConnectCommand => new AsyncRelayCommand(OpenDashPage, ButtonCanClick);

        private async Task OpenDashPage()
        {
            if (string.IsNullOrWhiteSpace(EsUrl)) return;

            await UpdateStatus();
            frame.Navigate(new DashPage(EsUrl, serviceProvider));

        }

        private async Task UpdateStatus()
        {
            var appService = serviceProvider.GetRequiredService<IElasticAppService>();

            var status = await appService
                .GetStatus(EsUrl);
            this.Status = status == StatusEnum.Default ? "未连接" : status.ToString();

            switch (status)
            {
                case StatusEnum.Green:
                    StatusBackground = Brushes.Green;
                    break;
                case StatusEnum.Yellow:
                    StatusBackground = Brushes.Yellow;
                    break;
                case StatusEnum.Red:
                    StatusBackground = Brushes.Red;
                    break;
                default:
                    break;
            }
        }


        [RelayCommand(CanExecute = nameof(ButtonCanClick))]
        private void NavToPage(int index)
        {
            SwitchPage(index);

            UpdateToggleButtonChecked();

        }

        private void UpdateToggleButtonChecked()
        {
            this.DashSelected = 0 == CurrentPageIndex;
            this.NodesSelected = 1 == CurrentPageIndex;
            this.IndicesSelected = 2 == CurrentPageIndex;
            //this.ShardsSelected = 3 == CurrentPageIndex;
            //this.SearchSelected = 4 == CurrentPageIndex;
        }

        private void SwitchPage(int index)
        {
            if (CurrentPageIndex == index) return;

            switch (index)
            {
                case 0:
                    frame.Navigate(new DashPage(EsUrl, serviceProvider));
                    break;
                case 1:
                    frame.Navigate(new NodesPage(EsUrl, serviceProvider));
                    break;
                case 2:
                    frame.Navigate(new IndicesPage(EsUrl, serviceProvider));
                    break;
                case 3:
                    frame.Navigate(new MappingPage());
                    break;
                case 4:
                    frame.Navigate(new QueryPage(EsUrl, serviceProvider));
                    break;
                default:
                    frame.Navigate(new HelpPage());
                    break;
            }

            if (frame.BackStack is not null)
            {
                while (frame.CanGoBack)
                {
                    frame.RemoveBackEntry();
                }
                System.GC.Collect();
            }

            this.CurrentPageIndex = index;
        }
    }


    //public class EsConnectViewModel
    //{
    //    public string Url { get; set; } = "http://127.0.0.1:9200";

    //    public string UserName { get; set; } = string.Empty;

    //    public string Password { get; set; } = string.Empty;

    //    public void SetPwd(string pwd)
    //    {
    //        this.Password = pwd;
    //    }
    //}
}
