using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Trombinoscope
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UCEmployes UCEmplo;
        private UCTrombi UCTrom;
        public MainWindow()
        {
           // Thread.CurrentThread.CurrentUICulture = new CultureInfo("En");
            InitializeComponent();
            MenuTrombi.Click += MenuTrombi_Click;
            MenuEmployés.Click += MenuEmployés_Click;
        }

        private void MenuEmployés_Click(object sender, RoutedEventArgs e)
        {
            if (!(Cc.Content is UCEmployes) || UCTrom == null)
            {
                UCEmplo = new UCEmployes();
                Cc.Content = UCEmplo;
            }
        }

        private void MenuTrombi_Click(object sender, RoutedEventArgs e)
        {
            if (!(Cc.Content is UCTrombi) || UCTrom == null)
            {
                UCTrom = new UCTrombi();
                Cc.Content =UCTrom;
            }
        }
    }
}
