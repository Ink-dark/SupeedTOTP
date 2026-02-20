using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using SupeedTOTP.UI.ViewModels;

namespace SupeedTOTP.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; set; }
        
        public MainWindow()
        {
            Console.WriteLine("MainWindow 构造函数开始");
            try
            {
                InitializeComponent();
                
                // 初始化 ViewModel
                ViewModel = new MainViewModel();
                DataContext = ViewModel;
                Console.WriteLine("ViewModel 初始化成功，DataContext 设置完成");
                
                // 添加窗口事件日志
                this.Loaded += (s, e) => Console.WriteLine("MainWindow 已加载");
                this.Closing += (s, e) => Console.WriteLine("MainWindow 正在关闭");
                this.Closed += (s, e) => Console.WriteLine("MainWindow 已关闭");
                
                // 显示窗口信息
                Console.WriteLine($"窗口标题: {this.Title}");
                Console.WriteLine($"窗口尺寸: {this.Width}x{this.Height}");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"MainWindow 初始化失败: {ex.GetType().Name}: {ex.Message}");
                Console.WriteLine($"堆栈跟踪: {ex.StackTrace}");
                MessageBox.Show($"窗口初始化失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
            
            Console.WriteLine("MainWindow 构造函数完成");
        }
        
        // 按钮点击事件处理
        private void AddAccount_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.AddAccount();
        }
        
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.OpenSettings();
        }
        
        // 列表项双击事件
        private void AccountsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ViewModel?.SelectedAccount != null)
            {
                ViewModel.EditAccount(ViewModel.SelectedAccount);
            }
        }
        
        // 搜索文本框事件
        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.TextBox textBox && textBox.Text == "搜索账号...")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }
        
        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.TextBox textBox && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "搜索账号...";
                textBox.Foreground = new SolidColorBrush(Color.FromRgb(0x99, 0x99, 0x99));
            }
        }
    }
}
