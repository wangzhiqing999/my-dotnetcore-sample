using System.Collections.Generic;
using W1001_ABP_With_Zero.Roles.Dto;

namespace W1001_ABP_With_Zero.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }

        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
