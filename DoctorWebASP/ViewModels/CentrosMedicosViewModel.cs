using DoctorWebASP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorWebASP.ViewModels
{
    public class CentrosMedicosViewModel
    {
        public SelectList CentrosMedicos { get; set; }
        [Required(ErrorMessage ="Por favor seleccione un centro medico")]

        public string CentroMedico { get; set; }
        public int CentroMedicoId { get; set; }
    }
}