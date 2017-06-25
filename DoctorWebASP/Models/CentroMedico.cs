using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models
{
    public class CentroMedico
    {
        public int CentroMedicoId { get; set; }
        public string Nombre { get; set; }
        public string Rif { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public virtual ICollection<Cita> Citas { get; set; }
        public virtual ICollection<RecursoHospitalario> RecursosHospitalarios { get; set; }
        public virtual ICollection<Medico> Medicos { get; set; }
    }
}