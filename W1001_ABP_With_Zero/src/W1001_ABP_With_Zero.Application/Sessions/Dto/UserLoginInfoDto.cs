using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using W1001_ABP_With_Zero.Authorization.Users;
using W1001_ABP_With_Zero.Users;

namespace W1001_ABP_With_Zero.Sessions.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserLoginInfoDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }
    }
}
