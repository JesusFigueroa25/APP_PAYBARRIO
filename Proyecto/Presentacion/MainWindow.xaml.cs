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
using Datos;
using Negocio;
namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnTienda_Click(object sender, RoutedEventArgs e)
        {
            ChooseTienda chooseTienda = new ChooseTienda();
            chooseTienda.Show();
            this.Close();
        }

        private void btnCliente_Click(object sender, RoutedEventArgs e)
        {
            ChooseCliente chooseCliente = new ChooseCliente();  
            chooseCliente.Show();
            this.Close();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
