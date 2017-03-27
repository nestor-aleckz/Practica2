using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practica2;
using Practica2.Controllers;
using Practica2.Servicios;
using Practica2.Models;
using System.Web;
namespace Practica2.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Login()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Login() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
       
        [TestMethod]
        public void existeUsuario() 
        { 
            //Arrange
            UserService serv = new UserService();
            String usuario = "pao";
            bool esperado = true;
            //Acts
            bool resultado = serv.existeUsuario(usuario);
            
            //Asert
            Assert.AreEqual(esperado,resultado);
        }

        [TestMethod]
        public void Ingresar() 
        {
            //Arrange 
            String codigo = "2";
            String user = "pao";
            String pass = "1234";
            bool esperado = true;
            UserService serv = new UserService();
            UsuarioModels usuarioEsperado = new UsuarioModels()
            {
                id_usuario = 2,
                usuario = "pao",
                correo = "pao@gmail.com",
                contrasenia = "1234",
                nombre = "Pao",
                apellido = "Del Cid"
            };
            //Acts
            UsuarioModels resultado = serv.getUsuario(codigo,user,pass) as UsuarioModels;
            bool respuesta = false;
            
            if(resultado != null){
                if(usuarioEsperado.nombre == resultado.nombre)
                {
                    respuesta = true;
                }
            }

            //Asert
            Assert.AreEqual(esperado, respuesta);
            
        }

        [TestMethod]
        public void Registrar() 
        {
            //Arrange 
            UserService serv = new UserService();
            UsuarioModels usuario = new UsuarioModels()
            {
                id_usuario = 16,
                usuario = "nuevo",
                correo = "nuevo@gmail.com",
                contrasenia = "1234",
                nombre = "Nuevo",
                apellido = "Usuario"
            };
            int esperado = 1;
            //Acts
            int resultado = serv.crearNuevoUsuario(usuario);
            //Asert
            Assert.AreEqual(esperado, resultado);
            
        }

        [TestMethod]
        public void obtenerCodigo() 
        { 
            //Arrange
            UserService serv = new UserService();
            int esperado = 17;
            //Acts
            int resultado = serv.getCodigoUsuario();
            //Asert
            Assert.AreEqual(esperado,resultado);
        }
       
    }
}
