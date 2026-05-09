using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace A5500_AvaloniaApp.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public string Greeting { get; } = "Welcome to Avalonia!";




        /// <summary>
        /// Tab 当前选择的
        /// </summary>
        [ObservableProperty]
        public int _tabSelectedIndex;



        /// <summary>
        /// 字符串列表，配置在配置文件中，用于测试画面上的  ComboBox，ListBox
        /// </summary>         
        public ObservableCollection<string> Channels { get; set; } = new ObservableCollection<string>();

        [ObservableProperty]
        private string? _selectedChannel;








        public Dictionary<string, string> ServicesDict { set; get; } = new Dictionary<string, string>();


        public ObservableCollection<string> Servers { get; set; } = new ObservableCollection<string>();



        private string ? _selectedServer;


        public string? SelectedServer
        {
            set
            {
                if (_selectedServer != value)
                {
                    _selectedServer = value;
                    OnPropertyChanged();
                }


                if (_selectedServer == null)
                {
                    SelectedServerHost = "";
                    return;
                }
                if (!ServicesDict.ContainsKey(_selectedServer))
                {
                    SelectedServerHost = "";
                    return;
                }

                SelectedServerHost = ServicesDict[_selectedServer];
            }
            get 
            {
                return _selectedServer;
            }
        }
        


        [ObservableProperty]
        private string? _selectedServerHost;





        /// <summary>
        /// 用于测试画面上的 TextBox
        /// </summary>
        [ObservableProperty]
        private string _username = "";



        
        private string _userPwd = "";

        public string UserPwd
        {
            set
            {
                // 只有值真正变化时才触发通知
                if (_userPwd != value)
                {
                    _userPwd = value;
                    // 触发属性变更通知（UI 自动刷新）
                    OnPropertyChanged();
                }
            }
            get
            {
                return _userPwd;
            }
        }







        /// <summary>
        /// 自定义控件的数据.
        /// </summary>
        public UserControl1ViewModel UserControl1Data { get; } = new UserControl1ViewModel();


    }
}
