using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace W4113_AntDesignProServer.Models.Test
{

    /// <summary>
    /// 模块动作.
    /// </summary>
    [DisplayName("模块动作")]
    public class MyAction
    {


        /// <summary>
        /// 动作代码.
        /// </summary>
        [Display(Name = "动作代码")]
        [Required]
        public string ActionCode { set; get; }


        /// <summary>
        /// 动作名称.
        /// </summary>
        [Display(Name = "动作名称")]
        [Required]
        public string ActionName { set; get; }




        /// <summary>
        /// 模块编号.
        /// </summary>
        [Display(Name = "模块编号")]
        [Required]
        public string ModuleCode { set; get; }




        public static List<MyAction> GetTestDataList(MyModule module)
        {
            var resultList = new List<MyAction>();

            resultList.Add(new MyAction()
            {
                ModuleCode = module.ModuleCode,
                ActionCode = $"{module.ModuleCode}_I",
                ActionName = $"新增 {module.ModuleName}",
            });

            resultList.Add(new MyAction()
            {
                ModuleCode = module.ModuleCode,
                ActionCode = $"{module.ModuleCode}_U",
                ActionName = $"编辑 {module.ModuleName}",
            });

            resultList.Add(new MyAction()
            {
                ModuleCode = module.ModuleCode,
                ActionCode = $"{module.ModuleCode}_D",
                ActionName = $"删除 {module.ModuleName}",
            });

            return resultList;
        }

    }
}
