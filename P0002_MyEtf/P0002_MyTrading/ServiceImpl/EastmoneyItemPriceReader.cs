using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using MyWebCrawler.Model;
using MyWebCrawler.Service;
using MyWebCrawler.ServiceImpl;

using P0002_MyTrading.Service;
using P0002_MyTrading.ServiceModel;


namespace P0002_MyTrading.ServiceImpl
{

    /// <summary>
    /// 从天天基金网抓取基金的数据.
    /// <br/>
    /// http://fund.eastmoney.com/110017.html
    /// </summary>
    public class EastmoneyItemPriceReader : IItemPriceReader
    {


        //private readonly IHttpClientFactory _HttpClientFactory;



        private readonly IHtmlDataReader<ItemPriceData> _HtmlDataReader = new DefaultHtmlDataReader<ItemPriceData>();





        //public EastmoneyItemPriceReader(IHttpClientFactory httpClientFactory)
        //{
        //    this._HttpClientFactory = httpClientFactory;
        //}




        public ServiceResult ReadPrice(string itemCode)
        {
            // 查询的地址.
            string url = $"http://fund.eastmoney.com/{itemCode}.html";



            // 抓取html.
            string htmlText = _HtmlDataReader.ReadHtmlText(url);


            // 移除 备注信息.
            htmlText = _HtmlDataReader.RemoveRemarkText(htmlText);

            // 移除 js 与 样式.
            htmlText = _HtmlDataReader.RemoveScriptAndStyle(htmlText);


            HtmlReaderConfig conf = new HtmlReaderConfig()
            {
                StartFlag = "<dl class=\"dataItem02\">",
                FinishFlag = "</dl>",
                RegexText= "\\(</span>([^<]+)</p></dt><dd class=\"dataNums\"><span class=\"[\\w\\W]+?\">([\\w\\W]+?)</span>",
                PropertyNameList= new List<string>()
                {
                    "Date",
                    "Price"
                }
            };
                                   
            ItemPriceData itemPriceData = _HtmlDataReader.ReadSingleData(htmlText, conf);

            ServiceResult result = new ServiceResult(ServiceResult.ResultCodeIsSuccess, "success");
            result.ResultData = itemPriceData;

            return result;
        }


    }
}
