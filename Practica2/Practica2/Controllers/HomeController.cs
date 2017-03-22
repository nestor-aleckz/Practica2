using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Practica2.Models;
using Practica2.Servicios;

namespace Practica2.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult User()
        {
            return View();
        }

        public ActionResult Ingresar(String codigo, String user, String pass)
        {
            /* Respuesta */
            String resultado = String.Empty;
            String mensaje = String.Empty;
            UserService serviceUser = new UserService();
            if (serviceUser.existeUsuario(user.Trim()))
            {
                UsuarioModels usuario = serviceUser.getUsuario(codigo.Trim(), user.Trim(), pass.Trim());
                if (usuario != null)
                {
                    resultado = "1";
                    mensaje = usuario.id_usuario.ToString();
                }
                else
                {
                    resultado = "0";
                    mensaje = "Codigo o Contraseña incorrectos, intente de nuevo.";
                }
            }
            else 
            {
                resultado = "0";
                mensaje = "Usuario no existe, intente de nuevo con un usuario que exista.";
            }
            
            return Json(new { resultado = resultado, msj = mensaje });
        }

        public ActionResult RegistrarUsuario(String nombre, String apellido, String correo, String user, String pass)
        {
            /* Respuesta */
            String resultado = String.Empty;
            String mensaje = String.Empty;

            /* Validación y registro de nuevo Usuario*/
            UserService serviceUser = new UserService();
            UsuarioModels usuario = new UsuarioModels();
            usuario.id_usuario = serviceUser.getCodigoUsuario();
            usuario.nombre = nombre.Trim();
            usuario.apellido = apellido.Trim();
            usuario.correo = correo.Trim();
            usuario.usuario = user.Trim();
            usuario.contrasenia = pass.Trim();
            if (!serviceUser.existeUsuario(usuario.usuario))
            {
                int result = serviceUser.crearNuevoUsuario(usuario);
                if (result != 0)
                {
                    resultado = "1";
                    mensaje = "Usuario Creado Correctamente, este es su código de ingreso: " + usuario.id_usuario;
                }
                else
                {
                    resultado = "0";
                    mensaje = "No se completó el Registro, intente nuevamente.";
                }

            }
            else
            {
                resultado = "0";
                mensaje = "No se pudo completar el registro, Usuario ya existe.";
            }
            return Json(new { resultado = resultado, msj = mensaje });
        }
    }
}