using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practica2.Controllers;
using Practica2.Models;
using Practica2.Servicios;

namespace Practica2.Tests.Controllers
{
    [TestClass]
    public class SaldoControllerTest
    {
        [TestMethod]
        public void getSaldo()
        {
            //Arrange
            ConsultasService service = new ConsultasService();
            int id_usuarioLogged =11;
            double esperado = 2210.00;
            //Acts
            CuentaModels cuenta = service.getCuenta(id_usuarioLogged);
            double resultado = cuenta.saldo;
            //Asert
            Assert.AreEqual(esperado, resultado);
        }
    }
}
