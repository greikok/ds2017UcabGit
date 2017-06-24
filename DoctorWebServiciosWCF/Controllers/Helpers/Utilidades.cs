using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Controllers.Helpers
{
    public class Utilidades
    {
        public static string ObtenerClave(string key)
        {
            var path = ConfigurationManager.AppSettings[key];
            if (String.IsNullOrEmpty(path))
                throw new KeyNotFoundException(String.Format("No se encuentro la clave ({0}) en el archivo de configuracion.", key));
            return path;
        }
    }

    public class CustomException : Exception
    {
        public CustomException() : base()
        {

        }

        public CustomException(string message) : base(message)
        {

        }

        public CustomException(String message, Exception innerException) : base(message, innerException)
        {

        }
    }
}