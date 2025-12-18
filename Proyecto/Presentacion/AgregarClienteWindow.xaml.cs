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
using Datos;
using Negocio;
namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para AgregarClienteWindow.xaml
    /// </summary>
    public partial class AgregarClienteWindow : Window
    {
        private NCliente nCliente = new NCliente();
        Cliente ClienteSeleccionado;


        public AgregarClienteWindow()
        {
            InitializeComponent();
            MostrarClientes(nCliente.ListarTodo());

        }
        private void MostrarClientes(List<Cliente> clientes)
        {
            dgClientes.ItemsSource = new List<Cliente>();
            dgClientes.ItemsSource = clientes;
        }
        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            ClienteCreditoWindow window = new ClienteCreditoWindow();
            window.Show();
            this.Close();
        }

        private void btnRegresar_Click_1(object sender, RoutedEventArgs e)
        {
            ClientesWindow window = new ClientesWindow();
            window.Show();
            this.Close();
        }

        private void btnRegistrarCliente_Click(object sender, RoutedEventArgs e)
        {
            Boolean estado;
            if (tbNombres.Text==""||tbDireccion.Text==""||
                tbCorreoElectronico.Text==""||tbTelefono.Text==""
                ||tbDNI.Text=="" || cbEstadoCliente.Text==""
                )
            {
                MessageBox.Show("Ingrese todos los camposs porfavor");
            }
            if (cbEstadoCliente.Text=="Activo")
            {
                estado = true;
            }
            else
            {
                estado = false;
            }
            Cliente clienteNuevo = new Cliente
            {
                NombresCompletos= tbNombres.Text,
                Direccion= tbDireccion.Text,
                Correo= tbCorreoElectronico.Text,
                Telefono= tbTelefono.Text,
                DNI= tbDNI.Text,
                EstadoCliente= estado

            };

            MessageBox.Show(nCliente.Registrar(clienteNuevo));
            MostrarClientes(nCliente.ListarTodo());

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            // Validación de selección
            if (ClienteSeleccionado == null)
            {
                MessageBox.Show("Seleccione un cliente por favor");
                return;
            }
            // Eliminar
            String mensaje = nCliente.Eliminar(ClienteSeleccionado.ID);
            MessageBox.Show(mensaje);
            // Mostrar en el DataGrid
            MostrarClientes(nCliente.ListarTodo());

        }

        private void dgClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClienteSeleccionado = dgClientes.SelectedItem as Cliente;
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Boolean estado;
            if (tbNombres.Text == "" || tbDireccion.Text == "" ||
                tbCorreoElectronico.Text == "" || tbTelefono.Text == ""
                || tbDNI.Text == "" || cbEstadoCliente.Text == ""
                )
            {
                MessageBox.Show("Ingrese todos los camposs porfavor");
            }
            if (cbEstadoCliente.Text == "Activo")
            {
                estado = true;
            }
            else
            {
                estado = false;
            }
            Cliente cliente = new Cliente
            {
                NombresCompletos = tbNombres.Text,
                Direccion = tbDireccion.Text,
                Correo = tbCorreoElectronico.Text,
                Telefono = tbTelefono.Text,
                DNI = tbDNI.Text,
                EstadoCliente = estado

            };

            MessageBox.Show(nCliente.Modificar(cliente));
            MostrarClientes(nCliente.ListarTodo());
        }
    }
}
