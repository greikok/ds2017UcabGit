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
    public class RecursoHospitalariosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RecursoHospitalarios
        public ActionResult Index()
        {
            var recursosHospitalarios = db.RecursosHospitalarios.Include(r => r.Almacen);
            return View(recursosHospitalarios.ToList());
        }

        // GET: RecursoHospitalarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecursoHospitalario recursoHospitalario = db.RecursosHospitalarios.Find(id);
            if (recursoHospitalario == null)
            {
                return HttpNotFound();
            }
            return View(recursoHospitalario);
        }

        // GET: RecursoHospitalarios/Create
        public ActionResult Create()
        {
            ViewBag.AlmacenId = new SelectList(db.Almacenes, "AlmacenId", "AlmacenId");
            return View();
        }

        // POST: RecursoHospitalarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecursoHospitalarioId,Nombre,Descripcion,Tipo,Componentes,Posologia,Recomendaciones,AlmacenId")] RecursoHospitalario recursoHospitalario)
        {
            if (ModelState.IsValid)
            {
                db.RecursosHospitalarios.Add(recursoHospitalario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AlmacenId = new SelectList(db.Almacenes, "AlmacenId", "AlmacenId", recursoHospitalario.AlmacenId);
            return View(recursoHospitalario);
        }

        // GET: RecursoHospitalarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecursoHospitalario recursoHospitalario = db.RecursosHospitalarios.Find(id);
            if (recursoHospitalario == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlmacenId = new SelectList(db.Almacenes, "AlmacenId", "AlmacenId", recursoHospitalario.AlmacenId);
            return View(recursoHospitalario);
        }

        // POST: RecursoHospitalarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecursoHospitalarioId,Nombre,Descripcion,Tipo,Componentes,Posologia,Recomendaciones,AlmacenId")] RecursoHospitalario recursoHospitalario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recursoHospitalario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AlmacenId = new SelectList(db.Almacenes, "AlmacenId", "AlmacenId", recursoHospitalario.AlmacenId);
            return View(recursoHospitalario);
        }

        // GET: RecursoHospitalarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecursoHospitalario recursoHospitalario = db.RecursosHospitalarios.Find(id);
            if (recursoHospitalario == null)
            {
                return HttpNotFound();
            }
            return View(recursoHospitalario);
        }

        // POST: RecursoHospitalarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RecursoHospitalario recursoHospitalario = db.RecursosHospitalarios.Find(id);
            db.RecursosHospitalarios.Remove(recursoHospitalario);
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
