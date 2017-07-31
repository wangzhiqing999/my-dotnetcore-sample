using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;

using W1000_ABP_HelloWorld.SystemConfig.Dtos;


namespace W1000_ABP_HelloWorld.SystemConfig
{


    public class DefaultSystemConfigService : W1000_ABP_HelloWorldAppServiceBase, ISystemConfigService
    {


        /// <summary>
        /// 系统配置类型.
        /// </summary>
        private readonly IRepository<SystemConfigType, string> _systemConfigTypeRepository;


        /// <summary>
        /// 系统配置属性
        /// </summary>
        private readonly IRepository<SystemConfigProperty, long> _systemConfigPropertyRepository;


        /// <summary>
        /// 系统配置值.
        /// </summary>
        private readonly IRepository<SystemConfigValue, long> _systemConfigValueRepository;




        public DefaultSystemConfigService(
            IRepository<SystemConfigType, string> systemConfigTypeRepository,
            IRepository<SystemConfigProperty, long> systemConfigPropertyRepository,
            IRepository<SystemConfigValue, long> systemConfigValueRepository
            )
        {
            _systemConfigTypeRepository = systemConfigTypeRepository;
            _systemConfigPropertyRepository = systemConfigPropertyRepository;
            _systemConfigValueRepository = systemConfigValueRepository;
        }



        /// <summary>
        /// 取得全部的 系统配置类型.
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<SystemConfigTypeDto>> GetAllSystemConfigType()
        {
            var result = await this._systemConfigTypeRepository
                .GetAll()
                .ToListAsync();

            return new ListResultDto<SystemConfigTypeDto>(
                ObjectMapper.Map<List<SystemConfigTypeDto>>(result)
            );
        }


        /// <summary>
        /// 取得指定的配置类型.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SystemConfigTypeDto> GetSystemConfigType(ConfigTypeCodeInput input)
        {
            var result = await this._systemConfigTypeRepository
                .FirstOrDefaultAsync(p => p.Id == input.Id);

            return ObjectMapper.Map<SystemConfigTypeDto>(result);
        }


        /// <summary>
        /// 取得指定类型的 系统配置属性.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ListResultDto<SystemConfigPropertyDto>> GetSystemConfigPropertyByType(ConfigTypeCodeInput input)
        {
            var result = await this._systemConfigPropertyRepository
                .GetAll()
                .WhereIf(!String.IsNullOrEmpty(input.Id), t=>t.ConfigTypeCode == input.Id)
                .ToListAsync();

            return new ListResultDto<SystemConfigPropertyDto>(
                ObjectMapper.Map<List<SystemConfigPropertyDto>>(result)
            );
        }



        /// <summary>
        /// 取得指定类型的 系统配置数值.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ListResultDto<SystemConfigValueDto>> GetSystemConfigValueByType(ConfigTypeCodeInput input)
        {
            var result = await this._systemConfigValueRepository
                .GetAll()
                .WhereIf(!String.IsNullOrEmpty(input.Id), t => t.ConfigTypeCode == input.Id)
                .ToListAsync();

            return new ListResultDto<SystemConfigValueDto>(
                ObjectMapper.Map<List<SystemConfigValueDto>>(result)
            );
        }



        /// <summary>
        /// 取得系统配置数值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SystemConfigValueDto> GetSystemConfigValue(GetSystemConfigValueInput input)
        {
            var result = await this._systemConfigValueRepository
                .FirstOrDefaultAsync(p => p.ConfigTypeCode == input.ConfigTypeCode && p.ConfigCode == input.ConfigCode);

            return ObjectMapper.Map<SystemConfigValueDto>(result);
        }


    }



}
