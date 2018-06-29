using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace Inei.ViewModel
{
    public class AddEditUsuarioViewModel
    {
        public int? UsuarioId { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Rol { get; set; }
        public string Estado { get; set; }

        public AddEditUsuarioViewModel()
        {

        }

        public void CargarDatos(int? UsuarioId)
        {
            INEIEntities context = new INEIEntities();

            this.UsuarioId = UsuarioId;
            this.Codigo = Codigo;
            this.Nombre = Nombre;
            this.Apellidos = Apellidos;
            this.Rol = Rol;
            this.Estado = Estado;

            if (UsuarioId.HasValue)
            {
                Usuario objUsuario = context.Usuario.FirstOrDefault(x => x.UsuarioId == UsuarioId);

                this.Codigo = objUsuario.Codigo;
                this.Nombre = objUsuario.Nombre;
                this.Apellidos = objUsuario.Apellidos;
                this.Rol = objUsuario.Rol;
                this.Estado = objUsuario.Estado;

            }
        }
    }
}