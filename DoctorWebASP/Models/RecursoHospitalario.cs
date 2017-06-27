using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Required]
        public string Tipo { get; set; }
        [Required]
        public string Componentes { get; set; }
        [Required]
        public string Posologia { get; set; }
        [Required]
        public string Recomendaciones { get; set; }
        [Required]
        public int AlmacenId { get; set; }
        [ForeignKey("AlmacenId")]
        public Almacen Almacen { get; set; }
    }
}