using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoctorWebASP.Models;

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
                db.Calendarios.Add(calendario);
                db.SaveChanges();
                return RedirectToAction("Index");
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
    }
}
