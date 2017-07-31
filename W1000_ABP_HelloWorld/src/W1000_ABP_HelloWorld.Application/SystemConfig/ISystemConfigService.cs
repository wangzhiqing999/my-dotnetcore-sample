using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;


using W1000_ABP_HelloWorld.SystemConfig.Dtos;



namespace W1000_ABP_HelloWorld.SystemConfig
{

    public interface ISystemConfigService : IApplicationService
    {

        /// <summary>
        /// 取得全部的 系统配置类型.
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<SystemConfigTypeDto>> GetAllSystemConfigType();


        /// <summary>
        /// 取得指定的配置类型.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SystemConfigTypeDto> GetSystemConfigType(ConfigTypeCodeInput input);




        /// <summary>
        /// 取得指定类型的 系统配置属性.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultDto<SystemConfigPropertyDto>> GetSystemConfigPropertyByType(ConfigTypeCodeInput input);



        /// <summary>
        /// 取得指定类型的 系统配置数值.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultDto<SystemConfigValueDto>> GetSystemConfigValueByType(ConfigTypeCodeInput input);



        /// <summary>
        /// 取得系统配置数值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SystemConfigValueDto> GetSystemConfigValue(GetSystemConfigValueInput input);

    }
}
