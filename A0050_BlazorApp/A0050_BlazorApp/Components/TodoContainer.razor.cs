using A0050_BlazorApp.Model;
using Microsoft.AspNetCore.Components;

namespace A0050_BlazorApp.Components
{
    public partial class TodoContainer
    {


        [Parameter] 
        public List<TodoData> Todos { get; set; }


        [Parameter] 
        public RenderFragment ChildContent { get; set; }



        public TodoData Payload { get; set; }  // 这个属性用来跟踪被用户拖拽的待办项



        /// <summary>
        /// 当子组件更新状态时，调用父组件的该方法，改变待办集合中被拖拽项的状态
        /// </summary>
        /// <param name="newStatus"></param>
        public async Task UpdateJobAsync(TodoStatusEnum newStatus)
        {
            var task = Todos.SingleOrDefault(x => x.Id == Payload.Id);

            if (task != null)
            {
                task.Status = newStatus;
                task.ClosingDate = DateTime.Now;
                await InvokeAsync(StateHasChanged);
            }
        }



    }
}
