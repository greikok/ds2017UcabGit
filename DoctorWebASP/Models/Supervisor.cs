using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models
{
    public class Supervisor
    {
        public int SupervisorId { get; set; }
        [Required]
        public Medico medico { get; set; }
        [Required]
        public String Horarios { get; set; }
        [Required]
        public int AreaId { get; set; }
        [ForeignKey("AreaId")]
        public Area Area { get; set; }

    }
}