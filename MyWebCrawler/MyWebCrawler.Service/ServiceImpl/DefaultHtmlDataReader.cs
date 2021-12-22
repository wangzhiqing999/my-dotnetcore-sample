using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Reflection;

using log4net;

using MyWebCrawler.Model;
using MyWebCrawler.Service;


namespace MyWebCrawler.ServiceImpl
{
    public class DefaultHtmlDataReader<T> : IHtmlDataReader<T> where T : new()
    {

        /// <summary>
        /// 日志处理类.
        /// </summary>
        private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);



        string IHtmlDataReader<T>.ReadHtmlText(string url, string encoding)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                using (Stream s = webClient.OpenRead(url))
                {
                    using (StreamReader sr = new StreamReader(s, Encoding.GetEncoding(encoding)))
                    {
                        string result = sr.ReadToEnd();
                        return result;
                    }
                }
            }
        }





        /// <summary>
        /// 移除其他的文本信息.
        /// </summary>
        /// <param name="html"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        private string RemoveOtherText(string html, HtmlReaderConfig config)
        {
            string result = html;

            if (!String.IsNullOrEmpty(config.StartFlag))
            {
                // 如果定义了起始标志. 尝试移除起始标志之前的文本.
                int startIndex = result.IndexOf(config.StartFlag);
                if (startIndex > 0)
                {
                    result = result.Substring(startIndex);
                }
            }

            if (!String.IsNullOrEmpty(config.FinishFlag))
            {
                // 如果定义了结束标志. 尝试移除起结束标志之前的文本.
                int finishIndex = result.IndexOf(config.FinishFlag);
                if (finishIndex > 0)
                {
                    result = result.Substring(0, finishIndex);
                }
            }

            return result;
        }



        /// <summary>
        /// 移除 html 中的 js 脚本.
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        string IHtmlDataReader<T>.RemoveScriptAndStyle(string html)
        {
            string result =Regex.Replace(html, @"(\<script(.+?)\</script\>)|(\<style(.+?)\</style\>)", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            return result;
        }



        /// <summary>
        /// 移除 html 的注释文本.
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        string IHtmlDataReader<T>.RemoveRemarkText(string html)
        {
            string result = Regex.Replace(html, @"(\<!--(.+?)--\>)", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            return result;
        }









        T IHtmlDataReader<T>.ReadSingleData(string html, HtmlReaderConfig config)
        {
            string text = this.RemoveOtherText(html, config);

            // 初始化 正则表达式  忽略大小写
            Regex r = new Regex(config.RegexText, RegexOptions.IgnoreCase | RegexOptions.Singleline);

            // 指定的输入字符串中搜索 Regex 构造函数中指定的正则表达式的第一个匹配项。
            Match m = r.Match(text);


            if (logger.IsDebugEnabled) 
            {
                logger.DebugFormat("尝试匹配正则表达式：{0} ", config.RegexText);
            }

            if (m.Success)
            {
                T result = GetValueFromMatch(m, config);
                return result;
            }

            return default(T);
        }






        List<T> IHtmlDataReader<T>.ReadMultiData(string html, HtmlReaderConfig config)
        {
            // 结果列表.
            List<T> resultList = new List<T>();

            // 移除头尾.
            string text = this.RemoveOtherText(html, config);

            // 初始化 正则表达式  忽略大小写
            Regex r = new Regex(config.RegexText, RegexOptions.IgnoreCase | RegexOptions.Singleline);


            // 指定的输入字符串中搜索 Regex 构造函数中指定的正则表达式的第一个匹配项。
            Match m = r.Match(text);

            // 匹配的 计数.
            int matchCount = 0;


            if (logger.IsDebugEnabled) 
            {
                logger.DebugFormat("尝试匹配正则表达式：{0} ", config.RegexText);
            }


            while (m.Success)
            {
                if (logger.IsDebugEnabled)
                {
                    logger.DebugFormat("第{0}次匹配！", (++matchCount));

                    for (int i = 0; i < m.Groups.Count; i++)
                    {
                        logger.DebugFormat("m.Groups[ {0} ] = {1}", i, m.Groups[i]);
                    }
                }

                T result = GetValueFromMatch(m, config);

                // 加入结果列表.
                resultList.Add(result);

                m = m.NextMatch();
            }



            // 返回.
            return resultList;
        }




        private T GetValueFromMatch(Match m, HtmlReaderConfig config)
        {
            T result = new T();

            // 对象类型.
            Type fromType = result.GetType();
            // 对象的所有属性.
            PropertyInfo[] fromPropArray = fromType.GetProperties();


            for (int i = 0; i < config.PropertyNameList.Count; i++)
            {
                // 属性名.
                string propertyName = config.PropertyNameList[i];

                PropertyInfo prop = fromPropArray.FirstOrDefault(p => p.Name == propertyName);

                if (prop == null)
                {
                    if (logger.IsWarnEnabled)
                    {
                        logger.WarnFormat("属性 {0} 不存在！", propertyName);
                    }
                    continue;
                }
                if (!prop.CanWrite)
                {
                    if (logger.IsWarnEnabled)
                    {
                        logger.WarnFormat("属性 {0} 不可写入！", propertyName);
                    }
                    continue;
                }


                if (m.Groups.Count <= i)
                {
                    if (logger.IsWarnEnabled)
                    {
                        logger.WarnFormat("属性的数量，大于正则表达式匹配的项目数.");
                    }
                    break;
                }


                // 正则匹配后的文本.
                string regText = m.Groups[i + 1].Value;


                // 为目标对象，设置属性值.
                prop.SetValue(result, regText, null);

            }

            return result;
        }



    }
}
