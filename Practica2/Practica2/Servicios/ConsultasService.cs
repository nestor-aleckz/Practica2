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

        public string acreditarDebitar(CreditoDebitoModels creditoDebito)
        {

            HttpContext.Current.Session["Error"] = "";
            bDservice.Upd_New_DelUnValorQry("insert into dbo.CREDITODEBITO (id_cuenta,descripcion,fecha,monto,tipo) values (" + creditoDebito.cuenta.id_cuenta + 
                ",'"+ creditoDebito.descripcion+"','" + creditoDebito.fecha + "'," + creditoDebito.monto + ","+ creditoDebito.tipo+")");
            if (HttpContext.Current.Session["Error"].ToString() != "")
                return "No existe cuenta";

            double saldo = getSaldoCuenta(creditoDebito.cuenta.id_cuenta);

            if (creditoDebito.tipo == 1)
                saldo += creditoDebito.monto;
            else
            {
                if (!tieneFondos(saldo, creditoDebito.monto))
                    return "No tiene suficientes fondos";
                saldo -= creditoDebito.monto;
            }
            bDservice.Upd_New_DelUnValorQry("update dbo.CUENTA set saldo = " + saldo + " where id_cuenta = " + creditoDebito.cuenta.id_cuenta);
            return "";
        }

        public string pagarServicio(ServicioModels serviciosModel)
        {
            double saldo_origen = getSaldoCuenta(serviciosModel.cuenta_origen.id_cuenta) ;
            if (!tieneFondos(saldo_origen,serviciosModel.monto))
                return "No tiene suficientes fondos";
    

            bDservice.Upd_New_DelUnValorQry("insert into dbo.SERVICIO (id_cuenta_origen,id_cuenta_servicio,nombre,monto,fecha,descripcion) values ("
                + serviciosModel.cuenta_origen.id_cuenta + "," + serviciosModel.cuenta_servicio.id_cuenta + ",'" + serviciosModel.nombre + "'," + serviciosModel.monto
                + ",'" + DateTime.Now + "','" + serviciosModel.descripcion + "')");

            double saldo_servicio = getSaldoCuenta(serviciosModel.cuenta_servicio.id_cuenta) + serviciosModel.monto;

            bDservice.Upd_New_DelUnValorQry("update dbo.CUENTA set saldo = " + (saldo_origen - serviciosModel.monto) + " where id_cuenta = " + serviciosModel.cuenta_origen.id_cuenta);
            bDservice.Upd_New_DelUnValorQry("update dbo.CUENTA set saldo = " + saldo_servicio + " where id_cuenta = " + serviciosModel.cuenta_servicio.id_cuenta);

            return "";
        }

        public double getSaldoCuenta(int id_Cuenta)
        {
            return Convert.ToDouble(bDservice.SelectUnValorQry("select saldo from dbo.CUENTA where id_cuenta = " + id_Cuenta));
        }
        public bool tieneFondos(double saldo_origen, double monto_Pagar)
        {
            if (saldo_origen < monto_Pagar)
                return false;
            return true;
        }
    }
}