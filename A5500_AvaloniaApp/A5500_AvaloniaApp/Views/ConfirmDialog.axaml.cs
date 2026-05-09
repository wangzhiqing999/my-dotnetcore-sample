using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace A5500_AvaloniaApp.Views;

public partial class ConfirmDialog : Window
{

    // 绑定用的 Message 属性
    public string? Message { 
        set
        {
            txtMessage.Text = value;
        }
        get
        {
            return txtMessage.Text;
        }
    } 


    public ConfirmDialog()
    {
        InitializeComponent();
    }


    private void OnOkClick(object? sender, RoutedEventArgs e)
    {
        Close(true);
    }

    private void OnCancelClick(object? sender, RoutedEventArgs e)
    {
        Close(false);
    }


}