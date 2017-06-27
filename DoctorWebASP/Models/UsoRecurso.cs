using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models
{
    public class UsoRecurso
    {
        public int UsoRecursoId { get; set; }
        public int Cantidad { get; set; }

        public virtual Cita Cita { get; set; }
        public virtual RecursoHospitalario RecursoHospitalario { get; set; }
    }
}