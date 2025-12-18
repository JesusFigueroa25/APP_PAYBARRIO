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
    /// Lógica de interacción para RegistroCliente.xaml
    /// </summary>
    public partial class RegistroCliente : Window
    {
        private NCliente nCliente = new NCliente();

        public RegistroCliente()
        {
            InitializeComponent();
        }

        private void btnIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            LoginCliente loginCliente = new LoginCliente();
            loginCliente.Show();
            this.Close();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            ChooseCliente chooseCliente = new ChooseCliente();
            chooseCliente.Show();
            this.Close();
        }

        private void btnRegistrate_Click(object sender, RoutedEventArgs e)
        {
            // Validación de campos
            if (tbContrasenia.Text == "" || tbCorreoElectronico.Text == ""
                || tbDireccion.Text == "" || tbDNI.Text == ""
                || tbTelefono.Text == "" || tbNombre.Text == ""
                )
            {
                MessageBox.Show("Ingrese todos los campos");
                return;
            }
            // Creación del objeto
            Cliente objeto = new Cliente
            {
                NombresCompletos = tbNombre.Text,
                DNI = tbDNI.Text,
                Telefono = tbTelefono.Text,
                Correo = tbCorreoElectronico.Text,
                Direccion = tbDireccion.Text,
                Contrasenia = tbContrasenia.Text,
                DiaPagoConfigurable=0
            };

            // Registrar el objeto
            String mensaje = nCliente.Registrar(objeto);
            MessageBox.Show(mensaje);
        }
    }
}
