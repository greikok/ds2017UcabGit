using DoctorWebASP.Controllers.Helpers;
using DoctorWebASP.Models.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models
{
    public class Notificacion
    {
        #region Instancia
        public int NotificacionId { get; set; }
        [Required]
        public NotificacionEstado Estado { get; set; }
        [Required]
        [StringLength(60)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(255)]
        public string Descripcion { get; set; }
        [Required]
        [StringLength(128)]
        public string Asunto { get; set; }
        [Required]
        public string Contenido { get; set; }
        #endregion
    }
}