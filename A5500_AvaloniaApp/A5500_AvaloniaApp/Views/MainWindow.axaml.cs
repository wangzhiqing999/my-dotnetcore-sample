using A5500_AvaloniaApp.Models;
using A5500_AvaloniaApp.Service;
using A5500_AvaloniaApp.ViewModels;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace A5500_AvaloniaApp.Views
{
    public partial class MainWindow : Window
    {
        private readonly SettingsService _settings;

        public MainWindow()
        {
            InitializeComponent();
            _settings = new SettingsService();
            RestoreWindowState();
        }

        private void RestoreWindowState()
        {
            var s = _settings.Load();
            if (s.WindowWidth > 0 && s.WindowHeight > 0)
            {
                Width = s.WindowWidth;
                Height = s.WindowHeight;
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            // Properties from XAML are set
            // Visual tree may not be available yet
        }

        protected override void OnClosing(WindowClosingEventArgs e)
        {
            var s = _settings.Load();
            s.WindowWidth = Width;
            s.WindowHeight = Height;
            _settings.Save(s);
            base.OnClosing(e);
        }

        protected override void OnLoaded(RoutedEventArgs e)
        {
            base.OnLoaded(e);
            // Control is fully ready
            // Layout has occurred, bindings are active


            MainWindowViewModel? dataModel = this.DataContext as MainWindowViewModel;


            if (dataModel != null)
            {
                // 读取配置文件，获取下拉列表中的配置数据.
                string? channels = Program.Configuration["AppConfig:Channels"];
                if (channels != null)
                {
                    string[] channelArray = channels.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    foreach (string channel in channelArray)
                    {
                        dataModel.Channels.Add(channel);
                    }
                }


                // 读取配置
                var section = Program.Configuration.GetSection("AppConfig:Servers");
                // 直接绑定为 Dictionary
                Dictionary<string, string> servicesDict = new Dictionary<string, string>();
                section.Bind(servicesDict);

                dataModel.ServicesDict = servicesDict;

                foreach (string server in servicesDict.Keys)
                {
                    dataModel.Servers.Add(server);
                }

                    

            }
        }







        private async void ButtonOpenDialogs_OnClick(object? sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"Open Dialogs Click!");

            var dialog = new ConfirmDialog();
            dialog.Message = "确定要完成操作么？";

            // ShowDialog returns a result when the dialog closes
            bool? result = await dialog.ShowDialog<bool?>(this);

            if (result == true)
            {
                Debug.WriteLine($"确定要完成操作么的对话框上点了 Yes.");
            }

        }



        private int testWindowsIndex = 1;

        private void ButtonOpenWindow_OnClick(object? sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"Open Window Click!");

            /*
            var window = new SecondaryWindow
            {
                DataContext = new SecondaryWindowViewModel() { Message = $"Hello {testWindowsIndex ++ }!" }
            };
            window.Show();
            */


            var postmanWindow = new PostmanWindow
            {
                DataContext = new PostmanWindowViewModel()
            };
            postmanWindow.Show();
        }


    }
}