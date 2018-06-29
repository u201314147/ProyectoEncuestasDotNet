using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace Inei.ViewModel
{

    public class LoginViewModel
    {
        INEIEntities context = new INEIEntities();
        public String DNI { get; set; }

        [Required(ErrorMessage = "Este campo es necesario")]
        public String Password { get; set; }

        public String Codigo { get; set; }
        public LoginViewModel()
        {

        }

        public Usuario findUser(string login)
        {
            return context.Usuario.FirstOrDefault(x => x.Codigo == login);
        }

        public PersonaNatural findPersona(string login)
        {
            return context.PersonaNatural.FirstOrDefault(x => x.DNI == login);
        }
    }
}