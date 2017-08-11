using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using W1001_ABP_With_Zero.Tasks.Dtos;




namespace W1001_ABP_With_Zero.Tasks
{


    /// <summary>
    /// 其他处理接口.
    /// </summary>
    public interface IOtherAppService : IAsyncCrudAppService<OtherDto, Int64, PagedResultRequestDto, CreateOtherDto, OtherDto>
    {





    }


}
