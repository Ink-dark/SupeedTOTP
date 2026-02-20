using System;
using System.Windows;
using SupeedTOTP.UI.Views;

namespace SupeedTOTP.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Console.WriteLine("=== SupeedTOTP Application Starting ===");
            Console.WriteLine("App constructor called");
            
            // 添加应用程序事件处理
            this.Startup += App_Startup;
            this.Activated += (s, e) => Console.WriteLine("App Activated event fired");
            this.Deactivated += (s, e) => Console.WriteLine("App Deactivated event fired");
            this.Exit += (s, e) => Console.WriteLine("App Exit event fired");
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }
        
        private void App_Startup(object sender, StartupEventArgs e)
        {
            Console.WriteLine("App Startup event fired");
            try
            {
                Console.WriteLine("App starting up...");
                // 使用 App.xaml 中的 StartupUri 自动创建 MainWindow
                Console.WriteLine("Using StartupUri to create MainWindow...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Startup error: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                this.Shutdown();
            }
        }
        
        private bool _isHandlingException = false;
        
        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            // 防止递归调用
            if (_isHandlingException)
            {
                Console.WriteLine("=== Recursive exception detected, skipping handling ===");
                e.Handled = true;
                return;
            }
            
            _isHandlingException = true;
            
            try
            {
                Console.WriteLine($"=== Unhandled Exception ===");
                Console.WriteLine($"Exception Type: {e.Exception.GetType().FullName}");
                Console.WriteLine($"Exception Message: {e.Exception.Message}");
                Console.WriteLine($"Stack Trace: {e.Exception.StackTrace}");
                
                // 简化错误处理，避免使用 MessageBox
                Console.WriteLine("=== Application Will Shutdown Due to Unhandled Exception ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in exception handler: {ex.Message}");
            }
            finally
            {
                _isHandlingException = false;
            }
            
            e.Handled = true;
            this.Shutdown();
        }
        
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Console.WriteLine("=== SupeedTOTP Application Exited ===");
        }
    }
}
