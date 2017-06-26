using DoctorWebServiciosWCF.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DoctorWebServiciosWCF.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ReportesService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ReportesService.svc o ReportesService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ReportesService : IReportesService
    {
        public ResultadoProceso DoWork(string codigo)
        {
            var resultado = new ResultadoProceso();
            try
            {
                resultado.Mensaje = "Bienvido!";
                resultado.SinProblemas = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }
    }
}
