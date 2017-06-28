using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoctorWebASP.Controllers;
using Microsoft.AspNet.Identity;

namespace DoctorWebASP.Models
{
    public class CitasConsultas : ICitasConsultas
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void EliminarCita(Cita cita,Calendario calendario)
        {
            var cNotificaciones = new DoctorWebServiciosWCF.Controllers.NotificacionController();
            var resp1 = cNotificaciones.Obtener("cancelarCita");
            var resp2 = cNotificaciones.Obtener("cancelarCita");

            if (resp1.SinProblemas)
            {
                resp1.Contenido.Enviar(calendario.Medico.Email, new { nombre = calendario.Medico.ConcatUserName });
            }
            if (resp2.SinProblemas)
            {
                resp2.Contenido.Enviar(cita.Paciente.Email, new { nombre = cita.Paciente.ConcatUserName });
            }

            db.Citas.Remove(cita);
            calendario.Disponible = 1;
            db.SaveChanges();
        }

        public void GuardarCita(Cita cita, Calendario calendario)
        {
            db.Citas.Add(cita);
            // Finalmente colocamos la Fecha Reservada como NO disponible
            calendario.Disponible = 0;
            db.SaveChanges();

            var cNotificaciones = new DoctorWebServiciosWCF.Controllers.NotificacionController();
            var resp1 = cNotificaciones.Obtener("generarCita");
            var resp2 = cNotificaciones.Obtener("generarCita");

            if (resp1.SinProblemas)
            {
                resp1.Contenido.Enviar(calendario.Medico.Email, new { nombre = calendario.Medico.ConcatUserName });
            }
            if (resp2.SinProblemas)
            {
                resp2.Contenido.Enviar(cita.Paciente.Email, new { nombre = cita.Paciente.ConcatUserName });
            }
        }

        public Calendario ObtenerCalendario(int calendarioId)
        {
           return db.Calendarios.Single(c => c.CalendarioId == calendarioId);
        }

        public Cita ObtenerCita(int id)
        {
            return db.Citas.Find(id);
        }

        public List<Cita> ObtenerCitasDoctor(string userId)
        {
            return db.Citas.Where(c => c.Calendario.Medico.ApplicationUser.Id == userId).ToList();
        }

        public SelectList ObtenerEsMedicasPorMedicosEnCentroMedico(CentroMedico cMedico)
        {
            return new SelectList(db.Personas.OfType<Medico>().Where(m => m.CentroMedico.CentroMedicoId == cMedico.CentroMedicoId).Select(c => c.EspecialidadMedica).Distinct().ToList(), "EspecialidadMedicaId", "Nombre");
        }

        public EspecialidadMedica ObtenerEspecialidadMedica(int espMedica)
        {
            return db.EspecialidadesMedicas.Single(e => e.EspecialidadMedicaId == espMedica);
        }

        public List<Cita> ObtenerListaCitas(string userId)
        {
            return db.Citas.Where(c => c.Paciente.ApplicationUser.Id == userId).ToList();
        }

        public Medico ObtenerMedico(string userId)
        {
            return db.Personas.OfType<Medico>().Single(p => p.ApplicationUser.Id == userId);
        }

        public Paciente ObtenerPaciente(string userId)
        {
            return db.Personas.OfType<Paciente>().Single(p => p.ApplicationUser.Id == userId);
        }

        public SelectList ObtenerSelectListCentrosMedicos()
        {
            return new SelectList(db.CentrosMedicos.ToList(), "Rif", "Nombre");
        }

        public SelectList ObtenerSelectListMedicosQueTrabajanEnCentroMedico(int centroMedicoId, int espMedica)
        {
            return new SelectList(db.Personas.OfType<Medico>().Where(p => p.CentroMedico.CentroMedicoId == centroMedicoId && p.EspecialidadMedica.EspecialidadMedicaId == espMedica).ToList(), "PersonaId", "ConcatUserName");
        }

        public CentroMedico ObtenerSingleCentroMedico(string centroMedico)
        {
            return db.CentrosMedicos.Single(m => m.Rif == centroMedico);
        }

        public CentroMedico ObtenerSingleCentroMedico(int centroMedicoId)
        {
            return db.CentrosMedicos.Single(c => c.CentroMedicoId == centroMedicoId);
        }

        public string ObtenerUsuarioLoggedIn(CitasController citasController)
        {
            return citasController.User.Identity.GetUserId();
        }

        CentroMedico ICitasConsultas.ObtenerCentroMedico(int centroMedicoId)
        {
            return db.CentrosMedicos.Single(m => m.CentroMedicoId == centroMedicoId);
        }

        
    }
}