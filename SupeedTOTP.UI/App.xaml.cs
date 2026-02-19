using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SupeedTOTP.UI.Views;
using System;

namespace SupeedTOTP.UI;

// 不再使用partial类，改为手动加载XAML
public class App : Application
{
    public override void Initialize()
    {
        Console.WriteLine("App.Initialize() 开始");
        try
        {
            // 手动加载XAML，不依赖自动生成的InitializeComponent方法
            Console.WriteLine("开始加载App.xaml");
            AvaloniaXamlLoader.Load(this);
            Console.WriteLine("App.xaml 加载成功");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"App.xaml 加载失败: {ex.GetType().Name}: {ex.Message}");
            Console.WriteLine($"堆栈跟踪: {ex.StackTrace}");
            throw;
        }
        Console.WriteLine("App.Initialize() 完成");
    }

    public override void OnFrameworkInitializationCompleted()
    {
        Console.WriteLine("App.OnFrameworkInitializationCompleted() 开始");
        try
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                Console.WriteLine("检测到桌面应用程序生命周期");
                Console.WriteLine("正在创建MainWindow...");
                desktop.MainWindow = new MainWindow();
                Console.WriteLine("MainWindow 创建成功");
                desktop.MainWindow.Closed += (s, e) => Console.WriteLine("MainWindow 已关闭");
            }
            else
            {
                Console.WriteLine($"应用程序生命周期类型: {ApplicationLifetime?.GetType().Name ?? "null"}");
            }

            base.OnFrameworkInitializationCompleted();
            Console.WriteLine("基类OnFrameworkInitializationCompleted调用完成");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"OnFrameworkInitializationCompleted 失败: {ex.GetType().Name}: {ex.Message}");
            Console.WriteLine($"堆栈跟踪: {ex.StackTrace}");
            throw;
        }
        Console.WriteLine("App.OnFrameworkInitializationCompleted() 完成");
    }
}