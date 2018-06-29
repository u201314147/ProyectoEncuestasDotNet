using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace Inei.ViewModel
{
    public class AddEditPersonaNaturalViewModel
    {
        public int? PersonaNaturalId { get; set; }
        public string DNI { get; set; }

        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }


        [StringLength(8)]
        public string Password { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public int? UsuarioRegistroId { get; set; }
        public string Estado { get; set; }

        public AddEditPersonaNaturalViewModel()
        {

        }

        public void CargarDatos(int? PersonaNaturalId)
        {
            INEIEntities context = new INEIEntities();

            this.PersonaNaturalId = PersonaNaturalId;
            this.DNI = DNI;
            this.Nombre = Nombre;
            this.ApellidoPaterno = ApellidoPaterno;
            this.ApellidoMaterno = ApellidoMaterno;
            this.Password = Password;
            this.FechaRegistro = FechaRegistro;
            this.UsuarioRegistroId = UsuarioRegistroId;
            this.Estado = Estado;

            if (PersonaNaturalId.HasValue)
            {
                PersonaNatural objPersona = context.PersonaNatural.FirstOrDefault(x => x.PersonaNaturalId == PersonaNaturalId);

                this.DNI = objPersona.DNI;
                this.Nombre = objPersona.Nombre;
                this.ApellidoPaterno = objPersona.ApellidoPaterno;
                this.ApellidoMaterno = objPersona.ApellidoMaterno;
                this.Password = objPersona.Password;
                this.FechaRegistro = objPersona.FechaRegistro;
                this.UsuarioRegistroId = objPersona.UsuarioRegistroId;
                this.Estado = objPersona.Estado;
            }
        }

        public void AgregarPersona(AddEditPersonaNaturalViewModel personaRegistro)
        {
            INEIEntities context = new INEIEntities();
            PersonaNatural persona = new PersonaNatural();
            persona.Nombre = personaRegistro.Nombre;
            persona.DNI = personaRegistro.DNI;
            persona.ApellidoPaterno = personaRegistro.ApellidoPaterno;
            persona.ApellidoMaterno = personaRegistro.ApellidoMaterno;
            persona.Password = personaRegistro.Password;
            persona.FechaRegistro = DateTime.Now;
            persona.UsuarioRegistroId = 1;
            persona.Estado = "ACT";

            context.PersonaNatural.Add(persona);
            context.SaveChanges();

        }
    }
}