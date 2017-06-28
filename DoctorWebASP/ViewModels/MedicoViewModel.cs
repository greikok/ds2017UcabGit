using DoctorWebASP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorWebASP.ViewModels
{
    public class MedicoViewModel
    {
        [Required(ErrorMessage = "Por favor seleccione un medico del listado")]
        public SelectList Medicos { get; set; }
        public int CentroMedicoId { get; set; }
        public string MedicoId { get; set; }
    }
}
