using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace Inei.ViewModel
{
    public class AddEditEncuestaViewModel
    {
        public List<Encuesta> LstEncuestas { get; set; }
        public int? EncuestaId { get; set; }

        [Required(ErrorMessage = "Este campo es necesario")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es necesario")]
        public string Descripcion { get; set; }
        public System.DateTime FechaTentativaAplicacion { get; set; }
        public decimal CostoEstimado { get; set; }
        public bool EsAlcanceNacional { get; set; }
        public int? NroEncuestasAplicadas { get; set; }
        public string Estado { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public int? UsuarioRegistroId { get; set; }
       

        public AddEditEncuestaViewModel()
        {
            INEIEntities context = new INEIEntities();
            LstEncuestas = context.Encuesta.ToList();
        }

        public void CargarDatos(int? encuestaId)
        {
            INEIEntities context = new INEIEntities();
            this.EncuestaId = encuestaId;

            if (encuestaId.HasValue)
            {
                Encuesta objEncuest = context.Encuesta.FirstOrDefault(x => x.EncuestaId == encuestaId);
               
                this.CostoEstimado = objEncuest.CostoEstimado;
                this.Descripcion = objEncuest.Descripcion;
                this.EsAlcanceNacional = objEncuest.EsAlcanceNacional;
                this.Estado = objEncuest.Estado;
                this.FechaRegistro = objEncuest.FechaRegistro;
                this.FechaTentativaAplicacion = objEncuest.FechaTentativaAplicacion;
                this.Nombre = objEncuest.Nombre;
                this.NroEncuestasAplicadas = objEncuest.NroEncuestasAplicadas;
                this.UsuarioRegistroId = objEncuest.UsuarioRegistroId;
            }
        }


    }
}