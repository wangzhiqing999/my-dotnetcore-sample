using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace A0009_EF_Postgres_V8.Model
{

    /// <summary>
    /// 产品.
    /// <br/>
    /// 这个类用于测试 Jsonb 类型.
    /// </summary>
    [Table("product")]
    public class Product
    {

        /// <summary>
        /// 产品ID.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 名称.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 有关产品的详细信息
        /// </summary>
        public Specifications Specifications { get; set; }

        /// <summary>
        /// 客户反馈
        /// </summary>
        public List<Review> Reviews { get; set; } = new();


        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// 多语言.
        /// </summary>
        public Dictionary<string, string> Translations { get; set; } = new();



        public override string ToString()
        {
            StringBuilder buff = new StringBuilder();
            buff.AppendLine($"产品ID：{Id}； 名称：{Name}； 创建时间：{CreatedAt}； 更新时间：{UpdatedAt}");
            buff.AppendLine($"详细信息: {Specifications}");
            buff.AppendLine($"客户反馈:");
            foreach (var item in Reviews)
            {
                buff.AppendLine($"{item}");
            }

            buff.AppendLine($"多语言:");
            foreach(var itemKey in Translations.Keys)
            {
                buff.AppendLine($"{itemKey}: {Translations[itemKey]}");
            }
            return buff.ToString();
        }

    }


    /// <summary>
    /// 有关产品的详细信息。
    /// </summary>
    public class Specifications
    {
        public string Material { get; set; }
        public string Color { get; set; }
        public string Dimensions { get; set; }


        public override string ToString()
        {
            return $"材质：{Material}； 颜色：{Color}； 尺寸：{Dimensions}";
        }
    }


    /// <summary>
    /// 客户反馈。
    /// </summary>
    public class Review
    {
        public string User { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }

        public override string ToString()
        {
            return $"用户：{User}； 评分：{Rating}； 内容：{Content}";
        }
    }



}
