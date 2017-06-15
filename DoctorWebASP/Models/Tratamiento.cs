using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models
{
    public class Tratamiento
    {
        public int TratamientoId { get; set; }
        public virtual Cita Cita { get; set; }
    }
}