using DoctorWebASP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorWebASP.ViewModels
{
    public class EspecialidadViewModel
    {
        public SelectList EspecialidadesMedicas { get; set; }
        public int CentroMedicoId { get; set; }
        [Required(ErrorMessage = "Por favor seleccione una especialidad del listado")]
        public string EspecialidadMedica { get; set; }

    }
}