using System;
using W4113_AntDesignProServer.Models.Test;

namespace W4113_AntDesignProServer.Pages.Test
{
    public partial class MySelect2
    {



        #region 简单绑定的使用


        List<MySelectModelB> _dataSourceB = MySelectModelB.GetTestDataList();


        int _selectedIDB;
        int _selectedIDB2;




		int _selectedIDB3;
        private void OnSelectedItemChangedHandler3(MySelectModelB value)
        {
            _selectedIDB3 = value.ID;
        }


        int _selectedIDB4;
        int _selectedIDB4ByHand;
        private void OnSelectedItemChangedHandler4(MySelectModelB value)
        {
            _selectedIDB4ByHand = value.ID;
        }



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



        int _myTestSelectValue5;
        private void OnSelectedItemChangedHandler5(MySelectModelB value)
        {
            _myTestSelectValue5 = value.ID;
        }

        
        int _myTestSelectValue6;
        int _myTestSelectValue6ByHand;
        private void OnSelectedItemChangedHandler6(MySelectModelB value)
        {
            _myTestSelectValue6ByHand = value.ID;
        }



        IEnumerable<int> _myTestSelectValues = new List<int>();

        #endregion


    }







}
