using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;

namespace SupeedTOTP.UI.Views;

// 不再使用partial类，改为手动加载XAML
public class MainWindow : Window
{
    public MainWindow()
    {
        Console.WriteLine("MainWindow 构造函数开始");
        try
        {
            // 手动加载XAML，不依赖自动生成的InitializeComponent方法
            Console.WriteLine("开始加载MainWindow.xaml");
            AvaloniaXamlLoader.Load(this);
            Console.WriteLine("MainWindow.xaml 加载成功");
            
            // 添加窗口事件日志
            this.Opened += (s, e) => {
                Console.WriteLine("MainWindow 已打开");
                Console.WriteLine($"窗口实际尺寸: {this.ClientSize.Width}x{this.ClientSize.Height}");
            };
            this.Closing += (s, e) => Console.WriteLine("MainWindow 正在关闭");
            this.Closed += (s, e) => Console.WriteLine("MainWindow 已关闭");
            
            // 显示窗口信息
            Console.WriteLine($"窗口标题: {this.Title}");
            Console.WriteLine($"窗口初始尺寸: {this.Width}x{this.Height}");
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"MainWindow 初始化失败: {ex.GetType().Name}: {ex.Message}");
            Console.WriteLine($"堆栈跟踪: {ex.StackTrace}");
            throw;
        }
        Console.WriteLine("MainWindow 构造函数完成");
    }
    
    // 处理关闭按钮点击事件
    public void OnCloseButtonClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Console.WriteLine("关闭按钮被点击");
        this.Close();
    }
}