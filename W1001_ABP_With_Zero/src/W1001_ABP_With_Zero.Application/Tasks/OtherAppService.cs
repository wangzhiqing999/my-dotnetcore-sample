using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using W1001_ABP_With_Zero.Tasks.Dtos;

using Abp.Linq.Extensions;


using Abp.AutoMapper;


namespace W1001_ABP_With_Zero.Tasks
{
    public class OtherAppService : AsyncCrudAppService<Other, OtherDto, Int64, PagedResultRequestDto, CreateOtherDto, OtherDto>, IOtherAppService
    {
        public OtherAppService(IRepository<Other, Int64> repository) : base(repository)
        {
        }




        // 在不写任何代码，仅仅简单继承  AsyncCrudAppService  的情况下。
        // 继承有下列的实现：

        // 创建的处理.
        // public virtual Task<TEntityDto> Create(TCreateInput input);

        // 删除的处理.
        // public virtual Task Delete(TDeleteInput input);

        // 按主键获取数据的处理.
        // public virtual Task<TEntityDto> Get(TGetInput input);

        // 翻页获取全部数据的处理.
        // public virtual Task<PagedResultDto<TEntityDto>> GetAll(TGetAllInput input);

        // 更新的处理.
        // public virtual Task<TEntityDto> Update(TUpdateInput input);





        /// <summary>
        /// 查询数据.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResultDto<OtherDto> Query(QueryOtherInput input)
        {
            var query = this.Repository.GetAll();

            // 指定查询条件.
            if (!String.IsNullOrEmpty(input.Filter))
            {
                query = query.Where(p => p.Name.Contains(input.Filter));
            }

            // 总行数.
            var resultCount = query.Count();




            // ##### 翻页.#####

            // 如果不 using Abp.Linq.Extensions;
            // 则使用下面的写法.
            // query = query.Skip(input.SkipCount).Take(input.MaxResultCount);

            // 如果 using Abp.Linq.Extensions;
            // 则使用下面的写法.
            query = query.PageBy(input);



            // 查询.
            var results = query.ToList();



            // ##### 数据类型转换.   数据库类 --->  Dto #####

            // 如果不 using Abp.AutoMapper;
            // 则使用下面的写法.
            // var dtoList = ObjectMapper.Map<List<OtherDto>>(results);

            // 如果使用 using Abp.AutoMapper;
            // 则使用下面的写法.
            var dtoList = results.MapTo<List<OtherDto>>();




            // 返回.
            return new PagedResultDto<OtherDto>(resultCount, dtoList);

        }

    }
}
