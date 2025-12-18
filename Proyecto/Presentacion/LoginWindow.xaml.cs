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
    /// Lógica de interacción para LoginWindow.xaml
    /// </summary>
    /// 
    public partial class LoginWindow : Window
    {
        private NTienda nTienda = new NTienda();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Tienda tiendaTemp = new Tienda();

            if ( tb_RUC.Text == "" || tb_Contrasenia.Text == "")
            {
                MessageBox.Show("Ingrese todos los campos");
                return;
            }

            tiendaTemp = nTienda.Login(tb_RUC.Text, tb_Contrasenia.Text);
            if (tiendaTemp==null)   {
                MessageBox.Show("Ingreso fallido");
            }
            else if (tiendaTemp.RUC == tb_RUC.Text ||
                     tiendaTemp.Contrasenia == tb_Contrasenia.Text)
            {
                ClienteCreditoWindow window3 = new ClienteCreditoWindow();
                ClasesGlobales.NombreTienda = tiendaTemp.Nombre;
                ClasesGlobales.Global_IDTienda = tiendaTemp.ID;
                MessageBox.Show("Bienvenido "+ ClasesGlobales.NombreTienda);

                window3.Show();
                this.Close();
            }

        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            ChooseTienda window = new ChooseTienda();
            window.Show();
            this.Close();
        }
    }
}
