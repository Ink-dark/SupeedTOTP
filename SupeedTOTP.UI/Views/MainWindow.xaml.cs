using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SupeedTOTP.UI.Views;

// 移除partial修饰符，因为我们不再使用XAML代码生成
public class MainWindow : Window
{
    public MainWindow()
    {
        // 直接初始化，不使用数据绑定
        AvaloniaXamlLoader.Load(this);
    }
}
