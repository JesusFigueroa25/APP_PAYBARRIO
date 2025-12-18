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
    /// Lógica de interacción para ChooseCreditos.xaml
    /// </summary>
    public partial class ChooseCreditos : Window
    {
        public ChooseCreditos()
        {
            InitializeComponent();
        }

        private void btnFuturo_Click(object sender, RoutedEventArgs e)
        {
            CreditoVF window = new CreditoVF();
            window.Show();
            this.Close();
        }

        private void btnAnualidad_Click(object sender, RoutedEventArgs e)
        {
            CreditosWindow window = new CreditosWindow();
            window.Show();
            this.Close();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            ClienteCreditoWindow window = new ClienteCreditoWindow();
            window.Show();
            this.Close();
        }
    }
}
