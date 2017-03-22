using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Practica2.Servicios;
using Practica2.Models;
namespace Practica2.Controllers
{
    public class BankController : Controller
    {
        //
        // GET: /Bank/
        public ActionResult Inicio(String usuario)
        {
            Session["usuario"] = usuario;
            UserService uSer = new UserService();

            UsuarioModels objUser = uSer.getUsuario(usuario);
            
            return View(objUser);
        }
        public ActionResult Transferencia()
        {
            return View();
        }

        public ActionResult Transferir(String cuenta, String monto)
        {
            /* Respuesta */
            String resultado = String.Empty;
            String mensaje = String.Empty;

            /* Realizar Transferencia */
            TransferenciaModels transferencia = new TransferenciaModels();
            transferencia.monto = double.Parse(monto.Trim());
            TransferenciaService serT = new TransferenciaService();
            DateTime hoy = DateTime.Today;
            transferencia.fecha = hoy;
            transferencia.cuenta_destino = serT.getCuenta(cuenta.Trim(), 0);
            transferencia.cuenta_origen = serT.getCuenta(Session["Usuario"].ToString(), 1);
            if (transferencia.cuenta_destino != null)
            {
                if (transferencia.cuenta_origen.saldo > transferencia.monto)
                {
                    if (serT.realizarTransferencia(transferencia))
                    {
                        resultado = "1";
                        mensaje = "Tranferencia realizada con exito";
                    }
                    else
                    {
                        resultado = "0";
                        mensaje = "No se pudo realizar la transferencia, intente mas tarde.";
                    }
                }
                else 
                {
                    resultado = "0";
                    mensaje = "No se pudo realizar la transferenci, el monto que desea transferir es mayor al Saldo actual.";
                }
            }
            else
            {
                resultado = "0";
                mensaje = "No se pudó realizar la transferencia, Cuenta destino no existe.";
            }
            return Json(new { resultado = resultado, msj = mensaje });
        }

	}
}