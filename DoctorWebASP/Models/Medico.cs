using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models
{
    public class Medico : Persona
    {
        [DataType(DataType.Currency)]
        public decimal Sueldo { get; set; }

        public EspecialidadMedica EspecialidadMedica { get; set; }
        public virtual ICollection<Calendario> Eventos { get; set; }
        public virtual CentroMedico CentroMedico { get; set; }
    }
}