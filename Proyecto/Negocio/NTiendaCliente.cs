using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
namespace Negocio
{
    public class NTiendaCliente
    {
        private DTiendaCliente dTiendaClientes = new DTiendaCliente();
        //private DTienda dTienda = new DTienda();

        public String Registrar(TiendaCliente ClientesTienda)
        {
            return dTiendaClientes.Registrar(ClientesTienda);
        }

        public String Eliminar(int id)
        {
            return dTiendaClientes.Eliminar(id);
        }

        public List<TiendaCliente> ListarTodo()
        {
            return dTiendaClientes.ListarTodo();
        }
        public List<TiendaCliente> ListarClientesPorTienda(int id)
        {
            return dTiendaClientes.ListarClientesPorTienda(id);
        }
        /* public Tienda getTienda(Tienda tienda)
        {
            return dTiendaClientes.getTienda(tienda);
        }*/

    }
}
