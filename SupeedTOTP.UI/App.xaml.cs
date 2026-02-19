using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SupeedTOTP.UI.Views;

namespace SupeedTOTP.UI;

// 不再使用partial类，改为手动加载XAML
public class App : Application
{
    public override void Initialize()
    {
        // 手动加载XAML，不依赖自动生成的InitializeComponent方法
        AvaloniaXamlLoader.Load(this);
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
