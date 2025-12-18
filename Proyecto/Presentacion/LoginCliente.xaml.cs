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
    /// Lógica de interacción para LoginCliente.xaml
    /// </summary>
    public partial class LoginCliente : Window
    {
        private NCliente nCliente = new NCliente();
        public LoginCliente()
        {
            InitializeComponent();
        }


        private void btnIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            Cliente clienteTemp = new Cliente();

            if (tb_DNI.Text == "" || tb_Contrasenia.Text == "")
            {
                MessageBox.Show("Ingrese todos los campos");
                return;
            }

            clienteTemp = nCliente.Login(tb_DNI.Text, tb_Contrasenia.Text);
            if (clienteTemp == null)
            {
                MessageBox.Show("Ingreso fallido");
            }
            else if (clienteTemp.DNI == tb_DNI.Text ||
                     clienteTemp.Contrasenia == tb_Contrasenia.Text)
            {
                ClienteMenu window3 = new ClienteMenu();
                ClasesGlobales.NombreCliente = clienteTemp.NombresCompletos;
                ClasesGlobales.ClienteGlobal = clienteTemp;
                ClasesGlobales.Global_IDCliente = clienteTemp.ID;
                MessageBox.Show("Bienvenido " + ClasesGlobales.NombreCliente);
                window3.Show();
                this.Close();
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            ChooseCliente chooseCliente = new ChooseCliente();
            chooseCliente.Show();
            this.Close();
        }
    }
}
