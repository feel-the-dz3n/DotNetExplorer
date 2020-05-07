using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace DotNetExplorer
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
#if DEBUG
                desktop.MainWindow = new AssemblyWindow(TestAssembly.Fetch.Get());
#else
                desktop.MainWindow = new WelcomeWindow();
#endif
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
