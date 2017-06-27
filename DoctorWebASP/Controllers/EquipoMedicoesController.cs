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
    public class EquipoMedicoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EquipoMedicoes
        public ActionResult Index()
        {
            var equipoMedico = db.EquipoMedico.Include(e => e.Area);
            return View(equipoMedico.ToList());
        }

        // GET: EquipoMedicoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipoMedico equipoMedico = db.EquipoMedico.Find(id);
            if (equipoMedico == null)
            {
                return HttpNotFound();
            }
            return View(equipoMedico);
        }

        // GET: EquipoMedicoes/Create
        public ActionResult Create()
        {
            ViewBag.AreaId = new SelectList(db.Areas, "AreaId", "Nombre");
            return View();
        }

        // POST: EquipoMedicoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EquipoMedicoId,Nombre,Descripcion,Indicaciones,AreaId")] EquipoMedico equipoMedico)
        {
          /*  try
            {*/

                if (ModelState.IsValid)
                {

                    db.EquipoMedico.Add(equipoMedico);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.AreaId = new SelectList(db.Areas, "AreaId", "Nombre", equipoMedico.AreaId);
                return View(equipoMedico);

        /*    }
            catch (Exception e) {
                throw new HttpException("debes seleccionar un area");
            }
         */   
        }

        // GET: EquipoMedicoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipoMedico equipoMedico = db.EquipoMedico.Find(id);
            if (equipoMedico == null)
            {
                return HttpNotFound();
            }
            ViewBag.AreaId = new SelectList(db.Areas, "AreaId", "Nombre", equipoMedico.AreaId);
            return View(equipoMedico);
        }

        // POST: EquipoMedicoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EquipoMedicoId,Nombre,Descripcion,Indicaciones,AreaId")] EquipoMedico equipoMedico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipoMedico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AreaId = new SelectList(db.Areas, "AreaId", "Nombre", equipoMedico.AreaId);
            return View(equipoMedico);
        }

        // GET: EquipoMedicoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipoMedico equipoMedico = db.EquipoMedico.Find(id);
            if (equipoMedico == null)
            {
                return HttpNotFound();
            }
            return View(equipoMedico);
        }

        // POST: EquipoMedicoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EquipoMedico equipoMedico = db.EquipoMedico.Find(id);
            db.EquipoMedico.Remove(equipoMedico);
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
