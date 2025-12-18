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
    /// Lógica de interacción para ClienteMenu.xaml
    /// </summary>
    public partial class ClienteMenu : Window
    {
        public ClienteMenu()
        {
            InitializeComponent();
        }

        private void btnEstado_Click(object sender, RoutedEventArgs e)
        {
            ClientePerfil clientePerfil = new ClientePerfil();
            clientePerfil.Show();   
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            LoginCliente loginCliente = new LoginCliente();
            loginCliente.Show();
            this.Close();
        }
    }
}
