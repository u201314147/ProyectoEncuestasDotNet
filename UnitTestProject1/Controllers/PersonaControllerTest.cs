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
     
        public void TestInit()
        {
            personacontroller = new PersonaController();
        }

        [Test]
        public void guardarRegistro()
        {
        
            try { 
            personaRegistrar.Nombre = "Marco";
            personaRegistrar.ApellidoPaterno = "Bruggman";
            personaRegistrar.ApellidoMaterno = "Gisler";
            personaRegistrar.DNI = "00034035";
            personaRegistrar.Estado = "ACT";
            personaRegistrar.Password = "123456799";
           
            personaRegistrar.AgregarPersona(personaRegistrar);
            }catch(Exception e)
            {
                //SI hay excepcion la prueba falla
                Assert.AreEqual(true, false);
            }

        }
        [Test]
        public void probarFuncionListarPersona()
        {
            try { 
            LoginViewModel view = new LoginViewModel();

            var result = personacontroller.ListPersNatural(view) as ViewResult;

            var model = result.ViewData.Model;

            //Assert fallara ya que modelo devuelve lista de persona mas no persona
            Assert.IsAssignableFrom(typeof(PersonaNatural), model);
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
