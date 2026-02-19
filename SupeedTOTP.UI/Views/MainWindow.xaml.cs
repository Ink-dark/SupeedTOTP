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
            this.Opened += (s, e) => Console.WriteLine("MainWindow 已打开");
            this.Closing += (s, e) => Console.WriteLine("MainWindow 正在关闭");
            
            // 显示窗口信息
            Console.WriteLine($"窗口标题: {this.Title}");
            Console.WriteLine($"窗口尺寸: {this.Width}x{this.Height}");
            
            // 确保窗口可见
            this.Show();
            Console.WriteLine("MainWindow.Show() 已调用");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"MainWindow 初始化失败: {ex.GetType().Name}: {ex.Message}");
            Console.WriteLine($"堆栈跟踪: {ex.StackTrace}");
            throw;
        }
        Console.WriteLine("MainWindow 构造函数完成");
    }
}