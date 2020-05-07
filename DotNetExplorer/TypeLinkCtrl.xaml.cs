using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;

namespace DotNetExplorer
{
    public class TypeLinkCtrl : UserControl, IModelContainer<Type>
    {
        public TypeLinkCtrl()
        {
            this.InitializeComponent();
        }

        public TypeLinkCtrl(Type type) : this() => Model = type;

        private Type _model;
        public Type Model
        {
            get => _model;
            set { _model = value; UpdateView(); }
        }

        private void UpdateView()
        {
            var tbNamespace = this.FindControl<TextBlock>("TbNamespace");
            var tbType = this.FindControl<TextBlock>("TbType");

            if (Model == null)
            {
                tbNamespace.Text = "UnknownNamespace.";
                tbType.Text = "UnknownType";
            }
            else
            {
                tbNamespace.Text = Model.Namespace + ".";
                tbType.Text = Model.GetFriendlyName();
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
