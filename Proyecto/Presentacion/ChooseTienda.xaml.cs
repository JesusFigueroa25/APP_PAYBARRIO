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
using System.Windows.Shapes;

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para ChooseTienda.xaml
    /// </summary>
    public partial class ChooseTienda : Window
    {
        public ChooseTienda()
        {
            InitializeComponent();
        }

        private void btnLoginTienda_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void btnRegistroTienda_Click(object sender, RoutedEventArgs e)
        {
            RegistroTienda registroTienda = new RegistroTienda();
            registroTienda.Show();
            this.Close();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
