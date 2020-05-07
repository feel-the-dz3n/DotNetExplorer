using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Reflection;

namespace DotNetExplorer
{
    public class AssemblyDetailsCtrl : UserControl, IModelContainer<Assembly>
    {
        public AssemblyDetailsCtrl()
        {
            this.InitializeComponent();
        }

        public AssemblyDetailsCtrl(Assembly assembly) : this()
        {
            Model = assembly;
        }

        private Assembly _model;
        public Assembly Model
        {
            get => _model;
            set { _model = value; UpdateView(); }
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
