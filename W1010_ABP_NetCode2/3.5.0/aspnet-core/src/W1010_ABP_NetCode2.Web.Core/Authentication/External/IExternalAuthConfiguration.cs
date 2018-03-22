using System.Collections.Generic;

namespace W1010_ABP_NetCode2.Authentication.External
{
    public interface IExternalAuthConfiguration
    {
        List<ExternalLoginProviderInfo> Providers { get; }
    }
}
