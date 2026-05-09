using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace A5500_AvaloniaApp.ViewModels
{

    public partial class PostmanWindowViewModel : ViewModelBase
    {

        /// <summary>
        /// 方法下拉列表数据.
        /// </summary>
        public ObservableCollection<string> Methods { get; } = new ObservableCollection<string>() { "GET", "POST"};

        /// <summary>
        /// 选择的方法
        /// </summary>
        [ObservableProperty]
        private string _selectedMethod = "GET";



        /// <summary>
        /// 请求的地址.
        /// </summary>
        [ObservableProperty]
        private string _urlAddress = "";



        /// <summary>
        /// 请求的数据.
        /// </summary>
        [ObservableProperty]
        private string _body = "";



        /// <summary>
        /// 发送请求处理.
        /// </summary>
        [RelayCommand]
        private async void Send()
        {
            Debug.WriteLine("### 点击了 Send 按钮！");

            if (string.IsNullOrEmpty(UrlAddress))
            {
                ResultMessage = "url 地址不能为空！";
                return;
            }

            using (HttpClient client = new HttpClient())
            {
                if(SelectedMethod == "POST")
                {
                    try
                    {
                        var content = new StringContent(Body, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(UrlAddress, content);
                        response.EnsureSuccessStatusCode();

                        string responseJson = await response.Content.ReadAsStringAsync();
                        string formattedJson = JsonSerializer.Serialize(
                            JsonDocument.Parse(responseJson).RootElement,
                            new JsonSerializerOptions { WriteIndented = true }
                        );

                        ResultMessage = "success";
                        ResultText = formattedJson;
                    }
                    catch (Exception ex)
                    {
                        ResultMessage = $"请求失败：{ex.Message}";
                    }
                } 
                else
                {
                    try
                    {
                        HttpResponseMessage response = await client.GetAsync(UrlAddress);
                        response.EnsureSuccessStatusCode();

                        string responseJson = await response.Content.ReadAsStringAsync();
                        string formattedJson = JsonSerializer.Serialize(
                            JsonDocument.Parse(responseJson).RootElement,
                            new JsonSerializerOptions { WriteIndented = true }
                        );

                        ResultMessage = "success";
                        ResultText = formattedJson;
                    }
                    catch (Exception ex)
                    {
                        ResultMessage = $"请求失败：{ex.Message}";
                    }
                }
            }


        }


        /// <summary>
        /// 结果消息.
        /// </summary>
        [ObservableProperty]
        private string _resultMessage = "";



        /// <summary>
        /// 响应的数据.
        /// </summary>
        [ObservableProperty]
        private string _resultText = "";

    }
}
