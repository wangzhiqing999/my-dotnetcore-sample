using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Domain.Repositories;

namespace W1001_ABP_With_Zero.Editions
{
    public class EditionManager : AbpEditionManager
    {
        public const string DefaultEditionName = "Standard";

        public EditionManager(
            IRepository<Edition> editionRepository, 
            IAbpZeroFeatureValueStore featureValueStore)
            : base(
                editionRepository,
                featureValueStore
            )
        {
        }
    }
}
