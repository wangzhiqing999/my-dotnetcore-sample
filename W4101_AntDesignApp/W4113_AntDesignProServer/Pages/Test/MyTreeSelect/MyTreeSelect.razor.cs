using W4113_AntDesignProServer.Models.Test;




namespace W4113_AntDesignProServer.Pages.Test
{
    public partial class MyTreeSelect
    {



		#region 直接把选项写在页面上的处理


		string _selectedDeptCode;



		/// <summary>
		/// 多选的数值.
		/// <br/>
		/// 注意：单选的，可以不设置初始的数值。
		/// 多选的，需要初始化 List， 如果是 null 的话，会报错。
		/// </summary>
		private IEnumerable<string> _selectedDeptCodes = new List<string>();



		/// <summary>
		/// 只返回子节点，不返回父节点.
		/// </summary>
		private IEnumerable<string> _selectedDeptCodes2 = new List<string>();

		
		/// <summary>
		/// 如果子节点全选了，那么只返回父节点，不返回子节点.
		/// </summary>
		private IEnumerable<string> _selectedDeptCodes2p = new List<string>();


		/// <summary>
		/// 返回全部选择的节点:父节点与子节点都返回
		/// </summary>
		private IEnumerable<string> _selectedDeptCodes2a = new List<string>();




		private IEnumerable<string> _selectedDeptCodes3 = new List<string>();


		#endregion





		#region 简单绑定的使用



		List<Area> _dataSource = Area.GetTestAreasTreeList();



		/// <summary>
		/// 单选：有初始值.
		/// </summary>
		string _selectedAreaCode = "72";



		private IEnumerable<string> _selectedAreaCodes = new List<string>();

		private IEnumerable<string> _selectedAreaCodes2 = new List<string>();



		private IEnumerable<string> _selectedAreaCodes2c = new List<string>();
		private IEnumerable<string> _selectedAreaCodes2p = new List<string>();
		private IEnumerable<string> _selectedAreaCodes2a = new List<string>();



		private IEnumerable<string> _selectedAreaCodes3 = new List<string>();

		#endregion




	}
}
