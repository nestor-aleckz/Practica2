using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Practica2.Models;

namespace Practica2.Servicios
{
    public class LogService
    {
        private ConsultasService consultaService;

        public LogService()
        {
            consultaService = new ConsultasService();
        }

        public UsuarioModels getLoggedUser()
        {
            int id_Usuario = int.Parse(HttpContext.Current.Session["usuario"].ToString());
            var usuario = consultaService.getUser(id_Usuario);

            return usuario;
        }
    }
}