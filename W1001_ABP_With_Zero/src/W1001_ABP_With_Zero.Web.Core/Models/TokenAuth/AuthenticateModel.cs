using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;
using W1001_ABP_With_Zero.Authorization.Users;

namespace W1001_ABP_With_Zero.Models.TokenAuth
{
    public class AuthenticateModel
    {
        [Required]
        [MaxLength(AbpUserBase.MaxEmailAddressLength)]
        public string UserNameOrEmailAddress { get; set; }

        [Required]
        [MaxLength(User.MaxPlainPasswordLength)]
        public string Password { get; set; }
        
        public bool RememberClient { get; set; }
    }
}
