using System.Collections.Generic;

namespace W1001_ABP_With_Zero.Authentication.External
{
    public interface IExternalAuthConfiguration
    {
        List<ExternalLoginProviderInfo> Providers { get; }
    }
}
