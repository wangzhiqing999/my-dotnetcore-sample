using Microsoft.EntityFrameworkCore;

using D0010_MyTodo.Model;

namespace D0010_MyTodo.DataAccess
{
    public class MyTodoContext : DbContext
    {

        public MyTodoContext() : base()
        {
        }

        public MyTodoContext(DbContextOptions<MyTodoContext> options) : base(options)
        {
        }


        /// <summary>
        /// 待办事项
        /// </summary>
        public DbSet<Todo> Todos { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            {
                if (optionsBuilder.IsConfigured == false)
                {
                    optionsBuilder.UseSqlite(@"Data Source=data/todo.db");
                }
                base.OnConfiguring(optionsBuilder);
            }
        }


    }
}
