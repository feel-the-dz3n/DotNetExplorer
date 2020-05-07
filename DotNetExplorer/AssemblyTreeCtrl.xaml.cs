using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DotNetExplorer.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace DotNetExplorer
{
    public class AssemblyTreeCtrl : UserControl, IModelContainer<Assembly>
    {
        public AssemblyTreeCtrl()
        {
            this.InitializeComponent();
        }

        private Assembly _model;
        public Assembly Model
        {
            get => _model;
            set { _model = value; UpdateView(); }
        }

        private void UpdateView()
        {
            var tree = this.FindControl<TreeView>("MainView");
            tree.Items = new ObservableCollection<AssemblyModel>() { new AssemblyModel(Model) };
        }

        private void UpdateViewEx()
        {
            var tree = this.FindControl<TreeView>("MainView");
            var items = new List<object>();

            if (Model != null)
            {
                var asmView = new TreeViewItem() { Header = new TextBlock() { Text = Model.FullName } };
                asmView.IsExpanded = true;
                items.Add(asmView);

                var asmItems = new List<TreeViewItem>();
                asmView.Items = asmItems;

                foreach (var type in Model.GetTypes())
                {
                    var typeView = new TreeViewItem() { Header = type.GetFriendlyName() };
                    asmItems.Add(typeView);

                    var typeItems = new List<TreeViewItem>();
                    typeView.Items = typeItems;

                    var fields = type.GetFields();
                    var props = type.GetProperties();
                    var methods = type.GetMethods();

                    var fieldsView = new TreeViewItem() { Header = "Fields" + $" ({fields.Length})" };
                    var propsView = new TreeViewItem() { Header = "Properties" + $" ({props.Length})" };
                    var methodsView = new TreeViewItem() { Header = "Methods" + $" ({methods.Length})" };

                    typeItems.AddRange(new TreeViewItem[] { fieldsView, propsView, methodsView });

                    fieldsView.Items = fields.Select(x => new TreeViewItem() { Header = x.GetTechnicalName(), Tag = x });
                    propsView.Items = props.Select(x => new TreeViewItem() { Header = x.GetTechnicalName(), Tag = x });
                    propsView.Items = methods.Select(x => new TreeViewItem() { Header = x.GetTechnicalName(), Tag = x });
                }
            }
            else
            {
                items.Add(new TreeViewItem() { Header = "Unknown Assembly" });
            }

            tree.Items = items.ToArray();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
