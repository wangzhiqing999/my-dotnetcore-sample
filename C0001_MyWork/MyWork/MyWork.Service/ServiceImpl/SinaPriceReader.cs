using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Net;

using MyWork.Service;


namespace MyWork.ServiceImpl
{

    /// <summary>
    /// 新浪数据读取服务
    /// </summary>
    public class SinaPriceReader : IPriceReader
    {
        public decimal GetClosePrice(string stockCode)
        {
            string url = String.Format("http://hq.sinajs.cn/list={0}", stockCode.ToLower());

            //访问该链接
            WebRequest request = WebRequest.Create(url);
            //获得返回值
            WebResponse response = request.GetResponse();

            // 从 Internet 资源返回数据流。
            using (Stream s = response.GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(s, Encoding.UTF8))
                {
                    string line = null;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (String.IsNullOrWhiteSpace(line))
                        {
                            // 只有空白， 结束.
                            break;
                        }

                        // 数据格式：
                        // var hq_str_sh601006="大秦铁路,13.41,13.69,13.07,13.69,13.05,13.06,13.07,115275247,1540387027,43400,13.06,51500,13.05,34900,13.04,187800,13.03,547600,13.02,13800,13.07,26000,13.08,19800,13.09,312424,13.10,79400,13.11,2015-04-24,11:35:42,00";

                        line = line.Trim();
                        int eqIndex = line.IndexOf('=');
                        line = line.Substring(eqIndex + 2);
                        line = line.Substring(0, line.Length - 2);
                        string[] itemArray = line.Split(',');

                        // 返回收盘价.
                        decimal closePrice = Convert.ToDecimal(itemArray[3]);
                        return closePrice;
                    }
                    // 数据不存在的情况下.
                    return -1;
                }
            }

        }
    }
}
