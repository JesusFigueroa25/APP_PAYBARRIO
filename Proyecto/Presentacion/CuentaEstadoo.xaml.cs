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
    /// Lógica de interacción para CuentaEstadoo.xaml
    /// </summary>
    public partial class CuentaEstadoo : Window
    {
        private DCredito dCredito = new DCredito();
        private List<Creditos> listCreditosaTemp = new List<Creditos>();
       private Cliente clienteTemp=ClasesGlobales.ClienteGlobal;
        decimal Intereses;
        decimal Consumos;
        public CuentaEstadoo()
        {
            InitializeComponent();
            listCreditosaTemp = dCredito.ListarTodoPorClienteTienda(clienteTemp.ID, ClasesGlobales.Global_IDTienda );
            CalcularSumaIntereses(listCreditosaTemp);
            CalcularConsumosRealizados(listCreditosaTemp);
            MostrarCreditos(listCreditosaTemp);
            MostrarDatos(clienteTemp);
        }
        private void CalcularSumaIntereses(List<Creditos> creditos)
        {
            Intereses = 0;
            foreach (Creditos c in creditos)
            {
                Intereses += c.Interes ?? 0;
            }
        }
        private void CalcularConsumosRealizados(List<Creditos> creditos)
        {
            Consumos = 0;
            foreach (Creditos c in creditos)
            {
                Consumos += c.MontoCredito;
            }
        }
        private void MostrarCreditos(List<Creditos> creditos)
        {
            dgCreditos.ItemsSource = new List<Creditos>();
            dgCreditos.ItemsSource = creditos;
        }
        private void MostrarDatos(Cliente cliente)
        {
            lbDNI.Content = cliente.DNI;
            lbNombre.Content = cliente.NombresCompletos;
            lbDireccion.Content = cliente.Direccion;
            lbTelefono.Content = cliente.Telefono;
            lbCorreo.Content = cliente.Correo;
            lbSaldo.Content = cliente.Saldo;
            lbConsumosRealizados.Content = Consumos;
            lbInteres.Content = Intereses;
            lb_DiaPago.Content = cliente.DiaPagoConfigurable;
        }
        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                // Configurar el tamaño de la página
                Size pageSize = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);

                // Escalar el contenido de la ventana al tamaño de la página
                MainGrid.Measure(pageSize);
                MainGrid.Arrange(new Rect(0, 0, pageSize.Width, pageSize.Height));

                // Crear un contenedor visual para la impresión
                var visual = new DrawingVisual();
                using (var context = visual.RenderOpen())
                {
                    // Dibujar el contenido de la ventana en el visual
                    var brush = new VisualBrush(MainGrid);
                    context.DrawRectangle(brush, null, new Rect(new Point(), new Size(MainGrid.ActualWidth, MainGrid.ActualHeight)));
                }

                // Imprimir el visual
                printDialog.PrintVisual(visual, "Estado de Cuenta");
            }
            }

    }
}
