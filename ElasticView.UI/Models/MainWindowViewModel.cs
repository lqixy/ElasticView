using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ElasticView.ApiRepository;
using ElasticView.AppService.Contracts;
using ElasticView.UI.Pages;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;
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
        private string esUrl = "http://127.0.0.1:9200";

        [ObservableProperty]
        private int currentPageIndex = 0;

        [ObservableProperty]
        private bool dashSelected = true;

        [ObservableProperty]
        private bool nodesSelected;

        [ObservableProperty]
        private bool indicesSelected;

        [ObservableProperty]
        private bool searchSelected;

        [ObservableProperty]
        private bool toolsSelected;

        [ObservableProperty]
        private bool canUse = false;

        public void Init(IServiceProvider serviceProvider,
            Frame frame)
        {
            this.serviceProvider = serviceProvider;
            this.frame = frame;
        }

        public IAsyncRelayCommand ConnectCommand => new AsyncRelayCommand(OpenDashPage);

        private async Task OpenDashPage()
        {
            if (string.IsNullOrWhiteSpace(EsUrl)) return;
            try
            {
                await UpdateStatus();
                frame.Navigate(new DashPage(EsUrl, serviceProvider));

            }
            catch (UserFriendlyException e)
            {
                HandyControl.Controls.MessageBox.Show(e.Message);
            }

        }

        private async Task UpdateStatus()
        {
            var appService = serviceProvider.GetRequiredService<IElasticAppService>();

            var status = await appService
                .GetStatus(EsUrl);
            this.Status = status == StatusEnum.Default ? "未连接" : status.ToString();

            CanUse = !string.IsNullOrWhiteSpace(Status) && Status != "未连接";
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


        [RelayCommand]
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
            this.SearchSelected = 3 == CurrentPageIndex;
            this.ToolsSelected = 4 == CurrentPageIndex;
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
