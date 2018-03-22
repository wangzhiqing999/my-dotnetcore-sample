using System.Collections.Generic;
using W1010_ABP_NetCode2.Roles.Dto;

namespace W1010_ABP_NetCode2.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }

        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
