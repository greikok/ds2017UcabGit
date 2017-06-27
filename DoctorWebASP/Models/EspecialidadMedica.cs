using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models
{
    public class EspecialidadMedica
    {
        public int EspecialidadMedicaId { get; set; }
        public string Nombre { get; set; }
        public virtual ICollection<Medico> Medicos { get; set; }
    }
}