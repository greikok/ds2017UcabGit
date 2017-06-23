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
        public ActionResult Index()
        {
            return View(db.Citas.ToList());
        }

        // GET: Citas/SolicitarCita
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
        [HttpPost]
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
        public ActionResult SeleccionarEspecialidad(CentroMedico cMedico)
        {
            var especialidadesMedicas = new SelectList("");
            try
            {
                especialidadesMedicas = new SelectList(db.Personas.OfType<Medico>().Where(m => m.CentroMedico.CentroMedicoId == cMedico.CentroMedicoId).Select(c => c.EspecialidadMedica).Distinct().ToList(), "EspecialidadMedicaId", "Nombre");

            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return new HttpNotFoundResult("La base de datos no ha podido ser contactada");
            }
            
            var viewModel = new EspecialidadViewModel
            {
                EspecialidadesMedicas = especialidadesMedicas,
                CentroMedicoId = cMedico.CentroMedicoId,
                
            };

            return View("SeleccionarEspecialidad",viewModel);
        }

        // POST: Citas/SeleccionarEspecialidad
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SeleccionarEspecialidad([Bind(Prefix = "EspecialidadMedica")] int espMedica,
                                                    [Bind(Prefix = "CentroMedicoId")] int centroMedicoId)
        {
            return RedirectToAction("SeleccionarMedico", "Citas", new { espMedica, centroMedicoId });
        }

        // GET: Citas/SeleccionarMedico
        public ActionResult SeleccionarMedico(int espMedica, int centroMedicoId)
        {
            var medicos = new SelectList("");
            try
            {
                CentroMedico centroMedico = db.CentrosMedicos.Single(c => c.CentroMedicoId == centroMedicoId);
                EspecialidadMedica especialidadMedica = db.EspecialidadesMedicas.Single(e => e.EspecialidadMedicaId == espMedica);
                medicos = new SelectList(db.Personas.OfType<Medico>().Where(p => p.CentroMedico.CentroMedicoId == centroMedicoId && p.EspecialidadMedica.EspecialidadMedicaId == espMedica).ToList(), "PersonaId", "ConcatUserName");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return new HttpNotFoundResult("La base de datos no ha podido ser contactada");
            }


            var viewModel = new MedicoViewModel
            {
                Medicos = medicos,
                CentroMedicoId = centroMedicoId
            };

            return View("SeleccionarMedico",viewModel);
        }

        // POST: Citas/SeleccionarMedico
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SeleccionarMedico([Bind(Prefix = "MedicoId")] string medicoId,
                                              [Bind(Prefix = "CentroMedicoId")] int centroMedicoId)
        {
            return RedirectToAction("SeleccionarHorario", "Citas", new { medicoId, centroMedicoId });
        }

        // GET: Citas/SeleccionarHorario
        public ActionResult SeleccionarHorario(string medicoId, int centroMedicoId, int? page)
        {
            const int pagesize = 1; // Numero de items en una pagina
            int pageNumber = (page ?? 1); // Pagina actual
            int mdId = int.Parse(medicoId);

            var viewModel = new CitaViewModel
            {
                ListaFechas = db.Calendarios.Where(m => m.Medico.PersonaId == mdId && m.Disponible == 1).OrderBy(m => m.HoraInicio).ToPagedList(pageNumber,pagesize),
                MedicoId = medicoId,
                CentroMedicoId = centroMedicoId,
            };

            return View("SeleccionarHorario",viewModel);
        }

        public ActionResult GenerarCita(int calendarioId, string medicoId, int centroMedicoId)
        {
            var cita = new Cita();
            string userId = User.Identity.GetUserId();
            cita.CentroMedico = db.CentrosMedicos.Single(m => m.CentroMedicoId == centroMedicoId);

            cita.Paciente = db.Personas.OfType<Paciente>().Single(p => p.ApplicationUser.Id == userId);
            var calendario = db.Calendarios.Single(c => c.CalendarioId == calendarioId);
            cita.Evento = calendario;
            cita.CitaId = 0;
            
            if (ModelState.IsValid)
            {
                db.Citas.Add(cita);
                calendario.Disponible = 0;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Finalmente colocamos la Fecha Reservada como NO disponible


            return View();
        }

        // GET: Citas/Details/5
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

        // GET: Citas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Citas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CitaId")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                db.Citas.Add(cita);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cita);
        }

        // GET: Citas/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Citas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CitaId")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cita);
        }

        // GET: Citas/Delete/5
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cita cita = db.Citas.Find(id);
            db.Citas.Remove(cita);
            db.SaveChanges();
            return RedirectToAction("Index");
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
