using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SupeedTOTP.UI.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
