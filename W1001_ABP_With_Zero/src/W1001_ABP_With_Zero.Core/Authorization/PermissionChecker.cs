using Abp.Authorization;
using W1001_ABP_With_Zero.Authorization.Roles;
using W1001_ABP_With_Zero.Authorization.Users;

namespace W1001_ABP_With_Zero.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
