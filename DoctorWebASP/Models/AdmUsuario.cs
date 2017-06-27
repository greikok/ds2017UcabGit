using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorWebASP.Models
{
    public class AdmUsuario : Controller
    {
        // GET: AdmUsuario
        public ActionResult Index()
        {
            return View();
        }

        // GET: AdmUsuario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdmUsuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdmUsuario/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdmUsuario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdmUsuario/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdmUsuario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdmUsuario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
