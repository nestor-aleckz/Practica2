using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practica2.Models
{
    public class UsuarioModels
    {
        public int id_usuario { get; set; }
        public string usuario { get; set; }
        public string correo { get; set; }
        public string contrasenia { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }


    }
}