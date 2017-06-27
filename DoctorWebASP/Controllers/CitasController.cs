using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoctorWebASP.Models;
using DoctorWebASP.ViewModels;
using PagedList;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DoctorWebASP.Controllers
{
    public class CitasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Citas
        /// <summary>
        /// Metodo que llama a la interfaz de consulta de citas principal, 
        /// Si el usuario conectado actual es medico se llama a IndexDoctor
        /// </summary>
        /// <returns> Interfaz de consulta de citas agendadas de paciente </returns>
        [Authorize]
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            try
            {
                var medico = db.Personas.OfType<Medico>().Single(p => p.ApplicationUser.Id == userId);
                if (medico != null)
                {
                    return RedirectToAction("IndexDoctor", "Citas", new { userId });
                }
            }
            catch (Exception e) { Console.WriteLine(e); }

            var citas = db.Citas.Where(c => c.Paciente.ApplicationUser.Id == userId).ToList();
            if (citas.Count == 0)
            {
                string mensaje = "Usted no ha registrado ninguna cita";
                return RedirectToAction("SadFace", "Citas", new { mensaje });
            }
            else
            {
                return View(db.Citas.Where(c => c.Paciente.ApplicationUser.Id == userId).ToList());

            }
        }

        // GET: Citas/IndexDoctor
        /// <summary>
        /// Este metodo muestra todas las consultas que tenga un doctor, las cuales fueron agendadas previamente
        /// por un paciente.
        /// </summary>
        /// <param name="userId"> Este parametro es el id el usuario conectado actualmente, fue pasado desde el metodo Index</param>
        /// <returns> Interfaz de consulta de citas de doctor </returns>
        public ActionResult IndexDoctor(string userId)
        {
            var citas = db.Citas.Where(c => c.Calendario.Medico.ApplicationUser.Id == userId).ToList();
            if (citas.Count == 0)
            {
                string mensaje = "Usted no tiene citas por atender";
                return RedirectToAction("SadFace", "Citas", new { mensaje });
            }
            else
            {
                var viewModel = new DoctorCitaViewModel
                {
                    Citas = citas
                };
                return View("IndexDoctor", viewModel);

            }
        }


        // GET: Citas/SolicitarCita
        /// <summary>
        /// Metodo inicial para la ejecucion del proceso de solicitud de citas.
        /// Este metodo lista todos los centros medicos almacenados en la DB y los envia a la vista.
        /// </summary>
        /// <param name="i"></param>
        /// <returns> Interfaz de seleccion de centro medico </returns>
        [Authorize]
        public ActionResult SolicitarCita(int? i)
        {
            var centrosMedicos = new SelectList("");
            try
            {
                centrosMedicos = new SelectList(db.CentrosMedicos.ToList(), "Rif", "Nombre");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new HttpNotFoundResult("La base de datos no ha podido ser contactada");
            }

            var viewModel = new CentrosMedicosViewModel
            {
                CentrosMedicos = centrosMedicos
            };

            return View("SolicitarCita", viewModel);
        }

        // POST: Citas/SolicitarCita
        /// <summary>
        /// Metodo que ejecuta la logica luego de la seleccion de un centro medico.
        /// Este metodo envia al siguiente los parametros necesarios para la seleccion de especialidad medica
        /// </summary>
        /// <param name="centroMedico"></param>
        /// <returns> Llamado a SeleccionarEspecialidad, envia el centro medico seleccionado (objeto) </returns>
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SolicitarCita([Bind(Prefix = "CentroMedico")] string centroMedico)
        {
            CentroMedico cMedico = new CentroMedico();
            try
            {
                cMedico = db.CentrosMedicos.Single(m => m.Rif == centroMedico);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new HttpNotFoundResult("La base de datos no ha podido ser contactada");
            }

            return RedirectToAction("SeleccionarEspecialidad", cMedico);
        }

        // GET: Citas/SeleccionarEspecialidad
        /// <summary>
        /// Metodo que permite al usuario seleccionar la especialidad medica que desea para su cita medica.
        /// Este metodo busca en la base de datos los medicos que se encuentren disponibles para el centro medico
        /// previamente seleccionado y lista uno por categoria disponible.
        /// </summary>
        /// <param name="cMedico"></param>
        /// <returns> Interfaz de seleccion de especialidad medica </returns>
        [Authorize]
        public ActionResult SeleccionarEspecialidad(CentroMedico cMedico)
        {
            var especialidadesMedicas = new SelectList("");
            try
            {
                especialidadesMedicas = new SelectList(db.Personas.OfType<Medico>().Where(m => m.CentroMedico.CentroMedicoId == cMedico.CentroMedicoId).Select(c => c.EspecialidadMedica).Distinct().ToList(), "EspecialidadMedicaId", "Nombre");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new HttpNotFoundResult("La base de datos no ha podido ser contactada");
            }

            var viewModel = new EspecialidadViewModel
            {
                EspecialidadesMedicas = especialidadesMedicas,
                CentroMedicoId = cMedico.CentroMedicoId,

            };

            return View("SeleccionarEspecialidad", viewModel);
        }

        // POST: Citas/SeleccionarEspecialidad
        /// <summary>
        /// Metodo que recibe la seleccion de parametros de especialidad medica y envia dichos parametros
        /// al siguiente metodo en el proceso de ejecucion
        /// </summary>
        /// <param name="espMedica"> Id de la especialidad medica </param>
        /// <param name="centroMedicoId"> Id del centro medico </param>
        /// <returns> Redireccion a metodo SeleccionarMedico </returns>
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SeleccionarEspecialidad([Bind(Prefix = "EspecialidadMedica")] int espMedica,
                                                    [Bind(Prefix = "CentroMedicoId")] int centroMedicoId)
        {
            return RedirectToAction("SeleccionarMedico", "Citas", new { espMedica, centroMedicoId });
        }

        // GET: Citas/SeleccionarMedico
        /// <summary>
        /// Metodo que permite la seleccion del medico para ser consultado por el paciente.
        /// El metodo lista todos los medicos disponibles para una especialidad seleccionada en el centro medico.
        /// </summary>
        /// <param name="espMedica"></param>
        /// <param name="centroMedicoId"></param>
        /// <returns> Interfaz de seleccion de medico </returns>
        [Authorize]
        public ActionResult SeleccionarMedico(int espMedica, int centroMedicoId)
        {
            var medicos = new SelectList("");
            try
            {
                CentroMedico centroMedico = db.CentrosMedicos.Single(c => c.CentroMedicoId == centroMedicoId);
                EspecialidadMedica especialidadMedica = db.EspecialidadesMedicas.Single(e => e.EspecialidadMedicaId == espMedica);
                medicos = new SelectList(db.Personas.OfType<Medico>().Where(p => p.CentroMedico.CentroMedicoId == centroMedicoId && p.EspecialidadMedica.EspecialidadMedicaId == espMedica).ToList(), "PersonaId", "ConcatUserName");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new HttpNotFoundResult("La base de datos no ha podido ser contactada");
            }
            var viewModel = new MedicoViewModel
            {
                Medicos = medicos,
                CentroMedicoId = centroMedicoId
            };

            return View("SeleccionarMedico", viewModel);
        }

        // POST: Citas/SeleccionarMedico
        /// <summary>
        /// Metodo que envia los parametros a la accion final de seleccion de horario
        /// </summary>
        /// <param name="medicoId"> Id del medico seleccionado </param>
        /// <param name="centroMedicoId"> Id del centro medico seleccionado </param>
        /// <returns> Redireccion a accion SeleccionarHorario</returns>
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SeleccionarMedico([Bind(Prefix = "MedicoId")] string medicoId,
                                              [Bind(Prefix = "CentroMedicoId")] int centroMedicoId)
        {
            return RedirectToAction("SeleccionarHorario", "Citas", new { medicoId, centroMedicoId });
        }

        // GET: Citas/SeleccionarHorario
        /// <summary>
        /// Metodo que permite finalmente la seleccion de un horario para finalmente generar una cita.
        /// </summary>
        /// <param name="medicoId"> Id del medico seleccionado </param>
        /// <param name="centroMedicoId"> Id del centro medico seleccionado </param>
        /// <param name="page"> Cantidad de items por pagina en PagedList </param>
        /// <returns> Interfaz de seleccion de horario </returns>
        [Authorize]
        public ActionResult SeleccionarHorario(string medicoId, int centroMedicoId, int? page)
        {
            const int pagesize = 10; // Numero de items en una pagina
            int pageNumber = (page ?? 1); // Pagina actual
            int mdId = int.Parse(medicoId);

            var viewModel = new CalendarioViewModel
            {
                ListaFechas = db.Calendarios.Where(m => m.Medico.PersonaId == mdId && m.Disponible == 1).OrderBy(m => m.HoraInicio).ToPagedList(pageNumber, pagesize),
                MedicoId = medicoId,
                CentroMedicoId = centroMedicoId,
            };

            if (viewModel.ListaFechas.Count == 0)
            {
                string mensaje = "El medico seleccionado no tiene disponibilidad";
                return RedirectToAction("SadFace", "Citas", new { mensaje });
            }
            else
            {
                return View("SeleccionarHorario", viewModel);
            }

        }

        // Citas/GenerarCita
        /// <summary>
        /// Metodo que genera el registro de cita en la base de datos
        /// </summary>
        /// <param name="calendarioId"> Id de la fecha del calendario (evento) seleccionado </param>
        /// <param name="medicoId"> Id del medico seleccionado </param>
        /// <param name="centroMedicoId"> Id del centro medico seleccionado </param>
        /// <returns> Index() </returns>
        [Authorize]
        public ActionResult GenerarCita(int calendarioId, string medicoId, int centroMedicoId)
        {
            try
            {
                var cita = new Cita();
                string userId = User.Identity.GetUserId();
                cita.CentroMedico = db.CentrosMedicos.Single(m => m.CentroMedicoId == centroMedicoId);

                cita.Paciente = db.Personas.OfType<Paciente>().Single(p => p.ApplicationUser.Id == userId);
                var calendario = db.Calendarios.Single(c => c.CalendarioId == calendarioId);
                cita.Calendario = calendario;
                cita.CitaId = 0;

                if (ModelState.IsValid)
                {
                    db.Citas.Add(cita);
                    // Finalmente colocamos la Fecha Reservada como NO disponible
                    calendario.Disponible = 0;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new HttpNotFoundResult("La base de datos no ha podido ser contactada");
            }

            return View();
        }

        // GET: Citas/Consultarcitas
        /// <summary>
        /// Metodo que permite la consulta de citas generadas por el usuario
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult ConsultarCitas()
        {
            string userId = User.Identity.GetUserId();
            return View("ConsultarCitas", db.Citas.Where(c => c.Paciente.ApplicationUser.Id == userId));
        }

        // GET: Citas/Details/5
        /// <summary>
        /// Metodo para listar los detalles de una cita seleccionada
        /// </summary>
        /// <param name="id"> Id de la cita a consultar </param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Citas.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            return View(cita);
        }

        // GET: Citas/Details/5
        /// <summary>
        /// Metodo para mostrar los detalles de consulta de un doctor
        /// </summary>
        /// <param name="id"> Id de la cita a consultar </param>
        /// <returns></returns>
        [Authorize]
        public ActionResult DetailsDoctor(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Citas.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            return View(cita);
        }

        // GET: Citas/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Citas.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            return View(cita);
        }

        // POST: Citas/Delete/5
        /// <summary>
        /// Metodo que elimina una cita previamente agendada
        /// Este metodo elimina la cita y devuelve el estado activo a la fecha liberada
        /// </summary>
        /// <param name="id"> Id de la cita a cancelar</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Cita cita = db.Citas.Find(id);
                var calendario = db.Calendarios.Single(c => c.CalendarioId == cita.Calendario.CalendarioId);
                calendario.Disponible = 1;
                db.Citas.Remove(cita);
                db.SaveChanges();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new HttpNotFoundResult("La base de datos no ha podido ser contactada");
            }
            return RedirectToAction("Index");
        }

        // GET: Citas/SadFace
        /// <summary>
        /// Metodo para informar al usuario de algun error
        /// </summary>
        /// <param name="mensaje"> Por favor colocar en mensaje: YouGotTrolled </param>
        /// <returns> Epic stuff </returns>
        public ActionResult SadFace(string mensaje)
        {

            var viewModel = new SadFaceViewModel
            {
                Mensaje = mensaje
            };
            return View("SadFace", viewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
