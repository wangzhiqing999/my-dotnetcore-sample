using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using W1010_ABP_NetCode2.Authorization.Users;
using W1010_ABP_NetCode2.Editions;

namespace W1010_ABP_NetCode2.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore)
        {
        }
    }
}
