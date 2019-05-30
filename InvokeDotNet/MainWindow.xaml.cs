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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public UIElement MainContent
        {
            get => MainContentGrid.Children.Count >= 1 ? MainContentGrid.Children[0] : null;
            set
            {
                MainContentGrid.Children.Clear();
                if (value != null) MainContentGrid.Children.Add(value);
            }
        }

        public MainWindow()
        {
            App.MainWnd = this;

            InitializeComponent();
        }
    }
}
