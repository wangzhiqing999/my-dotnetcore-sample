using AntDesign.TableModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace W4113_AntDesignProServer.Pages.Test
{
    public partial class MySelect
    {


        #region 直接把选项写在页面上的处理

        string _selectedCityCode;


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


        IEnumerable<string> _myTestSelectValues = new List<string>();

        #endregion

    }


    /// <summary>
    /// 下列列表的数据.
    /// <br/>
    /// 这里是测试，实际业务中，可能是数据库中的数据，或者从别的地方获取的。
    /// </summary>
    public class MySelectModelA
    {
        /// <summary>
        /// 代码.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称.
        /// </summary>
        public string Name { get; set; }


        public string Area { get; set; }


        /// <summary>
        /// 名称与代码.
        /// </summary>
        public string NameAndCode => $"{Name}({Code})";



        public override string ToString()
        {
            return this.NameAndCode;
        }



        public static List<MySelectModelA> GetTestDataList()
        {
            return new List<MySelectModelA>()
            {
                new MySelectModelA(){Area = "上海", Code="SH510050",Name="上证50ETF"},
                new MySelectModelA(){Area = "上海", Code="SH510180",Name="上证180ETF"},
                new MySelectModelA(){Area = "上海", Code="SH510300",Name="沪深300ETF"},
                new MySelectModelA(){Area = "上海", Code="SH510500",Name="中证500ETF"},
                new MySelectModelA(){Area = "上海", Code="SH510880",Name="红利ETF"},
                new MySelectModelA(){Area = "上海", Code="SH510900",Name="H股ETF"},

                new MySelectModelA(){Area = "上海", Code="SH513030",Name="德国ETF"},
                new MySelectModelA(){Area = "上海", Code="SH513080",Name="法国CAC40ETF"},
                new MySelectModelA(){Area = "上海", Code="SH513100",Name="纳指ETF"},
                new MySelectModelA(){Area = "上海", Code="SH513500",Name="标普500ETF"},
                new MySelectModelA(){Area = "上海", Code="SH513520",Name="日经ETF"},
                new MySelectModelA(){Area = "上海", Code="SH513550",Name="港股通50ETF"},

                new MySelectModelA(){Area = "上海", Code="SH518880",Name="黄金ETF"},

                new MySelectModelA(){Area = "深圳", Code="SZ159920",Name="恒生ETF"},
                new MySelectModelA(){Area = "深圳", Code="SZ159938",Name="医药卫生ETF"},
                new MySelectModelA(){Area = "深圳", Code="SZ159939",Name="信息技术ETF"},
                new MySelectModelA(){Area = "深圳", Code="SZ159949",Name="创业板50ETF"},
            }; 
        }


    }



    /// <summary>
    /// 一个枚举.
    /// 用来测试下拉列表中，是否能显示枚举类型的值.
    /// </summary>
    public enum ExportFormatType
    {
        NoFormat = 0,
        CrystalReport = 1,
        RichText = 2,
        WordForWindows = 3,
        Excel = 4,
        PortableDocFormat = 5,
        HTML32 = 6,
        HTML40 = 7,
        ExcelRecord = 8,
        Text = 9,
        CharacterSeparatedValues = 10,
        TabSeperatedText = 11,
        EditableRTF = 12,
        Xml = 13,
        RPTR = 14,
        ExcelWorkbook = 15
    }


}
