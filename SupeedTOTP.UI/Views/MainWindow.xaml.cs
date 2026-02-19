using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SupeedTOTP.UI.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        // Avalonia XAML编译器会自动生成InitializeComponent方法
        AvaloniaXamlLoader.Load(this);
    }
}
