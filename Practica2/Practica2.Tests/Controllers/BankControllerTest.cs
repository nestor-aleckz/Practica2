using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practica2.Controllers;
using Practica2.Models;
using Practica2.Servicios;
namespace Practica2.Tests.Controllers
{
    /// <summary>
    /// Summary description for BankController
    /// </summary>
    [TestClass]
    public class BankControllerTest
    {
       

        
        [TestMethod]
        public void existaCuentaDestino()
        {
            //Arrange
            TransferenciaService serv = new TransferenciaService();
            String cuenta = "100";
            bool esperado = true;
            //Acts
            CuentaModels cuenta_Destino = serv.getCuenta(cuenta,0);
            bool resultado = false;
            if(cuenta_Destino != null)
            {
                resultado = true;
            }

            //Asert
            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void verificarMonto()
        {
            //Arrange
            TransferenciaService serv = new TransferenciaService();
            String usuario = "2";
            double monto = 10.00;
            bool esperado = true;
            //Acts
            CuentaModels cuenta_Origen = serv.getCuenta(usuario, 1);
            bool resultado = false;
            if (cuenta_Origen != null)
            {
                if(cuenta_Origen.saldo > monto){
                    resultado = true;
                }
            }

            //Asert
            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void realizarTransferencia() 
        {
            //Arrange
            TransferenciaModels transferencia = new TransferenciaModels();
            transferencia.monto = 10.00;
            TransferenciaService serT = new TransferenciaService();
            DateTime hoy = DateTime.Today;
            transferencia.fecha = hoy;
            transferencia.cuenta_destino = serT.getCuenta("100", 0);
            transferencia.cuenta_origen = serT.getCuenta("2", 1);
            bool esperado = true;

            //Acts
            bool resultado = serT.realizarTransferencia(transferencia);

            //Asert
            Assert.AreEqual(esperado,resultado);
        }
    }
}
