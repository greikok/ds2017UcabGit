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
    public class HistoriaMedicasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HistoriaMedicas
        //   public ActionResult Index()
        //    {
        //      return View(db.HistoriaMedicas.ToList());
        //   }
        public ActionResult Inicio()
        {
            return View();
        }

        
        public ActionResult Historial()
        {
            return View();
        }

        public ActionResult HistPersona()
        {
            return View();
        }

        public ActionResult AgregarHistorial()
        {
            return View();
        }
        public ActionResult RecipeLista()
        {
            return View();
        }

        public ActionResult RecPersona()
        {
            return View();
        }

        public ActionResult AgregarRecipe()
        {
            return View();
        }




        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoriaMedica historiaMedica = db.HistoriaMedicas.Find(id);
            if (historiaMedica == null)
            {
                return HttpNotFound();
            }
            return View(historiaMedica);
        }

        // GET: HistoriaMedicas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HistoriaMedicas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HistoriaMedicaId")] HistoriaMedica historiaMedica)
        {
            if (ModelState.IsValid)
            {
                db.HistoriaMedicas.Add(historiaMedica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(historiaMedica);
        }

        // GET: HistoriaMedicas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoriaMedica historiaMedica = db.HistoriaMedicas.Find(id);
            if (historiaMedica == null)
            {
                return HttpNotFound();
            }
            return View(historiaMedica);
        }

        // POST: HistoriaMedicas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HistoriaMedicaId")] HistoriaMedica historiaMedica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historiaMedica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(historiaMedica);
        }

        // GET: HistoriaMedicas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoriaMedica historiaMedica = db.HistoriaMedicas.Find(id);
            if (historiaMedica == null)
            {
                return HttpNotFound();
            }
            return View(historiaMedica);
        }

        // POST: HistoriaMedicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HistoriaMedica historiaMedica = db.HistoriaMedicas.Find(id);
            db.HistoriaMedicas.Remove(historiaMedica);
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
