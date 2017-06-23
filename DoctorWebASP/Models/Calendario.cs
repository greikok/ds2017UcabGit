using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models
{
    public class Calendario
    {
        public int CalendarioId { get; set; }
        [Required]
        public DateTime HoraInicio { get; set; }
        [Required]
        public DateTime HoraFin { get; set; }

        public virtual Medico Medico { get; set; }
        // El atributo disponible indica con 1 si esta fecha esta libre para ser tomada por una cita
        // 0 indica que esta tomada

        public byte Disponible { get; set; }
    }
}