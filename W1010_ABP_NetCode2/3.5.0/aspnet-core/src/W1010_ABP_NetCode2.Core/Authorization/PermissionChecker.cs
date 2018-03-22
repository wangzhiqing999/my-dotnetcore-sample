using Abp.Authorization;
using W1010_ABP_NetCode2.Authorization.Roles;
using W1010_ABP_NetCode2.Authorization.Users;

namespace W1010_ABP_NetCode2.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
