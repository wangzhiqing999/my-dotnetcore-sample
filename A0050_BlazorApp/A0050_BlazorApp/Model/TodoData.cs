namespace A0050_BlazorApp.Model
{
    
    public class TodoData
    {


        public int Id { get; set; }



        public string? Title { get; set; }


        public bool IsDone { get; set; } = false;



        public int Minutes { set; get; }



        /// <summary>
        /// 截止日期
        /// </summary>
        public DateTime ClosingDate { get; set; }


        public TodoStatusEnum Status { get; set; }

    }





    public enum TodoStatusEnum
    {
        Todo,                     // 待办
        InProgress,               // 正在进行
        Completed                 // 已完成
    }


}
