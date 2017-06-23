using DoctorWebASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebASP.ViewModels
{
    public class ReportesViewModel
    {
    }

    public class ReportesIndexViewModel
    {
        public IEnumerable<Persona> Personas { get; set; }
    }
}