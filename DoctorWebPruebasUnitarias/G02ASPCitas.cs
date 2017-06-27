using DoctorWebASP.Controllers;
using DoctorWebASP.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DoctorWebPruebasUnitarias
{
    [TestClass]
    public class G02ASPCitas
    {
        [TestMethod]
        public void PruebaGenerarCita() {
            //Inicializar
            // Flujo exitoso

            int calendarioId = 0;
            string medicoId = "";
            int centroMedicoId = 0;
            string userId = "usuario x";
            Cita cita = new Cita();
            Calendario calendario = new Calendario();

            var controller = new CitasController();
            var mock = new Mock<ICitasConsultas>();
            mock.Setup(db => db.ObtenerCentroMedico(centroMedicoId)).Returns(new CentroMedico());
            mock.Setup(db => db.ObtenerPaciente(userId)).Returns(new Paciente());
            mock.Setup(db => db.ObtenerCalendario(calendarioId)).Returns(new Calendario());
            mock.Setup(db => db.GuardarCita(cita, calendario));            
            mock.Setup(db => db.ObtenerUsuarioLoggedIn(controller)).Returns(userId);

            controller.consulta = mock.Object;


            //Ejecutar
            var resultado = controller.GenerarCita(calendarioId, medicoId, centroMedicoId);

            //Evaluar
            Assert.IsInstanceOfType(resultado,typeof(RedirectToRouteResult));

            var redirect = (RedirectToRouteResult)resultado;
            Assert.IsNotNull(redirect.RouteValues);
            Assert.AreEqual(1, redirect.RouteValues.Keys.Count);
            Assert.AreEqual("Index", redirect.RouteValues["action"]);
        }
    }
}
