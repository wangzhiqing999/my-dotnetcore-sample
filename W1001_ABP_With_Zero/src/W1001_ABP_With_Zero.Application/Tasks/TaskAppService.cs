using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using W1001_ABP_With_Zero.Tasks.Dtos;
using Microsoft.EntityFrameworkCore;
using Abp.Authorization;
using W1001_ABP_With_Zero.Authorization;

namespace W1001_ABP_With_Zero.Tasks
{


    /// <summary>
    /// 任务服务.
    /// </summary>
    [AbpAuthorize(PermissionNames.Pages_Tasks)]
    public class TaskAppService : W1001_ABP_With_ZeroAppServiceBase, ITaskAppService
    {
        private readonly IRepository<Task> _taskRepository;

        public TaskAppService(IRepository<Task> taskRepository)
        {
            _taskRepository = taskRepository;
        }


        /// <summary>
        /// 获取所有任务.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ListResultDto<TaskListDto>> GetAll(GetAllTasksInput input)
        {
            var tasks = await _taskRepository
                .GetAll()
                .WhereIf(input.State.HasValue, t => t.State == input.State.Value)
                .OrderByDescending(t => t.CreationTime)
                .ToListAsync();

            return new ListResultDto<TaskListDto>(
                ObjectMapper.Map<List<TaskListDto>>(tasks)
            );
        }


        /// <summary>
        /// 创建任务.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task Create(CreateTaskInput input)
        {
            var task = ObjectMapper.Map<Task>(input);
            await _taskRepository.InsertAsync(task);
        }






        /// <summary>
        /// 更新任务.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public void UpdateTask(UpdateTaskInput input)
        {
            //Retrieving a task entity with given id using standard Get method of repositories.
            var task = _taskRepository.Get(input.TaskId);

            //Updating changed properties of the retrieved task entity.

            if (input.State.HasValue)
            {
                task.State = input.State.Value;
            }
        }
    }
}
