namespace W4113_AntDesignProServer.Models.Test
{
    /// <summary>
    /// 下列列表的数据.
    /// <br/>
    /// 这里是测试，实际业务中，可能是数据库中的数据，或者从别的地方获取的。
    /// <br/>
    /// 这里的下拉列表的数值，是 string 类型的.
    /// 有没有选择，可以通过 string.IsNullOrEmpty(value) 来判断。
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


}
