using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models
{
    public class Area
    {
        public int AreaId { get; set;}
        [Required]
        public String Nombre { get; set; }
        [Required]
        public String Descripcion { get; set; }
        [Required]
        public String Horarios { get; set; }
        [Required]
        public int CentroMedicoId { get; set; }
        [ForeignKey("CentroMedicoId")]
        public CentroMedico Centromedico { get; set; }

    }
}