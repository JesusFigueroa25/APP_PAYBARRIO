using Datos;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DCreditoValorFuturo
    {
        decimal TasaEfectivaDiaria;
        public String Registrar(CreditoValorFuturo oCreditoValorFuturo)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.CreditoValorFuturo.Add(oCreditoValorFuturo);
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
                    CreditoValorFuturo CreditoValorFuturoTemp = context.CreditoValorFuturo.Find(ID);
                    context.CreditoValorFuturo.Remove(CreditoValorFuturoTemp);
                    context.SaveChanges();
                }
                return "Eliminado exitosamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<CreditoValorFuturo> ListarTodoIDCredito(int idcredito)
        {
            List<CreditoValorFuturo> CreditoValorFuturos = new List<CreditoValorFuturo>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    CreditoValorFuturos = context.CreditoValorFuturo.Where(vf=>vf.Credito_ID.Equals(idcredito)).ToList();
                }
                return CreditoValorFuturos;
            }
            catch (Exception ex)
            {
                return CreditoValorFuturos;
            }
        }
        public CreditoValorFuturo ObtenerCreditoValorFuturo(int IDCredito)
        {
            CreditoValorFuturo creditotemp;
            try
            {
                using (var context = new BDEFEntities())
                {
                    creditotemp = context.CreditoValorFuturo.FirstOrDefault(c => c.Credito_ID == IDCredito);
                }
                return creditotemp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<CreditoValorFuturo> ListarTodo()
        {
            List<CreditoValorFuturo> CreditoValorFuturos = new List<CreditoValorFuturo>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    CreditoValorFuturos = context.CreditoValorFuturo.ToList();
                }
                return CreditoValorFuturos;
            }
            catch (Exception ex)
            {
                return CreditoValorFuturos;
            }
        }
        public void CalcularValorFuturo(CreditoValorFuturo oCVF, Creditos oCredito, int dias, DateTime fecha)
        {
            
            int IDCreditoVF = oCVF.ID;
            decimal valorfuturoCalcular;
            decimal MontoCredito= oCredito.MontoCredito;
            decimal TEA = ((oCredito.TEA)/100m);
            decimal interes = 0;


            //TEM
            TasaEfectivaDiaria = (decimal)Math.Pow((double)(1 + TEA), (double)(1.0 / 360.0)) - 1;
            ClasesGlobalDatos.TEM_CVF = TasaEfectivaDiaria;

            // Valor Futuro
            valorfuturoCalcular = MontoCredito * (decimal)Math.Pow((double)(1 + TasaEfectivaDiaria), dias);

            try
            {
                using (var context = new BDEFEntities())
                {

                    CreditoValorFuturo creditoVF_Temp = context.CreditoValorFuturo.Find(IDCreditoVF);

                    creditoVF_Temp.EstadoPago = true;
                    creditoVF_Temp.FechaPago = fecha;
                    creditoVF_Temp.ValorFuturo = valorfuturoCalcular;
                    interes = valorfuturoCalcular - MontoCredito;

                    ClasesGlobalDatos.PagoFinal = valorfuturoCalcular;
                    ClasesGlobalDatos.Interes = interes;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
            
        }
        public CreditoValorFuturo ValidarObjetoRepetido(int idcredito)
        {
            CreditoValorFuturo creditoValorFuturo = null;
            try
            {
                using (var context = new BDEFEntities())
                {
                    creditoValorFuturo = context.CreditoValorFuturo.FirstOrDefault(vf =>vf.Credito_ID== idcredito);
                }
                return creditoValorFuturo;
            }
            catch (Exception ex)
            {
                return creditoValorFuturo;
            }
        }

        /* public void Pagar(int id, DateTime fecha,int  mes, int plazo, decimal MontoCredito, decimal TasaMora, bool proceso)
        {
            CreditoValorFuturo creditoValorFuturo;
            decimal interes=0;
            decimal pagoFinal = 0;
            decimal numerador = 0;
            decimal denominador = 0;
            decimal TEM= ClasesGlobalDatos.TEM_CVF;
            int dias=0;
            decimal parte=0;
            try
            {
                using (var context = new BDEFEntities())
                {
                   
        creditoValorFuturo = context.CreditoValorFuturo.FirstOrDefault(vf => vf.Credito_ID == id);
                    creditoValorFuturo.EstadoPago = true;
                    creditoValorFuturo.FechaPago = fecha;
                    if (proceso==false)
                    {
                        numerador = (creditoValorFuturo.ValorFuturo);
                        denominador = (decimal) Math.Pow((double)(1 + TEM), (plazo - mes));
                        pagoFinal = numerador / denominador;
                        interes = pagoFinal - MontoCredito;
                    }else if(proceso)
                    {
                        decimal VF = creditoValorFuturo.ValorFuturo;
    dias = (mes - plazo)*30;
                        TasaMora =(TasaMora / 100m);
                         parte = (decimal) Math.Pow((double)(1m + (TasaMora / 30m)), (dias));
                        decimal interesOperacon = MontoCredito * (parte - 1m);
    pagoFinal = VF + interesOperacon;
                        interes = pagoFinal - MontoCredito;

                    }
creditoValorFuturo.PagoFinal = creditoValorFuturo.ValorFuturo;

ClasesGlobalDatos.PagoFinal = pagoFinal;
ClasesGlobalDatos.Interes = interes;
context.SaveChanges();
                }

            }
            catch (Exception ex)
            {

            }
        }*/
        public void CancelarPagar(int id)
        {
            try
            {
                using (var context = new BDEFEntities())
                {

                    CreditoValorFuturo creditoValorFuturo = context.CreditoValorFuturo.FirstOrDefault(vf => vf.Credito_ID == id);
                    creditoValorFuturo.EstadoPago = false;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
