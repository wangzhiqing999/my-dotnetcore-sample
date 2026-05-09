using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A5500_AvaloniaApp.ViewModels
{
    public partial class UserControl1ViewModel : ViewModelBase
    {

        /// <summary>
        /// 用于测试画面上的 TextBox
        /// </summary>
        [ObservableProperty]
        private string _testValue = "";


        [RelayCommand]
        private void Test1()
        {
            Debug.WriteLine("### 点击了 Test1 按钮！（事件代码写在 UserControl1ViewModel 类当中 ）");
        }


    }
}
