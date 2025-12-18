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
    /// Lógica de interacción para CreditoValorFuturoWindow.xaml
    /// </summary>
    public partial class CreditoValorFuturoWindow : Window
    {
        private NCreditoValorFuturo nCreditoValorFuturo = new NCreditoValorFuturo();
        
        private DCreditoValorFuturo dCreditoValorFuturo = new DCreditoValorFuturo();
        private DTiendaCliente dTiendaCliente = new DTiendaCliente();
        private DCredito dCredito=new DCredito();
        private DCliente dCliente=new DCliente();
        
        Creditos creditoParaRegistrar;
        CreditoValorFuturo creditoParaValidar;
        private NCredito nCredito = new NCredito();

        int idCredito;
        int diasCalculo = 0;

        public CreditoValorFuturoWindow()
        {
            InitializeComponent();
            idCredito = ClasesGlobales.CreditoGlobal.ID;
            creditoParaRegistrar = ClasesGlobales.CreditoGlobal;
            RegistrarCredito();
            MostrarDatosCredito();
            MostrarDatosCreditoValorFuturo();
           // MostrarCreditoValorFuturos(nCreditoValorFuturo.ListarTodoIDCredito(idCredito));
            creditoParaValidar = nCreditoValorFuturo.ObtenerCreditoValorFuturo(idCredito);
            ValidarEstadoPago(creditoParaValidar);
            //MessageBox.Show(creditoParaValidar.EstadoPago.ToString());
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            CreditoVF window = new CreditoVF();
            window.Show();
            this.Close();
        }
        private void MostrarDatosCredito()
        {
            int idCliente= dCredito.ObtenerCredito(idCredito).Cliente_ID;
            lb_Credito.Content = creditoParaRegistrar.MontoCredito;
            lb_TEM.Content = creditoParaRegistrar.TEA;
            lb_Interes.Content = dCredito.ObtenerCredito(idCredito).Interes;
            lb_Fecha_Compra.Content = dCredito.ObtenerCredito(idCredito).FechaCompra;

            lbNombreCliente.Content = dCliente.getObtenerCliente(idCliente).NombresCompletos;
            lbSaldoCliente.Content = dCliente.getObtenerCliente(idCliente).Saldo;
            lbDiaPago.Content = dCliente.getObtenerCliente(idCliente).DiaPagoConfigurable;
            lb_DiasCalculo.Content = diasCalculo;
        }
        private void MostrarDatosCreditoValorFuturo()
        {
            lb_VF.Content = dCreditoValorFuturo.ObtenerCreditoValorFuturo(idCredito).ValorFuturo;
            lb_FechaPago.Content = dCreditoValorFuturo.ObtenerCreditoValorFuturo(idCredito).FechaPago;
            lb_EstadoPago.Content = dCreditoValorFuturo.ObtenerCreditoValorFuturo(idCredito).EstadoPago;
        }
        private void RegistrarCredito()
        {
            CreditoValorFuturo ctemp = nCreditoValorFuturo.ValidarObjetoRepetido(idCredito);
            if (ctemp==null)
            {
                CreditoValorFuturo objeto = new CreditoValorFuturo
                {
                    Credito_ID = idCredito,
                    ValorFuturo = 0,
                    FechaPago= creditoParaRegistrar.FechaPago,
                    EstadoPago= creditoParaRegistrar.EstadoPago
                };
                // Registrar el objeto
                String message=nCreditoValorFuturo.Registrar(objeto);
            }
            else
            {
            }
            MostrarDatosCreditoValorFuturo();
        }
        private void btnPagar_Click(object sender, RoutedEventArgs e)
        {
            if (datePago.Text=="")
            {
                MessageBox.Show("Inserte la fecha de pago");
                return;
            }

            //Validamos si el credito ya se ha pagado
            if (dCredito.ObtenerCredito(idCredito).EstadoPago == true)
            {
                MessageBox.Show("Ya se pago el credito anteriormente");
                return;
            }


            //Fecha Pago
           
            DateTime FechaCompra= ClasesGlobales.CreditoGlobal.FechaCompra;
            DateTime FechaPago = (DateTime)datePago.SelectedDate;

            TimeSpan diferencia = FechaPago - FechaCompra;
            diasCalculo = diferencia.Days;
            
           
            //Calcular el monto del pago final al credito valor futuro con estado de pago verdadero
            dCreditoValorFuturo.CalcularValorFuturo(dCreditoValorFuturo.ObtenerCreditoValorFuturo(idCredito), creditoParaRegistrar, diasCalculo, FechaPago);
            
            //Guardamos el pago final al credito y los intereses
            dCredito.Pagar(idCredito, FechaPago, diasCalculo);

            //Modficar Estado de cuenta del cliente 
            dCliente.ModificarEstadoTrue(creditoParaRegistrar.Cliente_ID);
            dTiendaCliente.ModificarEstadoTrue(creditoParaRegistrar.Cliente_ID, ClasesGlobales.Global_IDTienda);


            //Modificar el saldo del cliente 
            dCliente.ModificarSaldo(ClasesGlobales.CreditoGlobal.Cliente_ID, dCredito.ObtenerCredito(idCredito).PagoFinal);

            //Mostrar los datos
            MostrarDatosCreditoValorFuturo();
            MostrarDatosCredito();

            //Mensaje
            MessageBox.Show("Pago realizado");

        }
        /*private void MostrarCreditoValorFuturos (List<CreditoValorFuturo> creditoValorFuturos)
        {
            dgCreditoValorFuturo.ItemsSource = new List<CreditoValorFuturo>();
            dgCreditoValorFuturo.ItemsSource = creditoValorFuturos;
        }*/

        public void ValidarEstadoPago(CreditoValorFuturo creditoParaValidar)
        {
            if (creditoParaValidar.EstadoPago == true)
            {
                nCredito.ModificarEstadoTrue(ClasesGlobales.CreditoGlobal.Cliente_ID);
            }
            else
            {
                nCredito.ModificarEstadoFalse(ClasesGlobales.CreditoGlobal.Cliente_ID);
            }
        }


        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            ClienteCreditoWindow window= new ClienteCreditoWindow();
            window.Show();
            this.Close();
        }

        

       
    }
}
