using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SupeedTOTP.UI.Views;

namespace SupeedTOTP.UI;

// 确保App是partial类，Avalonia UI 11.0需要partial类来生成InitializeComponent方法
public partial class App : Application
{
    public App()
    {
        // 构造函数中不需要手动调用Load，Avalonia会自动处理
    }
    
    public override void Initialize()
    {
        // Avalonia UI 11.0会自动生成InitializeComponent方法
        InitializeComponent();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
}
