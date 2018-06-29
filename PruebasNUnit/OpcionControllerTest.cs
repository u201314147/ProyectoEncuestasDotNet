using System;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Controllers;
using Inei.ViewModel;
using NUnit.Framework;
namespace UnitTestProject1
{
    public class OpcionControllerTest
    {

        private OpcionController opcioncontroller;

        AddEditEncuestaOpcionViewModel opcionResgistrar;
    
        private LoginViewModel objViewModel;
     
        [SetUp]
        public void TestInit()
        {
            opcioncontroller = new OpcionController();
            opcionResgistrar = new AddEditEncuestaOpcionViewModel();
        }

        [Test]
        public void GuardarOpcionPrueba()
        {
             try {

                //Debe existir una pregunta y un usuario administrador
                var entities = new INEIEntities();
                var usuario = entities.Usuario.FirstOrDefault();
                var pregunta = entities.EncuestaPregunta.FirstOrDefault();

                //Registro la opcion
                opcionResgistrar.Texto = "respuesta de prueba";
                opcionResgistrar.FechaRegistro = DateTime.Now;
                opcionResgistrar.UsuarioRegistroId = usuario.UsuarioId;
                opcionResgistrar.Estado = "ACT";
                opcionResgistrar.Orden = 1;

                opcioncontroller.RegistroOpcion(opcionResgistrar);

            }
            catch (Exception e)
            {
                //si hay error assert falla
                Assert.AreEqual(false, true);
            }

        }
        [Test]
        public void ProbarFuncionListarOpciones()
        {
            
            LoginViewModel view = new LoginViewModel();

            
            var result = opcioncontroller.ListOpciones() as ViewResult;

            var model = result.ViewData.Model;

            //Assert fallara ya que modelo devuelve lista de persona mas no persona
            Assert.IsAssignableFrom(typeof(AddEditEncuestaOpcionViewModel), model);
        }
        [Test]
        public void ProbarEliminarOpcion()
        {
            LoginViewModel view = new LoginViewModel();

            var entidades = new INEIEntities();
            var opcion = entidades.EncuestaOpcion.FirstOrDefault();

            var result = opcioncontroller.EliminarOpcion(opcion.EncuestaOpcionId);

            //Assert verificara si el modelo pertenecia a una opcion
            Assert.IsAssignableFrom(typeof(RedirectToRouteResult), result);

            var routeResult = result as RedirectToRouteResult;
            Assert.That(routeResult.RouteValues["action"], Is.EqualTo("ListOpciones"));

        }

    }
}
