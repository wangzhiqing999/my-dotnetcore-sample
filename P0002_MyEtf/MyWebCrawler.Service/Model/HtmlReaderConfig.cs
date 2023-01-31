using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebCrawler.Model
{

    /// <summary>
    /// HTML 读取配置.
    /// </summary>
    public class HtmlReaderConfig
    {


        /// <summary>
        /// 起始标志.
        /// </summary>
        public string StartFlag { set; get; }



        /// <summary>
        /// 结束标志.
        /// </summary>
        public string FinishFlag { set; get; }




        /// <summary>
        /// 正则表达式文本.
        /// </summary>
        public string RegexText { set; get; }



        /// <summary>
        /// 属性名称列表.
        /// </summary>
        public List<String> PropertyNameList { set; get; }


    }


}
