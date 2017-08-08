using System.Threading.Tasks;
using W1001_ABP_With_Zero.Controllers;
using W1001_ABP_With_Zero.Tasks;
using W1001_ABP_With_Zero.Tasks.Dtos;
using Microsoft.AspNetCore.Mvc;

using W1001_ABP_With_Zero.Web.Models.Tasks;


namespace W1001_ABP_With_Zero.Web.Controllers
{
    public class TasksController : W1001_ABP_With_ZeroControllerBase
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
