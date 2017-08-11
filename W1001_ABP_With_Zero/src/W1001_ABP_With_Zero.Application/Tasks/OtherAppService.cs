using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using W1001_ABP_With_Zero.Authorization;
using W1001_ABP_With_Zero.Authorization.Roles;
using W1001_ABP_With_Zero.Authorization.Users;
using W1001_ABP_With_Zero.Editions;
using W1001_ABP_With_Zero.Tasks.Dtos;
using Microsoft.AspNetCore.Identity;
using Abp.IdentityFramework;


namespace W1001_ABP_With_Zero.Tasks
{
    public class OtherAppService : AsyncCrudAppService<Other, OtherDto, Int64, PagedResultRequestDto, CreateOtherDto, OtherDto>, IOtherAppService
    {
        public OtherAppService(IRepository<Other, Int64> repository) : base(repository)
        {
        }


    }
}
