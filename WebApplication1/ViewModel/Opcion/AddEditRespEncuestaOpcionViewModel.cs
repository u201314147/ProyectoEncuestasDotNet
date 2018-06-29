using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace Inei.ViewModel
{
    public class AddEditRespEncuestaOpcionViewModel
    {
        public List<RespuestaEncuestaOpcion> LstRespEncOpc = new List<RespuestaEncuestaOpcion>();
        public int RespuestaEncuestaOpcionId { get; set; }
        public int RespuestaEncuestaId { get; set; }
        public int EncuestaOpcionId { get; set; }
        public string valor { get; set; }

        public AddEditRespEncuestaOpcionViewModel()
        {
            INEIEntities context = new INEIEntities();
            LstRespEncOpc = context.RespuestaEncuestaOpcion.ToList();
        }

    }
}