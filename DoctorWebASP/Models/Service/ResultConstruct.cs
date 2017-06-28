using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models.Service
{
    public class ResultConstruct<T> where T : class
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Object { get; set; }

        public ResultConstruct()
        {
            Status = false;
            Message = null;
            Object = null;
        }
    }
}