using W4113_AntDesignProServer.Models.Test;


namespace W4113_AntDesignProServer.Pages.Test
{
    public partial class MyCascader
    {


        IEnumerable<CascaderNode> _Options = Area.GetTestAreaCascaderNodes();


        #region 简单选择

        string _Value = "";

        #endregion





        #region 选择即改变


        string _CascaderValue = "";
        string _CascaderPath = "";

        void OnChange(CascaderNode[] selectedNodes)
        {
            _CascaderPath = string.Join(",", selectedNodes.Select(x => x.Value));
        }

        #endregion

    }
}
