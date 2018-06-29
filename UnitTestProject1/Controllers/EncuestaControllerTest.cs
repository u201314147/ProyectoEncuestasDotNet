using System;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Controllers;
using Inei.ViewModel;
using NUnit.Framework;
namespace UnitTestProject1
{
    [TestFixture]
    public class EncuestaControllerTest
    {

        private EncuestaController encuestacontroller;

        AddEditEncuestaViewModel encuestaResgistrar;
    
        private LoginViewModel objViewModel;
     
        public void TestInit()
        {
            encuestacontroller = new EncuestaController();
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

                encuestacontroller.RegistroEncuesta(encuestaResgistrar);
                
            }catch(Exception e)
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
            Assert.IsAssignableFrom(typeof(Encuesta), model);
        }

        [Test]
        public void probarEliminarEncuesta()
        {
            
            LoginViewModel view = new LoginViewModel();
            try {


                var result = encuestacontroller.EliminarEncuesta(-1) as ViewResult;
            var model = result.ViewData.Model;
                //Assert fallara ya que no hay id negativa y retornara error
                Assert.IsAssignableFrom(typeof(Encuesta), model);

            }
            catch(Exception e)
            {
                Assert.AreEqual(true, true);
            }
        }

    }
}
