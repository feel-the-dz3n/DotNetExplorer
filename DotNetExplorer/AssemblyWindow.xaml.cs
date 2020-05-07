using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
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
            this.FindControl<MenuItem>("MenuItemNewOpen").Click += NewOpenClick;
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
            {
                this.Title = $"{new FileInfo(Model.Location).Name} | .NET Explorer";
            }
            else
            {
                this.Title = ".NET Explorer";
            }

            this.FindControl<AssemblyTreeCtrl>("Tree").Model = Model;
            this.FindControl<AssemblyDetailsCtrl>("AssemblyDetailsCtrl").Model = Model;
        }


        private async void NewOpenClick(object sender, RoutedEventArgs e)
        {
            var files = await AssemblyLoader.ShowOpenDialog(this);
            foreach (var file in files)
            {
                try
                {
                    var asm = AssemblyLoader.Load(file);
                    new AssemblyWindow(asm).Show();
                }
                catch (Exception ex)
                {
                    AssemblyLoader.ShowExceptionDialog(this, file, ex);
                }
            }
        }

        private void WelcomeWindowClick(object sender, RoutedEventArgs e)
        {
            new WelcomeWindow().Show();
        }

        private async void OpenClick(object sender, RoutedEventArgs e)
        {
            var files = await AssemblyLoader.ShowOpenDialog(this);
            if (files.Length == 0) return;
            var file = files[0];

            try
            {
                var asm = AssemblyLoader.Load(file);
                this.Model = asm;
            }
            catch (Exception ex)
            {
                AssemblyLoader.ShowExceptionDialog(this, file, ex);
            }
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
