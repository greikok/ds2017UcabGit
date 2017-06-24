using DoctorWebServiciosWCF.Model;
using DoctorWebServiciosWCF.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DoctorWebServiciosWCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ReporteService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ReporteService.svc or ReporteService.svc.cs at the Solution Explorer and start debugging.
    public class ReporteService : IReporteService
    {
        private readonly ReporteController controller = new ReporteController();

        public string DoWork(string codigo)
        {
            return "Hola mundo";
        }

        public ResultadoProceso Reportes(string tipo, string codigo)
        {
            return controller.ComprobarParametros(tipo, codigo);
        }
    }
}
