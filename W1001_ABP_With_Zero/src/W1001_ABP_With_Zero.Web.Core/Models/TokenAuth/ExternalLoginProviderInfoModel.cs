using Abp.AutoMapper;
using W1001_ABP_With_Zero.Authentication.External;

namespace W1001_ABP_With_Zero.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
