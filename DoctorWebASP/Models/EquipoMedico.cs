using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models
{
    public class EquipoMedico
    {
        public int EquipoMedicoId { get; set; }
        [Required]
        public String Nombre { get; set; }
        [Required]
        public String Descripcion { get; set; }
        [Required]
        public String Indicaciones { get; set; }
        [Required]
        public int AreaId { get; set; }
        [ForeignKey("AreaId")]
        public Area Area { get; set; }
    }
}