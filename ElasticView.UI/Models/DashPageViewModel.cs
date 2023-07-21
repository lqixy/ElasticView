using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ElasticView.ApiRepository;
using ElasticView.AppService.Contracts;
using ElasticView.AppService.Contracts.InputDto;
using ElasticView.UI.Pages;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ElasticView.UI.Models
{
    public partial class DashPageViewModel : ObservableObject
    {


        [ObservableProperty]
        private IndicesInfoViewModel indicesInfo = new();

        [ObservableProperty]
        private ClusterHealthInfoViewModel clusterHealthInfo = new();

        [ObservableProperty]
        private ClusterStatsInfoViewModel clusterStatsInfo = new();

        [ObservableProperty]
        private JvmInfoViewModel jvmInfo = new();

        [ObservableProperty]
        private OsMemberInfoViewModel memberInfo = new();

        [ObservableProperty]
        private EsFileSystemInfoViewModel fileSystemInfo = new();

        [ObservableProperty]
        private FieldDataInfoViewModel fieldDataInfo = new();

        [ObservableProperty]
        private QueryCacheInfoViewModel queryCacheInfo = new();

        [ObservableProperty]
        private OSCpuInfoViewModel cpuInfo = new();

        public async Task InitAsync(string url, IServiceProvider serviceProvider)
        {
            if (string.IsNullOrWhiteSpace(url)) return;
            var mapper = serviceProvider.GetRequiredService<IMapper>();
            var appService = serviceProvider.GetRequiredService<IElasticAppService>();

            try
            {
                var summary = await appService.GetSummary(url);
                //var indicesInfo = await appService.GetIndices(url);
                this.IndicesInfo = mapper.Map<IndicesInfoViewModel>(summary.IndicesInfo);

                //var clusterHealthInfo = await appService.GetClusterHealth(url);
                this.ClusterHealthInfo = mapper.Map<ClusterHealthInfoViewModel>(summary.ClusterInfo.ClusterHealthInfo);

                //var clusterStatsInfo = await appService.GetClusterStatsInfo(url);
                this.ClusterStatsInfo = mapper.Map<ClusterStatsInfoViewModel>(summary.ClusterInfo.ClusterStatsInfo);

                //var fileSystemInfo = await appService.GetEsFileSystemInfo(url);
                this.FileSystemInfo = mapper.Map<EsFileSystemInfoViewModel>(summary.ClusterInfo.FileSystemInfo);

                //var jvmInfo = await appService.GetJvm(url);
                this.JvmInfo = mapper.Map<JvmInfoViewModel>(summary.ClusterInfo.Jvm);

                //var memberInfo = await appService.GetOsMemberInfo(url);
                this.MemberInfo = mapper.Map<OsMemberInfoViewModel>(summary.ClusterInfo.MemberInfo);

                //var cpuInfo = await appService.GetCpuInfo(url);
                this.CpuInfo = mapper.Map<OSCpuInfoViewModel>(summary.ClusterInfo.CpuInfo);

            }
            catch (UserFriendlyException e)
            {
                HandyControl.Controls.MessageBox.Show(e.Message);
            }
        }

        //public IAsyncRelayCommand LoadDataCommand { get; }
    }
}
