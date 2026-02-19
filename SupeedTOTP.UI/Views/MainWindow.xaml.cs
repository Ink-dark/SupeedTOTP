using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SupeedTOTP.UI.Views;

// 不再使用partial类，改为手动加载XAML
public class MainWindow : Window
{
    public MainWindow()
    {
        // 手动加载XAML，不依赖自动生成的InitializeComponent方法
        AvaloniaXamlLoader.Load(this);
    }
}
