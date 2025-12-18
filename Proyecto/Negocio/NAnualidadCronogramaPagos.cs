using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
namespace Negocio
{
    public class NAnualidadCronogramaPagos
    {
        private DAnualidadCrononogramaPagos dCronogramaPagos = new DAnualidadCrononogramaPagos();

        public String Registrar(AnualidadCronogramaPagos CronogramaPagos)
        {
            return dCronogramaPagos.Registrar(CronogramaPagos);
        }


        public String Eliminar(int id)
        {
            return dCronogramaPagos.Eliminar(id);
        }

        public List<AnualidadCronogramaPagos> ListarTodo(int idCredito)
        {
            return dCronogramaPagos.ListarTodo(idCredito);
        }
        public decimal CalcularCuota(Creditos creditos, string tipoGracia)
        {
            return dCronogramaPagos.CalcularCuota(creditos, tipoGracia);
        }

    }
}
