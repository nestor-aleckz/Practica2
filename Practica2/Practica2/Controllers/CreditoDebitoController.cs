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
                TransferenciaService service = new TransferenciaService();
                CuentaModels cuentaDestino = service.getCuenta(cuenta.Trim(),0);
                if (cuentaDestino != null)
                {
                    CreditoDebitoModels credito = new CreditoDebitoModels()
                    {
                        cuenta = cuentaDestino,
                        monto = Convert.ToDouble(monto),
                        descripcion = descripcion,
                        fecha = DateTime.Now,
                        tipo = 1
                    };
                    string msgResultado = consultaService.acreditarDebitar(credito);
                    if (msgResultado != "")
                    {
                        ViewBag.MsgSaldo += "Error: " + msgResultado;
                        return View("Credito");
                    }
                    ViewBag.MsgSaldo = "Acreditacion exitosa";
                    return View("Credito");
                }
                else 
                {
                    ViewBag.MsgSaldo = "No existe cuenta Destino, favor verificar.";
                    return View("Debito");
                }
            }
            catch (Exception)
            {
                ViewBag.MsgSaldo += "Hay problemas al realizar el credito o hay datos incorrectos";
                return View("Credito");
            }
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
                TransferenciaService service = new TransferenciaService();
                CuentaModels cuentaDestino = service.getCuenta(cuenta.Trim(),0);
                if (cuentaDestino != null)
                {
                    CreditoDebitoModels debito = new CreditoDebitoModels()
                    {
                        cuenta = cuentaDestino,
                        monto = Convert.ToDouble(monto),
                        descripcion = descripcion,
                        fecha = DateTime.Now,
                        tipo = 0
                    };
                    string msgResultado = consultaService.acreditarDebitar(debito);
                    if (msgResultado != "")
                    {
                        ViewBag.MsgSaldo += "Error: " + msgResultado;
                        return View("Debito");
                    }
                    ViewBag.MsgSaldo = "Debitacion exitosa";
                    return View("Debito");
                }
                else
                {
                    ViewBag.MsgSaldo = "No existe cuenta Destino, favor verificar.";
                    return View("Debito");
                }
            }
            catch (Exception)
            {
                ViewBag.MsgSaldo += "Hay problemas al realizar el debito o hay datos incorrectos";
                return View("Debito");
            }
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
                string msgResultado = consultaService.pagarServicio(serviciosModel);
                if (msgResultado != "")
                {
                    ViewBag.MsgSaldo += "Error: " + msgResultado;
                    return View("PagoServicios");
                }
                ViewBag.MsgSaldo = "Se pagaron Q." + monto + " a nombre de " + nombre + " en la cuenta  #" + cuenta_servicio;
                if(descripcion != "")
                    ViewBag.MsgSaldo += "\npor motivo de " + descripcion;

                return View("PagoServicios");
            }
            catch (Exception)
            {
                ViewBag.MsgSaldo += "Hay problemas al realizar el pago o hay datos incorrectos";
                return View("PagoServicios");
            }
        }

        
    }
}