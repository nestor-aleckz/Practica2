using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practica2.Servicios;
using Practica2.Models;
using Practica2.Controllers;

namespace Practica2.Tests.Controllers
{
    /// <summary>
    /// Summary description for DebitoCreditoControllerTest
    /// </summary>
    [TestClass]
    public class DebitoCreditoControllerTest
    {
        
        [TestMethod]
        public void existeCuenta()
        {
            //Arrange
            TransferenciaService serv = new TransferenciaService();
            String cuenta = "100"; 
            bool esperado = true;
            //Acts
            bool resultado = false;
            CuentaModels cuenta_Destino = serv.getCuenta(cuenta,0);
            if(cuenta_Destino != null)
            {
                resultado = true;
            }
            //Asert
            Assert.AreEqual(esperado, resultado);
        }

         [TestMethod]
        public void tieneFondos()
        {
            //Arrange
            ConsultasService service = new ConsultasService();
            double saldo = 970.00;
            double monto = 10.00;
            bool esperado = true;
            //Acts
            bool resultado = service.tieneFondos(saldo,monto);
            //Asert
            Assert.AreEqual(esperado, resultado);
        }

         [TestMethod]
         public void existeSaldo()
         {
             //Arrange
             ConsultasService service = new ConsultasService();
             int id_cuenta = 100;
             bool esperado = true;
             //Acts
             double saldo = service.getSaldoCuenta(id_cuenta); ;
             bool resultado =  saldo > 0.00 ? true: false; 
             //Asert
             Assert.AreEqual(esperado, resultado);
         }

         [TestMethod]
         public void debitar()
         {
             //Arrange
             ConsultasService service = new ConsultasService();
             TransferenciaService serv = new TransferenciaService();
             CuentaModels cuentaDestino = serv.getCuenta("500", 0);
             CreditoDebitoModels debito = new CreditoDebitoModels()
             {
                 cuenta = cuentaDestino,
                 monto = 100.00,
                 descripcion = "Nota de Debito",
                 fecha = DateTime.Now,
                 tipo = 0
             };
             bool esperado = true;

             
             //Acts
             string msgResultado = service.acreditarDebitar(debito);
             
             bool resultado = msgResultado == "" ? true : false;
             //Asert
             Assert.AreEqual(esperado, resultado);
         }

         [TestMethod]
         public void acreditar()
         {
             //Arrange
             ConsultasService service = new ConsultasService();
             TransferenciaService serv = new TransferenciaService();
             CuentaModels cuentaDestino = serv.getCuenta("900", 0);
             CreditoDebitoModels debito = new CreditoDebitoModels()
             {
                 cuenta = cuentaDestino,
                 monto = 200.00,
                 descripcion = "Nota de Credito",
                 fecha = DateTime.Now,
                 tipo = 1
             };
             bool esperado = true;


             //Acts
             string msgResultado = service.acreditarDebitar(debito);

             bool resultado = msgResultado == "" ? true : false;
             //Asert
             Assert.AreEqual(esperado, resultado);
         }

         [TestMethod]
         public void pagoServicio()
         {
             //Arrange
             ConsultasService service = new ConsultasService();
             int usuarioLogged = 2;
             ServicioModels serviciosModel = new ServicioModels()
             {
                 cuenta_servicio = new CuentaModels()
                 {
                     id_cuenta = 100
                 },
                 nombre = "Tigo",
                 monto = 100.00,
                 descripcion = "Pago de Servicio",
                 fecha = DateTime.Now,
                 cuenta_origen = service.getCuenta(usuarioLogged)
             };

             bool esperado = true;
             //Acts
             string msgResultado = service.pagarServicio(serviciosModel);
             bool resultado = msgResultado == "" ? true : false;
             
             //Asert
             Assert.AreEqual(esperado, resultado);
         }

                

    }
}
