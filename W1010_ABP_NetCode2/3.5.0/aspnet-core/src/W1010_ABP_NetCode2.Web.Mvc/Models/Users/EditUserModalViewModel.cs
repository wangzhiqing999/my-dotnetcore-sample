using System.Collections.Generic;
using System.Linq;
using W1010_ABP_NetCode2.Roles.Dto;
using W1010_ABP_NetCode2.Users.Dto;

namespace W1010_ABP_NetCode2.Web.Models.Users
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
