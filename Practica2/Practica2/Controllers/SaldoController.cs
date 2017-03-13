using Practica2.Models;
using Practica2.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practica2.Controllers
{
    public class SaldoController : Controller
    {
        private ConsultasService consultaService;
        private LogService logService;

        private UsuarioModels usuarioLogged;
        public SaldoController()
        {
            consultaService = new ConsultasService();
            logService = new LogService();

            usuarioLogged = logService.getLoggedUser();
        }

        // GET: Saldo
        public ActionResult Consulta()
        {
            var cuenta = consultaService.getCuenta(usuarioLogged.id_usuario);
            cuenta.usuario = usuarioLogged;
            return View(cuenta);
        }

        // GET: Saldo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

      
    }
}
