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
    public class CentroMedicoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CentroMedicoes
        public ActionResult Index()
        {
            return View(db.CentrosMedicos.ToList());
        }

        // GET: CentroMedicoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CentroMedico centroMedico = db.CentrosMedicos.Find(id);
            if (centroMedico == null)
            {
                return HttpNotFound();
            }
            return View(centroMedico);
        }

        // GET: CentroMedicoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CentroMedicoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CentroMedicoId,Nombre,Rif,Direccion,Telefono")] CentroMedico centroMedico)
        {
            if (ModelState.IsValid)
            {
                db.CentrosMedicos.Add(centroMedico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(centroMedico);
        }

        // GET: CentroMedicoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CentroMedico centroMedico = db.CentrosMedicos.Find(id);
            if (centroMedico == null)
            {
                return HttpNotFound();
            }
            return View(centroMedico);
        }

        // POST: CentroMedicoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CentroMedicoId,Nombre,Rif,Direccion,Telefono")] CentroMedico centroMedico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(centroMedico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(centroMedico);
        }

        // GET: CentroMedicoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CentroMedico centroMedico = db.CentrosMedicos.Find(id);
            if (centroMedico == null)
            {
                return HttpNotFound();
            }
            return View(centroMedico);
        }

        // POST: CentroMedicoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CentroMedico centroMedico = db.CentrosMedicos.Find(id);
            db.CentrosMedicos.Remove(centroMedico);
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
