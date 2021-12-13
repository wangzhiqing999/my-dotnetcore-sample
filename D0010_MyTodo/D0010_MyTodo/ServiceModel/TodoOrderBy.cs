namespace D0010_MyTodo.ServiceModel
{

    /// <summary>
    /// 待办事项排序方式.
    /// </summary>
    public enum TodoOrderBy
    {

        /// <summary>
        /// 啥排序也不指定.
        /// </summary>
        None = 0,


        /// <summary>
        /// 重要度 升序
        /// </summary>
        ByImportanceAsc = 1,


        /// <summary>
        /// 重要度 逆序
        /// </summary>
        ByImportanceDesc = 2,



        /// <summary>
        /// 截止日期 升序
        /// </summary>
        ByClosingDateAsc = 3,


        /// <summary>
        /// 截止日期 逆序
        /// </summary>
        ByClosingDateDesc = 4,

    }
}
