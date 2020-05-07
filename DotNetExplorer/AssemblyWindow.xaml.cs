using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DotNetExplorer
{
    public class AssemblyWindow : Window, IModelContainer<Assembly>
    {
        public AssemblyWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.FindControl<MenuItem>("MenuItemOpen").Click += OpenClick;
            this.FindControl<MenuItem>("MenuItemClose").Click += CloseClick;
        }

        public AssemblyWindow(Assembly assembly = null) : this()
        {
            Model = assembly;
        }

        private Assembly _model;
        public Assembly Model
        {
            get => _model;
            set
            {
                _model = value;
                UpdateView();
            }
        }

        private void UpdateView()
        {
            if (Model != null)
                this.Title = $"{new FileInfo(Model.Location).Name} | .NET Explorer";
            else
                this.Title = ".NET Explorer";

            this.FindControl<AssemblyDetailsCtrl>("AssemblyDetailsCtrl").Model = Model;
        }

        private async void OpenClick(object sender, RoutedEventArgs e)
        {
            await new WelcomeWindow().ShowDialog(this);
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
