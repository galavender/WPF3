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

namespace RelevésMétéo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DALMeteo Météo;
        public MainWindow()
        {
            
            InitializeComponent();
            Météo = new DALMeteo();
            BtnPath.Click += BtnPath_Click;
            CBMétéo.SelectionChanged += LstBoxMétéo_SelectionChanged;
            
        }

        private void LstBoxMétéo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                LstBoxMétéo.ItemTemplate = (DataTemplate)Resources[CBMétéo.SelectedValue];
        }

        private void BtnPath_Click(object sender, RoutedEventArgs e)
        {
            var dial = new Microsoft.Win32.OpenFileDialog();
            dial.ShowDialog();
            try
            {
                Météo.ChargerDonnées(dial.FileName);
                window.DataContext = Météo;
            }
            catch (Exception)
            {
                MessageBox.Show("Ce fichier n'est pas valide");                
            }

        }
    }
}
