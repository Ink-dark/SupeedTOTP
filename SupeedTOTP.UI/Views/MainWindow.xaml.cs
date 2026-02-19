using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;

namespace SupeedTOTP.UI.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        try
        {
            // Avalonia XAML编译器会自动生成InitializeComponent方法
            AvaloniaXamlLoader.Load(this);
            // 确保窗口显示
            this.Show();
        }
        catch (Exception ex)
        {
            Console.WriteLine("MainWindow initialization error: " + ex.Message);
            Console.WriteLine(ex.StackTrace);
            throw;
        }
    }
}
