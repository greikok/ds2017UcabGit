using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models
{
    public class Medico : Persona
    {
        //public int MedicoId { get; set; }
        public string Especialidad { get; set; }
        public double Sueldo { get; set; }

        public virtual ICollection<Calendario> Eventos { get; set; }
        public virtual CentroMedico CentroMedico { get; set; }
    }
}