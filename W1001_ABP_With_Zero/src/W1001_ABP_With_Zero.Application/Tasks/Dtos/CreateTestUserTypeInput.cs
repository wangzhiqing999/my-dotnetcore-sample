using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.MultiTenancy;


namespace W1001_ABP_With_Zero.Tasks.Dtos
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
