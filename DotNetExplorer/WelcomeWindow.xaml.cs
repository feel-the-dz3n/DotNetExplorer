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

#if DEBUG
            new AssemblyWindow(TestAssembly.Fetch.Get()).Show();
#endif
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

            if (files.Length == 0)
                return;

            var loadedAssemblies = 0;

            foreach (var file in files)
            {
                try
                {
                    var asm = Assembly.LoadFile(new FileInfo(file).FullName);
                    new AssemblyWindow(asm).Show();
                    loadedAssemblies++;
                }
                catch (Exception ex)
                {
                    var b = new StringBuilder();
                    b.AppendLine("Unable to load assembly.");
                    b.AppendLine();
                    b.AppendLine("File: " + file);
                    b.AppendLine("Exception: " + ex.GetType().Name);
                    b.AppendLine("Message: " + ex.Message);
                    await new MessageBox(b.ToString(), ".NET Explorer: Error").ShowDialog(this);
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
