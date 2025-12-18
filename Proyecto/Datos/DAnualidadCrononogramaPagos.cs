using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DAnualidadCrononogramaPagos
    {
        public String Registrar(AnualidadCronogramaPagos oAnualidadCronogramaPagos)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    context.AnualidadCronogramaPagos.Add(oAnualidadCronogramaPagos);
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
                    AnualidadCronogramaPagos AnualidadCronogramaPagosTemp = context.AnualidadCronogramaPagos.Find(ID);
                    context.AnualidadCronogramaPagos.Remove(AnualidadCronogramaPagosTemp);
                    context.SaveChanges();
                }
                return "Eliminado exitosamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<AnualidadCronogramaPagos> ListarTodo(int idCredito)
        {
            List<AnualidadCronogramaPagos> listAnualidadCronogramaPagoss = new List<AnualidadCronogramaPagos>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    listAnualidadCronogramaPagoss = context.AnualidadCronogramaPagos.Where(cr =>cr.Credito_ID.Equals(idCredito)).ToList();
                }
                return listAnualidadCronogramaPagoss;
            }
            catch (Exception ex)
            {
                return listAnualidadCronogramaPagoss;
            }
        }
        public AnualidadCronogramaPagos ValidarObjetoRepetido(int idCredito)
        {
            AnualidadCronogramaPagos creditoAnualidadTemp=null;
            try
            {
                using (var context = new BDEFEntities())
                {
                    creditoAnualidadTemp = context.AnualidadCronogramaPagos.FirstOrDefault(CA => CA.Credito_ID==idCredito);
                }
                return creditoAnualidadTemp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void ModificarEstadoPagoAnualidad(int idAnualidad)
        {
            AnualidadCronogramaPagos creditoAnualidadTemp;
            try
            {
                using (var context = new BDEFEntities())
                {
                    creditoAnualidadTemp = context.AnualidadCronogramaPagos.Find(idAnualidad);
                    creditoAnualidadTemp.EstadoPago= true;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }
        public List<AnualidadCronogramaPagos> ObtenerListaAnualidades(int IDCredito)
        {
            List<AnualidadCronogramaPagos> listAnualidadCronogramaPagoss = new List<AnualidadCronogramaPagos>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    listAnualidadCronogramaPagoss = context.AnualidadCronogramaPagos.Where(cr => cr.Credito_ID.Equals(IDCredito)).ToList();
                }
                return listAnualidadCronogramaPagoss;
            }
            catch (Exception ex)
            {
                return listAnualidadCronogramaPagoss;
            }
        }



        /*public void ModificarEstadoPagoTodos(List<AnualidadCronogramaPagos> listAnualidades)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    foreach (AnualidadCronogramaPagos creditoAnualidadTemp in listAnualidades)
                    {
                        AnualidadCronogramaPagos creditoAnualidad = context.AnualidadCronogramaPagos.Find( creditoAnualidadTemp.ID);
                        creditoAnualidad.EstadoPago=true;
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }*/

        public void Pagar(List<AnualidadCronogramaPagos> listAnualidades , int mes, int plazo, decimal MontoCredito, decimal Cuota,decimal TasaMora, bool proceso)
        {
            decimal interes = 0;
            decimal pagoFinal = 0;
            decimal numerador = 0;
            decimal denominador = 0;
            decimal TEM = ClasesGlobalDatos.TEM_Anualidad;
            int dias = 0;
            decimal parte = 0;
            try
            {
                using (var context = new BDEFEntities())
                {
                    //Modificamos el estado de pago de las anualidades
                    foreach (AnualidadCronogramaPagos creditoAnualidadTemp in listAnualidades)
                    {
                        AnualidadCronogramaPagos creditoAnualidad = context.AnualidadCronogramaPagos.Find(creditoAnualidadTemp.ID);
                        creditoAnualidad.EstadoPago = true;
                    }

                    //Calculamos el pago final y el interes
                    if (proceso == false)
                    {
                        numerador = (Cuota * plazo);
                        denominador = (decimal)Math.Pow((double)(1 + TEM), (plazo - mes));
                        pagoFinal = numerador / denominador;
                        interes = pagoFinal - MontoCredito;
                    }
                    else if (proceso)
                    {
                        decimal PagoSinMora = Cuota * plazo;
                        dias = (mes - plazo) * 30;
                        TasaMora = (TasaMora / 100m);
                        parte = (decimal)Math.Pow((double)(1m + (TasaMora / 30m)), (dias));
                        decimal interesOperacion = MontoCredito * (parte - 1m);
                        pagoFinal = PagoSinMora + interesOperacion;
                        interes = pagoFinal - MontoCredito;

                    }


                    ClasesGlobalDatos.Interes_Anualidad = interes;
                    ClasesGlobalDatos.PagoFinal_Anualidad = pagoFinal;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public AnualidadCronogramaPagos ObtenerCreditoAnualidad(int IDCredito)
        {
            AnualidadCronogramaPagos creditotemp;
            try
            {
                using (var context = new BDEFEntities())
                {
                    creditotemp = context.AnualidadCronogramaPagos.FirstOrDefault(c => c.Credito_ID == IDCredito);
                }
                return creditotemp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public decimal CalcularCuota(Creditos credito, string tipoGracia)
        {
            decimal TEM;
            int plazoConGracia;
            decimal denominador;
            decimal numerador;
            decimal montoCredito;
            decimal cuota;
            int plazo;
            decimal TEA;
            int diasGracia;
            decimal montoCreditoActualizado;
            //Calculoss

            montoCredito = credito.MontoCredito;
            plazo = credito.Plazo;
            TEA = (credito.TEA / 100m);
            diasGracia = (int)credito.DiasGracia;

            //Calculo de TEM
            TEM = (decimal)Math.Pow((double)(1 + TEA), (double)(30.0 / 360.0)) - 1;
            ClasesGlobalDatos.TEM_Anualidad= TEM;

            /*------Calculo del nuevo plazo---------*/
            plazoConGracia = (plazo * 30) - diasGracia;


            if (tipoGracia=="Total")
            {
                //Credito actualizado
                montoCreditoActualizado = montoCredito * ((decimal)Math.Pow(((double)(1 + TEM)), ((double)(diasGracia / 30m))));

                denominador = montoCreditoActualizado * TEM;
                numerador = 1 - ((decimal)(Math.Pow((double)(1 + TEM), -((double)(plazoConGracia / 30m)))));
                
                //Calculo de la Cuota
                cuota = denominador / numerador;
                cuota = Math.Round(cuota, 3);
                return cuota;
            }
            else
            {
                //Credito actualizado
                montoCreditoActualizado = montoCredito;

                denominador = montoCreditoActualizado * TEM;
                numerador = 1 - ((decimal)(Math.Pow((double)(1 + TEM), -((double)(plazoConGracia / 30m)))));
                
                //Calculo de la Cuota
                cuota = denominador / numerador;
                cuota = Math.Round(cuota, 3);


                return cuota;  
            }
        }
    }
}
