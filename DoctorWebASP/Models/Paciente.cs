using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models
{
    public class Paciente : Persona
    {
        public string TipoSangre { get; set; }

        public virtual ICollection<HistoriaMedica> HistoriasMedicas { get; set; }
        public virtual ICollection<Cita> Citas { get; set; }
    }
}