using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Contexts;
using System.Diagnostics.Contracts;

namespace Datos
{
    public class DCredito
    {
        public String Registrar(Creditos oCreditos)
        {
            
                using (var dbContext = new BDEFEntities())
                {
                    using (var transaction = dbContext.Database.BeginTransaction())
                    {
                        try
                        {
                            // Agregar y guardar el crédito
                            dbContext.Creditos.Add(oCreditos);
                            dbContext.SaveChanges();

                            transaction.Commit();
                        }
                        catch (DbUpdateException ex)
                        {
                            transaction.Rollback();
                            // Capturar y manejar la excepción interna
                            var innerException = ex.InnerException;
                            while (innerException != null)
                            {
                                Console.WriteLine(innerException.Message);
                                innerException = innerException.InnerException;
                            }
                            Console.WriteLine("DbUpdateException: " + ex.Message);
                            throw;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            Console.WriteLine("Error al registrar el crédito: " + ex.Message);
                            throw;
                        }
                    }
                    return "Registrado exitosamente";
                }
           
            

        }

        public String Modificar(Creditos oCreditos)
        {
            try
            {
                using (var context = new BDEFEntities())
                {
                    Creditos CreditosTemp = context.Creditos.Find(oCreditos.ID);
                    CreditosTemp.TipoCredito = oCreditos.TipoCredito;
                    CreditosTemp.MontoCredito = oCreditos.MontoCredito;
                    CreditosTemp.Plazo = oCreditos.Plazo;
                    CreditosTemp.TEA = oCreditos.TEA;
                    CreditosTemp.TasaMora = oCreditos.TasaMora;
                    CreditosTemp.DiasGracia = oCreditos.DiasGracia;
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
                    Creditos CreditosTemp = context.Creditos.Find(ID);
                    context.Creditos.Remove(CreditosTemp);
                    context.SaveChanges();
                }
                return "Eliminado exitosamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<Creditos> ListarTodo(int IDtienda)
        {
            List<Creditos> Creditoss = new List<Creditos>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    Creditoss = context.Creditos.Where(c =>c.Tienda_ID.Equals(IDtienda)).ToList();
                }
                return Creditoss;
            }
            catch (Exception ex)
            {
                return Creditoss;
            }
        }
        public List<Creditos> ListarTodoPorCliente(int IDCliente)
        {
            List<Creditos> Creditoss = new List<Creditos>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    Creditoss = context.Creditos.Where(c => c.Cliente_ID.Equals(IDCliente)).ToList();
                }
                return Creditoss;
            }
            catch (Exception ex)
            {
                return Creditoss;
            }
        }

        public List<Creditos> ListarTodoPorClienteTienda(int idCliente, int idTienda)
        {
            List<Creditos> creditos = new List<Creditos>();

            try
            {
                using (var context = new BDEFEntities())
                {
                    creditos = context.Creditos.Where(c => c.Cliente_ID.Equals(idCliente) && c.Tienda_ID.Equals(idTienda)).ToList();
                }
                return creditos;
            }
            catch (Exception ex)
            {
                return creditos;
            }
        }
        public List<Creditos> ListarTodoCreditossFiltradosValorFuturo(int id)
        {
            List<Creditos> creditos = new List<Creditos>();

            try
            {
                using (var context = new BDEFEntities())
                {
                    creditos = context.Creditos.Where(c => c.TipoCredito.Equals("ValorFuturo") && c.Tienda_ID.Equals(id)).ToList();
                }
                return creditos;
            }
            catch (Exception ex)
            {
                return creditos;
            }
        }
        public List<Creditos> ListarTodoCreditossFiltradosAnualidad(int idTienda)
        {
            List <Creditos>  creditos = new List<Creditos>();
            try
            {
                using (var context = new BDEFEntities())
                {
                    creditos = context.Creditos.Where(c => c.TipoCredito.Equals("Anualidad") && c.Tienda_ID.Equals(idTienda)).ToList();
                }
                return creditos;
            }
            catch (Exception ex)
            {
                return creditos;
            }
        }
        public void Pagar(int idCredito, DateTime fecha,int dias)
        {
            Creditos CreditosTemp;
            try
            {
                using (var context = new BDEFEntities())
                {
                    CreditosTemp = context.Creditos.Find(idCredito);
                    CreditosTemp.EstadoPago=true;
                    CreditosTemp.FechaPago= fecha;
                    CreditosTemp.Interes= ClasesGlobalDatos.Interes;
                    CreditosTemp.PagoFinal= ClasesGlobalDatos.PagoFinal;
                    CreditosTemp.Plazo = dias;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void ModificarEstadoPagoTrue(int idCredito)
        {
            Creditos CreditosTemp;
            try
            {
                using (var context = new BDEFEntities())
                {
                    CreditosTemp = context.Creditos.Find(idCredito);
                    CreditosTemp.EstadoPago = true;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void PagarAnualidad(int idCredito, DateTime fecha)
        {
            Creditos CreditosTemp;
            try
            {
                using (var context = new BDEFEntities())
                {
                    CreditosTemp = context.Creditos.Find(idCredito);
                    CreditosTemp.EstadoPago = true;
                    CreditosTemp.FechaPago = fecha;
                    CreditosTemp.Interes = ClasesGlobalDatos.Interes_Anualidad;
                    CreditosTemp.PagoFinal = ClasesGlobalDatos.PagoFinal_Anualidad;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void CalcularPagoFinalInteres(int idCredito, decimal cuota )
        {
            decimal PagoFinal;
            decimal interes;
            Creditos CreditosTemp;
            try
            {
                using (var context = new BDEFEntities())
                {
                    decimal Montocredito;
                    CreditosTemp = context.Creditos.Find(idCredito);
                    Montocredito = CreditosTemp.MontoCredito;
                    PagoFinal = CreditosTemp.Plazo*cuota;
                    CreditosTemp.PagoFinal = PagoFinal;
                    interes = PagoFinal - Montocredito;
                    CreditosTemp.Interes = interes;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void SumarCuotas(int IDCredito, decimal cuota)
        {
            Creditos creditotemp;
            decimal? interes;
            try
            {
                using (var context = new BDEFEntities())
                {
                    creditotemp = context.Creditos.Find(IDCredito);
                    creditotemp.PagoFinal = creditotemp.PagoFinal+cuota;
                    interes = creditotemp.PagoFinal - creditotemp.MontoCredito;
                    if (interes<0)
                    {
                        interes = 0m;
                    }
                    creditotemp.Interes = interes;
                    ClasesGlobalDatos.Interes = creditotemp.Interes;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }

        }
        public decimal CalcularInteres(int IDCredito)
        {
            decimal interes;
            decimal? PagoFinal = null;
            Creditos creditotemp;
            try
            {
                using (var context = new BDEFEntities())
                {
                    creditotemp = context.Creditos.Find(IDCredito);
                    PagoFinal = creditotemp.PagoFinal; // PagoFinal puede ser nulo

                    if (PagoFinal.HasValue) // Verificar si PagoFinal tiene un valor
                    {
                        interes = PagoFinal.Value - creditotemp.MontoCredito;
                        return interes;
                    }
                    else
                    {
                        // Puedes asignar un valor predeterminado o lanzar una excepción, según tu lógica de negocio
                        throw new InvalidOperationException("El PagoFinal es nulo.");
                    }

                } 
                   
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public Creditos ObtenerCredito(int ID)
        {
            Creditos creditotemp;
            try
            {
                using (var context = new BDEFEntities())
                {
                    creditotemp = context.Creditos.Find(ID);
                    context.SaveChanges();
                }
                return creditotemp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
