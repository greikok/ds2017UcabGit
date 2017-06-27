using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models
{
    public class RecursoHospitalario
    {
        public int RecursoHospitalarioId { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public string Componentes { get; set; }
        public string Posologia { get; set; }
        public string Recomendaciones { get; set; }
    }
}