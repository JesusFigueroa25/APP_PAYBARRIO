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
    /// Lógica de interacción para ClienteCreditoWindow.xaml
    /// </summary>
    public partial class ClienteCreditoWindow : Window
    {

        public ClienteCreditoWindow()
        {
            InitializeComponent();
        }

        private void btnClientes_Click(object sender, RoutedEventArgs e)
        {
            ClientesWindow window = new ClientesWindow();
            window.Show();
            this.Close();
        }


        private void btnCreditos_Click(object sender, RoutedEventArgs e)
        {
            ChooseCreditos window = new ChooseCreditos();   
            window.Show();
            this.Close();
        }

        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow window = new LoginWindow(); 
            window.Show();
            this.Close();
        }
    }
}
