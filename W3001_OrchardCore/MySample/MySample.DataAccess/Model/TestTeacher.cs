using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MySample.Model
{
    /// <summary>
    /// 测试教师.
    /// 本类主要用于测试 一对多的情况.
    /// </summary>
    [Serializable]
    [Table("my_sample_test_teacher")]
    public class TestTeacher
    {
        /// <summary>
        /// 教师ID.
        /// </summary>
        [Key]
        [Column("teacher_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeacherID { set; get; }


        #region 一对多关联.

        /// <summary>
        /// 学校代码
        /// </summary>
        [Column("school_id")]
        public int SchoolID { set; get; }


        /// <summary>  
        /// 执教的 学校.
        /// 这里是 一对多 关系中
        /// 一个学校，对应多个教师.
        /// </summary>  
        public TestSchool InSchool { get; set; }


        #endregion


        /// <summary>
        /// 教师名.
        /// </summary>
        [Column("teacher_name")]
        public string TeacherName { set; get; }


    }
}
