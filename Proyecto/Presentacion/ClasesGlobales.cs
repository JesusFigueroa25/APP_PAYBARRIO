using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Presentacion
{
    public class ClasesGlobales
    {
       public static int Global_IDTienda { get; set; }
       public static int Global_IDCliente{ get; set; }
        public static List<Creditos> Global_ListCreditosPorTiendaAnualidad { get; set; } = new List<Creditos>();
        public static List<Creditos> Global_ListCreditosPorTiendaValorFuturo{ get; set; } = new List<Creditos>();
        public static String NombreTienda { get; set; }
        public static String NombreCliente { get; set; }
        public static Creditos CreditoGlobal { get; set; }=new Creditos();
        public static CreditoValorFuturo CreditoVFGlobal { get; set; }=new CreditoValorFuturo();
        public static AnualidadCronogramaPagos CreditoAnualidadlobal { get; set; }=new AnualidadCronogramaPagos();
        public static Cliente ClienteGlobal { get; set; }=new Cliente();
        public decimal CalcularMontoMoratorio(int dias, Creditos credito)
        {
            decimal montoCredito = credito.MontoCredito;
            decimal interesMora =( (credito.TasaMora) / 100m);
            decimal resultado = montoCredito * ((decimal)Math.Pow((double)(1 + (interesMora / 30m)), (double)(dias)) - 1);
             resultado = Math.Round(resultado, 2);
            return resultado;
        }
   
    }

}
