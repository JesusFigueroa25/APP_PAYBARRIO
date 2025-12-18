using System;
using System.Collections.Generic;
using System.Data.Common;
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
    /// Lógica de interacción para ClientesWindow.xaml
    /// </summary>
    public partial class ClientesWindow : Window
    {
        public NTiendaCliente nClientesTienda = new NTiendaCliente();
        public NTienda nTiendaa = new NTienda();
        public NCliente nCliente = new NCliente();
        public DCliente dCliente = new DCliente();
        Tienda tiendaTemp = new Tienda();

        String nombreCliente;
        int idCliente;
        int ID_Tienda;
        TiendaCliente tiendaclienteSeleccionado;
        public ClientesWindow()
        {
            InitializeComponent();
            // tiendaTemp = nTiendaa.LoginReturnIDTienda();
            // String message = (tiendaTemp.RUC.ToString());
            ID_Tienda = ClasesGlobales.Global_IDTienda;
            MostrarClientesTienda(nClientesTienda.ListarClientesPorTienda(ID_Tienda));
            MostrarClientesTiendaComboBox(nCliente.ListarTodo());

        }
        private void MostrarClientesTienda(List<TiendaCliente> clientesTiendas)
        {
            dgClientesTienda.ItemsSource = new List<TiendaCliente>();
            dgClientesTienda.ItemsSource = clientesTiendas;
        }
        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            ClienteCreditoWindow window = new ClienteCreditoWindow();
            window.Show();
            this.Close();
        }

        private void btnAgregarCliente_Click(object sender, RoutedEventArgs e)
        {
            AgregarClienteWindow window = new AgregarClienteWindow();   
                window.Show();
            this.Close();
        }
        private void MostrarClientesTiendaComboBox(List<Cliente> clientes)
        {
            cbClientes.ItemsSource = new List<Cliente>();
            cbClientes.ItemsSource = clientes;
            cbClientes.DisplayMemberPath = "NombresCompletos";
            cbClientes.SelectedValuePath = "ID";
            
        }
        private void btnRegistrarCliente_Click(object sender, RoutedEventArgs e)
        {
            if (cbClientes.Text=="")
            {
                MessageBox.Show("Seleccione el cliente porfavor");
                return;
            }
            if (cbClientes.SelectedItem != null)
            {
                // Obtiene el cliente seleccionado como objeto
                Cliente clienteSeleccionado = (Cliente)cbClientes.SelectedItem;
                // Obtiene el nombre completo del cliente seleccionado
                nombreCliente = clienteSeleccionado.NombresCompletos;
                // Obtiene el ID del cliente seleccionado usando SelectedValue
                idCliente = (int)cbClientes.SelectedValue;
                TiendaCliente objecto = new TiendaCliente
                {
                    Cliente_ID = idCliente,
                    Tienda_ID = ClasesGlobales.Global_IDTienda,
                    NombresCliente = nombreCliente,
                    EstadoCliente = true
                };
                String mensaje = nClientesTienda.Registrar(objecto);
                MessageBox.Show(mensaje);
              
                MostrarClientesTienda(nClientesTienda.ListarClientesPorTienda(ID_Tienda));

            }
            
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            // Validación de selección
            if (tiendaclienteSeleccionado == null)
            {
                MessageBox.Show("Seleccione un cliente por favor");
                return;
            }
            // Eliminar
            String mensaje = nClientesTienda.Eliminar(tiendaclienteSeleccionado.ID);
            MessageBox.Show(mensaje);
            // Mostrar en el DataGrid
            MostrarClientesTienda(nClientesTienda.ListarClientesPorTienda(ID_Tienda));

        }

        private void dgClientesTienda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tiendaclienteSeleccionado = dgClientesTienda.SelectedItem as TiendaCliente;

        }

        private void btnVerEstadoCuenta_Click(object sender, RoutedEventArgs e)
        {
            if (tiendaclienteSeleccionado==null)
            {
                MessageBox.Show("Seleccione un registro porfavor");
                return ;
            }
            ClasesGlobales.ClienteGlobal = dCliente.getObtenerCliente(tiendaclienteSeleccionado.Cliente_ID);
            CuentaEstadoo cuentaEstadoo = new CuentaEstadoo();
            cuentaEstadoo.Show();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            // Validación de selección
            if (tiendaclienteSeleccionado == null || tbDiaPago.Text == "")
            {
                MessageBox.Show("Seleccione todos los datos por favor");
                return;
            }
            int dia = int.Parse(tbDiaPago.Text);

            dCliente.ModificarDiaPago(tiendaclienteSeleccionado.Cliente_ID, dia);

            // Mostrar en el DataGrid
            MostrarClientesTienda(nClientesTienda.ListarClientesPorTienda(ID_Tienda));

            MessageBox.Show("Se ha realizado la modificacion");
        }
    }
}
