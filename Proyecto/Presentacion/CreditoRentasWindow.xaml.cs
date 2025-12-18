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
using Datos;
namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para CreditoRentasWindow.xaml
    /// </summary>
    public partial class CreditoRentasWindow : Window
    {
        private DAnualidadCrononogramaPagos dCreditoAnualidad = new DAnualidadCrononogramaPagos();
        private DCredito dCredito = new DCredito();
        private DCliente dCliente = new DCliente();
        private DTiendaCliente dTiendaCliente = new DTiendaCliente();


        AnualidadCronogramaPagos CronogramaPagoSeleccionado;
        Creditos creditoParaRegistrar;
        decimal CuotaAnualidad;
        int idCredito;
        decimal interes;
        public CreditoRentasWindow()
        {
            InitializeComponent();
            idCredito = ClasesGlobales.CreditoGlobal.ID;
            MostrarCreditoAnualidad(dCreditoAnualidad.ListarTodo(idCredito));
            creditoParaRegistrar = ClasesGlobales.CreditoGlobal;
            CuotaAnualidad = dCreditoAnualidad.CalcularCuota(creditoParaRegistrar, creditoParaRegistrar.TipoGracia);
            RegistrosAutomaticos(creditoParaRegistrar);
            MostrarDatos();
            ValidarListaPagada();
        }
        private void ValidarListaPagada()
        {
            List<AnualidadCronogramaPagos> listAnualidades = dCreditoAnualidad.ListarTodo(idCredito);
            // Primero recorremos la lista para validar el estado de pago de todas las anualidades
            foreach (AnualidadCronogramaPagos anualidad in listAnualidades)
            {
                if (anualidad.EstadoPago == false)
                {
                    MessageBox.Show("Todavia hay cuotas sin pagar");
                    return; // Salimos de la función ya que encontramos una cuota sin pagar
                }
            }
            MessageBox.Show("Todas las cuotas estan pagadas");
            // Si todas las cuotas están pagadas, ejecutamos las acciones necesarias

            /*if (dCredito.ObtenerCredito(idCredito).EstadoPago == false)
            {
                dCliente.ModificarSaldo(ClasesGlobales.CreditoGlobal.Cliente_ID, dCredito.ObtenerCredito(idCredito).Interes);
            }*/

            //Modificar el estado de pago del credito
            dCredito.ModificarEstadoPagoTrue(idCredito);

            //Mostramos los datos
            MostrarDatos();
            MostrarCreditoAnualidad(dCreditoAnualidad.ObtenerListaAnualidades(idCredito));

            //Modificamos el estado de pago al cliente
            dCliente.ModificarEstadoTrue(creditoParaRegistrar.Cliente_ID);
            dTiendaCliente.ModificarEstadoTrue(creditoParaRegistrar.Cliente_ID, ClasesGlobales.Global_IDTienda);


        }


        private void MostrarDatos()
        {
            int idCliente = dCredito.ObtenerCredito(idCredito).Cliente_ID;
            Creditos creditoss = dCredito.ObtenerCredito(idCredito);
            lb_Cuota.Content= CuotaAnualidad;
            lb_Credito.Content = creditoss.MontoCredito;
            lb_TEM.Content = creditoss.TEA;
            lb_Plazo.Content = creditoss.Plazo;
            lb_PagoFinal.Content = creditoss.PagoFinal;
            lb_Interes.Content = creditoss.Interes;
            //Clientes
            lbNombreCliente.Content = dCliente.getObtenerCliente(idCliente).NombresCompletos;
            lbSaldoCliente.Content = dCliente.getObtenerCliente(idCliente).Saldo;

        }
        public void RegistrosAutomaticos(Creditos credito)
        {
            AnualidadCronogramaPagos ctemp = dCreditoAnualidad.ValidarObjetoRepetido(idCredito);
            if (ctemp==null)
            {
                for (int i = 0; i < credito.Plazo; i++)
                {
                    AnualidadCronogramaPagos creditoAnualidad = new AnualidadCronogramaPagos
                    {
                        Cliente_ID = creditoParaRegistrar.Cliente_ID,
                        Credito_ID = creditoParaRegistrar.ID,
                        Periodo = i + 1,
                        Cuota = CuotaAnualidad,
                        EstadoPago = false,
                    };
                    string mensaje = dCreditoAnualidad.Registrar(creditoAnualidad);
                }
            }
            MostrarCreditoAnualidad(dCreditoAnualidad.ListarTodo(idCredito));
        }
        private void MostrarCreditoAnualidad(List<AnualidadCronogramaPagos> creditoAnualidads)
        {
            dgCreditoRentas.ItemsSource = new List<AnualidadCronogramaPagos>();
            dgCreditoRentas.ItemsSource = creditoAnualidads;
        }
        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            CreditosWindow window1 = new CreditosWindow();  
            window1.Show();
            this.Close();
        }
        private void dgCreditoRentas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CronogramaPagoSeleccionado = dgCreditoRentas.SelectedItem as AnualidadCronogramaPagos;
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            ClienteCreditoWindow window= new ClienteCreditoWindow();    
            window.Show();
            this.Close();
        }

        private void btnPagar_Click(object sender, RoutedEventArgs e)
        {
            if (CronogramaPagoSeleccionado == null)
            {
                MessageBox.Show("Seleccione la cuota");
                return;
            }
            if (CronogramaPagoSeleccionado.EstadoPago == true)
            {
                MessageBox.Show("Ya está pagada esta cuota");
                return;
            }

            
            // Modificar el estado de la anualidad seleccionada
            dCreditoAnualidad.ModificarEstadoPagoAnualidad(CronogramaPagoSeleccionado.ID);

            //Sumar las cuotas al pago Final
            dCredito.SumarCuotas(idCredito,CuotaAnualidad);

            //Modificar el saldo del cliente 
            dCliente.ModificarSaldo(ClasesGlobales.CreditoGlobal.Cliente_ID, CuotaAnualidad);

            // Mostrar los datos del credito y la lista de anualidades actualizada
            MostrarCreditoAnualidad(dCreditoAnualidad.ObtenerListaAnualidades(idCredito));
            MostrarDatos();

            // Validar si todas las anualidades están pagadas
            ValidarListaPagada();

            MessageBox.Show("Pago de cuota realizado");


        }

        private void btnPagoAdelantado_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Mes.Text=="")
            {
                MessageBox.Show("Introduzca el mes porfavor");
                return ;
            }

            //Validamos si el credito ya se ha pagado
            if (dCredito.ObtenerCredito(idCredito).EstadoPago == true)
            {
                MessageBox.Show("Ya se pago el credito anteriormente");
                return;
            }

            //Calculamos la fecha de pago
            int mes = int.Parse(tb_Mes.Text);
            DateTime FechaCompra = creditoParaRegistrar.FechaCompra;
            DateTime nuevaFecha = FechaCompra.AddMonths(mes);

            //Guardamos los datos del credito
            int plazo = creditoParaRegistrar.Plazo;
            decimal Mora = creditoParaRegistrar.TasaMora;
            decimal Montocredito = creditoParaRegistrar.MontoCredito;
            bool proceso;
            if (mes > plazo)
            {
                proceso = true;
            }
            else
            {
                proceso = false;
            }

           
            //Calcular el monto del pago final y el estado de pago verdadero
            dCreditoAnualidad.Pagar(dCreditoAnualidad.ListarTodo(idCredito), mes, plazo, Montocredito, CuotaAnualidad, Mora, proceso);
            dCredito.PagarAnualidad(idCredito, nuevaFecha);

            //Modificamos el estado de pago al cliente
            dCliente.ModificarEstadoTrue(creditoParaRegistrar.Cliente_ID);
            dTiendaCliente.ModificarEstadoTrue(creditoParaRegistrar.Cliente_ID, ClasesGlobales.Global_IDTienda);

            //Modificar el saldo del cliente 
            dCliente.ModificarSaldo(ClasesGlobales.CreditoGlobal.Cliente_ID, dCredito.ObtenerCredito(idCredito).PagoFinal);

            //Mostramos el datagrip y los datos
            MostrarDatos();
            MostrarCreditoAnualidad(dCreditoAnualidad.ListarTodo(idCredito));

            //Validamos si la lista de las anualidades estan pagadas
            MessageBox.Show("Pago realizado");

        }
    }
}
