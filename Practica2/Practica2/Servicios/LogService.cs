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
            int id_Usuario = 2;
            var usuario = consultaService.getUser(id_Usuario);

            return usuario;
        }
    }
}