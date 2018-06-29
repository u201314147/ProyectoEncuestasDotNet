using System;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Controllers;
using Inei.ViewModel;
using NUnit.Framework;

namespace UnitTestProject1
{
    public class PersonaControllerTest
    {

        private PersonaController personacontroller;

        AddEditPersonaNaturalViewModel personaRegistrar;

        private LoginViewModel objViewModel;
     
        [SetUp]
        public void TestInit()
        {
            personacontroller = new PersonaController();
            personaRegistrar = new AddEditPersonaNaturalViewModel();
        }

        [Test]
        public void probarFuncionListarPersona()
        {
            try { 
            LoginViewModel view = new LoginViewModel();

            var result = personacontroller.ListPersNatural(view) as ViewResult;


                Assert.IsInstanceOf(typeof(ViewResult), result);
            }
            catch(Exception e)
            {
                //asser falla si la prueba da error
                Assert.AreEqual(true, false);
            }
        }
        [Test]
        public void probarCambioEstadoAInactivo()
        {

           
        }

    }
}
