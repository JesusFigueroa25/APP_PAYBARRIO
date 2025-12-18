using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public  class DCliente
    {
        Cliente clienteTemp= new Cliente();
        decimal MontoSaldo;
        public String Registrar(Cliente ocliente)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Cliente.Add(ocliente);
                    context.SaveChanges();
                }
                return "Registrado exitosamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public String Modificar(Cliente ocliente)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Cliente clienteTemp = context.Cliente.Find(ocliente.ID);
                    clienteTemp.DNI = ocliente.DNI;
                    clienteTemp.NombresCompletos = ocliente.NombresCompletos;
                    clienteTemp.Direccion = ocliente.Direccion;
                    clienteTemp.Telefono = ocliente.Telefono;
                    clienteTemp.Correo = ocliente.Correo;
                    clienteTemp.EstadoCliente = ocliente.EstadoCliente;
                    context.SaveChanges();
                }
                return "Modificado exitosamente";
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
                    Cliente clienteTemp = context.Cliente.Find(ID);
                    context.Cliente.Remove(clienteTemp);
                    context.SaveChanges();
                }
                return "Eliminado exitosamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<Cliente> ListarTodo()
        {
            List<Cliente> clientes = new List<Cliente>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    clientes = context.Cliente.ToList();
                }
                return clientes;
            }
            catch (Exception ex)
            {
                return clientes;
            }
        }

        public Cliente Login(string dni, string contra)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    clienteTemp = context.Cliente.FirstOrDefault(c => c.DNI == dni && c.Contrasenia == contra);
                }
                return clienteTemp;
            }
            catch (Exception ex)
            {
                return clienteTemp;
            }
        }
        public Cliente getObtenerCliente(int id)
        {
            Cliente cliente = new Cliente();
            try
            {
                using (var context = new BDEFEntities())
                {
                    cliente = context.Cliente.Find(id);
                }
                return cliente;
            }
            catch (Exception ex)
            {
                return cliente;
            }
        }

        public decimal GetClienteSaldo(int id)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Cliente clienteTemp = context.Cliente.Find(id);
                    MontoSaldo= clienteTemp.Saldo;
                }
                return MontoSaldo;
            }
            catch (Exception ex)
            {
                return MontoSaldo;
            }
        }
        public void ModificarSaldo(int idCliente, decimal? monto)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Cliente clienteTemp = context.Cliente.Find(idCliente);
                    clienteTemp.Saldo = (decimal)(clienteTemp.Saldo- monto);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }

        }
        public bool ValidarCliente(int ID)
        {
            bool estado;
            try
            {
                using (var context = new BDEFEntities())
                {
                    Cliente clienteTemp = context.Cliente.Find(ID);
                    estado = clienteTemp.EstadoCliente;
                }
                return estado;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public void ModificarEstadoFalse(int ID)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Cliente clienteTemp = context.Cliente.FirstOrDefault(c => c.ID == ID);
                    clienteTemp.EstadoCliente=false;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void ModificarEstadoTrue(int ID)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Cliente clienteTemp = context.Cliente.FirstOrDefault(c => c.ID == ID);
                    clienteTemp.EstadoCliente = true;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void ModificarDiaPago(int ID, int dia)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Cliente clienteTemp = context.Cliente.FirstOrDefault(c => c.ID == ID);
                    clienteTemp.DiaPagoConfigurable = dia;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }

    }
}
