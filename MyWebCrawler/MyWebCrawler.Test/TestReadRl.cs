using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebCrawler.Model;
using MyWebCrawler.Service;
using MyWebCrawler.ServiceImpl;

namespace MyWebCrawler.Test
{
    public class TestReadRl
    {


        private IHtmlDataReader<RlData> _HtmlDataReader = new DefaultHtmlDataReader<RlData>();


        private const string SourceUrl = @"http://rl.fx678.com/wap";


        private HtmlReaderConfig _HtmlReaderConfig = new HtmlReaderConfig()
        {
            // 起始标志.
            StartFlag = "<ul class=\"common_data\">",

            // 正则表达式.
            RegexText = @"<li[\s\n\r\w\W]+?<div [\s\n\r\w\W]+?>[\s\n\r]+?<div class=""([\w\W]+?)"">[\s\n\r\w\W]+?<span>([\w\W]+?)</span>[\s\n\r]+?<img src=""/Public/wap/images/([\w\W]+?).png""[\s\n\r\w\W]+?<p>([\w\W]+?)</p>[\s\n\r\w\W]+?<li>前值<span>([\w\W]+?)</span></li>[\s\n\r]+?<li>预期<span>([\w\W]+?)</span></li>[\s\n\r]+?<li>实际<span [\s\n\r\w\W]+?>([\w\W]+?)</span></li>",

            // 属性名列表.
            PropertyNameList = new List<string>()
            {
                "Country",
                "Time",
                "StartImg",
                "Content",
                "Previous",
                "Predict",
                "CurrentValue"
            }
        };



        public void TestRead()
        {

            // 读取.
            string htmlText = this._HtmlDataReader.ReadHtmlText(SourceUrl);

            // 移除脚本与样式.
            htmlText = this._HtmlDataReader.RemoveScriptAndStyle(htmlText);

            // 移除备注.
            htmlText = this._HtmlDataReader.RemoveRemarkText(htmlText);


            List<RlData> resultList = this._HtmlDataReader.ReadMultiData(htmlText, _HtmlReaderConfig);

            foreach(RlData data in resultList)
            {
                Console.WriteLine(data);
            }

        }

    }





    public class RlData
    {

        /// <summary>
        /// 国家.
        /// </summary>
        public string Country { set; get; }


        /// <summary>
        /// 时间
        /// </summary>
        public string Time { set; get; }


        /// <summary>
        /// 重要度图片.
        /// </summary>
        public string StartImg { set; get; }



        /// <summary>
        /// 名称.
        /// </summary>
        public string Content { set; get; }



        /// <summary>
        /// 前值.
        /// </summary>
        public string Previous { set; get; }



        /// <summary>
        /// 预测值.
        /// </summary>
        public string Predict { set; get; }



        /// <summary>
        /// 公布值.
        /// </summary>
        public string CurrentValue { set; get; }




        public override string ToString()
        {
            return $"[财经日历] 国家={Country};时间={Time};重要度={StartImg};名称={Content};前值={Previous};预测值={Predict};公布值={CurrentValue}";
        }


    }

}
