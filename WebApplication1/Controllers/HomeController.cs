using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using Inei.ViewModel;
using WebApplication1.Security;
namespace WebApplication1.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Login()
        {
            AddEditPersonaNaturalViewModel objViewModel = new AddEditPersonaNaturalViewModel();
            return View("Login", objViewModel);


        }

        [HttpPost]
        public ActionResult Login(LoginViewModel objviewmodel)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (!ModelState.IsValid)
            {
                TempData["Mensaje"] = "Rellene todos los campos";
                return View();
            }
            
            try
            {
                PersonaNatural persona = context.PersonaNatural.FirstOrDefault(x => x.DNI == objviewmodel.DNI && x.Password == objviewmodel.Password);
                if (persona == null)
                {
                    TempData["Mensaje"] = "Password o DNI incorrectos";
                    return View();
                }

                if (persona.Estado != null)
                {
                    TempData["Mensaje"] = "Usuario no activo";
                    return View();
                }
                SessionPersister.persona = persona;


                return RedirectToAction("ListEncuestasPersona", "Encuesta");
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error!" + ex.Message.ToList();
                return View(objviewmodel);
                throw;
            }
        }


        public ActionResult Index()
        {
            AddEditPersonaNaturalViewModel objViewModel = new AddEditPersonaNaturalViewModel();
            return View("Login", objViewModel);
        }

        [CustomAuthorize(Roles = "ADM")]
        public ActionResult Principal()
        {
            return View();
        }

       
        
        public ActionResult Contact()
        {
            ViewBag.Message = "Contacto.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Acerca del proyecto.";

            return View();
        }
    }
}