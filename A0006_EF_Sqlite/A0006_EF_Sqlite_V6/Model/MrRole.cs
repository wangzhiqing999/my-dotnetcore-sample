using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace A0006_EF_Sqlite_V6.Model
{


	/// <summary>
	/// 角色
	/// </summary>
    [Serializable]
	[DataContract(Namespace="")]
	[Table("mr_role")]
    public partial class MrRole
    {
	
	
		/// <summary>
		/// 角色代码
		/// </summary>
        [DataMember]
		[Column("role_code")]
		[Key]
		[Display(Name = "角色代码")]
        [StringLength(32)]
        [Required]
		public string RoleCode { set; get; }



		/// <summary>
		/// 角色名称
		/// </summary>
        [DataMember]
		[Column("role_name")]
		[Display(Name = "角色名称")]
        [StringLength(64)]
        [Required]
        public string RoleName { set; get; }



        #region 一对多的部分.  (用户-角色关系)



        /// <summary>
        /// 用户-角色关系
        /// </summary>
        public List<MrUserRole> UserRoles { set; get; }


        #endregion





	}

}