using Practica2.Models;
using Practica2.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practica2.Controllers
{
    public class CreditoDebitoController : Controller
    {
        private ConsultasService consultaService;
        private LogService logService;

        private UsuarioModels usuarioLogged;

        public CreditoDebitoController()
        {
            consultaService = new ConsultasService();
            logService = new LogService();

            usuarioLogged = logService.getLoggedUser();
        }

        // GET: Credito
        public ActionResult Credito()
        {

            return View();
        }

        
        [HttpPost]
        public ActionResult Acreditar(string cuenta, string monto, string descripcion)
        {
            try
            {
                CreditoDebitoModels credito = new CreditoDebitoModels()
                {
                    cuenta = new CuentaModels()
                    {
                        id_cuenta = Convert.ToInt32(cuenta) 
                    },
                    monto = Convert.ToDouble(monto),
                    descripcion = descripcion,
                    fecha = DateTime.Now,
                    tipo = 1
                };
                consultaService.acreditarDebitar(credito);
                ViewBag.MsgSaldo = "Acreditacion exitosa";
            }
            catch (Exception)
            {
                ViewBag.MsgSaldo += "Hay problemas al realizar el credito";
                throw;
            }
            return View("Credito");
        }

        // GET: Credito
        public ActionResult Debito()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Debitar(string cuenta, string monto, string descripcion)
        {
            try
            {
                CreditoDebitoModels debito = new CreditoDebitoModels()
                {
                    cuenta = new CuentaModels()
                    {
                        id_cuenta = Convert.ToInt32(cuenta)
                    },
                    monto = Convert.ToDouble(monto),
                    descripcion = descripcion,
                    fecha = DateTime.Now,
                    tipo = 0
                };
                consultaService.acreditarDebitar(debito);
                ViewBag.MsgSaldo = "Debitacion exitosa";
            }
            catch (Exception)
            {
                ViewBag.MsgSaldo += "Hay problemas al realizar el debito";
                throw;
            }
            return View("Debito");
        }

        // GET: Credito
        public ActionResult PagoServicios()
        {
            return View();
        }


        [HttpPost]
        public ActionResult PagarServicios(string nombre, string cuenta_servicio, string monto, string descripcion)
        {
            try
            {
                ServicioModels serviciosModel = new ServicioModels()
                {
                    cuenta_servicio = new CuentaModels()
                    {
                        id_cuenta = Convert.ToInt32(cuenta_servicio)
                    },
                    nombre = nombre,
                    monto = Convert.ToDouble(monto),
                    descripcion = descripcion,
                    fecha = DateTime.Now,
                    cuenta_origen = consultaService.getCuenta(usuarioLogged.id_usuario)
                };
                consultaService.pagarServicio(serviciosModel);
                ViewBag.MsgSaldo = "Se pagaron Q." + monto + " a nombre de " + nombre + " en la cuenta  #" + cuenta_servicio;
                if(descripcion != "")
                    ViewBag.MsgSaldo += "\npor motivo de " + descripcion;
            }
            catch (Exception)
            {
                ViewBag.MsgSaldo += "Hay problemas al realizar el pago";
                throw;
            }
            return View("PagoServicios");
        }

        
    }
}