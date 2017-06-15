using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models
{
    public class Notificacion
    {
        // Agregar los Data Annotations
        public int NotificacionId { get; set; }
        public string Nombre { get; set; }
        public byte TipoContenido { get; set; }
        public string Contenido { get; set; }
        public byte TipoDestinatarios { get; set; }
        public string Destinatarios { get; set; }
    }
}