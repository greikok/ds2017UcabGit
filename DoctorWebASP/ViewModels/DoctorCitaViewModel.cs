using DoctorWebASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebASP.ViewModels
{
    public class DoctorCitaViewModel
    {
        public IEnumerable<Cita> Citas { get; set; }
    }
}