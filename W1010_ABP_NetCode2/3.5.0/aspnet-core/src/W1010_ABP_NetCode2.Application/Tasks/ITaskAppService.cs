using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using W1010_ABP_NetCode2.Tasks.Dtos;

namespace W1010_ABP_NetCode2.Tasks
{


    /// <summary>
    /// 任务接口.
    /// </summary>
    public interface ITaskAppService : IApplicationService
    {

        /// <summary>
        /// 获取所有的任务.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultDto<TaskListDto>> GetAll(GetAllTasksInput input);


        /// <summary>
        /// 创建任务.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        System.Threading.Tasks.Task Create(CreateTaskInput input);


        /// <summary>
        /// 更新任务.
        /// </summary>
        /// <param name="input"></param>
        void UpdateTask(UpdateTaskInput input);

    }
}