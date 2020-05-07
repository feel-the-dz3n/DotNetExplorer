using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Linq;

namespace DotNetExplorer
{
    public class TypeViewCtrl : UserControl, IModelContainer<Type>
    {
        public TypeViewCtrl()
        {
            this.InitializeComponent();
        }

        public TypeViewCtrl(Type type) : this() => Model = type;

        private Type _model;
        public Type Model 
        {
            get => _model;
            set { _model = value; UpdateView(); }
        }

        private void UpdateView()
        {
            var headerPanel = this.FindControl<StackPanel>("HeaderPanel");
            var fieldsPanel = this.FindControl<StackPanel>("FieldsPanel");
            var propsPanel = this.FindControl<StackPanel>("PropsPanel");
            var methodsPanel = this.FindControl<StackPanel>("MethodsPanel");

            headerPanel.Children.Clear();
            fieldsPanel.Children.Clear();
            propsPanel.Children.Clear();
            methodsPanel.Children.Clear();

            headerPanel.Children.Add(new TypeLinkCtrl(Model));

            if (Model != null)
            {
                // kinda stubs:

                fieldsPanel.Children.AddRange(Model.GetFields()
                    .Select(x => new TextBlock() { Text = x.Name }));

                propsPanel.Children.AddRange(Model.GetProperties()
                    .Select(x => new TextBlock() { Text = x.Name }));

                propsPanel.Children.AddRange(Model.GetMethods()
                    .Select(x => new TextBlock() { Text = x.Name }));
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
