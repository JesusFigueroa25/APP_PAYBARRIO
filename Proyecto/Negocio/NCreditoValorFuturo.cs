using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
namespace Negocio
{
    public class NCreditoValorFuturo
    {
        private DCreditoValorFuturo dCreditoValorFuturo = new DCreditoValorFuturo();

        public String Registrar(CreditoValorFuturo CreditoValorFuturo)
        {
            return dCreditoValorFuturo.Registrar(CreditoValorFuturo);
        }
       
        public String Eliminar(int id)
        {
            return dCreditoValorFuturo.Eliminar(id);
        }

        public List<CreditoValorFuturo> ListarTodoIDCredito(int id)
        {
            return dCreditoValorFuturo.ListarTodoIDCredito(id);
        }
        public CreditoValorFuturo ObtenerCreditoValorFuturo(int id)
        {
            return dCreditoValorFuturo.ObtenerCreditoValorFuturo(id);

        }
        public List<CreditoValorFuturo> ListarTodo()
        {
            return dCreditoValorFuturo.ListarTodo();
        }

        /*public CreditoValorFuturo Pagar(int id, DateTime fecha, int mes, int plazo, decimal credito, decimal TasaMora)
        {
            return dCreditoValorFuturo.Pagar(id, fecha, mes, plazo, credito, TasaMora);
        }*/

        public void CancelarPagar(int id)
        {
            dCreditoValorFuturo.CancelarPagar(id);
        }
        public CreditoValorFuturo ValidarObjetoRepetido(int id)
        {
            return dCreditoValorFuturo.ValidarObjetoRepetido(id);
        }
        /*
         public String Modificar(CreditoValorFuturo CreditoValorFuturo)
        {
            return dCreditoValorFuturo.Modificar(CreditoValorFuturo);
        }
         */

    }
}
