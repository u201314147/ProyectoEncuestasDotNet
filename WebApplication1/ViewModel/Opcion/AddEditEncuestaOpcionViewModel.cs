using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Models;
namespace Inei.ViewModel
{
    public class AddEditEncuestaOpcionViewModel
    {
        public List<EncuestaOpcion> ListOpciones = new List<EncuestaOpcion>();
        public int? EncuestaOpcionId { get; set; }
        public int EncuestaPreguntaId { get; set; }

        [Required(ErrorMessage = "Este campo es necesario")]
        public string Texto { get; set; }

        [Required(ErrorMessage = "Este campo es necesario")]
        public int Orden { get; set; }

        [StringLength(3)]
        [Required(ErrorMessage = "Este campo es necesario")]
        public string Estado { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public int? UsuarioRegistroId { get; set; }

        public AddEditEncuestaOpcionViewModel()
        {
            INEIEntities context = new INEIEntities();
            ListOpciones = context.EncuestaOpcion.ToList();
        }
        public void CargarDatos(int? opcionId)
        {
            INEIEntities context = new INEIEntities();
            this.EncuestaOpcionId = opcionId;

            if (opcionId.HasValue)
            {
                EncuestaOpcion objEncuest = context.EncuestaOpcion.FirstOrDefault(x => x.EncuestaOpcionId == opcionId);

                this.EncuestaPreguntaId = objEncuest.EncuestaPreguntaId;
                this.Texto = objEncuest.Texto;
                this.Orden = objEncuest.Orden;
                this.Estado = objEncuest.Estado;
                this.FechaRegistro = objEncuest.FechaRegistro;
                this.UsuarioRegistroId = objEncuest.UsuarioRegistroId;

            }
        }

    }
}