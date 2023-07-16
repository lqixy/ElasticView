using ElasticView.AppService.Contracts;
using ElasticView.AppService.Contracts.InputDto;
using ElasticView.AppService.Contracts.Output;
using ElasticView.UI.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticView.UI.Models.Mappers
{
    public class ViewModelProfiles : AutoMapper.Profile
    {
        public ViewModelProfiles()
        {
            //CreateMap<EsConnectViewModel, EsConnectInput>();


            CreateMap<Summary, SummaryViewModel>()
                //.ForMember(vm => vm.IndexInfos, options => options.MapFrom(x => x.IndexInfos))
                ;
            CreateMap<ClusterInfo, ClusterInfoViewModel>()
                .ForMember(vm => vm.ClusterHealthInfoViewModel, options => options.MapFrom(x => x.ClusterHealthInfo));
            CreateMap<IndexInfoDto, IndexInfoViewModel>()
                .AfterMap((dto, vm) =>
                {
                    vm.PriRep = $"{dto.Primary}/{dto.Replica}";
                    vm.Aliase = string.Join(" , ", dto.Aliases);
                })
                ;
            CreateMap<IndicesInfo, IndicesInfoViewModel>()
                 ;
            CreateMap<ClusterHealthInfo, ClusterHealthInfoViewModel>();
            CreateMap<ClusterStatsInfo, ClusterStatsInfoViewModel>();
            CreateMap<QueryCacheInfo, QueryCacheInfoViewModel>();
            CreateMap<FieldDataInfo, FieldDataInfoViewModel>();
            CreateMap<EsFileSystemInfo, EsFileSystemInfoViewModel>()
                .AfterMap((dto, vm) =>
                {
                    vm.Used = dto.UsedInBytes.BytesToString();
                    vm.Free = dto.FreeInBytes.BytesToString();

                    var usedPercent = 0d;
                    var freePercent = 1d;
                    if (dto.TotalInBytes > 0)
                    {
                        usedPercent = ViewExtensions.GetPercent(dto.UsedInBytes, dto.TotalInBytes);
                        freePercent = 100 - usedPercent;
                    }
                    vm.UsedPercentValue = usedPercent;
                    vm.FreePercentValue = freePercent;
                    vm.Desc = $"已用:{vm.Used},剩余:{vm.Free}";
                });
            CreateMap<OSCpuInfo, OSCpuInfoViewModel>()
                .AfterMap((dto, vm) =>
                {

                    vm.Desc = $"已用:{vm.Used}%,剩余:{vm.Free}%";
                })
                ;
            CreateMap<OsMemberInfo, OsMemberInfoViewModel>()
                .AfterMap((dto, vm) =>
                {
                    var usedInBytes = (dto.TotalBytes - dto.FreeBytes);
                    vm.Used = usedInBytes.BytesToString();
                    vm.Free = dto.FreeBytes.BytesToString();
                    vm.Total = dto.TotalBytes.BytesToString();
                    vm.Desc = $"已用:{vm.Used},剩余:{vm.Free}";
                });
            CreateMap<JvmInfo, JvmInfoViewModel>()
                .ForMember(vm => vm.JvmMemberInfos, options => options.MapFrom(x => x.JvmMemberInfos))
                .AfterMap((dto, vm) =>
                {
                    vm.HeapMax = dto.HeapMaxInBytes.BytesToString();
                    vm.Used = dto.HeapUsedInBytes.BytesToString();
                    vm.HeapFreeInBytes = dto.HeapMaxInBytes - dto.HeapUsedInBytes;
                    vm.Free = vm.HeapFreeInBytes.BytesToString();
                    var usedPercent = 0d;
                    var freePercent = 1d;
                    if (dto.HeapMaxInBytes > 0)
                    {
                        usedPercent = ViewExtensions.GetPercent(dto.HeapUsedInBytes, dto.HeapMaxInBytes);// Math.Round((double)dto.HeapUsedInBytes / dto.HeapMaxInBytes * 100, 2);
                        //freePercent = (dto.HeapMaxInBytes - dto.HeapUsedInBytes) / dto.HeapMaxInBytes;
                        freePercent = 100 - usedPercent;

                    }
                    //vm.UsedPercentValue = new LiveCharts.ChartValues<double> { usedPercent };
                    //vm.FreePercentValue = new LiveCharts.ChartValues<double> { freePercent };

                    vm.UsedPercentValue = usedPercent;
                    vm.FreePercentValue = freePercent;

                    vm.Desc = $"已用:{vm.Used},剩余:{vm.Free}";

                    if (dto.UpTimeInMillis > 0)
                    {
                        var timespan = TimeSpan.FromTicks(dto.UpTimeInMillis);
                        var sec = Math.Floor(timespan.TotalSeconds);
                        var min = Math.Floor(timespan.TotalMinutes);
                        var hour = Math.Floor(timespan.TotalHours);
                        var day = Math.Floor(timespan.TotalDays);
                        var month = Math.Floor(timespan.TotalDays / 30);
                        var year = Math.Floor(timespan.TotalDays / 365.25);
                        if (sec > 0)
                        {
                            vm.UpTime = $"{sec}秒";
                        }
                        if (min > 0)
                        {
                            vm.UpTime = $"{min}分";
                        }
                        if (hour > 0)
                        {
                            vm.UpTime = $"{hour}小时";
                        }
                        if (day > 0)
                        {
                            vm.UpTime = $"{day}天";
                        }
                        if (month > 0)
                        {
                            vm.UpTime = $"{month}月";
                        }
                        if (year > 0)
                        {
                            vm.UpTime = $"{year}年";
                        }
                    }

                });
            CreateMap<JvmMemberInfo, JvmMemberInfoViewModel>()
                ;

            CreateMap<CatNodesOutput, CatNodesViewModel>()
                .AfterMap((dto, vm) =>
                {
                    vm.CpuPercent = $"{dto.Cpu}%";
                    vm.HeapUsedPercentDesc = $"{dto.HeapUsedPercent}%";
                    vm.RamUsedPercentDesc = $"{dto.RamUsedPercent}%";
                    vm.HeapDesc = $"{dto.HeapUsed}/{dto.HeapMax}";
                    vm.RamDesc = $"{dto.RamUsed}/{dto.RamMax}";
                    vm.MasterDesc = dto.Master == "*" ? "yes" : "no";

                    vm.DiskDesc = $"{dto.DiskUsed}/{dto.DiskTotal}";
                    vm.DiskUsedPercentDesc = $"{dto.DiskUsedPercent}/%";
                })
                ;

        }
    }
}
