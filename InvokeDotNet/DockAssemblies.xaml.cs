using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for DockAssemblies.xaml
    /// </summary>
    public partial class DockAssemblies : UserControl
    {
        public DockAssemblies()
        {
            InitializeComponent();
        }

        public TreeViewItem AddAssembly(FriendlyAssembly.FriendlyAssembly asm)
        {
            TreeViewItem item = new TreeViewItem();
            item.Tag = asm;
            item.Header = asm;
            item.Selected += (a, b) => 
            {
                App.MainWnd.MainContent = new DockAssemblyViewer(asm);
            };
            treeView.Items.Add(item);

            return item;
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            if (f.ShowDialog() == true)
            {
                var asm = new FriendlyAssembly.FriendlyAssembly(f.FileName);
                AddAssembly(asm);
                App.MainWnd.MainContent = new DockAssemblyViewer(asm);
            }
        }
    }
}
