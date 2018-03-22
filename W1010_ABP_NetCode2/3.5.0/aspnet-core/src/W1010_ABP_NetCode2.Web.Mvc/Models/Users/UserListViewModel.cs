using System.Collections.Generic;
using W1010_ABP_NetCode2.Roles.Dto;
using W1010_ABP_NetCode2.Users.Dto;

namespace W1010_ABP_NetCode2.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
