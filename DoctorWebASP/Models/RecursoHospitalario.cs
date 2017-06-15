using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models
{
    public class RecursoHospitalario
    {
        public int RecursoHospitalarioId { get; set; }
        public virtual CentroMedico CentroMedico { get; set; }
    }
}