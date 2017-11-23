using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace A0030_Authentication.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {

        /// <summary>
        /// 员工代码.
        /// 本属性用于测试 Claims-Based Authorization
        /// </summary>
        [StringLength(32)]
        [DataType(DataType.Text)]
        public string EmployeeNumber { set; get; }

    }
}
