using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DoctorWebASP.Controllers;

namespace ReportesUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void pp()
        {
            //PLANTEAMIENTO
            int esperado = 4;

            //PRUEBA
            ReportesController rc = new ReportesController();
            int actual = rc.pruebaunitaria();

            //ASSERT
            Assert.AreEqual(actual, esperado);

        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void testPromedioEdadPacientes()
        {
            //PLANTEAMIENTO
            int esperado = 50;

            //PRUEBA
            ReportesController rc = new ReportesController();

            //ASSERT
            Assert.Equals(rc.getPromedioEdadPaciente(), esperado);

        }

        [TestMethod]
        public void testPromedioCitasPorMedico()
        {
            //PLANTEAMIENTO
            double esperado = 0.6;

            //PRUEBA
            ReportesController rc = new ReportesController();
            double actual = rc.getPromedioCitasPorMedico();

            //ASSERT
            Assert.AreEqual(actual, esperado);

        }


    }

}
