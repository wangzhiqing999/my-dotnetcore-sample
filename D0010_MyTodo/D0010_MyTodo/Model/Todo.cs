using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace D0010_MyTodo.Model
{

    /// <summary>
    /// 待办事项.
    /// </summary>
    [Table("my_todo_list")]
    public class Todo
    {

        /// <summary>
        /// ID
        /// </summary>
        [Column("id")]
        [Display(Name = "ID")]
        [Key]
        public long ID { set; get; }


        /// <summary>
        /// 标题
        /// </summary>
        [Column("title")]
        [Display(Name = "标题")]
        [StringLength(64)]
        [Required]
        public string Title { set; get; } = "";


        /// <summary>
        /// 详情
        /// </summary>
        [Column("detail")]
        [Display(Name = "详情")]
        [StringLength(1024)]
        [DataType(DataType.MultilineText)]
        public string Detail { set; get; }



        /// <summary>
        /// 重要度
        /// </summary>
        [Column("importance")]
        [Display(Name = "重要度")]
        public byte Importance { set; get; }



        /// <summary>
        /// 截止日期.
        /// </summary>
        [Column("closing_date")]
        [Display(Name = "重要度")]
        public DateTime? ClosingDate { set; get; }



        /// <summary>
        /// 是否已完成
        /// </summary>
        [Column("is_done")]
        [Display(Name = "是否已完成")]
        public bool IsDone { set; get; }


    }
}
