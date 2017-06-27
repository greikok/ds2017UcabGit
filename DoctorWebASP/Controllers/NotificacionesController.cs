using DoctorWebASP.Controllers.Helpers;
using DoctorWebASP.Controllers.Services;
using DoctorWebASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorWebASP.Controllers
{
    public class NotificacionesController : Controller
    {
        #region Instancia NotificacionesController
        private IServicioNotificaciones Servicio { get; set; }

        public NotificacionesController() : this(new ServicioNotificaciones())
        {

        }

        public NotificacionesController(IServicioNotificaciones servicio) : base()
        {
            this.Servicio = servicio;
        }
        #endregion

        // GET: Notificaciones
        public ActionResult Index(string nombre = null, int indice = 0, int filas = 5)
        {
            var cantidadPaginas = 0;
            List<Notificacion> notificaciones = null;
            try
            {
                ViewData["error"] = null;
                notificaciones = Servicio.ObtenerTodos(out cantidadPaginas, nombre, indice, filas);
            }
            catch (Exception ex)
            {
                ViewData["error"] = ex.Message;
                notificaciones = new List<Notificacion>();
            }
            ViewData["nombre"] = nombre;
            ViewData["filas"] = filas;
            ViewData["permitirSiguiente"] = indice < cantidadPaginas-1;            
            ViewData["permitirAnterior"] = indice > 0;
            ViewData["siguienteIndice"] = indice + 1;
            ViewData["anteriorIndice"] = indice - 1;
            return View(model: notificaciones);
        }

        public ActionResult Detail(int codigo)
        {
            Notificacion model = null;
            if (codigo != 0)
            {
                model = Servicio.Obtener(codigo);
            }
            else
            {
                model = new Notificacion();
            }
            if (model != null)
            {
                this.RellenarCombos(model);
                return View(model: model);
            }
            else
                return RedirectToAction("Index");
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(FormCollection collection)
        {
            var model = new Notificacion();
            try
            {
                model.NotificacionId = 0;
                model.Actualizar(collection);
                var mensaje = String.Empty;
                var sinProblemas = Servicio.Guardar(model, out mensaje);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ModelState.AddModelError("Error", exception.Message);
                this.RellenarCombos(model);
                return View("Detail", model);
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(FormCollection collection)
        {
            var model = new Notificacion();
            try
            {
                model.Actualizar(collection);
                var mensaje = String.Empty;
                var sinProblemas = Servicio.Guardar(model, out mensaje);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                if (model == null)
                    return RedirectToAction("Index");

                ModelState.AddModelError("Error", exception.Message);

                this.RellenarCombos(model);
                return View("Detail", model);
            }
        }

        public ActionResult Delete(int codigo)
        {
            var model = Servicio.Obtener(codigo);
            if (model != null)
                return View(model: model);
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            try
            {
                var codigo = int.Parse(collection["NotificacionId"]);
                var mensaje = String.Empty;
                var sinProblemas = Servicio.Borrar(codigo, out mensaje);
            }
            catch
            {
                
            }
            return RedirectToAction("Index");
        }
    }
}