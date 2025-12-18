using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
namespace Negocio
{
    public class NTienda
    {
        private DTienda dTienda = new DTienda();

        public String Registrar(Tienda Tienda)
        {
            return dTienda.Registrar(Tienda);
        }
        public Tienda Login(string ruc, string Contrasenia )
        {
            return dTienda.Login(ruc, Contrasenia);
        }
        /* 
         public Tienda LoginReturnIDTienda()
        {
            return dTienda.LoginReturnIDTienda();
        }*/

    }
}
