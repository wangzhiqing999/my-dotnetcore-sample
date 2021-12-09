using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace A0010_TestWebApiV6.Models
{
    public class LoginViewModel
    {

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
