using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models
{
    public class DoctorWebException : Exception
    {
        public DoctorWebException(string mensaje) : base(mensaje)
        {

        }

        public DoctorWebException(string mensaje, Exception interna) : base(mensaje, interna)
        {

        }
    }
}