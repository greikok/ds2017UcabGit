using DoctorWebServiciosWCF.Controllers.Helpers;
using DoctorWebServiciosWCF.Models;
using DoctorWebServiciosWCF.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Controllers
{
    public class ReporteController
    {
        public ResultadoProceso ComprobarParametros(string tipo, string codigo)
        {
            var resultado = new ResultadoProceso();

            try
            {
                int id = 0;
                if (!int.TryParse(codigo, out id))
                    throw new FormatException("El código debe ser un número.");

                if (!tipo.Equals(ReporteTipo.preestablecido.ToString()) && !tipo.Equals(ReporteTipo.configurado.ToString()))
                    throw new CustomException("No se puede realizar ninguna operación para el tipo " + tipo);

                if (tipo.Equals(ReporteTipo.preestablecido.ToString()) && !(id >= 1 && id <= 6))
                    throw new CustomException("No se puede realizar ninguna operación para el código " + codigo);
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }

            return resultado;
        }
    }
}