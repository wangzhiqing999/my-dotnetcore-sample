using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Diagnostics;

namespace A5500_AvaloniaApp.Controls;

public partial class UserControl1 : UserControl
{
    public UserControl1()
    {
        InitializeComponent();
    }

    private void OnTest2Click(object? sender, RoutedEventArgs e)
    {
        Debug.WriteLine("### 点击了 Test2 按钮！（事件代码写在 UserControl1 类当中 ）");
    }

}