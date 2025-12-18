using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
namespace Negocio
{
    public class NCredito
    {
        private DCredito dCreditos = new DCredito();
        private DCliente dCliente = new DCliente();

        public String Registrar(Creditos Creditos)
        {
            return dCreditos.Registrar(Creditos);
        }

        public String Modificar(Creditos Creditos)
        {
            return dCreditos.Modificar(Creditos);
        }

        public String Eliminar(int id)
        {
            return dCreditos.Eliminar(id);
        }

        public List<Creditos> ListarTodo(int id)
        {
            return dCreditos.ListarTodo(id);
        }
        public List<Creditos> ListarTodoPorCliente(int id)
        {
            return dCreditos.ListarTodoPorCliente(id);
        }
        public List<Creditos> ListarTodoCreditossFiltradosValorFuturo(int id)
        {
            return dCreditos.ListarTodoCreditossFiltradosValorFuturo(id);
        }
        public List<Creditos> ListarTodoCreditossFiltradosAnualidad(int id)
        {
            return dCreditos.ListarTodoCreditossFiltradosAnualidad(id);
        }
        public bool ValidarCliente(int ID)
        {
            return dCliente.ValidarCliente(ID);
        }
        public void ModificarEstadoFalse(int ID)
        {
             dCliente.ModificarEstadoFalse(ID);
        }
        public void ModificarEstadoTrue(int ID)
        {
            dCliente.ModificarEstadoTrue(ID);
        }
    }
}
