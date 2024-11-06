using W4113_AntDesignProServer.Models.Test;

namespace W4113_AntDesignProServer.Pages.Test
{
    public partial class MySelect2
    {



        #region 简单绑定的使用


        List<MySelectModelB> _dataSourceB = MySelectModelB.GetTestDataList();


        int _selectedIDB;
        int _selectedIDB2;


        IEnumerable<int> _selectedIDBs = new List<int>();


        #endregion




        #region 允许清除.



        int? _selectedIDBWithAllowClear;



        #endregion







        #region 自定义的组件

        int _myTestSelectValue;

        int _myTestSelectValue2;

        int _myTestSelectValue3;

        int _myTestSelectValue4 = 4;


        IEnumerable<int> _myTestSelectValues = new List<int>();

        #endregion


    }







}
