using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using Inei.ViewModel;
using WebApplication1.Security;
using System.Transactions;
namespace WebApplication1.Controllers
{
    public class EncuestaController : BaseController
    {
        public ActionResult VerEncuesta(int? encuestaId, int? personaId, int? respuestaId)
        {
            var ListaRespuestas = new AddEditRespEncuestaOpcionViewModel().LstRespEncOpc;

            var xd = context.RespuestaEncuesta.FirstOrDefault(x => x.EncuestaId == encuestaId && x.PersonaNaturalId == personaId && x.RespuestaEncuestaId ==respuestaId);
            var xd2 = context.RespuestaEncuestaOpcion.Where(x => x.RespuestaEncuestaId == xd.RespuestaEncuestaId).ToList();
                 ViewData["LstaResps"] = xd2;
            

                return View();
        }

        public ActionResult ResponderEncuesta(int? encuestaId)
        {
            int? encuestaSelect = encuestaId;

            if (encuestaSelect.HasValue)
            {
                Encuesta encuesta = context.Encuesta.Find(encuestaSelect);
                List<EncuestaPregunta> preguntas = context.EncuestaPregunta.Where(x => x.EncuestaId == encuestaSelect).ToList();

                List<List<EncuestaOpcion>> fullopciones = new List<List<EncuestaOpcion>>();
                foreach (var preg in preguntas)
                {
                    int preguntaID = preg.EncuestaPreguntaId;
                    List<EncuestaOpcion> opciones = context.EncuestaOpcion.Where(x => x.EncuestaPreguntaId == preguntaID).ToList();

                    fullopciones.Add(opciones);
                }
                ViewData["encuesta"] = encuesta;
                ViewData["preguntas"] = preguntas;
                ViewData["opciones"] = fullopciones;

                return View();
            }
            else
            {

                return RedirectToAction("Principal", "Home");
            }




        }


        [HttpPost]
        public ActionResult ResponderEncuesta(List<Int32> LstOpcionesResp, List<String> LstOpcionString , List<String> LstOpcionStringNum, int EncuestaIdx)
        {
          
            try {


             //   using (var transactionScope = new TransactionScope())
             //   {
                    RespuestaEncuesta respuestaEnc = new RespuestaEncuesta();

                    if (LstOpcionesResp.Count != 0)
                    {
                        respuestaEnc.EncuestaId = EncuestaIdx;
                        respuestaEnc.PersonaNaturalId = SessionPersister.persona.PersonaNaturalId;
                        respuestaEnc.FechaEncuesta = DateTime.Now;
                        context.RespuestaEncuesta.Add(respuestaEnc);


                        //context.SaveChanges();

                        foreach (var opc in LstOpcionesResp)
                        {
                            RespuestaEncuestaOpcion Respuesta = new RespuestaEncuestaOpcion();

                            Respuesta.EncuestaOpcionId = opc;
                            Respuesta.RespuestaEncuesta= respuestaEnc;
                            Respuesta.Valor = "1";

                            context.RespuestaEncuestaOpcion.Add(Respuesta);
                            //context.SaveChanges();
                        }
                    }


                    var preguntaOpc = context.EncuestaOpcion.Where(x => x.EncuestaPregunta.Tipo == "TXT" && x.EncuestaPregunta.EncuestaId == EncuestaIdx).ToList();

                if(LstOpcionString !=null)
                { 
                    int i = LstOpcionString.Count - 1;
                    foreach (var prTx in preguntaOpc)
                    {
                        RespuestaEncuestaOpcion Respuesta = new RespuestaEncuestaOpcion();

                        Respuesta.EncuestaOpcionId = prTx.EncuestaOpcionId;
                        Respuesta.RespuestaEncuesta = respuestaEnc;
                        Respuesta.Valor = LstOpcionString.ElementAt(i);

                        context.RespuestaEncuestaOpcion.Add(Respuesta);
                      //  context.SaveChanges();
                        i++;


                    }
                }

                var preguntaOpcNum = context.EncuestaOpcion.Where(x => x.EncuestaPregunta.Tipo == "NUM" && x.EncuestaPregunta.EncuestaId == EncuestaIdx).ToList();

                if (LstOpcionStringNum != null)
                {
                    int j = LstOpcionStringNum.Count - 1;
                    foreach (var prTxN in preguntaOpcNum)
                    {
                        RespuestaEncuestaOpcion Respuesta = new RespuestaEncuestaOpcion();

                        Respuesta.EncuestaOpcionId = prTxN.EncuestaOpcionId;
                        Respuesta.RespuestaEncuesta = respuestaEnc;
                        Respuesta.Valor = LstOpcionStringNum.ElementAt(j);

                        context.RespuestaEncuestaOpcion.Add(Respuesta);
                        //  context.SaveChanges();
                        j++;


                    }
                }

                context.SaveChanges();
                TempData["Mensaje"] = "Se registro correctamente su encuesta, gracias por participar!";
                    return RedirectToAction("ListEncuestasPersona", "Encuesta");
              //  }
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error! " + ex.Message.ToList();
                return RedirectToAction("ListEncuestas", "Encuesta");
            }
        
        }

        [CustomAuthorize(Roles = "ADM")]
        public ActionResult ListEncuestas()
        {
            var objViewModel = new AddEditEncuestaViewModel();
            return View(objViewModel);

        }

        public ActionResult ListEncuestasPersona()
        {
           
            var objViewModel = new AddEditEncuestaViewModel();
            return View(objViewModel);

        }


        public ActionResult ListRespuestaEncuestasPersona(int? encuestaId)
        {
            ViewData["encuesta"] = encuestaId;
            var objViewModel = new AddEditRespuestaEncuestaViewModel();
            return View(objViewModel);

        }


       public ActionResult ListRespuestaEncuestasPersonaTodas()
        {
            var objViewModel = new AddEditRespuestaEncuestaViewModel();
            return View(objViewModel);

        }

        [CustomAuthorize(Roles = "ADM")]
        public ActionResult RegistroEncuesta(int? encuestaId)
        {

            AddEditEncuestaViewModel objAddEdit = new AddEditEncuestaViewModel();
            objAddEdit.CargarDatos(encuestaId);
            
            return View(objAddEdit);
          
        }

        [CustomAuthorize(Roles = "ADM")]
        [HttpPost]
        public ActionResult RegistroEncuesta(AddEditEncuestaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
           try
            {
                var encuesta = new Encuesta();
                if (model.EncuestaId.HasValue)
                {

                    encuesta = context.Encuesta.FirstOrDefault(x => x.EncuestaId == model.EncuestaId);
                    model.EncuestaId = model.EncuestaId.Value;

                }

                else
                {
                    context.Encuesta.Add(encuesta);
                }
                  
                encuesta.Nombre = model.Nombre;
                encuesta.CostoEstimado = model.CostoEstimado;
                encuesta.Descripcion = model.Descripcion;
                encuesta.Estado = "ACT";
                encuesta.FechaRegistro = DateTime.Now;
                encuesta.FechaTentativaAplicacion = model.FechaTentativaAplicacion;
                encuesta.NroEncuestasAplicadas = 0;
                if (!model.UsuarioRegistroId.HasValue)
                    encuesta.UsuarioRegistroId = SessionPersister.usuario.UsuarioId;
                else
                    encuesta.UsuarioRegistroId = model.UsuarioRegistroId.Value;
                encuesta.EsAlcanceNacional = model.EsAlcanceNacional;


                context.SaveChanges();
                TempData["Mensaje"] = "Exito! La operación se realizó con éxito";
                return RedirectToAction("ListEncuestas", "Encuesta");
            }
            catch(Exception ex)
            {
                TempData["Mensaje"] = "Error! " + ex.Message.ToList();

                return View(model);
            }

        }
        [CustomAuthorize(Roles = "ADM")]
        public ActionResult EliminarEncuesta(int? encuestaId)
        {
            try
            {
                Encuesta objEncuesta = context.Encuesta.FirstOrDefault(x => x.EncuestaId == encuestaId);
                context.Encuesta.Remove(objEncuesta);


                context.SaveChanges();
                TempData["Mensaje"] = "Exito! La operación se realizó con éxito";
                return RedirectToAction("ListEncuestas", "Encuesta");

            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error! " + ex.Message.ToList();
                return RedirectToAction("ListEncuestas", "Encuesta");
            }
        }

    }
}