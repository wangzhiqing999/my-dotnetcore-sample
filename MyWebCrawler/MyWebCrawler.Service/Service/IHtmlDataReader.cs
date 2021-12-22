using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using MyWebCrawler.Model;


namespace MyWebCrawler.Service
{


    /// <summary>
    /// HTML 数据读取.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IHtmlDataReader<T> where T : new()
    {

        /// <summary>
        /// 从 url 抓取 html 文本信息.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        string ReadHtmlText(string url, string encoding = "utf-8");


        /// <summary>
        /// 移除 html 文本中的 script 与 style
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        string RemoveScriptAndStyle(string html);



        /// <summary>
        /// 移除 html 中的注释文本.
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        string RemoveRemarkText(string html);






        /// <summary>
        /// 从 html 文本中，抓取单独的数据.
        /// </summary>
        /// <param name="html"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        T ReadSingleData(string html, HtmlReaderConfig config);



        /// <summary>
        /// 从 html 文本中，抓取多行数据.
        /// </summary>
        /// <param name="html"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        List<T> ReadMultiData(string html, HtmlReaderConfig config);


    }

}
