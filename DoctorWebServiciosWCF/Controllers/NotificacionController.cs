using DoctorWebServiciosWCF.Model;
using DoctorWebServiciosWCF.Model.ORM;
using DoctorWebServiciosWCF.Models.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Controllers
{
    public class NotificacionController
    {
        public ResultadoServicioPaginado<Notificacion> ObtenerTodos(string nombre = null, int pagina = 0, int numeroFilas = 30)
        {
            var resultado = new ResultadoServicioPaginado<Notificacion>();
            try
            {
                using (var db = new ContextoBD())
                {
                    var cantidadRegistros = db.Notificaciones.Count();
                    var cantidadPaginas = (int)Math.Ceiling(cantidadRegistros / (double)numeroFilas);

                    IQueryable<Notificacion> consulta = db.Notificaciones
                        .OrderBy(notificacion => notificacion.Nombre);

                    if (!String.IsNullOrEmpty(nombre))
                        consulta = consulta.Where(notificaion => notificaion.Nombre.Contains(nombre));

                    var notificaciones = consulta.Skip(pagina * numeroFilas)
                        .Take(numeroFilas).ToList();

                    resultado.Inicializar(
                        PaginaActual: pagina,
                        CantidadFilas: numeroFilas,
                        CantidadPaginas: cantidadPaginas,
                        Contenido: notificaciones
                    );
                }
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        internal ResultadoProceso Borrar(string codigo)
        {
            var resultado = new ResultadoProceso();
            try
            {
                int id = 0;
                if (!int.TryParse(codigo, out id))
                    throw new FormatException("el codigo debe ser un numero.");

                using (var db = new ContextoBD())
                {
                    var notificacion = db.Notificaciones.Find(id);
                    if (notificacion != null)
                    {
                        db.Notificaciones.Remove(notificacion);
                        db.SaveChanges();
                    }
                    else
                    {
                        resultado.Mensaje = "No se encontro la notificacion con el codigo indicado.";
                    }
                }
                resultado.SinProblemas = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        internal ResultadoProceso Guardar(Notificacion notificacion)
        {
            var resultado = new ResultadoProceso();
            try
            {
                using (var db = new ContextoBD())
                {
                    if (notificacion.NotificacionId > 0)
                    {
                        var registrada = db.Notificaciones.Find(notificacion.NotificacionId);
                        db.Entry(registrada).CurrentValues.SetValues(notificacion);
                    }
                    else
                        db.Notificaciones.Add(notificacion);
                    db.SaveChanges();
                }
                resultado.SinProblemas = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        public ResultadoServicio<Notificacion> Obtener(string nombre)
        {
            var resultado = new ResultadoServicio<Notificacion>();
            try
            {
                using (var db = new ContextoBD())
                {
                    var notificaciones = db.Notificaciones.Where( notificaion => notificaion.Nombre == nombre);

                    var notificacion = notificaciones.ToList().First<Notificacion>();

                    if (notificacion == null)
                        throw new Exception("No se encontro el registro que busca");

                    resultado.Inicializar(
                        Contenido: notificacion
                    );
                }
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        public ResultadoServicio<Notificacion> Obtener(int codigo)
        {
            var resultado = new ResultadoServicio<Notificacion>();
            try
            {                
                using (var db = new ContextoBD())
                {
                    var notificacion = db.Notificaciones.Find(codigo);

                    if (notificacion == null)
                        throw new Exception("No se encontro el registro que busca");

                    resultado.Inicializar(
                        Contenido: notificacion
                    );
                }
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

    }
}