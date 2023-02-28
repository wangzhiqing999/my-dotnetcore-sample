using A0050_BlazorApp.Model;
using Microsoft.AspNetCore.Components;

namespace A0050_BlazorApp.Components
{
    public partial class TodoDisplay
    {

        [CascadingParameter] 
        TodoContainer Container { get; set; }


        [Parameter] 
        public TodoData TodoItemData { get; set; }



        /// <summary>
        /// 通过每次drag开始将Container的Payload赋值以跟踪被拖拽的待办项
        /// </summary>
        /// <param name="selectedJob"></param>
        private void HandleDragStart(TodoData selectedJob)
        {
            Container.Payload = selectedJob;
        }


    }
}
