using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace A0009_EF_Postgres_V6.Model
{

    /// <summary>
    /// 文档.
    /// </summary>
    [Table("document")]
    public class Document
    {


        /// <summary>
        /// 文档代码.
        /// </summary>
        [Column("document_id")]
        [Display(Name = "文档代码")]
        [Key]
        public long DocumentID { set; get; }



        #region 一对多.

        /// <summary>
        /// 文档类型代码.
        /// </summary>
        [Column("document_type_code")]
        [Display(Name = "文档类型代码")]
        [StringLength(32)]
        public string DocumentTypeCode { set; get; }


        /// <summary>
        /// 文档类型.
        /// </summary>
        public virtual DocumentType DocumentType { set; get; }


        #endregion 一对多.




        /// <summary>
        /// 文档标题.
        /// </summary>
        [Column("document_title")]
        [Display(Name = "文档标题")]
        [StringLength(64)]
        [Required]
        public string DocumentTitle { set; get; }



        /// <summary>
        /// 文章内容.
        /// </summary>
        [Column("document_content")]
        [Display(Name = "文章内容")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Html)]
        public string DocumentContent { set; get; }




        public override string ToString()
        {
            return String.Format("{0} : {1} : {2}", this.DocumentID, this.DocumentTitle, this.DocumentContent);
        }

    }

}
