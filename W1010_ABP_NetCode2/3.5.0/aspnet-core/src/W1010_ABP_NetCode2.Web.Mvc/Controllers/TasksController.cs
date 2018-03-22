using System.Threading.Tasks;
using W1010_ABP_NetCode2.Controllers;
using W1010_ABP_NetCode2.Tasks;
using W1010_ABP_NetCode2.Tasks.Dtos;
using Microsoft.AspNetCore.Mvc;

using W1010_ABP_NetCode2.Web.Models.Tasks;


namespace W1010_ABP_NetCode2.Web.Controllers
{
    public class TasksController : W1010_ABP_NetCode2ControllerBase
    {
        private readonly ITaskAppService _taskAppService;
        

        public TasksController(ITaskAppService taskAppService)
        {
            _taskAppService = taskAppService;
        }


        public async Task<ActionResult> Index(GetAllTasksInput input)
        {
            var output = await _taskAppService.GetAll(input);

            var model = new IndexViewModel(output.Items)
            {
                SelectedTaskState = input.State
            };
            return View(model);
        }


        public ActionResult Create()
        {
            return View();
        }
    }
}
