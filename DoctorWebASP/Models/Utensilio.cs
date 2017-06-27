using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models
{
    public class Utensilio
    {
       
        public int UtensilioId { get; set; }
        [Required]
        public String Nombre { get; set; }
        [Required]
        public String Descripcion { get; set; }
        [Required]
        public int CantidadDisponible { get; set; }
        [Required]
        public int AlmacenId { get; set; }
        [ForeignKey("AlmacenId")]
        public Almacen Almacen { get; set; }
    }
}