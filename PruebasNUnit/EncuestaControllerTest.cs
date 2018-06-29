using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Inei.ViewModel;
using NUnit.Framework;
using WebApplication1.Controllers;
using WebApplication1.Models;

namespace PruebasNUnit
{
    public class EncuestaControllerTest
    {
        private EncuestaController encuestacontroller;

        AddEditEncuestaViewModel encuestaResgistrar;

        private LoginViewModel objViewModel;

        [SetUp]
        public void TestInit()
        {
            encuestacontroller = new EncuestaController();
            encuestaResgistrar = new AddEditEncuestaViewModel();
        }

        [Test]
        public void guardarEncuestaPrueba()
        {
            try
            {
                encuestaResgistrar.CostoEstimado = 20;
                encuestaResgistrar.Descripcion = "Encuesta para los alumnos de la UPC";
                encuestaResgistrar.EsAlcanceNacional = true;
                encuestaResgistrar.FechaRegistro = DateTime.Now;
                encuestaResgistrar.FechaTentativaAplicacion = DateTime.Now.AddMonths(5);
                encuestaResgistrar.Nombre = "Encuesta UPC";
                encuestaResgistrar.NroEncuestasAplicadas = 20;
                encuestaResgistrar.UsuarioRegistroId = 1;

                var result = encuestacontroller.RegistroEncuesta(encuestaResgistrar);
                Assert.IsInstanceOf(typeof(RedirectToRouteResult), result);
                var routeResult = result as RedirectToRouteResult;
                Assert.That(routeResult.RouteValues["action"], Is.EqualTo("ListEncuestas"));
            }
            catch (Exception e)
            {
                //si hay error assert falla
                Assert.AreEqual(false, true);
            }

        }
        [Test]
        public void probarFuncionListarEncuestas()
        {
            //ahora con listar

            LoginViewModel view = new LoginViewModel();

            var result = encuestacontroller.ListEncuestas() as ViewResult;

            var model = result.ViewData.Model;

            //Assert fallara ya que modelo devuelve lista de encuesta mas no objeto encuesta
            Assert.IsAssignableFrom(typeof(AddEditEncuestaViewModel), model);
        }

        [Test]
        public void probarEliminarEncuesta()
        {

            LoginViewModel view = new LoginViewModel();
            try
            {


                var result = encuestacontroller.EliminarEncuesta(-1) as ViewResult;
                var model = result.ViewData.Model;
                //Assert fallara ya que no hay id negativa y retornara error
                Assert.IsAssignableFrom(typeof(Encuesta), model);

            }
            catch (Exception e)
            {
                Assert.AreEqual(true, true);
            }
        }

    }
}
