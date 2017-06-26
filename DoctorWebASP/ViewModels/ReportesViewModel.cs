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
        public int cantidadUsuariosRegistrados { get; set; }
        public double promedioEdadPacientes { get; set; }
        public double promedioCitasPorMedico { get; set; }
        public double promedioRecursosDisponibles { get; set; }
        public double promedioCitasCanceladasPorMedico { get; set; }
    }
}