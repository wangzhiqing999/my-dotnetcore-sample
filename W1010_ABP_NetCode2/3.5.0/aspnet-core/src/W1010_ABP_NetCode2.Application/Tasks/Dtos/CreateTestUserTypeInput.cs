using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.MultiTenancy;


namespace W1010_ABP_NetCode2.Tasks.Dtos
{
    /// <summary>
    /// CreateOtherDto
    /// </summary>
    [AutoMapTo(typeof(TestUserType))]
    public class CreateTestUserTypeInput
    {

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(TestUserType.MaxNameLength)]
        public string Name { get; set; }



        /// <summary>
        /// 有效
        /// </summary>
        public bool IsActive { get; set; }


    }
}
