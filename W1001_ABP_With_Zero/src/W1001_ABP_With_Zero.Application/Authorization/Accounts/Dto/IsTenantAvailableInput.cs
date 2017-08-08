using System.ComponentModel.DataAnnotations;
using Abp.MultiTenancy;

namespace W1001_ABP_With_Zero.Authorization.Accounts.Dto
{
    public class IsTenantAvailableInput
    {
        [Required]
        [MaxLength(AbpTenantBase.MaxTenancyNameLength)]
        public string TenancyName { get; set; }
    }
}

