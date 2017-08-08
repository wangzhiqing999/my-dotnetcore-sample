using System.Collections.Generic;
using W1001_ABP_With_Zero.Roles.Dto;
using W1001_ABP_With_Zero.Users.Dto;

namespace W1001_ABP_With_Zero.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}