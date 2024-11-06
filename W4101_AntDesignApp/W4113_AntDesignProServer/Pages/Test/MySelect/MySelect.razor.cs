using AntDesign.TableModels;
using W4113_AntDesignProServer.Models.Test;

namespace W4113_AntDesignProServer.Pages.Test
{
    public partial class MySelect
    {


        #region 直接把选项写在页面上的处理

        string _selectedCityCode;
        string _selectedCityCode2;


        List<string> _selectedCityCodes = new List<string>();

        void handleItemsChange(IEnumerable<string> value)
        {
            _selectedCityCodes.Clear();
            _selectedCityCodes.AddRange(value);
        }


        List<string> _selectedCityCodes2 = new List<string>();

        void handleItemsChange2(IEnumerable<string> value)
        {
            _selectedCityCodes2.Clear();
            _selectedCityCodes2.AddRange(value);
        }




        IEnumerable<string> _selectedCityCodes3 = new List<string>();


        #endregion





        #region 简单绑定的使用

        List<MySelectModelA> _dataSourceA = MySelectModelA.GetTestDataList();


        string _selectedCodeA;
        string _selectedCodeA2;

        List<string> _selectedCodeAs = new List<string>();



        void handleMySelectModelAChange(IEnumerable<MySelectModelA> value)
        {
            _selectedCodeAs.Clear();
            _selectedCodeAs.AddRange(value.Select(p=>p.Code));
        }



        #endregion






        #region 搜索.



        string _selectedCodeAWithSearch;



        #endregion




        #region 允许清除.



        string _selectedCodeAWithAllowClear;



        #endregion






        #region 列表选择.


        ITable _selectTable;


        IEnumerable<MySelectModelA> _selectedRows = [];

        List<MySelectModelA> _tableData = MySelectModelA.GetTestDataList();

        bool _selectOpen = false;

        bool _multiple;

        void OnSearch(string searchValue)
        {
            _tableData = _dataSourceA.Where(x => x.NameAndCode.Contains(searchValue, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        string _selectedCodeAWithTable;


        void OnRowClick(RowData<MySelectModelA> rowData)
        {
            if (_multiple)
            {
                _selectedRows = _selectedRows.Contains(rowData.Data) ? _selectedRows.Except([rowData.Data]) : _selectedRows.Concat([rowData.Data]);
            }
            else
            {
                _selectedRows = [rowData.Data];
                _selectOpen = false;
            }
        }


        #endregion




        #region 枚举.



        ExportFormatType _selectedExportFormatType;





        #endregion



        #region 分组.

        string _selectedCodeAWithGroup;

        #endregion




        #region 自定义的组件

        string _myTestSelectValue;

        string _myTestSelectValue2;

        string _myTestSelectValue4 = "SZ159949";


        IEnumerable<string> _myTestSelectValues = new List<string>();

        #endregion

    }


}
