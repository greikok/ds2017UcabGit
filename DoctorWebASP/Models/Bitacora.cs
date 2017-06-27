using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DoctorWebASP.Models
{
    public class Bitacora
    {
        public byte Id { get; set; }
        [Required]
        public string Usuario { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public string Accion { get; set; }
        public bool Status { get; set; }
    }
}