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
    public class UserService
    {
        BDService serviceBD;
        
        public UserService() {
            serviceBD = new BDService();
        }


        public int getCodigoUsuario() 
        {
            int cod_Usuario = 0;
            DataTable dt = serviceBD.FillTableData("SELECT id_usuario FROM dbo.USUARIO order by id_usuario desc ");
            if (dt.Rows.Count != 0)
            {
                DataRow row = dt.Rows[0];
                cod_Usuario = int.Parse(row["id_usuario"].ToString());
                cod_Usuario++;
            }
            return cod_Usuario;
        }
        public bool  existeUsuario(String user)
        {
            return  serviceBD.Verify_Query("SELECT usuario FROM dbo.USUARIO WHERE usuario = '" + user+ "'");
        }

        public int crearNuevoUsuario(UsuarioModels user)
        {
            String campos = "INSERT INTO dbo.USUARIO(id_usuario,usuario,correo,contraseña,nombre,apellido)"; 
            String valores = string.Format("VALUES({0},'{1}','{2}','{3}','{4}','{5}')",user.id_usuario,user.usuario,user.correo,user.contrasenia,user.nombre,user.apellido);
            String query = campos + valores;
            return serviceBD.Upd_New_DelUnValorQry_SLID(query);
        }

        public UsuarioModels getUsuario(String id_usuario,String user,String password)
        {
            UsuarioModels Usuario = null;
            DataTable dt  = serviceBD.FillTableData("SELECT * FROM dbo.USUARIO WHERE id_usuario = " +id_usuario +" AND usuario = '" + user + "' AND contraseña = '" + password+ "'");
            if(dt.Rows.Count != 0)
            {
                Usuario = new UsuarioModels();
                DataRow row = dt.Rows[0];
                Usuario.id_usuario = int.Parse(row["id_usuario"].ToString());
                Usuario.nombre = row["nombre"].ToString();
                Usuario.apellido = row["apellido"].ToString();
                Usuario.usuario = row["usuario"].ToString();
                Usuario.correo = row["correo"].ToString();
                Usuario.contrasenia = row["contraseña"].ToString();
            }
            return Usuario;
        }
        public UsuarioModels getUsuario(String user)
        {
            UsuarioModels Usuario = null;
            DataTable dt = serviceBD.FillTableData("SELECT * FROM dbo.USUARIO WHERE id_usuario = " + user);
            if (dt.Rows.Count != 0)
            {
                Usuario = new UsuarioModels();
                DataRow row = dt.Rows[0];
                Usuario.id_usuario = int.Parse(row["id_usuario"].ToString());
                Usuario.nombre = row["nombre"].ToString();
                Usuario.apellido = row["apellido"].ToString();
                Usuario.usuario = row["usuario"].ToString();
                Usuario.correo = row["correo"].ToString();
                Usuario.contrasenia = row["contraseña"].ToString();
            }
            return Usuario;
        }
    }
}