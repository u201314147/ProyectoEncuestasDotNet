using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using System.Security.Principal;

namespace WebApplication1.Security
{
    public class CustomPrincipal : IPrincipal
    {
        public Usuario usuarios;
        public IIdentity Identity { get; set; }

        public CustomPrincipal(Usuario usuario)
        {
            usuarios = usuario;
            Identity = new GenericIdentity(usuario.Codigo);
        }

        public bool IsInRole(string role)
        {
            return role.Contains(usuarios.Rol);
        }
    }
}