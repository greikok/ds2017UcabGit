using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models
{
    public class Cita
    {
        public int CitaId { get; set; }

        public virtual Paciente Paciente { get; set; }
        public virtual Calendario Calendario { get; set; }
        public virtual CentroMedico CentroMedico { get; set; }
        public virtual ICollection<Tratamiento> Tratamientos { get; set; }

        private ApplicationDbContext db = new ApplicationDbContext();

        public Boolean isDoctor(string id)
        {
            var user = db.Personas.OfType<Medico>().Where(p => p.ApplicationUser.Id == id);
            if (user == null)
            {
                return false;
            }
            return true;
        }
    }
}