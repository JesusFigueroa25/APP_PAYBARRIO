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
    /// Lógica de interacción para CreditoVF.xaml
    /// </summary>
    public partial class CreditoVF : Window
    {
        private DCliente dCliente = new DCliente();
        private DTiendaCliente dTiendaCliente = new DTiendaCliente();

        private NCredito nCredito = new NCredito();
        private NTiendaCliente nTiendaCliente = new NTiendaCliente();
        private List<Creditos> listCreditosaTemp = new List<Creditos>();
        int ID_Tienda;
        Creditos creditoSeleccionado;
        public CreditoVF()
        {
            InitializeComponent();
            ID_Tienda = ClasesGlobales.Global_IDTienda;
            listCreditosaTemp = nCredito.ListarTodoCreditossFiltradosValorFuturo(ID_Tienda);
            MostrarCreditos(listCreditosaTemp);
            MostrarClientesComboBox(nTiendaCliente.ListarClientesPorTienda(ID_Tienda));
        }
        private void MostrarCreditos(List<Creditos> creditos)
        {
            dgCreditos.ItemsSource = new List<Creditos>();
            dgCreditos.ItemsSource = creditos;
        }
        public void MostrarClientesComboBox(List<TiendaCliente> tiendaClientes)
        {
            cbCliente.ItemsSource = new List<TiendaCliente>();
            cbCliente.ItemsSource = tiendaClientes;
            cbCliente.DisplayMemberPath = "NombresCliente";
            cbCliente.SelectedValuePath = "Cliente_ID";
        }
        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            ClienteCreditoWindow window = new ClienteCreditoWindow();
            window.Show();    
            this.Close();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            ChooseCreditos window = new ChooseCreditos();
            window.Show();
            this.Close();
        }

        private void btnDetalle_Click(object sender, RoutedEventArgs e)
        {

            if (creditoSeleccionado == null)
            {
                MessageBox.Show("Seleccione el credito");
                return;
            } if (creditoSeleccionado.TipoCredito == "ValorFuturo")
            {
                //nCredito.ModificarEstado(ClasesGlobales.CreditoGlobal.Cliente_ID);
                ClasesGlobales.CreditoGlobal = creditoSeleccionado;
                CreditoValorFuturoWindow window1 = new CreditoValorFuturoWindow();
                window1.Show();
                this.Close();
            }
        }

        private void btnAgregarCredito_Click(object sender, RoutedEventArgs e)
        {
            if (cbCliente.Text == "" || tbMontoCredito.Text == ""
                   || tbTEA.Text == "" || dateCompra.Text == ""
                  )
            {
                MessageBox.Show("Ingrese todos los datos porfavor");
                return;
            }
            int IDCliente = (int)cbCliente.SelectedValue;
            decimal MontoSaldoCliente =dCliente.GetClienteSaldo(IDCliente);
           decimal MontoCredito= decimal.Parse(tbMontoCredito.Text);

            if (nCredito.ValidarCliente(IDCliente))
             {

                if (MontoCredito > MontoSaldoCliente)
                {
                    MessageBox.Show("El monto sobrepasa el limite del saldo de cliente: " + MontoSaldoCliente);
                    return;
                }

                Creditos credito = new Creditos
                {
                    Cliente_ID = IDCliente,
                    Tienda_ID = ID_Tienda,
                    TipoCredito = "ValorFuturo",
                    MontoCredito = decimal.Parse(tbMontoCredito.Text),
                    Plazo = 0,
                    TEA = decimal.Parse(tbTEA.Text),
                    TasaMora =0,
                    FechaCompra = (DateTime)dateCompra.SelectedDate,
                    FechaPago = null,
                    PagoFinal = 0,
                    Interes = 0,
                    TipoGracia = "",
                    DiasGracia = 0,
                    EstadoPago = false
                };
                String mensaje = nCredito.Registrar(credito);

                
                //Modificamos el estado del cliente en falso
                dCliente.ModificarEstadoFalse(IDCliente);
                dTiendaCliente.ModificarEstadoFalse(IDCliente, ClasesGlobales.Global_IDTienda);
               
                MessageBox.Show(mensaje);

                // Mostrar en el DataGrid
                MostrarCreditos(nCredito.ListarTodoCreditossFiltradosValorFuturo(ID_Tienda));
            }
            else{
                 MessageBox.Show("El cliente no ha pagado");
                 return;
             }


            
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            // Validación de selección
            if (creditoSeleccionado == null)
            {
                MessageBox.Show("Seleccione un cliente por favor");
                return;
            }
            // Eliminar
            String mensaje = nCredito.Eliminar(creditoSeleccionado.ID);
            MessageBox.Show(mensaje);
            // Mostrar en el DataGrid
            MostrarCreditos(nCredito.ListarTodoCreditossFiltradosValorFuturo(ID_Tienda));
        }

        private void dgCreditos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            creditoSeleccionado = dgCreditos.SelectedItem as Creditos;
        }
    }
}
