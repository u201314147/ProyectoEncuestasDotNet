using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
namespace WebApplication1.Security
{
    public class SessionPersister
    {
        static string usuarioSession = "objusuario";

        public static Usuario usuario
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return null;
                }
                var sessionvar = HttpContext.Current.Session[usuarioSession];
                if (sessionvar != null)
                {
                    return sessionvar as Usuario;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[usuarioSession] = value;
            }
        }

        public static PersonaNatural persona
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return null;
                }
                var sessionvar = HttpContext.Current.Session[usuarioSession];
                if (sessionvar != null)
                {
                    return sessionvar as PersonaNatural;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[usuarioSession] = value;
            }
        }
    }
}