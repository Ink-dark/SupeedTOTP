using Avalonia.Controls;

namespace SupeedTOTP.UI.Views;

// MainWindow必须是partial类，Avalonia UI 11.0需要它来生成InitializeComponent方法
public partial class MainWindow : Window
{
    public MainWindow()
    {
        // 使用自动生成的InitializeComponent方法
        InitializeComponent();
    }
}
