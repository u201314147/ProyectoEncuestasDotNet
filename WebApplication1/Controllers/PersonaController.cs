using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using Inei.ViewModel;
using WebApplication1.Security;
using ExcelDataReader;
using System.Data;
using System.IO;
using System.Transactions;

namespace WebApplication1.Controllers
{
    public class PersonaController : BaseController
    {
        public ActionResult RegistrarPersona()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult RegistrarPersona(AddEditPersonaNaturalViewModel personaRegisto)
        {
            if (!ModelState.IsValid)
                return View(personaRegisto);

            try
            {
                PersonaNatural objPersNatural = context.PersonaNatural.FirstOrDefault(x => x.DNI == personaRegisto.DNI);

                if (objPersNatural == null)
                {
                    if (personaRegisto.DNI == "" || personaRegisto.Password == "" || personaRegisto.DNI == null || personaRegisto.Password == null)
                    {
                        TempData["Mensaje"] = "DNI y contraseña son obligatorios!";
                        return View("RegistrarPersona");
                    }
                    else
                    {
                        personaRegisto.AgregarPersona(personaRegisto);
                        TempData["Mensaje"] = "Se registro correctamente al usuario";
                        return RedirectToAction("Login", "Home");
                    }
                }
                else
                {
                    TempData["Mensaje"] = "Ya existe persona con dni registrado!";
                    return View("RegistrarPersona");
                }



            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Hubo un error: " + ex.Message.ToList();
                return RedirectToAction("Login", "Home");

            }

        }

        [CustomAuthorize(Roles = "ADM")]
        [HttpPost]
        public ActionResult ListPersNatural(LoginViewModel objViewModel)
        {
            PersonaNatural objPersNatural = context.PersonaNatural.FirstOrDefault(x => x.DNI == objViewModel.DNI && x.Password == objViewModel.Password);


            List<PersonaNatural> ListPersNatural = context.PersonaNatural.ToList();
            ViewData["ListPersNatural"] = ListPersNatural;
            ViewData["objPersNatural"] = objPersNatural;

            return View("ListPersNatural");

        }


        [CustomAuthorize(Roles = "ADM")]

        public ActionResult ListPersNatural(int? idPerNat)
        {
            PersonaNatural objPersNatural = context.PersonaNatural.FirstOrDefault(x => x.PersonaNaturalId == idPerNat);
            ViewData["objPersNatural"] = objPersNatural;
            List<PersonaNatural> ListPersNatural = context.PersonaNatural.ToList();
            ViewData["ListPersNatural"] = ListPersNatural;
            return View();
        }

        [CustomAuthorize(Roles = "ADM")]
        public ActionResult RegistrarMasivo()
        {
            return View();
        }

        [CustomAuthorize(Roles = "ADM")]
        [HttpPost]
        public ActionResult AddPersonaMasivo(HttpPostedFileBase Archivo)
        {
            String errores = "";
            IExcelDataReader excelReader = null;

            if (Path.GetExtension(Archivo.FileName).ToLower() == ".xls")
                excelReader = ExcelReaderFactory.CreateBinaryReader(Archivo.InputStream);
            if (Path.GetExtension(Archivo.FileName).ToLower() == ".xlsx")
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(Archivo.InputStream);

            if (excelReader == null)
            {
                TempData["Mensaje"] = "Debe seleccionar un archivo excel";
            }
            else
            {


                DataSet result = excelReader.AsDataSet();
                DataTable dataTable = result.Tables[0];

                using (var transactionScope = new TransactionScope())
                {
                    for (int i = 1; i < dataTable.Rows.Count; i++)
                    {
                        var dni = dataTable.Rows[i][0].ToString();
                        var nombre = dataTable.Rows[i][1].ToString();
                        var appellidopat = dataTable.Rows[i][2].ToString();
                        var appellidomat = dataTable.Rows[i][3].ToString();

                        Random r = new Random();
                        var personaNatural = new PersonaNatural();
                        personaNatural.Estado = "ACT";
                        personaNatural.FechaRegistro = DateTime.Now;       
                        personaNatural.UsuarioRegistroId = SessionPersister.usuario.UsuarioId;
                        personaNatural.Password = r.Next(10000000,99999999).ToString();
                        personaNatural.Nombre = nombre;

                        

                        personaNatural.DNI = dni;
                        personaNatural.ApellidoPaterno = appellidopat;
                        personaNatural.ApellidoMaterno = appellidomat;

                        if (context.PersonaNatural.Where(x => x.DNI == dni).ToList().Count>0)
                        {
                           errores = errores + dni + ", ";
                        }
                        else
                        {
                            context.PersonaNatural.Add(personaNatural);
                          
                        }

                          }

                    if (errores != "")
                        errores = ", sin embargo, hay usuarios que no se agregaron ya que cuentan con DNI registradas: " + errores;

                    TempData["Mensaje"] = "Se realizo la operacion con exito" + errores;

                    context.SaveChanges();
                    transactionScope.Complete();
                }

            
            }
            return RedirectToAction("ListPersNatural", "Persona");

        }


    }
}