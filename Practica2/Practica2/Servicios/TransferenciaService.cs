using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Practica2.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Practica2.Servicios
{
    public class TransferenciaService
    {
        BDService serviceBD;

        public TransferenciaService()
        {
            serviceBD = new BDService();
        }

        public CuentaModels getCuenta(String id, int tipo) 
        {
            CuentaModels cuenta = null;
            String query = String.Empty;
            DataTable dt;
            if (tipo == 1)
            {
                query = "SELECT * FROM dbo.CUENTA WHERE id_usuario = " + id;
            }
            else 
            {
                query = "SELECT * FROM dbo.CUENTA WHERE id_cuenta = " + id;
            }
            dt = serviceBD.FillTableData(query);
            if (dt.Rows.Count != 0)
            {
                cuenta = new CuentaModels();
                DataRow row = dt.Rows[0];
                UserService serU =new UserService();
                String id_usuario = row["id_usuario"].ToString(); 
                cuenta.usuario = serU.getUsuario(id_usuario);
                cuenta.id_cuenta = int.Parse(row["id_cuenta"].ToString());
                cuenta.saldo = double.Parse(row["saldo"].ToString());
            }
            return cuenta;
        }

        public void guardarTransferencia(TransferenciaModels transferencia) 
        {
            String campos = "INSERT INTO dbo.TRANSFERENCIA(monto,fecha,id_cuenta_destino,id_cuenta_origen)";
            String format = "MM/dd/yyyy";
            String fecha = transferencia.fecha.ToString(format);
            String valores = string.Format(" VALUES({0},'{1}',{2},{3})",transferencia.monto,fecha,transferencia.cuenta_destino.id_cuenta,transferencia.cuenta_origen.id_cuenta);
            String query = campos + valores;
            serviceBD.Upd_New_DelUnValorQry(query);
        }
        public bool realizarTransferencia(TransferenciaModels transferencia)
        {
            try
            {
                CuentaModels origen = transferencia.cuenta_origen;
                CuentaModels destino = transferencia.cuenta_destino;
                //Comprueba montos
                if(transferencia.monto > origen.saldo)
                {
                    return false;
                }
                else 
                { 
                    origen.saldo = origen.saldo - transferencia.monto;
                    destino.saldo = destino.saldo + transferencia.monto;
                    String queryDebito = string.Format("UPDATE dbo.CUENTA SET  saldo = {0} where id_cuenta = {1} ", origen.saldo, origen.id_cuenta);
                    serviceBD.Upd_New_DelUnValorQry(queryDebito);
                    String queryCredito = string.Format("UPDATE dbo.CUENTA SET  saldo = {0} where id_cuenta = {1} ", destino.saldo, destino.id_cuenta);
                    serviceBD.Upd_New_DelUnValorQry(queryCredito);
                    guardarTransferencia(transferencia);
                    return true;
                }
            }
            catch 
            {
                return false;
            }
        }
    }
}