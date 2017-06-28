using DoctorWebASP.Models;
using DoctorWebASP.Models.Service;
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
        public ResultadoProceso resultadoProcesoR2 { get; set; }
        public ResultadoProceso resultadoProcesoR3 { get; set; }
        public ResultadoProceso resultadoProcesoR5 { get; set; }
    }
}