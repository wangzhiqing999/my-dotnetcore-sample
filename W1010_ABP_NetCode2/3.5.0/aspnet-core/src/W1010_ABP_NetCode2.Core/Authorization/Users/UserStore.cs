using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Linq;
using W1010_ABP_NetCode2.Authorization.Roles;

namespace W1010_ABP_NetCode2.Authorization.Users
{
    public class UserStore : AbpUserStore<Role, User>
    {
        public UserStore(
            IUnitOfWorkManager unitOfWorkManager, 
            IRepository<User, long> userRepository, 
            IRepository<Role> roleRepository, 
            IAsyncQueryableExecuter asyncQueryableExecuter, 
            IRepository<UserRole, long> userRoleRepository, 
            IRepository<UserLogin, long> userLoginRepository, 
            IRepository<UserClaim, long> userClaimRepository, 
            IRepository<UserPermissionSetting, long> userPermissionSettingRepository) 
            : base(
                  unitOfWorkManager, 
                  userRepository, 
                  roleRepository, 
                  asyncQueryableExecuter, 
                  userRoleRepository, 
                  userLoginRepository, 
                  userClaimRepository,
                  userPermissionSettingRepository)
        {
        }
    }
}