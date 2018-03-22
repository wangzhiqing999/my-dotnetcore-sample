using System.ComponentModel.DataAnnotations;
using Abp.MultiTenancy;

namespace W1010_ABP_NetCode2.Authorization.Accounts.Dto
{
    public class IsTenantAvailableInput
    {
        [Required]
        [StringLength(AbpTenantBase.MaxTenancyNameLength)]
        public string TenancyName { get; set; }
    }
}
