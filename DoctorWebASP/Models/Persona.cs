using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models
{
    public class Persona
    {
        [Key]
        public int PersonaId { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string Cedula { get; set; }
        [Required]
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Direccion { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}