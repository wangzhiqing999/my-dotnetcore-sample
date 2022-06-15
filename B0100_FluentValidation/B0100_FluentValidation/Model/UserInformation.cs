using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B0100_FluentValidation.Model
{

    /// <summary>
    /// 用户信息.
    /// </summary>
    public class UserInformation
    {

        /// <summary>
        /// 用户名.
        /// </summary>
        public string UserName { set; get; }


        /// <summary>
        /// 性别.
        /// </summary>
        public string Sex { set; get; }



        /// <summary>
        /// 证件号码.
        /// </summary>
        public string CardNo { set; get; }



        /// <summary>
        /// 年龄.
        /// </summary>
        public int Age { set; get; }



        /// <summary>
        /// 电子邮件.
        /// </summary>
        public string Email { set; get; }

    }
}
