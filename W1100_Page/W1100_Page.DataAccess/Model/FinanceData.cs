using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using System.Runtime.Serialization;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace W1100_Page.Model
{
    
    /// <summary>
    /// 财经数据.
    /// </summary>
    public class FinanceData
    {


        /// <summary>
        /// 自增ID.
        /// </summary>
        [DataMember]
        [Column("finance_id")]
        [Display(Name = "ID")]
        [XmlIgnoreAttribute()]
        [Key]
        public long ID { set; get; }



        /// <summary>
        /// 财经日期与时间.
        /// </summary>
        [DataMember]
        [Column("FinanceDateTime")]
        [Display(Name = "财经日期与时间")]
        public DateTime FinanceDateTime { set; get; }



        /// <summary>
        /// 显示用财经日期
        /// </summary>
        [Display(Name = "财经日期")]
        [NotMapped]
        public string DisplayFinanceDate
        {
            get
            {
                return this.FinanceDateTime.ToString("yyyy-MM-dd");
            }
        }


        /// <summary>
        /// 显示用财经时间
        /// </summary>
        [Display(Name = "财经时间")]
        [NotMapped]
        public string DisplayFinanceTime
        {
            get
            {
                return this.FinanceDateTime.ToString("HH:mm");
            }
        }



        /// <summary>
        /// 国家/地区.
        /// </summary>
        [DataMember]
        [Column("CountryName")]
        [Display(Name = "国家/地区")]
        [Required]
        [StringLength(32)]
        public string CountryName { set; get; }



        /// <summary>
        /// 国家样式表.
        /// </summary>
        [NotMapped]
        public string CountryClassName
        {
            get
            {
                if (String.IsNullOrEmpty(CountryName))
                {
                    // 没有输入国家.
                    return "World";
                }

                switch (CountryName)
                {

                    case "韩国":
                        return "Korea";
                    case "日本":
                        return "Japan";
                    case "泰国":
                        return "Thailand";
                    case "新加坡":
                        return "Singapore";
                    case "印度":
                        return "Singapore";
                    case "印度尼西亚":
                        return "Indonesia";
                    case "越南":
                        return "Vietnam";
                    case "中国":
                        return "China";
                    case "中国台湾":
                    case "台湾":
                        return "Taiwan";
                    case "中国香港":
                    case "香港":
                        return "Hongkong";




                    case "德国":
                        return "Germany";
                    case "俄罗斯":
                        return "Russia";
                    case "法国":
                        return "France";
                    case "欧盟":
                        return "EuropeanUnion";
                    case "瑞士":
                        return "Switzerland";
                    case "意大利":
                        return "Italy";
                    case "英国":
                        return "England";



                    case "加拿大":
                        return "Canada";
                    case "美国":
                        return "America";
                    case "墨西哥":
                        return "Mexico";



                    case "澳大利亚":
                        return "Australia";
                    case "新西兰":
                        return "NewZealand";



                    case "巴西":
                        return "Brazil";
                    case "奥地利":
                        return "Austria";
                    case "西班牙":
                        return "Spain";
                    case "希腊":
                        return "Greece";


                    case "匈牙利":
                        return "Hungary";



                    case "荷兰":
                        return "Holland";

                    case "挪威":
                        return "Norway";



                    case "南非":
                        return "SouthAfrica";
                }


                // 其他.
                return "World";
            }

        }






        /// <summary>
        /// 指标名称.
        /// </summary>
        [DataMember]
        [Column("Content")]
        [Display(Name = "指标名称")]
        [Required]
        [StringLength(32)]
        public string Content { set; get; }



        /// <summary>
        /// 重要性
        /// </summary>
        [DataMember]
        [Column("Weightiness")]
        [Display(Name = "重要性")]
        public int Weightiness { set; get; }





        /// <summary>
        /// 前值.
        /// </summary>
        [DataMember]
        [Column("Previous")]
        [Display(Name = "前值")]
        [StringLength(32)]
        public string Previous { set; get; }



        /// <summary>
        /// 预测值.
        /// </summary>
        [DataMember]
        [Column("Predict")]
        [Display(Name = "预测值")]
        [StringLength(32)]
        public string Predict { set; get; }



        /// <summary>
        /// 公布值.
        /// </summary>
        [DataMember]
        [Column("CurrentValue")]
        [Display(Name = "公布值")]
        [StringLength(32)]
        public string CurrentValue { set; get; }



        /// <summary>
        /// 利多.
        /// </summary>
        [DataMember]
        [Column("Profitable")]
        [Display(Name = "利多")]
        [StringLength(32)]
        public string Profitable { set; get; }


        /// <summary>
        /// 利空.
        /// </summary>
        [DataMember]
        [Column("Unprofitable")]
        [Display(Name = "利空")]
        [StringLength(32)]
        public string Unprofitable { set; get; }



        /// <summary>
        /// 解读的地址.
        /// </summary>
        [DataMember]
        [Column("more_url")]
        [Display(Name = "解读的地址")]
        [StringLength(64)]
        public string MoreUrl { set; get; }





        public override string ToString()
        {
            return String.Format(
                "FinanceDateTime={0}; CountryName={1}; Content={2}; Previous={3}; Predict={4}; CurrentValue={5}; Weightiness={6}; Profitable={7}; Unprofitable={8}; MoreUrl={9}",
                FinanceDateTime,
                CountryName,
                Content,
                Previous,
                Predict,
                CurrentValue,
                Weightiness,
                Profitable,
                Unprofitable,
                MoreUrl
                );
        }

    }

}
