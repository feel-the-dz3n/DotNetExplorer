using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Reflection;

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
            var dialog = new OpenFileDialog();
            dialog.AllowMultiple = true;

            var extNet = new FileDialogFilter();
            extNet.Name = ".NET Assembles";
            extNet.Extensions.AddRange(new string[] { "dll", "exe" });

            var extAll = new FileDialogFilter();
            extAll.Name = "All Files";
            extAll.Extensions.Add("*");

            dialog.Filters.AddRange(new FileDialogFilter[] { extNet, extAll });

            var files = await dialog.ShowAsync(this);
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
