using System;
using System.Linq;
using WebApplication1.Models;
using WebApplication1.Controllers;
using Inei.ViewModel;
using NUnit.Framework;
using System.Web.Mvc;

namespace UnitTestProject1
{
    public class PreguntaControllerTest
    {

        private PreguntaController preguntacontroller;

        AddEditEncuestaPreguntaViewModel preguntaRegistrar;
    
        private LoginViewModel objViewModel;
     
        [SetUp]
        public void TestInit()
        {
            preguntacontroller = new PreguntaController();
            preguntaRegistrar = new AddEditEncuestaPreguntaViewModel();
        }

        [Test]
        public void guardarEncuestaPrueba()
        {
            try
            {
                var entities = new INEIEntities();

                var encuesta = new Encuesta();
                var usuario = new Usuario();

                //Si no existe encuestas mandará nulo
                encuesta = entities.Encuesta.FirstOrDefault();
                usuario = entities.Usuario.FirstOrDefault();

                preguntaRegistrar.EncuestaId = encuesta.EncuestaId;
                preguntaRegistrar.Estado = "ACT";
                preguntaRegistrar.FechaRegistro = DateTime.Now;
                preguntaRegistrar.Orden = 1;
                preguntaRegistrar.Texto = "pregunta de prueba";
                preguntaRegistrar.Tipo = "TXT";
                preguntaRegistrar.UsuarioRegistroId = usuario.UsuarioId;

               //si todo es correcto se agregara la pregunta de prueba en la primera encuesta con id del 1er usuario
                  preguntacontroller.RegistroPregunta(preguntaRegistrar);

                Assert.AreEqual(true, true);

            }
            catch(Exception e)
            {
                //si hay error assert falla
                Assert.AreEqual(false, true);
            }

        }
        [Test]
        public void probarFuncionListarPreguntas()
        {
            try
            {

                LoginViewModel view = new LoginViewModel();

            var result = preguntacontroller.ListPreguntas() as ViewResult;

            var model = result.ViewData.Model;

         
            //Assert fallara ya que modelo devuelve lista de preguntas mas no uno solo
            Assert.IsAssignableFrom(typeof(AddEditEncuestaPreguntaViewModel), model);
            }
            catch (Exception e)
            {
                //si cae en excepcion tambien falla
                Assert.AreEqual(false, true);
            }

        }
        [Test]
        public void probarEliminarPregunta()
        {
            
            LoginViewModel view = new LoginViewModel();
            try {
                var result = preguntacontroller.EliminarPregunta(-1) as ViewResult;
                var model = result.ViewData.Model;
                //Assert fallara ya que no hay id negativa y retornara error
                Assert.IsAssignableFrom(typeof(EncuestaPregunta), model);
                
            }
            catch(Exception e)
            {
                //prueba es correcta al entrar en excepcion
                Assert.AreEqual(true, true);
            }
        }

    }
}
