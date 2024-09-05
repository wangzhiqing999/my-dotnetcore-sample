using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace A0006_EF_Sqlite_V8.Model
{

    /// <summary>
    /// 测试数据表.
    /// </summary>
    [Table("test_data")]
    public class TestData
    {

        /// <summary>
        /// ID
        /// </summary>
        [Column("id")]
        [Display(Name = "ID")]
        [Key]
        public long ID{ set; get; }


        /// <summary>
        /// 姓名
        /// </summary>
        [Column("name")]
        [Display(Name = "姓名")]
        [StringLength(16)]
        [Required]
        public string Name { set; get; }


        /// <summary>
        /// 电话
        /// </summary>
        [Column("phone")]
        [Display(Name = "电话")]
        [StringLength(16)]
        public string Phone { set; get; }

        /// <summary>
        /// 地址
        /// </summary>
        [Column("address")]
        [Display(Name = "地址")]
        [StringLength(64)]
        public string Address { set; get; }



        /// <summary>
        /// 明细数据.
        /// <br/>
        /// 以json格式存储.
        /// </summary>
        [Column("detail_data")]
        [Required]
        public JsonDocument DetailData { get; set; } = JsonDocument.Parse("{ }");


        /// <summary>
        /// 创建时间.
        /// </summary>
        [Column("created_time")]
        public DateTime CreatedTime { get; set; }

    }
}
