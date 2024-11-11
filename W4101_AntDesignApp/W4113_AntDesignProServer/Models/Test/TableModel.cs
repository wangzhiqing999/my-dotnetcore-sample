using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace W4113_AntDesignProServer.Models.Test
{


    /// <summary>
    /// 用于测试 Table 组件的数据
    /// </summary>
    public class TableModel
    {


        /// <summary>
        /// 报表ID
        /// </summary>
        [Display(Name = "报表ID")]
        public int Id { set; get; }


        /// <summary>
        /// 报表类型代码
        /// </summary>
        [Required]
        [Display(Name = "报表类型")]
        [DisplayName("报表类型")]
        public string ReportTypeCode { set; get; }



        /// <summary>
        /// 报表数据源代码.
        /// </summary>
        [Required]
        [Display(Name = "报表数据源")]
        [DisplayName("报表数据源")]
        public string ReportDataSourceCode { set; get; }



        /// <summary>
        /// 报表名.
        /// </summary>
        [Required]
        [Display(Name = "报表名")]
        [DisplayName("报表名")]
        public string ReportName { set; get; }


        /// <summary>
        /// 报表文件名.
        /// </summary>
        [Display(Name = "报表文件名")]
        [DisplayName("报表文件名")]
        public string ReportFileName { set; get; }



        /// <summary>
        /// 默认可见标志.
        /// </summary>
        [Display(Name = "默认可见标志", Description = "可以访问报表类型的人， 自动可访问该报表")]
        public bool DefaultVisable { set; get; }




        /// <summary>
        /// 备注.
        /// </summary>
        [Display(Name = "备注")]
        [DisplayName("备注")]
        public string Remark { set; get; }




        /// <summary>
        /// 是否处理成功.
        /// </summary>
        [Display(Name = "是否处理成功")]
        [DisplayName("是否处理成功")]
        public bool IsProcessSuccess { set; get; }



        public static List<TableModel> GetTestDataList(int dataCount = 10)
        {
            var resultList = new List<TableModel>();

            for(int i = 0; i < dataCount; i++)
            {

                var oneResult = new TableModel
                {
                    Id = i + 1,

                    ReportTypeCode = $"TEST_0{(i % 9) + 1}",
                    ReportDataSourceCode = $"SQL_0{(i % 7) + 3}",

                    ReportName = $"测试报表_{i+1:000}",
                    ReportFileName = $"result_file_{i+1:000}.xlsx",
                    DefaultVisable = true,

                    Remark = @$"源编号：{Guid.NewGuid().ToString()}
临时编号：{Guid.NewGuid().ToString()}
目的编号：{Guid.NewGuid().ToString()}
报表：测试报表_{i + 1:000}
数据源：SQL_0{(i % 7) + 3}
文件：result_file_{i + 1:000}.xlsx
生成时间：{DateTime.Now.AddSeconds(12345 * i):yyyy-MM-dd HH:mm}",

                    IsProcessSuccess = (i % 10 != 7)
                };


                resultList.Add(oneResult);
                
            };

            return resultList;
        }




    }
}
