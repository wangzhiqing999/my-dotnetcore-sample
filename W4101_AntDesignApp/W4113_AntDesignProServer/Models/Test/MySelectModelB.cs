namespace W4113_AntDesignProServer.Models.Test
{
    /// <summary>
    /// 下列列表的数据.
    /// <br/>
    /// 这里是测试，实际业务中，可能是数据库中的数据，或者从别的地方获取的。
    /// <br/>
    /// 这里的下拉列表的数值，是 int 类型的.
    /// 
    /// <br/>
    /// 如果是 数据库的 自增主键列，有没有选择，可以通过 value != 0 来判断。
    /// 如果是查询条件，可以通过 TItemValue="int?" 来实现，最后通过 value == null 来判断。
    /// </summary>
    public class MySelectModelB
    {

        /// <summary>
        /// ID.
        /// <br/>
        /// 用于模拟数据库里面的自增主键列.
        /// </summary>
        public int ID { get; set; }



        /// <summary>
        /// 名称.
        /// </summary>
        public string Name { get; set; }




        public static List<MySelectModelB> GetTestDataList()
        {
            return new List<MySelectModelB>()
            {
                new MySelectModelB(){ID=1, Name="张三"},
                new MySelectModelB(){ID=2, Name="李四"},
                new MySelectModelB(){ID=3, Name="王五"},
                new MySelectModelB(){ID=4, Name="赵六"},
                new MySelectModelB(){ID=5, Name="孙七"},
            };
        }
    }

}
