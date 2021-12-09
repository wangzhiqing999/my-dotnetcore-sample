using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace A0005_EF_Mysql_V6.Model
{

    /// <summary>
    /// 文档类型.
    /// </summary>
    [Serializable]
    [Table("document_type")]
    public class DocumentType
    {


        /// <summary>
        /// 文档类型代码.
        /// </summary>
        [Column("document_type_code")]
        [Display(Name = "文档类型代码")]
        [StringLength(32)]
        [Key]
        public string DocumentTypeCode { set; get; }



        /// <summary>
        /// 文档类型名称.
        /// </summary>
        [Column("document_type_name")]
        [Display(Name = "文档类型名称")]
        [StringLength(64)]
        public string DocumentTypeName { set; get; }



        #region 一对多.


        /// <summary>
        /// 文档列表.
        /// </summary>
        public virtual List<Document> DocumentList { set; get; }


        #endregion




        public override string ToString()
        {
            return String.Format("{0} : {1}", this.DocumentTypeCode, this.DocumentTypeName);
        }

    }
}
