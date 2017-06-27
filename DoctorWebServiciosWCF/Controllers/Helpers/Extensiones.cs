using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DoctorWebServiciosWCF.Controllers.Helpers
{
    public static class Extensiones
    {
        public static string ColocarParametros(this string contenido, object parametros)
        {
            StringBuilder resultado = new StringBuilder(contenido);
            if (parametros != null)
            {
                foreach (var parametro in parametros.GetType().GetProperties())
                {
                    try
                    {
                        var valor = parametro.GetValue(parametros).ToString();
                        var attibuto = parametro.Name;

                        resultado = resultado.Replace($"{{{{{attibuto}}}}}", valor);
                    }
                    catch (Exception){}
                }
            }
            resultado = resultado.Replace($"{{{{FechaActual}}}}", DateTime.Now.ToShortDateString());

            return resultado.ToString();
        }
    }
}