using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.IO;
using System.Reflection;
using System.Text;

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
            var title = this.FindControl<TextBlock>("TbTitle");
            var info = this.FindControl<TextBlock>("TbInfo");

            if (Model == null)
            {
                title.Text = "Unknown Assembly";
                info.Text = "";
            }
            else
            {
                var name = Model.GetName();

                if (name.Name != null)
                    title.Text = name.Name;
                else
                    title.Text = new FileInfo(Model.Location).Name;

                var s = new StringBuilder();

                if (name.Version != null)
                    s.AppendLine($"version {name.Version}");
                if (name.Version != null)
                    s.AppendLine(name.FullName);

                s.AppendLine("Runtime: " + Model.ImageRuntimeVersion);

                info.Text = s.ToString();
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
