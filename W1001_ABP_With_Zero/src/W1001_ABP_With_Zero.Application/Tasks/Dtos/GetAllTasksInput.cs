namespace W1001_ABP_With_Zero.Tasks.Dtos
{

    /// <summary>
    /// 获取全部任务的输入.
    /// </summary>
    public class GetAllTasksInput
    {
        public TaskState? State { get; set; }
    }
}