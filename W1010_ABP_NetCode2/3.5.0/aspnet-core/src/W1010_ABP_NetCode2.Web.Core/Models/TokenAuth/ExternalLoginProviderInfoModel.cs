using Abp.AutoMapper;
using W1010_ABP_NetCode2.Authentication.External;

namespace W1010_ABP_NetCode2.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
