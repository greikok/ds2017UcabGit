using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models
{
    public class RecursoHospitalario
    {
        public int RecursoHospitalarioId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public string Componentes { get; set; }
        public string Posologia { get; set; }
        public string Recomendaciones { get; set; }

        public virtual CentroMedico CentroMedico { get; set; }
    }
}