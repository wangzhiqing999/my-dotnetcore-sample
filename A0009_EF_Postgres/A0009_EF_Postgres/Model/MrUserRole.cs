using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace A0009_EF_Postgres.Model
{


	/// <summary>
	/// 用户-角色关系
	/// </summary>
    [Serializable]
	[DataContract(Namespace="")]
	[Table("mr_user_role")]
    public partial class MrUserRole
    {



        #region 一对多的部分.  (与用户)


        /// <summary>
		/// 用户代码
		/// </summary>
        [DataMember]
		[Column("user_code")]
		[Display(Name = "用户代码")]
        [StringLength(32)]
        [ForeignKey("User")]
        [Required]
		public string UserCode { set; get; }




        /// <summary>
        /// 用户.
        /// </summary>        
        public MrUser User { set; get; }


        #endregion




        #region 一对多的部分.  (与角色)

        /// <summary>
		/// 角色代码
		/// </summary>
        [DataMember]
		[Column("role_code")]
		[Display(Name = "角色代码")]
        [StringLength(32)]
        [ForeignKey("Role")]
        [Required]
		public string RoleCode { set; get; }



        /// <summary>
        /// 角色
        /// </summary>        
        public MrRole Role { set; get; }


        #endregion


	}

}