using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyWork.Web.Models
{
    public class LoginViewModel
    {

        /// <summary>
        /// 组织代码.
        /// </summary>
        public string Organization { get; set; }


        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        public string User { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Password { get; set; }


    }
}
