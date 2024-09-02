using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace A0009_EF_Postgres_V8.Model
{


	/// <summary>
	/// 用户
	/// </summary>
    [Serializable]
	[DataContract(Namespace="")]
	[Table("mr_user")]
    public partial class MrUser
    {
	
	
		/// <summary>
		/// 用户代码
		/// </summary>
        [DataMember]
		[Column("user_code")]
		[Key]
		[Display(Name = "用户代码")]
        [StringLength(32)]
        [Required]
		public string UserCode { set; get; }



		/// <summary>
		/// 用户名
		/// </summary>
        [DataMember]
		[Column("user_name")]
		[Display(Name = "用户名")]
        [StringLength(32)]
        [Required]
        public string UserName { set; get; }




        #region 一对多的部分.  (用户-角色关系)



        /// <summary>
        /// 用户-角色关系
        /// </summary>
        public virtual List<MrUserRole> UserRoles { set; get; }


        #endregion






        #region 一对一的部分

        /// <summary>
        /// 用户扩展信息.
        /// </summary>        
        public virtual MrUserExp UserExp { set; get; }


        #endregion

    }

}