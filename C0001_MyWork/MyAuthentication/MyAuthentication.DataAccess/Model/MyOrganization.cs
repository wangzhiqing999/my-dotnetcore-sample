using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

using MyFramework.Model;


namespace MyAuthentication.Model
{

    /// <summary>
    /// 组织.
    /// </summary>
    [Serializable]
    [Table("my_organization")]
    public class MyOrganization : CommonData
    {

        /// <summary>
        /// 组织ID
        /// </summary>
        [Column("organization_id")]
        [Key]
        [Display(Name = "组织ID")]
        public long OrganizationID { set; get; }


        /// <summary>
        /// 组织名
        /// </summary>
        [Column("organization_name")]
        [Display(Name = "组织名")]
        [StringLength(32)]
        [Required]
        public string OrganizationName { set; get; }


        /// <summary>
        /// 登录组织代码
        /// </summary>
        [Column("login_organization_code")]
        [Display(Name = "登录组织代码")]
        [StringLength(32)]
        [Required]
        public string LoginOrganizationCode { set; get; }






        #region 一对多.


        /// <summary>
        /// 组织下的用户.
        /// </summary>
        [JsonIgnore]
        public virtual List<MyUser> Users { set; get; }


        #endregion 一对多.


    }
}
