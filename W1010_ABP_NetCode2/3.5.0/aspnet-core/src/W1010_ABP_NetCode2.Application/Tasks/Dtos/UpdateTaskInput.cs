using System.ComponentModel.DataAnnotations;
using Abp.Runtime.Validation;

namespace W1010_ABP_NetCode2.Tasks.Dtos
{
    /// <summary>
    /// This DTO class is used to send needed data to <see cref="ITaskAppService.UpdateTask"/> method.
    /// 
    /// Implements <see cref="ICustomValidate"/> for additional custom validation.
    /// </summary>
    public class UpdateTaskInput : ICustomValidate
    {
        [Range(1, int.MaxValue)] //Data annotation attributes work as expected.
        public int TaskId { get; set; }


        public TaskState? State { get; set; }

        //Custom validation method. It's called by ABP after data annotation validations.
        public void AddValidationErrors(CustomValidationContext context)
        {
        }

        public override string ToString()
        {
            return string.Format("[UpdateTaskInput > TaskId = {0}, State = {1}]", TaskId, State);
        }
    }
}
