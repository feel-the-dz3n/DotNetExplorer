using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InvokeDotNet
{
    /// <summary>
    /// Interaction logic for DockAssemblyViewer.xaml
    /// </summary>
    public partial class DockAssemblyViewer : UserControl
    {
        FriendlyAssembly.FriendlyAssembly Asm;

        public DockAssemblyViewer()
        {
            InitializeComponent();
        }

        public DockAssemblyViewer(FriendlyAssembly.FriendlyAssembly asm)
        {
            Asm = asm;

            InitializeComponent();

            Asm.LoadBegin += Asm_LoadBegin;
            Asm.LoadEnd += Asm_LoadEnd;
            Asm.UnloadBegin += Asm_UnloadBegin;
            Asm.UnloadEnd += Asm_UnloadEnd;

            ReloadPage();
        }

        public void ReloadPage()
        {
            if (!Asm.IsLoaded)
                GridNotLoaded.Visibility = Visibility.Visible;
            else
                GridNotLoaded.Visibility = Visibility.Hidden;
        }

        private void Asm_UnloadEnd(FriendlyAssembly.FriendlyAssembly sender, Exception exception)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new Action(() => Asm_UnloadEnd(sender, exception)));
                return;
            }

            this.IsEnabled = true;

            ReloadPage();
        }

        private void Asm_UnloadBegin(FriendlyAssembly.FriendlyAssembly sender)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new Action(() => Asm_UnloadBegin(sender)));
                return;
            }

            this.IsEnabled = false;
        }

        private void Asm_LoadEnd(FriendlyAssembly.FriendlyAssembly sender, Exception exception)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new Action(() => Asm_LoadEnd(sender, exception)));
                return;
            }

            this.IsEnabled = true;

            ReloadPage();
        }

        private void Asm_LoadBegin(FriendlyAssembly.FriendlyAssembly sender)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new Action(() => Asm_LoadBegin(sender)));
                return;
            }

            this.IsEnabled = false;
        }

        private void BtnLoadAsm_Click(object sender, RoutedEventArgs e)
        {
            if (!Debugger.IsAttached)
                Asm.LoadAsync();
            else
            {
                Asm_LoadBegin(Asm);
                Asm.Load();
                Asm_LoadEnd(Asm, null);
            }
        }
    }
}
