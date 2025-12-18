using Datos;
using Negocio;
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
    /// Lógica de interacción para RegistroTienda.xaml
    /// </summary>
    public partial class RegistroTienda : Window
    {
        private NTienda nTienda = new NTienda();

        public RegistroTienda()
        {
            InitializeComponent();
        }

        private void btnRegistrate_Click(object sender, RoutedEventArgs e)
        {
            // Validación de campos
            if (tbContrasenia.Text == "" || tbCorreoElectronico.Text == ""
                || tbDireccion.Text == "" || tbRUC.Text == ""
                || tbTelefono.Text == "" || tbNombre.Text == ""
                )
            {
                MessageBox.Show("Ingrese todos los campos");
                return;
            }
            // Creación del objeto
            Tienda objeto = new Tienda
            {
                Nombre = tbNombre.Text,
                RUC = tbRUC.Text,
                Telefono = tbTelefono.Text,
                Correo = tbCorreoElectronico.Text,
                Direccion = tbDireccion.Text,
                Contrasenia = tbContrasenia.Text,
            };

            // Registrar el objeto
            String mensaje = nTienda.Registrar(objeto);
            MessageBox.Show(mensaje);
        }

        private void btnIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow window = new LoginWindow();
            window.Show();
            this.Close();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
