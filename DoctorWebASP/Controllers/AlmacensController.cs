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
    public class AlmacensController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Almacens
        public ActionResult Index()
        {
            var almacenes = db.Almacenes.Include(a => a.Centromedico);
            return View(almacenes.ToList());
        }

        // GET: Almacens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Almacen almacen = db.Almacenes.Find(id);
            if (almacen == null)
            {
                return HttpNotFound();
            }
            return View(almacen);
        }

        // GET: Almacens/Create
        public ActionResult Create()
        {
            ViewBag.CentroMedicoId = new SelectList(db.CentrosMedicos, "CentroMedicoId", "Nombre");
            return View();
        }

        // POST: Almacens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlmacenId,Disponible,CentroMedicoId")] Almacen almacen)
        {
            if (ModelState.IsValid)
            {
                db.Almacenes.Add(almacen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CentroMedicoId = new SelectList(db.CentrosMedicos, "CentroMedicoId", "Nombre", almacen.CentroMedicoId);
            return View(almacen);
        }

        // GET: Almacens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Almacen almacen = db.Almacenes.Find(id);
            if (almacen == null)
            {
                return HttpNotFound();
            }
            ViewBag.CentroMedicoId = new SelectList(db.CentrosMedicos, "CentroMedicoId", "Nombre", almacen.CentroMedicoId);
            return View(almacen);
        }

        // POST: Almacens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlmacenId,Disponible,CentroMedicoId")] Almacen almacen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(almacen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CentroMedicoId = new SelectList(db.CentrosMedicos, "CentroMedicoId", "Nombre", almacen.CentroMedicoId);
            return View(almacen);
        }

        // GET: Almacens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Almacen almacen = db.Almacenes.Find(id);
            if (almacen == null)
            {
                return HttpNotFound();
            }
            return View(almacen);
        }

        // POST: Almacens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Almacen almacen = db.Almacenes.Find(id);
            db.Almacenes.Remove(almacen);
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
