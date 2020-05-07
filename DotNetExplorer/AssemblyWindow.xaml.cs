using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

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
            this.Title = $"{new FileInfo(Model.Location).Name} | .NET Explorer";

            var items = Model.GetTypes().Select(x => new TextBlock() { Text = x.Name });

            // this.FindControl<ListBox>("LbTest").Items = items;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
