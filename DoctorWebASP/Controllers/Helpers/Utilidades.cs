using DoctorWebASP.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorWebASP.Controllers.Helpers
{
    public class Utilidades
    {
        #region Grupo09       
        internal static Uri GetUrlBase(string servicio)
        {
            var host = ObtenerClave("WebServiceUrl");
            var builder = new UriBuilder(host);
            builder.Path = $"Services/{servicio}.svc/";
            return builder.Uri;
        }

        internal static string ObtenerClave(string key)
        {
            var path = ConfigurationManager.AppSettings[key];
            if (String.IsNullOrEmpty(path))
                throw new KeyNotFoundException($"No se encontro un valor para '{key}', en el archivo de configuracion.");
            return path;
        }        
        #endregion
    }
}