using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DTiendaCliente
    {
        public DTienda dTienda = new DTienda(); 
        public String Registrar(TiendaCliente oClientesTienda)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.TiendaCliente.Add(oClientesTienda);
                    context.SaveChanges();
                }
                return "Registrado exitosamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public String Eliminar(int ID)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    TiendaCliente ClientesTiendaTemp = context.TiendaCliente.Find(ID);
                    context.TiendaCliente.Remove(ClientesTiendaTemp);
                    context.SaveChanges();
                }
                return "Eliminado exitosamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<TiendaCliente> ListarTodo()
        {
            List<TiendaCliente> ClientesTiendas = new List<TiendaCliente>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    ClientesTiendas = context.TiendaCliente.ToList();

                }
                return ClientesTiendas;
            }
            catch (Exception ex)
            {
                return ClientesTiendas;
            }
        }

        public List<TiendaCliente> ListarClientesPorTienda(int id)
        {
            List<TiendaCliente> ClientesTiendas = new List<TiendaCliente>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    ClientesTiendas = context.TiendaCliente.Where(ct => ct.Tienda_ID.Equals(id)).ToList();

                }
                return ClientesTiendas;
            }
            catch (Exception ex)
            {
                return ClientesTiendas;
            }
        }

        public void ModificarEstadoFalse(int IDCliente, int IDTienda)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    TiendaCliente tiendaCliente = context.TiendaCliente.FirstOrDefault(c => c.Cliente_ID == IDCliente && c.Tienda_ID== IDTienda);
                    tiendaCliente.EstadoCliente = false;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void ModificarEstadoTrue(int IDCliente, int IDTienda)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    TiendaCliente tiendaCliente = context.TiendaCliente.FirstOrDefault(c => c.Cliente_ID == IDCliente && c.Tienda_ID == IDTienda);
                    tiendaCliente.EstadoCliente = true;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }

        /*public List<ClientesTienda> ReturnTienda()
        {
            List<ClientesTienda> ClientesTiendas = new List<ClientesTienda>();
            int idTienda = TiendaTemp.ID;
            try
            {
                using (var context = new BDEFEntities())
                {
                    ClientesTiendas = context.ClientesTienda.Where(ct => ct.Tienda_ID.Equals(idTienda)).ToList();

                }
                return ClientesTiendas;
            }
            catch (Exception ex)
            {
                return ClientesTiendas;
            }
        }*/

    }
}
