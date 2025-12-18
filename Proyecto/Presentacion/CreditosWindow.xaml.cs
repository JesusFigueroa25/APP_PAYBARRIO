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
    /// Lógica de interacción para CreditosWindow.xaml
    /// </summary>
    public partial class CreditosWindow : Window
    {
        private DCliente dCliente = new DCliente();
        private DTiendaCliente dTiendaCliente = new DTiendaCliente();

        private NCredito nCredito = new NCredito();
        private NTiendaCliente nTiendaCliente = new NTiendaCliente();
        private List<Creditos> listCreditosaTemp = new List<Creditos>();
        int ID_Tienda;
        Creditos creditoSeleccionado;
        public CreditosWindow()
        {
            InitializeComponent();
             ID_Tienda = ClasesGlobales.Global_IDTienda;
            listCreditosaTemp = nCredito.ListarTodoCreditossFiltradosAnualidad(ID_Tienda);
            MostrarCreditos(listCreditosaTemp);
            //ClasesGlobales.Global_ListCreditosPorTiendaAnualidad = listCreditosaTemp;
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
        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            ChooseCreditos window = new ChooseCreditos();
            window.Show();
            this.Close();
        }

        private void btnAgregarCredito_Click(object sender, RoutedEventArgs e)
        {
            if (cbCliente.Text == "" || tbMontoCredito.Text == ""
                 || tbPlazo.Text == "" || tbTEA.Text == "" || tbInteresMora.Text == "" ||
                 cbTipoGracia.Text == "" || dateCompra.Text == "" || tbDiasGracia.Text == ""
                 )
            {
                MessageBox.Show("Ingrese todos los datos porfavor");
                return;
            }

            int IDCliente = (int)cbCliente.SelectedValue;
            decimal MontoSaldoCliente = dCliente.GetClienteSaldo(IDCliente);
            decimal MontoCredito = decimal.Parse(tbMontoCredito.Text);

          

            if (nCredito.ValidarCliente(IDCliente))
            {

                if (MontoCredito > MontoSaldoCliente)
                {
                    MessageBox.Show("El monto sobrepasa el limite del saldo de cliente: " + MontoSaldoCliente);
                    return;
                }
              
                DateTime FechaCompra=(DateTime)dateCompra.SelectedDate;
                DateTime newFecha= FechaCompra.AddMonths(int.Parse(tbPlazo.Text));
                Creditos credito = new Creditos
                {
                    Cliente_ID = IDCliente,
                    Tienda_ID = ID_Tienda,
                    TipoCredito = "Anualidad",
                    MontoCredito = decimal.Parse(tbMontoCredito.Text),
                    Plazo = int.Parse(tbPlazo.Text),
                    TEA = decimal.Parse(tbTEA.Text),
                    TasaMora = decimal.Parse(tbInteresMora.Text),
                    FechaCompra = (DateTime)dateCompra.SelectedDate,
                    FechaPago = newFecha,
                    PagoFinal = 0,
                    Interes = 0,
                    TipoGracia = cbTipoGracia.Text,
                    DiasGracia = int.Parse(tbDiasGracia.Text),
                    EstadoPago = false

                };
                // Registrar el objeto
                String mensaje = nCredito.Registrar(credito);

                //Modificar el saldo del cliente
                //dCliente.ModificarSaldo(IDCliente, decimal.Parse(tbMontoCredito.Text));

                //Modificar el estado la cuenta del cliente
                dCliente.ModificarEstadoFalse(IDCliente);
                dTiendaCliente.ModificarEstadoFalse(IDCliente, ClasesGlobales.Global_IDTienda);
                

                MessageBox.Show(mensaje);

                // Mostrar en el DataGrid
                MostrarCreditos(nCredito.ListarTodoCreditossFiltradosAnualidad(ID_Tienda));

            }
            else
            {
                MessageBox.Show("El cliente no ha pagado");
                return;
            }

        }

        private void btnTipoCredito_Click(object sender, RoutedEventArgs e)
        {
            if (creditoSeleccionado==null)
            {
                MessageBox.Show("Seleccione el tipo de credito");
                return;
            }
            else if (creditoSeleccionado.TipoCredito == "Anualidad")
            {
                ClasesGlobales.CreditoGlobal=creditoSeleccionado; 
                CreditoRentasWindow window = new CreditoRentasWindow();
                window.Show();
                this.Close();
            }
        }

        private void dgCreditos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            creditoSeleccionado = dgCreditos.SelectedItem as Creditos;
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
            MostrarCreditos(nCredito.ListarTodoCreditossFiltradosAnualidad(ID_Tienda));
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            ClienteCreditoWindow window = new ClienteCreditoWindow();
            window.Show();
            this.Close();
        }
    }
}
