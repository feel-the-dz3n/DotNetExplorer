using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Reflection;

namespace DotNetExplorer
{
    public class AssemblyWindow : Window, IModelContainer<Assembly>
    {
        public AssemblyWindow(Assembly assembly = null)
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
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

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
