// <copyright file="CitasControllerTest.cs">Copyright ©  2017</copyright>
using System;
using System.Web.Mvc;
using DoctorWebASP.Controllers;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoctorWebASP.Controllers.Tests
{
    /// <summary>This class contains parameterized unit tests for CitasController</summary>
    [PexClass(typeof(CitasController))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class CitasControllerTest
    {
        /// <summary>Test stub for DeleteConfirmed(Int32)</summary>
        [PexMethod]
        public ActionResult DeleteConfirmedTest([PexAssumeUnderTest]CitasController target, int id)
        {
            ActionResult result = target.DeleteConfirmed(id);
            return result;
            // TODO: add assertions to method CitasControllerTest.DeleteConfirmedTest(CitasController, Int32)
        }
    }
}
