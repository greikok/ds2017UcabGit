using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoctorWebASP.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace DoctorWebASP.Controllers
{
    public class CalendariosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Calendarios
        public ActionResult Index()
        {
            return View(db.Calendarios.ToList());
        }

        // GET: Calendarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calendario calendario = db.Calendarios.Find(id);
            if (calendario == null)
            {
                return HttpNotFound();
            }
            return View(calendario);
        }

        // GET: Calendarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Calendarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CalendarioId,HoraInicio,HoraFin,Cancelada,Disponible")] Calendario calendario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var calendarios = new SelectList("");
                    string userID = User.Identity.GetUserId();
                    calendario.Medico = db.Personas.OfType<Medico>().Single(p => p.ApplicationUser.Id == userID);
                    calendarios = new SelectList(db.Calendarios.Where(c => c.Medico.PersonaId == calendario.Medico.PersonaId && c.HoraInicio == calendario.HoraInicio ));
                    if ((calendarios.Count() == 0) && (calendario.HoraInicio >= System.DateTime.Now ))
                    {
                        try
                        {
                            calendario.Cancelada = false;
                            calendario.HoraFin = calendario.HoraInicio.AddHours(2);
                            calendario.Disponible = 1;
                            db.Calendarios.Add(calendario);
                            db.SaveChanges();
                            return RedirectToAction("Create");
                        }
                        catch (Exception e)
                        {
                            return new HttpNotFoundResult("Error al insertar");
                        }
                    }
                    else
                        return new HttpNotFoundResult("Fecha inválida!");
                }
                catch (Exception e)
                {
                    return new HttpNotFoundResult("Error al conectarse a la base de datos!");
                }
            }

            return View(calendario);
        }

        // GET: Calendarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calendario calendario = db.Calendarios.Find(id);
            if (calendario == null)
            {
                return HttpNotFound();
            }
            return View(calendario);
        }

        // POST: Calendarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CalendarioId,HoraInicio,HoraFin,Cancelada,Disponible")] Calendario calendario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(calendario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(calendario);
        }

        // GET: Calendarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calendario calendario = db.Calendarios.Find(id);
            if (calendario == null)
            {
                return HttpNotFound();
            }
            return View(calendario);
        }

        // POST: Calendarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Calendario calendario = db.Calendarios.Find(id);
            db.Calendarios.Remove(calendario);
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

        /*public JsonResult Json()
        {
            var calendarsDates = GetCalendario();
            string cal = JsonConvert.SerializeObject(calendarsDates.ToArray());
            return Json(calendarsDates, JsonRequestBehavior.AllowGet);
        }*/



        /*private SelectList GetCalendario()
        {
            var calendarList = new SelectList("");
            string userID = User.Identity.GetUserId();
            Medico login = new Medico();
            int medicoid;
            login = db.Personas.OfType<Medico>().Single(p => p.ApplicationUser.Id == userID);
            medicoid = login.PersonaId;
            calendarList = new SelectList(db.Calendarios.Where(c => c.Medico.PersonaId == medicoid));
            return calendarList;
        }*/
        [HttpPost]
        public ActionResult Json(Calendario obj)
        {
            
            string userID = User.Identity.GetUserId();
            Medico login = new Medico();
            int medicoid;
            login = db.Personas.OfType<Medico>().Single(p => p.ApplicationUser.Id == userID);
            medicoid = login.PersonaId;
            // retrive the data from table  
            var callist = db.Calendarios.Where(c => c.Medico.PersonaId == medicoid).ToList()
                .Select(c => new {id = c.CalendarioId, title = "Tiempo de cita", start = c.HoraInicio.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"), end = c.HoraFin.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"), c.Disponible, c.Cancelada, backgroundColor = "#00a65a" });
            // Pass the "personlist" object for conversion object to JSON string
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            string jsondata = serializer.Serialize(callist);
            string path = Server.MapPath("~/Content/");
            // Write that JSON to txt file,  
            System.IO.File.WriteAllText(path + "calendario.json", jsondata);
            TempData["msg"] = "Json file Generated! check this in your App_Data folder";
            return RedirectToAction("Index");
        }
    }
}
