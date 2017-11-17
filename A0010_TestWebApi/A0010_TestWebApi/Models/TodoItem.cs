using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace A0010_TestWebApi.Models
{

    /// <summary>
    /// Todo 项目.
    /// </summary>
    public class TodoItem
    {
        /// <summary>
        /// Todo 项目编号.
        /// </summary>
        public long Id { get; set; }


        /// <summary>
        /// Todo 项目名称
        /// </summary>
        [Required]
        public string Name { get; set; }


        /// <summary>
        /// 是否完成.
        /// </summary>
        [DefaultValue(false)]
        public bool IsComplete { get; set; }

    }
}
