using System.Collections.Generic;
using System.Linq;
using W1001_ABP_With_Zero.Roles.Dto;
using W1001_ABP_With_Zero.Users.Dto;

namespace W1001_ABP_With_Zero.Web.Models.Users
{
    public class EditUserModalViewModel
    {
        public UserDto User { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }

        public bool UserIsInRole(RoleDto role)
        {
            return User.RoleNames != null && User.RoleNames.Any(r => r == role.NormalizedName);
        }
    }
}