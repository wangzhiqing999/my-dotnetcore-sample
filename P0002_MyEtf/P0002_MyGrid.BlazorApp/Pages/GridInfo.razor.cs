using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

using P0002_MyEtf.Service;
using P0002_MyGrid.Service;

using P0002_MyGrid.ServiceModel;
using P0002_MyGrid.Model;
using P0002_MyEtf.Model;
using BootstrapBlazor.Components;

namespace P0002_MyGrid.BlazorApp.Pages
{
    public partial class GridInfo
    {


        /// <summary>
        /// 外部传入的参数.
        /// </summary>
        [Parameter]
        public string? ItemCode { get; set; }



        [Inject]
        [NotNull]
        private IEtfDayService? _EtfDayService { get; set; }



        [Inject]
        [NotNull]
        private IGridService? _GridService { get; set; }





        /// <summary>
        /// 网格数据.
        /// </summary>
        private List<Grid> _Grids;


        /// <summary>
        /// 最新的行情数据.
        /// </summary>
        private EtfDayLine _LastEtfDayLine;


        /// <summary>
        /// 待办事项.
        /// </summary>
        private List<GetTodoResponse> _TodoList;



        /// <summary>
        /// 表格组件.
        /// </summary>
        [NotNull]
        private Table<Grid>? _MainTable { get; set; }





        protected override Task OnParametersSetAsync()
        {

            _Grids = this._GridService.GetItemGrids(ItemCode);

            _LastEtfDayLine = _EtfDayService.GetLastEtfDayLines(ItemCode);


            GetTodoRequest request = new GetTodoRequest()
            {
                ItemCode = ItemCode,
                CurrentPrice = _LastEtfDayLine.ClosePrice
            };

            _TodoList = _GridService.GetTodoList(request);

            return base.OnParametersSetAsync();
        }





        /// <summary>
        /// 设置表格的样式.
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public string? SetRowClassFormatter(Grid grid)
        {
            if (grid.BuyPrice < _LastEtfDayLine.ClosePrice && grid.SellPrice > _LastEtfDayLine.ClosePrice)
            {
                return "highlight";
            }

            return null;
        }



    }
}
