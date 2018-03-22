using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;



namespace W1010_ABP_NetCode2.Tasks.Dtos
{

    /// <summary>
    /// 查询条件.
    /// </summary>
    public class QueryOtherInput : IPagedResultRequest, ISortedResultRequest
    {

        #region 实现 IPagedResultRequest 接口.

        // IPagedResultRequest 为一个用于翻页的接口.

        /// <summary>
        /// 跳过多少行.
        /// </summary>
        [Range(0, int.MaxValue)]
        public int SkipCount { get; set; }


        /// <summary>
        /// 取多少行.
        /// </summary>
        [Range(1, 100)]
        public int MaxResultCount { get; set; }


        #endregion




        /// <summary>
        /// 筛选条件.
        /// </summary>
        public string Filter { get; set; }


        /// <summary>
        /// 排序.
        /// </summary>
        public string Sorting{ get; set; }



        /// <summary>
        /// 初始化.
        /// </summary>
        public QueryOtherInput()
        {
            // 默认是  每页 10 行.  第1页.            
            MaxResultCount = 10;
            SkipCount = 0;
        }


    }



}
