using D0010_MyTodo.Model;
using D0010_MyTodo.Service;
using D0010_MyTodo.ServiceModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace D0010_MyTodo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {

        private ITodoService _TodoService;


        public TodoController(ITodoService todoService)
        {
            _TodoService = todoService;
        }



        [HttpGet]
        [Route("GetTodoList")]
        public List<Todo> GetTodoList(TodoOrderBy todoOrderBy, bool showIsDone = false)
        {
            List<Todo> resultList = _TodoService.GetTodoList(todoOrderBy, showIsDone);
            return resultList;
        }




        [HttpPost]
        [Route("UpdateTodo")]
        public ServiceResult UpdateTodo(Todo todo)
        {
            return _TodoService.UpdateTodo(todo);
        }


        [HttpPost]
        [Route("NewTodo")]
        public ServiceResult NewTodo(Todo todo)
        {
            return _TodoService.NewTodo(todo);
        }


        [HttpPost]
        [Route("DeleteTodo")]
        public ServiceResult DeleteTodo(int todoID)
        {
            return _TodoService.DeleteTodo(todoID);
        }


    }
}
