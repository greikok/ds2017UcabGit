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
        public double promedioEdadPacientes { get; set; }
        public double promedioCitasPorMedico { get; set; }
        public double promedioUsoApp { get; set; }

    }
}