using System;
using System.Collections.Generic;
using System.Text;

namespace A3001_Json.Model
{

    /// <summary>
    /// 用于测试 json 序列化的类.
    /// </summary>
    public class TestData
    {

        /// <summary>
        /// 测试代码.
        /// </summary>
        public string TestCode { set; get; }


        /// <summary>
        /// 测试名称.
        /// </summary>
        public string TestName { set; get; }


        /// <summary>
        /// 测试年龄.
        /// </summary>
        public int TestAge { set; get; }


        /// <summary>
        /// 是否成人.
        /// (主要用于测试 只读属性)
        /// </summary>
        public bool IsAdult
        {
            get {
                return this.TestAge >= 18;
            }
        }



        /// <summary>
        /// 测试备注
        /// （主要用于测试 可为空的属性 ）
        /// </summary>
        public string TestRemark { set; get; }






        /// <summary>
        /// 测试稍微复杂一点的数据类型.
        /// </summary>
        public List<string> TestList { set; get; }


        public HashSet<string> TestSet { set; get; }



        public override string ToString()
        {
            StringBuilder buff = new StringBuilder();

            buff.AppendLine( $"TestCode={TestCode}; TestName={TestName}; TestAge={TestAge}；IsAdult={IsAdult}；TestRemark={TestRemark}");

            buff.AppendLine($"TestList = {string.Join(",", TestList)}");

            buff.AppendLine($"TestSet = {string.Join(",", TestSet)}");

            return buff.ToString();
        }

    }

}
