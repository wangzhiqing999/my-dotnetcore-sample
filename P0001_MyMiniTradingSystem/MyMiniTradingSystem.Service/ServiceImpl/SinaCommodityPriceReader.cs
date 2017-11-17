using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



using log4net;


using System.IO;
using System.Net;

using MyMiniTradingSystem.Model;
using MyMiniTradingSystem.Service;



namespace MyMiniTradingSystem.ServiceImpl
{


    /// <summary>
    /// 新浪数据读取服务.
    /// </summary>
    public class SinaCommodityPriceReader : ICommodityPriceReader
    {


        /// <summary>
        /// 日志处理类.
        /// </summary>
        protected static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);



        /// <summary>
        /// 取得 Sina股票数据接口地址.
        /// 
        /// http://hq.sinajs.cn/list=sh601006
        /// </summary>
        /// <param name="stockCode"></param>
        /// <param name="startDate"></param>
        /// <param name="finishDate"></param>
        /// <returns></returns>
        private string FormatUrl(string stockCode, DateTime startDate, DateTime finishDate)
        {
            return String.Format("http://hq.sinajs.cn/list={0}", stockCode);
        }




        /// <summary>
        /// 读取一行数据.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private CommodityPrice GetOneCommodityPriceDay(string line)
        {
            if(logger.IsDebugEnabled)
            {
                logger.Debug(line);
            }

            // 数据格式：
            // var hq_str_sh601006="大秦铁路,13.41,13.69,13.07,13.69,13.05,13.06,13.07,115275247,1540387027,43400,13.06,51500,13.05,34900,13.04,187800,13.03,547600,13.02,13800,13.07,26000,13.08,19800,13.09,312424,13.10,79400,13.11,2015-04-24,11:35:42,00";


            line = line.Trim();
            int eqIndex = line.IndexOf('=');
            line = line.Substring(eqIndex + 2);
            line = line.Substring(0, line.Length - 2);


            string[] itemArray = line.Split(',');

            // 处理日期.
            string dateStr = itemArray[itemArray.Length - 3];
            DateTime processDate = Convert.ToDateTime(dateStr);

            CommodityPrice result = new CommodityPrice()
            {
                // 日期.
                TradingStartDate = processDate,
                TradingFinishDate = processDate,
                // 开.
                OpenPrice = Convert.ToDecimal(itemArray[1]),
                // 高.
                HighestPrice = Convert.ToDecimal(itemArray[4]),
                // 低.
                LowestPrice = Convert.ToDecimal(itemArray[5]),
                // 平.
                ClosePrice = Convert.ToDecimal(itemArray[3]),
                // 成交.
                Volume = Convert.ToInt64(itemArray[8])
            };

            return result;
        }






        public List<CommodityPrice> GetCommodityPriceList(string stockCode, DateTime startDate, DateTime finishDate)
        {

             List<CommodityPrice> resultList = new List<CommodityPrice>();

             string url = FormatUrl(stockCode, startDate, finishDate);


             try
             {
                 //访问该链接
                 WebRequest request = WebRequest.Create(url);
                 //获得返回值
                 WebResponse response = request.GetResponse();
                 // 从 Internet 资源返回数据流。
                 Stream s = response.GetResponseStream();

                 using (StreamReader sr = new StreamReader(s, Encoding.UTF8))
                 {
                     int lineIndex = 0;
                     string line;
                     while ((line = sr.ReadLine()) != null)
                     {
                         lineIndex++;

                         if (String.IsNullOrWhiteSpace(line))
                         {
                             // 只有空白， 结束.
                             break;
                         }

                         // 读取一行.
                         CommodityPrice oneResult = GetOneCommodityPriceDay(line);

                         // 加入列表.
                         resultList.Add(oneResult);
                     }
                 }

             }
             catch (Exception ex)
             {
                logger.Error(ex.Message, ex);

                 throw ex;
             }

             return resultList;
        }


    }
}
