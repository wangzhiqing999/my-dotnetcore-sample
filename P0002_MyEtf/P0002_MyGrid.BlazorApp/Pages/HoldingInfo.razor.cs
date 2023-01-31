using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using BootstrapBlazor.Components;

using P0002_MyTrading.Service;
using P0002_MyTrading.ServiceModel;
using P0002_MyTrading.Model;


namespace P0002_MyGrid.BlazorApp.Pages
{
    public partial class HoldingInfo
    {


        [Inject]
        [NotNull]
        private IHoldingService? _HoldingService { set; get; }



        /// <summary>
        /// 持仓数据.
        /// </summary>
        private List<HoldingReport> _HoldingReports;


        /// <summary>
        /// 表格组件.
        /// </summary>
        [NotNull]
        private Table<HoldingReport>? _MainTable { get; set; }




        private AggregateType Aggregate { get; set; } = AggregateType.Sum;



        protected override void OnInitialized()
        {
            base.OnInitialized();

            _HoldingReports = this._HoldingService.GetLastHoldingReport();
        }
    }
}
