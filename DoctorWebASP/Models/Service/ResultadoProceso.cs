using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models.Service
{
    public class ResultadoProceso
    {
        public bool SinProblemas { get; set; }
        public string Mensaje { get; set; }

        public ResultadoProceso()
        {
            SinProblemas = false;
            Mensaje = null;
        }

        internal void Inicializar(string mensaje)
        {
            this.SinProblemas = true;
            this.Mensaje = mensaje;
        }
    }
}