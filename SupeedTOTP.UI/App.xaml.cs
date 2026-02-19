using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SupeedTOTP.UI.Views;
using System;

namespace SupeedTOTP.UI;

public partial class App : Application
{
    public override void Initialize()
    {
        try
        {
            Console.WriteLine("Initializing App...");
            AvaloniaXamlLoader.Load(this);
            Console.WriteLine("App initialized successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine("App initialization error: " + ex.Message);
            Console.WriteLine(ex.StackTrace);
            throw;
        }
    }

    public override void OnFrameworkInitializationCompleted()
    {
        try
        {
            Console.WriteLine("OnFrameworkInitializationCompleted called");
            
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                Console.WriteLine("Creating MainWindow...");
                desktop.MainWindow = new MainWindow();
                Console.WriteLine("MainWindow created, showing...");
                desktop.MainWindow.Show();
                Console.WriteLine("MainWindow shown");
            }
            else
            {
                Console.WriteLine("ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime: " + ApplicationLifetime?.GetType().Name);
            }

            base.OnFrameworkInitializationCompleted();
            Console.WriteLine("OnFrameworkInitializationCompleted completed");
        }
        catch (Exception ex)
        {
            Console.WriteLine("OnFrameworkInitializationCompleted error: " + ex.Message);
            Console.WriteLine(ex.StackTrace);
            throw;
        }
    }
}
