using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace A0006_EF_Sqlite_V8.TypeConverters
{

    /// <summary>
    /// JsonDocument 转换器.
    /// <br/>
    /// 数据库中，列的数据类型，没有 Json 类型。
    /// 属性的类型为 JsonDocument 的时候，用这个转换器，将其转换为 string 类型，存储到数据库表中。
    /// </summary>
    internal class JsonDocumentToStringConverter : ValueConverter<JsonDocument, string?>
    {


        public JsonDocumentToStringConverter() : base(
            jsonDocument => jsonDocument.RootElement.GetRawText(),
            jsonString => JsonStringToJsonDocument(jsonString, GetJsonDocumentOptions()))
        { }




        #region 私有方法


        /// <summary>
        /// json 字符串转换 JsonDocument 对象
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        private static JsonDocument JsonStringToJsonDocument(string? jsonString, JsonDocumentOptions? options)
        {
            if (jsonString == null)
                return JsonDocument.Parse("{}");

            return JsonDocument.Parse(jsonString);
        }



        /// <summary>
        /// JsonDocument 配置信息
        /// </summary>
        /// <returns></returns>
        public static JsonDocumentOptions GetJsonDocumentOptions()
        {
            var options = new JsonDocumentOptions()
            {
                MaxDepth = 128, // 设置最大深度
                CommentHandling = JsonCommentHandling.Skip, // 允许跳过注释
                AllowTrailingCommas = true // 允许尾随逗号
            };
            return options;
        }
        #endregion
    }
}
