using Avalonia;
using System;
using System.IO;

namespace SupeedTOTP.UI;

class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("=== SupeedTOTP 启动开始 ===");
            Console.WriteLine($"启动时间: {DateTime.Now}");
            Console.WriteLine($"命令行参数: {(args.Length > 0 ? string.Join(" ", args) : "无")}");
            
            var app = BuildAvaloniaApp();
            Console.WriteLine("Avalonia应用构建完成");
            
            app.StartWithClassicDesktopLifetime(args);
            Console.WriteLine("Avalonia应用正常退出");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"=== 应用程序启动时发生致命异常 ===");
            Console.WriteLine($"异常类型: {ex.GetType().FullName}");
            Console.WriteLine($"异常消息: {ex.Message}");
            Console.WriteLine($"堆栈跟踪: {ex.StackTrace}");
            
            // 尝试将错误日志写入文件
            try
            {
                var logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "SupeedTOTP_crash.log");
                File.WriteAllText(logPath, $"[{DateTime.Now}] 应用程序崩溃\n{ex}");
                Console.WriteLine($"错误日志已保存到: {logPath}");
            }
            catch (Exception logEx)
            {
                Console.WriteLine($"保存错误日志失败: {logEx.Message}");
            }
            
            // 等待用户按任意键退出，方便查看错误信息
            Console.WriteLine("按任意键退出...");
            Console.ReadKey();
        }
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
    {
        Console.WriteLine("开始构建Avalonia应用");
        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .AfterSetup(_ => Console.WriteLine("Avalonia应用设置完成"));
    }
}