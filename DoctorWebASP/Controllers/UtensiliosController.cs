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
    [Authorize(Users = "admin@gmail.com")]
    public class UtensiliosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Utensilios
        public ActionResult Index()
        {
            var utensilios = db.Utensilios.Include(u => u.Almacen);
            return View(utensilios.ToList());
        }

        // GET: Utensilios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utensilio utensilio = db.Utensilios.Find(id);
            if (utensilio == null)
            {
                return HttpNotFound();
            }
            return View(utensilio);
        }

        // GET: Utensilios/Create
        public ActionResult Create()
        {
            ViewBag.AlmacenId = new SelectList(db.Almacenes, "AlmacenId", "AlmacenId");
            return View();
        }

        // POST: Utensilios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UtensilioId,Nombre,Descripcion,CantidadDisponible,AlmacenId")] Utensilio utensilio)
        {
            if (ModelState.IsValid)
            {
                db.Utensilios.Add(utensilio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AlmacenId = new SelectList(db.Almacenes, "AlmacenId", "AlmacenId", utensilio.AlmacenId);
            return View(utensilio);
        }

        // GET: Utensilios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utensilio utensilio = db.Utensilios.Find(id);
            if (utensilio == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlmacenId = new SelectList(db.Almacenes, "AlmacenId", "AlmacenId", utensilio.AlmacenId);
            return View(utensilio);
        }

        // POST: Utensilios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UtensilioId,Nombre,Descripcion,CantidadDisponible,AlmacenId")] Utensilio utensilio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utensilio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AlmacenId = new SelectList(db.Almacenes, "AlmacenId", "AlmacenId", utensilio.AlmacenId);
            return View(utensilio);
        }

        // GET: Utensilios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utensilio utensilio = db.Utensilios.Find(id);
            if (utensilio == null)
            {
                return HttpNotFound();
            }
            return View(utensilio);
        }

        // POST: Utensilios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Utensilio utensilio = db.Utensilios.Find(id);
            db.Utensilios.Remove(utensilio);
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
