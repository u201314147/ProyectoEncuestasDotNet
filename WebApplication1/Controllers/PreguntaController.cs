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
    public class PreguntaController : BaseController
    {
        [CustomAuthorize(Roles = "ADM")]
        public ActionResult ListPreguntas()
        {
            var objViewModel = new AddEditEncuestaPreguntaViewModel();
            return View(objViewModel);
           
        }

        [CustomAuthorize(Roles = "ADM")]
        public ActionResult RegistroPregunta(int? preguntaId)
        {

            var objAddEdit = new AddEditEncuestaPreguntaViewModel();
            objAddEdit.CargarDatos(preguntaId);

            List<Encuesta> ListEncuestas = context.Encuesta.ToList();
            ViewData["ListEncuestas"] = ListEncuestas;

            return View(objAddEdit); 
        }
        [CustomAuthorize(Roles = "ADM")]
        [HttpPost]
        public ActionResult RegistroPregunta(AddEditEncuestaPreguntaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var encuestaP = new EncuestaPregunta();
                if (model.EncuestaPreguntaId.HasValue)
                {

                    encuestaP = context.EncuestaPregunta.FirstOrDefault(x => x.EncuestaPreguntaId == model.EncuestaPreguntaId);
                    model.EncuestaPreguntaId = model.EncuestaPreguntaId.Value;

                }

                else
                {
                    context.EncuestaPregunta.Add(encuestaP);
                }


                encuestaP.EncuestaId = model.EncuestaId;
                encuestaP.Texto = model.Texto;
                encuestaP.Tipo = model.Tipo;
                encuestaP.Orden = model.Orden;
                encuestaP.Estado = model.Estado;
                encuestaP.FechaRegistro = DateTime.Now;
                encuestaP.UsuarioRegistroId = SessionPersister.usuario.UsuarioId; ;

                context.SaveChanges();

                TempData["Mensaje"] = "Exito! La operación se realizó con éxito";
                return RedirectToAction("ListPreguntas", "Pregunta");
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error! " + ex.Message.ToList();

                return View(model);
            }


        }
        [CustomAuthorize(Roles = "ADM")]
        public ActionResult EliminarPregunta(int? preguntaId)
        {
            try
            {
                EncuestaPregunta objEncuestaP = context.EncuestaPregunta.FirstOrDefault(x => x.EncuestaPreguntaId == preguntaId);
                context.EncuestaPregunta.Remove(objEncuestaP);


                context.SaveChanges();
                TempData["Mensaje"] = "Exito! La operación se realizó con éxito";
                return RedirectToAction("ListPreguntas", "Pregunta");

            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error! " + ex.Message.ToList();
                return RedirectToAction("ListPreguntas", "Pregunta");
            }
        }


    }
}