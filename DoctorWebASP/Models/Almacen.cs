using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models
{
    public class Almacen
    {
        public int AlmacenId { get; set; }
        public int Disponible { get; set; }

        public virtual CentroMedico CentroMedico { get; set; }
        public virtual RecursoHospitalario RecursoHospitalario { get; set; }
    }
}