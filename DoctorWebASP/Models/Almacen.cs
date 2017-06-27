using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models
{
    public class Almacen
    {
        public int AlmacenId { get; set; } 
        [Required]
        public int Disponible { get; set; }
        [Required]
        public int CentroMedicoId { get; set; }
        [ForeignKey("CentroMedicoId")]
        public CentroMedico Centromedico { get; set; }

    }
}