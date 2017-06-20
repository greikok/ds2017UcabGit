using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models
{
    public class Empleado : Persona
    {
        public string Rol { get; set; }
        [DataType(DataType.Currency)]
        public decimal Sueldo { get; set; }
    }
}