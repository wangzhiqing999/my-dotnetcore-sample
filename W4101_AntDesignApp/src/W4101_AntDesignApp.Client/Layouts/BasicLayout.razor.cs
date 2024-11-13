using AntDesign.Extensions.Localization;
using AntDesign.ProLayout;
using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Net.Http.Json;

namespace W4101_AntDesignApp.Layouts
{
    public partial class BasicLayout : LayoutComponentBase, IDisposable
    {
        private MenuDataItem[] _menuData;

        [Inject] private ReuseTabsService TabService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _menuData = new[] {
                new MenuDataItem
                {
                    Path = "/",
                    Name = "welcome",
                    Key = "welcome",
                    Icon = "smile",
                },


                new MenuDataItem
                {
                    Path = "/helloworld",
                    Name = "helloworld",
                    Key = "helloworld",
                    Icon = "cloud",
                },

                new MenuDataItem
                {
                    Path = "/counter",
                    Name = "counter",
                    Key = "counter",
                    Icon = "tool",
                },


                new MenuDataItem
                {
                    Path = "",
                    Name = "测试主/子菜单",
                    Key = "test_main_sub",
                    Icon = "file",
                    Children = new[]
                    {
                        new MenuDataItem
                        {
                            Path = "/testpage1",
                            Name = "测试子菜单1-全小写",
                            Key = "testpage1",
                            Icon = "file-excel",
                        },
                        new MenuDataItem
                        {
                            Path = "/TESTPAGE2",
                            Name = "测试子菜单2-全大写",
                            Key = "TESTPAGE2",
                            Icon = "file-gif",
                        },
                        new MenuDataItem
                        {
                            Path = "/TestPage3",
                            Name = "测试子菜单3-大小写",
                            Key = "TestPage3",
                            Icon = "file-jpg",
                        },
                    }
                },



                new MenuDataItem
                {
                    Path = "",
                    Name = "测试输入",
                    Key = "test_input",
                    Icon = "edit",
                    Children = new[]
                    {
                        new MenuDataItem
                        {
                            Path = "/myautocomplete",
                            Name = "AutoComplete",
                            Key = "myautocomplete",
                            Icon = "file-excel",
                        },

                        new MenuDataItem
                        {
                            Path = "/mycascader",
                            Name = "Cascader",
                            Key = "mycascader",
                            Icon = "file-excel",
                        },

                        new MenuDataItem
                        {
                            Path = "/mycheckbox",
                            Name = "Checkbox",
                            Key = "mycheckbox",
                            Icon = "file-excel",
                        },

                        new MenuDataItem
                        {
                            Path = "/mydatepicker",
                            Name = "DatePicker",
                            Key = "mydatepicker",
                            Icon = "file-excel",
                        },

                        new MenuDataItem
                        {
                            Path = "/myinput",
                            Name = "Input",
                            Key = "myinput",
                            Icon = "file-excel",
                        },

                        new MenuDataItem
                        {
                            Path = "/myinputnumber",
                            Name = "InputNumbe",
                            Key = "myinputnumber",
                            Icon = "file-excel",
                        },

                        new MenuDataItem
                        {
                            Path = "/myradio",
                            Name = "Radio",
                            Key = "myradio",
                            Icon = "file-excel",
                        },

                        new MenuDataItem
                        {
                            Path = "/myrate",
                            Name = "Rate",
                            Key = "myrate",
                            Icon = "file-excel",
                        },

                        new MenuDataItem
                        {
                            Path = "/myselect",
                            Name = "Select-字符串值",
                            Key = "myselect",
                            Icon = "file-excel",
                        },

                        new MenuDataItem
                        {
                            Path = "/myselect2",
                            Name = "Select-数字值",
                            Key = "myselect2",
                            Icon = "file-excel",
                        },

                        new MenuDataItem
                        {
                            Path = "/myswitch",
                            Name = "Switch",
                            Key = "myswitch",
                            Icon = "file-excel",
                        },

                        new MenuDataItem
                        {
                            Path = "/mytimepicker",
                            Name = "TimePicker",
                            Key = "mytimepicker",
                            Icon = "file-excel",
                        },
                        
                        new MenuDataItem
                        {
                            Path = "/mytreeselect",
                            Name = "TreeSelect",
                            Key = "mytreeselect",
                            Icon = "file-excel",
                        },
                    }
                },


				new MenuDataItem
				{
					Path = "",
					Name = "测试展示",
					Key = "test_display",
					Icon = "file",
					Children = new[]
					{
						new MenuDataItem
						{
							Path = "/mytable01",
							Name = "Table-自动生成",
							Key = "mytable01",
							Icon = "file-excel",
						},
						new MenuDataItem
						{
							Path = "/mytable02",
							Name = "Table-手动指定",
							Key = "mytable02",
							Icon = "file-gif",
						},
						new MenuDataItem
						{
							Path = "/mytable03",
							Name = "Table-固定表头",
							Key = "mytable03",
							Icon = "file-jpg",
						},
                        new MenuDataItem
                        {
                            Path = "/mytable04",
                            Name = "Table-固定列",
                            Key = "mytable04",
                            Icon = "file-jpg",
                        },
                        new MenuDataItem
                        {
                            Path = "/mytable05",
                            Name = "Table-固定头和列",
                            Key = "mytable05",
                            Icon = "file-jpg",
                        },
                        new MenuDataItem
                        {
                            Path = "/mytable06",
                            Name = "Table-修改每行样式",
                            Key = "mytable06",
                            Icon = "file-jpg",
                        },
                        new MenuDataItem
                        {
                            Path = "/mytable07",
                            Name = "Table-自动省略",
                            Key = "mytable07",
                            Icon = "file-jpg",
                        },
                        new MenuDataItem
                        {
                            Path = "/mytable08",
                            Name = "Table-可伸缩",
                            Key = "mytable08",
                            Icon = "file-jpg",
                        },
                        new MenuDataItem
                        {
                            Path = "/mytable10",
                            Name = "Table-嵌套",
                            Key = "mytable10",
                            Icon = "file-jpg",
                        },
                    }
				},

			};
        }

        void Reload()
        {
            TabService.ReloadPage();
        }

        public void Dispose()
        {
            
        }

    }
}
