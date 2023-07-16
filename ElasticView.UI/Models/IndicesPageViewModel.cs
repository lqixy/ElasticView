using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticView.UI.Models
{
    public partial class IndicesPageViewModel : ObservableObject
    {

        [ObservableProperty]
        private IEnumerable<IndexInfoViewModel> indexInfos = Enumerable.Empty<IndexInfoViewModel>();


        [ObservableProperty]
        private IEnumerable<TestDto> dtos = Enumerable.Empty<TestDto>();

        //public IndicesPageViewModel()
        //{
        //    Dtos = Enumerable.Range(1, 30)
        //        .Select(x => new TestDto(x,$"name:{x}",$"remark{x}"));
        //}
        [ObservableProperty]
        private int totalPage = 1;

        [ObservableProperty]
        private int pageIndex = 1;

        [ObservableProperty]
        private int pageSize = 10;



        private readonly IEnumerable<TestDto> Sources;

        public IndicesPageViewModel()
        {
            Sources = Enumerable.Range(1, 230)
                .Select(x => new TestDto(x, $"name:{x}", $"remark{x}", x % 2 == 0));

            TotalPage = (Sources.Count() + PageSize - 1) / PageSize;

            //PageUpdateCmd = new AsyncRelayCommand<int>(PageUpdate);
        }

        public async Task InitAsync()
        {
            Dtos = Sources.Skip(PageIndex - 1)
                .Take(PageSize);
        }


        public IAsyncRelayCommand<FunctionEventArgs<int>> PageUpdateCmd =>
            new AsyncRelayCommand<FunctionEventArgs<int>>(PageUpdate);

        private async Task PageUpdate(FunctionEventArgs<int> info)
        {
            Dtos = Sources.Skip((info.Info - 1) * PageSize)
                .Take(PageSize);
        }

        //public RelayCommand<FunctionEventArgs<int>> PageUpdateCmd =>  new(PageUpdate);

        //private void PageUpdate(FunctionEventArgs<int> info)
        //{
        //    Dtos = Sources.Skip((info.Info - 1)*PageSize)
        //        .Take(PageSize);
        //}


    }

    public class TestPageDto
    {
        public int TotalPage { get; set; } = 2;

        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        //private IEnumerable<TestDto> dtos = Enumerable.Empty<TestDto>();

    }

    public class TestDto
    {
        public TestDto()
        {
        }

        public TestDto(int id, string name, string remark, bool selected)
        {
            Index = id;
            Name = name;
            Remark = remark;
            Selected = selected;
        }

        public int Index { get; set; }

        public bool Selected { get; set; }


        public string Name { get; set; }

        public string Remark { get; set; }


    }
}
