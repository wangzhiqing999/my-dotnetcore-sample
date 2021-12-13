using D0010_MyTodo.DataAccess;
using D0010_MyTodo.Model;
using D0010_MyTodo.Service;
using D0010_MyTodo.ServiceModel;



namespace D0010_MyTodo.ServiceImpl
{
    public class DefaultTodoService : ITodoService
    {

        
        private MyTodoContext _MyTodoContext;


        public DefaultTodoService(MyTodoContext myTodoContext)
        {
            _MyTodoContext = myTodoContext;
        }



        List<Todo> ITodoService.GetTodoList(TodoOrderBy todoOrderBy, bool showIsDone)
        {

            var query =
                from data in _MyTodoContext.Todos
                select
                    data;

            if (!showIsDone)
            {
                // 不显示已完成的.
                query = query.Where(data => data.IsDone == false);
            }

            switch (todoOrderBy)
            {
                case TodoOrderBy.ByClosingDateAsc:
                    query = query.OrderBy(p => p.ClosingDate);
                    break;

                case TodoOrderBy.ByClosingDateDesc:
                    query = query.OrderByDescending(p => p.ClosingDate);
                    break;

                case TodoOrderBy.ByImportanceAsc:
                    query = query.OrderBy(p => p.Importance);
                    break;

                case TodoOrderBy.ByImportanceDesc:
                    query = query.OrderByDescending(p => p.Importance);
                    break;

                default:
                    break;
            }


            List<Todo> resultList = query.ToList();

            return resultList;
        }


        ServiceResult ITodoService.NewTodo(Todo todo)
        {
            try
            {
                this._MyTodoContext.Todos.Add(todo);
                this._MyTodoContext.SaveChanges();

                return ServiceResult.DefaultSuccessResult;
            } 
            catch (Exception ex)
            {
                return new ServiceResult(ex);
            }
        }



        ServiceResult ITodoService.UpdateTodo(Todo todo)
        {
            try
            {
                Todo dbTodo = _MyTodoContext.Todos.Find(todo.ID);

                if (dbTodo == null)
                {
                    return ServiceResult.DataNotFoundResult;
                }


                dbTodo.Title = todo.Title;
                dbTodo.Detail = todo.Detail;
                dbTodo.ClosingDate = todo.ClosingDate;
                dbTodo.Importance = todo.Importance;
                dbTodo.IsDone = todo.IsDone;

                this._MyTodoContext.SaveChanges();

                return ServiceResult.DefaultSuccessResult;
            }
            catch (Exception ex)
            {
                return new ServiceResult(ex);
            }
        }



        ServiceResult ITodoService.DeleteTodo(int todoID)
        {
            try
            {
                Todo todo = _MyTodoContext.Todos.Find(todoID);

                if(todo == null)
                {
                    return ServiceResult.DataNotFoundResult;
                }


                _MyTodoContext.Todos.Remove(todo);


                this._MyTodoContext.SaveChanges();

                return ServiceResult.DefaultSuccessResult;
            }
            catch (Exception ex)
            {
                return new ServiceResult(ex);
            }
        }


    }
}
