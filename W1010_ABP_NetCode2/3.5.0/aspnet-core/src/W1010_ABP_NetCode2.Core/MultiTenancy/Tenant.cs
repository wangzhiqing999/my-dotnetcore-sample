using Abp.MultiTenancy;
using W1010_ABP_NetCode2.Authorization.Users;

namespace W1010_ABP_NetCode2.MultiTenancy
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
