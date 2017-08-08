using Abp.MultiTenancy;
using W1001_ABP_With_Zero.Authorization.Users;

namespace W1001_ABP_With_Zero.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {
            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}