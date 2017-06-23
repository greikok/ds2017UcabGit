using DoctorWebASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebASP.ViewModels
{
    public class CitaViewModel
    {
        public PagedList.IPagedList<Calendario> ListaFechas { get; set; }
        public string MedicoId { get; set; }
        public int CentroMedicoId { get; set; }
        public int CalendarioId { get; set; }
    }
}