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
        // IAsyncCrudAppService 接口中， 定义了 基本的 增/删/改/查  的处理接口.



        // ##### 下面是 自己额外定义的处理接口. #####

        /// <summary>
        /// 查询数据.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResultDto<OtherDto> Query (QueryOtherInput input);

    }


}
