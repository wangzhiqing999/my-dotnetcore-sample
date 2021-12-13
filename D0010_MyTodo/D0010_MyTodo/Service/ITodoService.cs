using D0010_MyTodo.Model;
using D0010_MyTodo.ServiceModel;

namespace D0010_MyTodo.Service
{
    public interface ITodoService
    {


        /// <summary>
        /// 获取待办事项列表.
        /// </summary>
        /// <param name="todoOrderBy"></param>
        /// <param name="showIsDone"></param>
        /// <returns></returns>
        List<Todo> GetTodoList(TodoOrderBy todoOrderBy, bool showIsDone = false);


        /// <summary>
        /// 新增待办事项.
        /// </summary>
        /// <param name="todo"></param>
        ServiceResult NewTodo(Todo todo);


        /// <summary>
        /// 更新待办事项.
        /// </summary>
        /// <param name="todo"></param>
        ServiceResult UpdateTodo(Todo todo);


        /// <summary>
        /// 删除待办事项.
        /// </summary>
        /// <param name="todoID"></param>
        ServiceResult DeleteTodo(int todoID);
    }
}
