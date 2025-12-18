using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DTienda
    {
        Tienda TiendaTemp = new Tienda();
        public String ructienda;
        public String contratienda;
        public String Registrar(Tienda oTienda)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.Tienda.Add(oTienda);
                    context.SaveChanges();
                }
                return "Registrado exitosamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public Tienda Login(string ruc,string contra)
        {
           
            try
            {
                using (var context = new BDEFEntities())
                {
                    //validacion = Tienda.Equals(ruc, contra);
                    TiendaTemp = context.Tienda.FirstOrDefault(c => c.RUC == ruc && c.Contrasenia == contra);
                }
                return TiendaTemp;
            }
            catch (Exception ex)
            {
                return TiendaTemp;
            }
        }
        


        /*
         public String Modificar(Tienda oTienda)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Tienda TiendaTemp = context.Tienda.Find(oTienda.ID);
                    TiendaTemp.Nombre = oTienda.Nombre;
                    TiendaTemp.RUC = oTienda.RUC;
                    TiendaTemp.Telefono = oTienda.Telefono;
                    TiendaTemp.Correo = oTienda.Correo;
                    TiendaTemp.Direccion = oTienda.Direccion;
                    TiendaTemp.Contrasenia = oTienda.Contrasenia;
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
                    Tienda TiendaTemp = context.Tienda.Find(ID);
                    context.Tienda.Remove(TiendaTemp);
                    context.SaveChanges();
                }
                return "Eliminado exitosamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<Tienda> ListarTodo()
        {
            List<Tienda> Tiendas = new List<Tienda>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    Tiendas = context.Tienda.ToList();
                }
                return Tiendas;
            }
            catch (Exception ex)
            {
                return Tiendas;
            }
        }
    }
         */
    }
}
