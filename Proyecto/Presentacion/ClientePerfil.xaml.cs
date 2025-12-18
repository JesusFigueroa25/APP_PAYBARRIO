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
    /// Lógica de interacción para ClientePerfil.xaml
    /// </summary>
    public partial class ClientePerfil : Window
    {
        private NCredito nCredito = new NCredito();
        private List<Creditos> listCreditosaTemp = new List<Creditos>();
        decimal Intereses;
        decimal Consumos;

        public ClientePerfil()
        {
            InitializeComponent();
            listCreditosaTemp = nCredito.ListarTodoPorCliente(ClasesGlobales.ClienteGlobal.ID);
            CalcularSumaIntereses(listCreditosaTemp);
            CalcularConsumosRealizados(listCreditosaTemp);
            MostrarCreditos(listCreditosaTemp);
            MostrarDatos(ClasesGlobales.ClienteGlobal);
        }
        private void MostrarDatos(Cliente cliente)
        {
            lbDNI.Content = cliente.DNI;
            lbNombre.Content = cliente.NombresCompletos;
            lbDireccion.Content = cliente.Direccion;
            lbTelefono.Content = cliente.Telefono;
            lbCorreo.Content = cliente.Correo;
            lbSaldo.Content = cliente.Saldo;
            lb_DiaPago.Content = cliente.DiaPagoConfigurable;
            lbConsumosRealizados.Content = Consumos;
            lbInteres.Content = Intereses;
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


        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            /* PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                // Guardar el tamaño original de la ventana
                Size originalSize = new Size(this.ActualWidth, this.ActualHeight);

                // Configurar el tamaño de la ventana para ajustarse a la página de impresión
                Transform originalTransform = dgCreditos.LayoutTransform;
                dgCreditos.LayoutTransform = null;

                // Escalar la ventana para ajustarla al tamaño de la página de impresión
                double scale = Math.Min(printDialog.PrintableAreaWidth / originalSize.Width, printDialog.PrintableAreaHeight / originalSize.Height);
                dgCreditos.LayoutTransform = new ScaleTransform(scale, scale);

                // Imprimir la ventana
                printDialog.PrintVisual(dgCreditos, "Imprimir Perfil de Cliente");

                // Restaurar el tamaño original de la ventana
                dgCreditos.LayoutTransform = originalTransform;
            }*/


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
