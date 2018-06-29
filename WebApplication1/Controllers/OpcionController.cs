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
    public class OpcionController : BaseController
    {
        [CustomAuthorize(Roles = "ADM")]
        public ActionResult ListOpciones()
        {

            var objViewModel = new AddEditEncuestaOpcionViewModel();
            return View(objViewModel);
        
        }
        [CustomAuthorize(Roles = "ADM")]
        public ActionResult RegistroOpcion(int? opcionId)
        {

            var objAddEdit = new AddEditEncuestaOpcionViewModel();
            objAddEdit.CargarDatos(opcionId);

            List<EncuestaPregunta> ListPreguntas = context.EncuestaPregunta.ToList();
            ViewData["ListPreguntas"] = ListPreguntas;
            return View(objAddEdit);


        }
        [CustomAuthorize(Roles = "ADM")]
        [HttpPost]
        public ActionResult RegistroOpcion(AddEditEncuestaOpcionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
           try
            {
                var encuestaO = new EncuestaOpcion();
                if (model.EncuestaOpcionId.HasValue)
                {

                    encuestaO = context.EncuestaOpcion.FirstOrDefault(x => x.EncuestaOpcionId == model.EncuestaOpcionId);
                    model.EncuestaOpcionId = model.EncuestaOpcionId.Value;

                }

                else
                {
                    context.EncuestaOpcion.Add(encuestaO);
                }

                encuestaO.EncuestaPreguntaId = model.EncuestaPreguntaId;
                encuestaO.Texto = model.Texto;
                encuestaO.Orden = model.Orden;
                encuestaO.Estado = model.Estado;
                encuestaO.FechaRegistro = DateTime.Now;
                encuestaO.UsuarioRegistroId = SessionPersister.usuario.UsuarioId; ;

            
                context.SaveChanges();

                TempData["Mensaje"] = "Exito! La operación se realizó con éxito";
                return RedirectToAction("ListOpciones", "Opcion");
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error! " + ex.Message.ToList();

                return View(model);
            }

        }

        [CustomAuthorize(Roles = "ADM")]
        public ActionResult EliminarOpcion(int? opcionId)
        {
            try
            {
                EncuestaOpcion objEncuestaO = context.EncuestaOpcion.FirstOrDefault(x => x.EncuestaOpcionId == opcionId);
                context.EncuestaOpcion.Remove(objEncuestaO);


                context.SaveChanges();
                TempData["Mensaje"] = "Exito! La operación se realizó con éxito";
                return RedirectToAction("ListOpciones", "Opcion");

            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error! " + ex.Message.ToList();
                return RedirectToAction("ListOpciones", "Opcion");
            }
        }


    }
}