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
        public SelectList Medicos { get; set; }
        [Required(ErrorMessage = "Por favor seleccione un medico del listado")]
        public int MedicoId { get; set; }
    }
}