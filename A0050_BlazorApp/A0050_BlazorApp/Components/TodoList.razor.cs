using A0050_BlazorApp.Model;
using Microsoft.AspNetCore.Components;

namespace A0050_BlazorApp.Components
{
    public partial class TodoList
    {

        [CascadingParameter] 
        TodoContainer Container { get; set; }



        [Parameter] 
        public TodoStatusEnum ListStatus { get; set; }



        [Parameter] 
        public TodoStatusEnum AllowedStatuses { get; set; }



        List<TodoData> Todos = new List<TodoData>();
        private string _dropClass = "";  // 用来判断当前list能否放置




        protected override void OnParametersSet()
        {
            Todos.Clear();
            Todos.AddRange(Container.Todos.Where(x => x.Status == ListStatus));
        }

        private void HandleDragEnter()
        {
            if (ListStatus == Container.Payload.Status) return;

            if (AllowedStatuses != Container.Payload.Status)
            {
                _dropClass = "can-drop";
            }
        }

        private void HandleDragLeave()
        {
            _dropClass = "";
        }

        private async Task HandleDrop()
        {
            _dropClass = "";

            if (AllowedStatuses == Container.Payload.Status) return;

            await Container.UpdateJobAsync(ListStatus);
        }


    }
}
