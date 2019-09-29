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
    /// 测试学校.
    /// 本类主要用于测试 一对多的情况.
    /// </summary>
    [Serializable]
    [Table("my_sample_test_school")]
    public class TestSchool
    {

        /// <summary>
        /// 学校代码.
        /// </summary>
        [Key]
        [Column("school_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SchoolID { set; get; }


        /// <summary>
        /// 学校名.
        /// </summary>
        [Column("school_name")]
        public string SchoolName { set; get; }


        /// <summary>
        /// 学校中的教师.
        /// 这里是 一对多 关系中
        /// 一个学校，对应多个教师.
        /// </summary>
        public List<TestTeacher> SchoolTeachers { get; set; }

    }
}
