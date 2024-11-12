using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace W4113_AntDesignProServer.Models.Test
{


    /// <summary>
    /// 功能模块.
    /// </summary>
    [DisplayName("功能模块")]
    public class MyModule
    {


        /// <summary>
        /// 模块编号.
        /// </summary>
        [Display(Name = "模块编号")]
        [Required]
        public string ModuleCode { set; get; }



        /// <summary>
        /// 模块名称.
        /// </summary>
        [Display(Name = "模块名称")]
        [Required]
        public string ModuleName { set; get; }




        /// <summary>
        /// 模块下的 动作列表.
        /// </summary>
        public List<MyAction> Actions { set; get; }






        public static List<MyModule> GetTestDataList(int dataCount = 10)
        {
            var resultList = new List<MyModule>();

            for (int i = 0; i < dataCount; i++)
            {
                var oneResult = new MyModule()
                {
                    ModuleCode = $"TEST_{i + 1:000}",
                    ModuleName = $"測試模組_{i + 1:000}",
                };

                resultList.Add(oneResult);
            }
            return resultList;
        }



    }
}
