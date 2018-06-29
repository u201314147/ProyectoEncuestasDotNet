using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Models;
namespace Inei.ViewModel
{
    public class AddEditEncuestaPreguntaViewModel
    {
        public List<EncuestaPregunta> LstPreguntas = new List<EncuestaPregunta>();
        public int? EncuestaPreguntaId { get; set; }
        public int EncuestaId { get; set; }

        [Required(ErrorMessage = "Este campo es necesario")]
        public string Texto { get; set; }

        [StringLength(3)]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "Este campo es necesario")]
        public int Orden { get; set; }

        [StringLength(3)]
        public string Estado { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public int? UsuarioRegistroId { get; set; }

        public AddEditEncuestaPreguntaViewModel()
        {
            INEIEntities context = new INEIEntities();
            LstPreguntas = context.EncuestaPregunta.ToList();
        }


        public void CargarDatos(int? preguntaId)
        {
            INEIEntities context = new INEIEntities();
            this.EncuestaPreguntaId = preguntaId;

            if (preguntaId.HasValue)
            {
                EncuestaPregunta objEncuest = context.EncuestaPregunta.FirstOrDefault(x => x.EncuestaPreguntaId == preguntaId);

                this.EncuestaId = objEncuest.EncuestaId;
                this.Estado = objEncuest.Estado;
                this.FechaRegistro = objEncuest.FechaRegistro;
                this.Orden = objEncuest.Orden;
                this.Texto = objEncuest.Texto;
                this.Tipo = objEncuest.Tipo;
                this.UsuarioRegistroId = objEncuest.UsuarioRegistroId;
               
            }
        }

    }
}