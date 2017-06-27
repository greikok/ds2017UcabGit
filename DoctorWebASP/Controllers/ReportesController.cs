using DoctorWebASP.Controllers.Helpers;
using DoctorWebASP.Models;
using DoctorWebASP.ViewModels;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace DoctorWebASP.Controllers
{
    public class ReportesController : Controller
    {
        private ApplicationDbContext db;
        private string lastTimeOnDay = "11:59:59 PM";
        private string firstTimeOnDay = "12:00:00 AM";


        public ReportesController()
        {
            db = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        // GET: Reportes
        public ActionResult Index()
        {
            var indexViewModel = new ReportesIndexViewModel();

            // REPORTE #2 - Promedio de edad de los pacientes
            indexViewModel.promedioEdadPacientes = getPromedioEdadPaciente();

            // REPORTE #3 - Promedio de citas por médico
            indexViewModel.promedioCitasPorMedico = getPromedioCitasPorMedico();

            // REPORTE #5 - Promedio de uso de la aplicación

            return View(indexViewModel);
        }

        public ActionResult Configurados()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult getCantidadUsuariosRegistrados(string fechaInicioStr, string fechaFinStr)
        {
            DateTime fechaInicio = DateTime.Parse(fechaInicioStr + " " + firstTimeOnDay, CultureInfo.InvariantCulture);
            DateTime fechaFin = DateTime.Parse(fechaFinStr + " " + lastTimeOnDay, CultureInfo.InvariantCulture);

            var result = from p in db.Personas
                        where p.FechaCreacion >= fechaInicio & p.FechaCreacion <= fechaFin
                        select p;

            return Json(new { cantidad = result.Count(), fechaInicio = fechaInicio.ToString(), fechaFin = fechaFin.ToString() } );
        }

        private double getPromedioEdadPaciente()
        {
            var result = from p in db.Personas
                         where (p is Paciente)
                         select p.FechaNacimiento;

            int total = 0;

            foreach (var r in result.ToList())
            {
                Age age = new Age(r, DateTime.Today.AddDays(1).AddTicks(-1));
                total = total + age.Years;
            }

            return total / result.Count();

        }

        private double getPromedioCitasPorMedico()
        {
            double cantidadCitas = (from c in db.Calendarios
                                 where !c.Cancelada & c.Disponible == 0
                                 select c).Count();
            double cantidadMedicos = (from p in db.Personas
                                   where p is Medico
                                   select p).Count();

            return cantidadCitas / cantidadMedicos;
        }

        [HttpPost]
        public ActionResult getPromedioRecursosDisponibles(string fechaInicioStr, string fechaFinStr)
        {
            DateTime dtFechaInicio = DateTime.Parse(fechaInicioStr + " " + firstTimeOnDay, CultureInfo.InvariantCulture);
            DateTime dtFechaFin = DateTime.Parse(fechaFinStr + " " + lastTimeOnDay, CultureInfo.InvariantCulture);

            var result = from ur in db.UsoRecursos
                         join ci in db.Citas on ur.Cita equals ci
                         join ca in db.Calendarios on ci.Calendario equals ca
                         where ca.HoraInicio >= dtFechaInicio & ca.HoraInicio <= dtFechaFin & !ca.Cancelada
                         select ur;

            var almacen = (from a in db.Almacenes
                           select a);

            double cantidadRecursos = (from rh in db.RecursosHospitalarios
                                       select rh).Count();

            double totalCantidadRecursos = 0;

            foreach (var a in almacen.ToList())
            {
                foreach (var ur in result.ToList())
                {
                    if (a.RecursoHospitalario == ur.RecursoHospitalario)
                    {
                        if (a.Disponible - ur.Cantidad >= 0)
                        {
                            totalCantidadRecursos = totalCantidadRecursos + (a.Disponible - ur.Cantidad);
                        }
                    }
                }
            }

            return Json(new { cantidad = totalCantidadRecursos/cantidadRecursos, fechaInicio = dtFechaInicio.ToString(), fechaFin = dtFechaFin.ToString() });
        }

        [HttpPost]
        public ActionResult getPromedioCitasCanceladasPorMedico(string fechaInicioStr, string fechaFinStr)
        {
            DateTime dtFechaInicio = DateTime.Parse(fechaInicioStr, CultureInfo.InvariantCulture);
            DateTime dtFechaFin = DateTime.Parse(fechaFinStr, CultureInfo.InvariantCulture);

            double cantidadCitasCanceladas = (from c in db.Calendarios
                                    where c.Cancelada & c.Disponible == 1 & c.HoraInicio >= dtFechaInicio & c.HoraFin <= dtFechaFin
                                    select c).Count();
            double cantidadMedicos = (from p in db.Personas
                                      where p is Medico
                                      select p).Count();

            return Json(new { cantidad = cantidadCitasCanceladas/cantidadMedicos, fechaInicio = dtFechaInicio.ToString(), fechaFin = dtFechaFin.ToString() });
        }

        public int pruebaunitaria()
        {
            var result = 2 + 2;
            return result;
        }
    }
}