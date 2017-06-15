using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models
{
    public class Cita
    {
        public int CitaId { get; set; }
        public virtual Paciente Paciente { get; set; }
        // Evento = Clase Calendario
        public virtual Cita Evento { get; set; }
        public virtual CentroMedico CentroMedico { get; set; }
        public virtual ICollection<Tratamiento> Tratamientos { get; set; }

    }
}