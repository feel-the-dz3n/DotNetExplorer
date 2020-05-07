using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace DotNetExplorer
{
    public class WelcomeWindow : Window
    {
        public WelcomeWindow()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            var version = Assembly.GetExecutingAssembly().GetName().Version;

            if (version != null)
                this.FindControl<TextBlock>("TbVersion").Text = "v" + version;
        }

        private async void LoadButtonClick(object sender, RoutedEventArgs e)
        {
            var files = await AssemblyLoader.ShowOpenDialog(this);

            if (files.Length == 0)
                return;

            var loadedAssemblies = 0;

            foreach (var file in files)
            {
                try
                {
                    var asm = AssemblyLoader.Load(file);
                    new AssemblyWindow(asm).Show();
                    loadedAssemblies++;
                }
                catch (Exception ex)
                {
                    AssemblyLoader.ShowExceptionDialog(this, file, ex);
                }
            }

            if(loadedAssemblies >= 1)
                this.Close();
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
