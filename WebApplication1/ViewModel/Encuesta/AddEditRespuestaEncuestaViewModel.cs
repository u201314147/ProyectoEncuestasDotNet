using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
namespace Inei.ViewModel
{
    public class AddEditRespuestaEncuestaViewModel
    {
        public List<RespuestaEncuesta> LstRespuestaEncuestas { get; set; }
        public int PersonaNaturalId { get; set; }
        public int EncuestaId { get; set; }
        public System.DateTime FechaEncuesta { get; set; }

        public AddEditRespuestaEncuestaViewModel()
        {
            INEIEntities context = new INEIEntities();
            LstRespuestaEncuestas = context.RespuestaEncuesta.ToList();
        }

    }
}