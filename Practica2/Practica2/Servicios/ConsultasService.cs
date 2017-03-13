using Practica2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Practica2.Servicios
{
    public class ConsultasService
    {
        BDService bDservice;

        public ConsultasService()
        {
            bDservice = new BDService();
        }


        public double getSaldo(int id_usuario)
        {
            string saldo = bDservice.SelectUnValorQry("select saldo from dbo.CUENTA where id_usuario = " + id_usuario);
            if (saldo == "")
                return 0;
            return Convert.ToDouble(saldo);
        }
        public CuentaModels getCuenta(int id_Usuario)
        {
            DataTable tabla = bDservice.FillTableData("select * from dbo.CUENTA where id_usuario = " + id_Usuario);
            CuentaModels cuenta = new CuentaModels()
            {
                id_cuenta = Convert.ToInt32(tabla.Rows[0][0].ToString()),
                saldo = Convert.ToDouble(tabla.Rows[0][1].ToString())
            };
            return cuenta;
        }

        public UsuarioModels getUser(int id_Usuario)
        {
            DataTable tabla = bDservice.FillTableData("select * from dbo.USUARIO where id_usuario = " + id_Usuario);
            UsuarioModels usuario = new UsuarioModels()
            {
                usuario = tabla.Rows[0][1].ToString(),
                nombre = tabla.Rows[0][4].ToString(),
                apellido = tabla.Rows[0][5].ToString(),
                correo = tabla.Rows[0][2].ToString(),
                contrasenia = tabla.Rows[0][3].ToString(),
                id_usuario = id_Usuario
            };
            return usuario;
        }

        public void acreditarDebitar(CreditoDebitoModels creditoDebito)
        {
            bDservice.Upd_New_DelUnValorQry("insert into dbo.CREDITODEBITO (id_cuenta,descripcion,fecha,monto,tipo) values (" + creditoDebito.cuenta.id_cuenta + 
                ",'"+ creditoDebito.descripcion+"','" + creditoDebito.fecha + "'," + creditoDebito.monto + ","+ creditoDebito.tipo+")");
            double saldo = Convert.ToDouble(bDservice.SelectUnValorQry("select saldo from dbo.CUENTA where id_cuenta = " + creditoDebito.cuenta.id_cuenta));

            if (creditoDebito.tipo == 1)
                saldo += creditoDebito.monto;
            else
                saldo -= creditoDebito.monto;
            bDservice.Upd_New_DelUnValorQry("update dbo.CUENTA set saldo = " + saldo + " where id_cuenta = " + creditoDebito.cuenta.id_cuenta);
        }

        public void pagarServicio(ServicioModels serviciosModel)
        {
            bDservice.Upd_New_DelUnValorQry("insert into dbo.SERVICIO (id_cuenta_origen,id_cuenta_servicio,nombre,monto,fecha,descripcion) values ("
                + serviciosModel.cuenta_origen.id_cuenta + "," + serviciosModel.cuenta_servicio.id_cuenta + ",'" + serviciosModel.nombre + "'," + serviciosModel.monto
                + ",'" + DateTime.Now + "','" + serviciosModel.descripcion + "')");

            double saldo_origen =  Convert.ToDouble(bDservice.SelectUnValorQry("select saldo from dbo.CUENTA where id_cuenta = " + serviciosModel.cuenta_origen.id_cuenta)) - serviciosModel.monto;
            double saldo_servicio = Convert.ToDouble(bDservice.SelectUnValorQry("select saldo from dbo.CUENTA where id_cuenta = " + serviciosModel.cuenta_servicio.id_cuenta)) + serviciosModel.monto;

            bDservice.Upd_New_DelUnValorQry("update dbo.CUENTA set saldo = " + saldo_origen + " where id_cuenta = " + serviciosModel.cuenta_origen.id_cuenta);
            bDservice.Upd_New_DelUnValorQry("update dbo.CUENTA set saldo = " + saldo_servicio + " where id_cuenta = " + serviciosModel.cuenta_servicio.id_cuenta);
        }
    }
}