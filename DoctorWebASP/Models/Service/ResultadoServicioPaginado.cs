using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models.Service
{
    public class ResultadoServicioPaginado<T> where T : class
    {
        public bool SinProblemas { get; set; }
        public string Mensaje { get; set; }

        public int PaginaActual { get; set; }
        public int CantidadFilas { get; set; }
        public int CantidadPaginas { get; set; }

        public IEnumerable<T> Contenido { get; set; }        

        public ResultadoServicioPaginado() {
            SinProblemas = false;
            Contenido = null;
        }

        internal void Inicializar(int PaginaActual, int CantidadFilas, int CantidadPaginas, List<T> Contenido)
        {
            this.SinProblemas = true;
            this.PaginaActual = PaginaActual;
            this.CantidadFilas = CantidadFilas;
            this.CantidadPaginas = CantidadPaginas;
            this.Contenido = Contenido;
        }
    }
}