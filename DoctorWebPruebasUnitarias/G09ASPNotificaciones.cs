using DoctorWebASP.Controllers;
using DoctorWebASP.Controllers.Services;
using DoctorWebASP.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using static DoctorWebPruebasUnitarias.Extensiones;

namespace DoctorWebPruebasUnitarias
{
    [TestClass]
    public class G09ASPNotificaciones
    {
        private Mock<IServicioNotificaciones> servicio_mocked { get; set; }

        #region DoctorWebASP
        [TestInitialize]
        public void Inicializar() {
            this.servicio_mocked = new Mock<IServicioNotificaciones>();
        }

        [TestMethod]
        // El controlador no recibe notificaciones del servicio, lista vacia.
        public void ASPNotificacionCtrlIndexCaso1()
        {
            //Inicializar
            int cantidadRegistros = 0;
            string nombreFiltro = null;
            int numeroPagina = 0;
            int numeroFilas = 5;
            var respuestaServicio = new List<Notificacion>() { };
            servicio_mocked
                .Setup(servicio => servicio.ObtenerTodos(out cantidadRegistros, nombreFiltro, numeroPagina, numeroFilas))
                .OutCallback(new OutAction<int, string, int, int>((out int cantidadPaginas, string name, int a, int b) => cantidadPaginas = 0))
                .Returns(() => { return respuestaServicio; });

            var controlador = new NotificacionesController(servicio_mocked.Object);

            //Ejecutar
            var resultado = controlador.Index(nombreFiltro, numeroPagina, numeroFilas);

            //Evaluar

            // El resultado del metodo es una Vista?
            Assert.IsInstanceOfType(resultado, typeof(ViewResult));

            ViewResult vista = (ViewResult)resultado;
            // La vista tiene errores?
            Assert.IsNull(vista.ViewData["error"]);
            // La vista contiene el mismo filtro?
            Assert.AreEqual(nombreFiltro, vista.ViewData["nombre"]);
            // La vista tiene el mismo numero de filas?
            Assert.AreEqual(numeroFilas, vista.ViewData["filas"]);
            // La vista permite paginar a la derecha?
            Assert.AreEqual(false, vista.ViewData["permitirSiguiente"]);
            // La vista permite paginar a la izquierda?
            Assert.AreEqual(false, vista.ViewData["permitirAnterior"]);
            // En caso de poder paginar cual es el indice de la siguiente pagina?
            Assert.AreEqual(1, vista.ViewData["siguienteIndice"]);
            // En caso de poder paginar cual es el indice de la anterior pagina?
            Assert.AreEqual(-1, vista.ViewData["anteriorIndice"]);
            // El modelo de la vista es la respuesta del servicio?
            Assert.AreSame(respuestaServicio, vista.Model);
        }

        [TestMethod]
        // El controlador recibe notificaciones del servicio, lista con 1 elemento de un total de 3 en base de datos.
        public void ASPNotificacionCtrlIndexCaso2()
        {
            //Inicializar
            int cantidadRegistros = 3;
            string nombreFiltro = null;
            int numeroPagina = 1;
            int numeroFilas = 1;
            var respuestaServicio = new List<Notificacion>() { new Notificacion() };
            servicio_mocked
                .Setup(servicio => servicio.ObtenerTodos(out cantidadRegistros, nombreFiltro, numeroPagina, numeroFilas))
                .OutCallback(new OutAction<int, string, int, int>((out int cantidadPaginas, string name, int a, int b) => cantidadPaginas = cantidadRegistros))
                .Returns(() => { return respuestaServicio; });

            var controlador = new NotificacionesController(servicio_mocked.Object);

            //Ejecutar
            var resultado = controlador.Index(nombreFiltro, numeroPagina, numeroFilas);

            //Evaluar

            // El resultado del metodo es una Vista?
            Assert.IsInstanceOfType(resultado, typeof(ViewResult));

            ViewResult vista = (ViewResult)resultado;
            // La vista tiene errores?
            Assert.IsNull(vista.ViewData["error"]);
            // La vista contiene el mismo filtro?
            Assert.AreEqual(nombreFiltro, vista.ViewData["nombre"]);
            // La vista tiene el mismo numero de filas?
            Assert.AreEqual(numeroFilas, vista.ViewData["filas"]);
            // La vista permite paginar a la derecha?
            Assert.AreEqual(true, vista.ViewData["permitirSiguiente"]);
            // La vista permite paginar a la izquierda?
            Assert.AreEqual(true, vista.ViewData["permitirAnterior"]);
            // En caso de poder paginar cual es el indice de la siguiente pagina?
            Assert.AreEqual(2, vista.ViewData["siguienteIndice"]);
            // En caso de poder paginar cual es el indice de la anterior pagina?
            Assert.AreEqual(0, vista.ViewData["anteriorIndice"]);
            // El modelo de la vista es la respuesta del servicio?
            Assert.AreSame(respuestaServicio, vista.Model);
        }

        [TestMethod]
        // El controlador captura una excepcion tras invocar el servicio.
        public void ASPNotificacionCtrlIndexCaso3()
        {
            //Inicializar
            int cantidadRegistros = 0;
            string nombreFiltro = null;
            int numeroPagina = 1;
            int numeroFilas = 1;
            var respuestaServicio = new List<Notificacion>() { new Notificacion() };
            servicio_mocked
                .Setup(servicio => servicio.ObtenerTodos(out cantidadRegistros, nombreFiltro, numeroPagina, numeroFilas))
                .OutCallback(new OutAction<int, string, int, int>((out int cantidadPaginas, string name, int a, int b) => cantidadPaginas = cantidadRegistros))
                .Returns(() => { throw new Exception(); });

            var controlador = new NotificacionesController(servicio_mocked.Object);

            //Ejecutar
            var resultado = controlador.Index(nombreFiltro, numeroPagina, numeroFilas);

            //Evaluar

            // El resultado del metodo es una Vista?
            Assert.IsInstanceOfType(resultado, typeof(ViewResult));

            ViewResult vista = (ViewResult)resultado;
            // La vista tiene errores?
            Assert.IsNotNull(vista.ViewData["error"]);
            // La vista contiene el mismo filtro?
            Assert.AreEqual(nombreFiltro, vista.ViewData["nombre"]);
            // La vista tiene el mismo numero de filas?
            Assert.AreEqual(numeroFilas, vista.ViewData["filas"]);
            // La vista permite paginar a la derecha?
            Assert.AreEqual(false, vista.ViewData["permitirSiguiente"]);
            // La vista permite paginar a la izquierda?
            Assert.AreEqual(true, vista.ViewData["permitirAnterior"]);
            // En caso de poder paginar cual es el indice de la siguiente pagina?
            Assert.AreEqual(2, vista.ViewData["siguienteIndice"]);
            // En caso de poder paginar cual es el indice de la anterior pagina?
            Assert.AreEqual(0, vista.ViewData["anteriorIndice"]);
            // El modelo de la vista no es la respuesta del servicio?
            Assert.AreNotSame(respuestaServicio, vista.Model);
        }

        [TestMethod]
        public void ASPNotificacionCtrlDetail()
        {
            //Inicializar
            //Ejecutar
            //Evaluar
        }

        [TestMethod]
        public void ASPNotificacionCtrlPost()
        {
            //Inicializar
            //Ejecutar
            //Evaluar
        }

        [TestMethod]
        public void ASPNotificacionCtrlDelete()
        {
            //Inicializar
            //Ejecutar
            //Evaluar
        }
        #endregion

    }
}
