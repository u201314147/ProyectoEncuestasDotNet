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
    public class LoginController : BaseController
    {

        public ActionResult LoginAdmin()
        {
            LoginViewModel objviewmodel = new LoginViewModel();
            return View(objviewmodel);
        }
         [HttpPost]
        public ActionResult LoginAdmin(LoginViewModel objviewmodel)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (!ModelState.IsValid)
            {
                TempData["Mensaje"] = "Rellene todos los campos";
                return View();
            }

            try
            {
                Usuario usuario = context.Usuario.FirstOrDefault(x => x.Codigo == objviewmodel.Codigo && x.Password == objviewmodel.Password);
                if (usuario == null)
                {
                    TempData["Mensaje"] = "Password o Codigo incorrectos";
                    return View();
                }
                SessionPersister.usuario = usuario;


                return RedirectToAction("Principal", "Home");
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error!" + ex.Message.ToList();
                return View(objviewmodel);
                throw;
            }
        }

        public ActionResult CerrarSesion()
        {
            Session.Clear();
            return RedirectToAction("LoginAdmin", "Login");
        }



    }

       
    
}