using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Practica2.Servicios
{
    public class BDService
    {
            public SqlConnection connection;

            //String de Conexion
            public void Initialize()
            {

                string connectionString = ConfigurationManager.ConnectionStrings["SQLSTR"].ConnectionString;
                connection = new SqlConnection(connectionString);

            }

            //Abre conexion a la base
            public bool OpenConnection()
            {
                Initialize();
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
            //Limpar Consulta SQLInjection
            public string Limpiar_Query(string query)
            {
                return query.ToString().Trim(new Char[] { ';' });
            }
            //Obtiene tabla 
            public DataTable FillTableData(string pQuery)
            {
                pQuery = Limpiar_Query(pQuery);
                DataTable DtResultado = new DataTable();

                try
                {
                    SqlDataAdapter cmd = new SqlDataAdapter(pQuery, connection);
                    cmd.Fill(DtResultado);
                }
                catch
                {
                    Initialize();
                    SqlDataAdapter cmd = new SqlDataAdapter(pQuery, connection);
                    try
                    {
                        cmd.Fill(DtResultado);
                    }
                    catch (Exception ex)
                    {
                        HttpContext.Current.Session["Error"] = ex.Message;
                    }

                }
                return DtResultado;
            }

            //Select statement
            public string SelectUnValorQry(string pQuery)
            {
                string valor = "";
                pQuery = Limpiar_Query(pQuery);
                try
                {
                    //Crea Command
                    SqlCommand cmd = new SqlCommand(pQuery, connection);
                    //Crea data reader y Execute command
                    valor = cmd.ExecuteScalar().ToString();
                    return valor;
                }
                catch
                {
                    //Abre conexion
                    OpenConnection();
                    //Crea Command
                    SqlCommand cmd = new SqlCommand(pQuery, connection);
                    try
                    {
                        //Crea data reader y Execute command
                        valor = cmd.ExecuteScalar().ToString();
                    }
                    catch (Exception ex)
                    {
                        HttpContext.Current.Session["Error"] = ex.Message;
                    }
                    return valor;
                }
            }
            //Update_New_Delete statment con Select last ID
            public Int32 Upd_New_DelUnValorQry_SLID(string pQuery)
            {
                try
                {
                    //Crea Command
                    SqlCommand cmd = new SqlCommand(pQuery, connection);
                    //Execute command
                    return (Int32)cmd.ExecuteScalar();

                }
                catch
                {
                    OpenConnection();
                    //Crea Command
                    SqlCommand cmd = new SqlCommand(pQuery, connection);
                    //Execute command
                    try
                    {
                        return (Int32)cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        HttpContext.Current.Session["Error"] = ex.Message;
                    }
                }
                return 0;
            }
            //Update_New_Delete statment
            public void Upd_New_DelUnValorQry(string pQuery)
            {
                pQuery = Limpiar_Query(pQuery);
                try
                {
                    //Crea Command
                    SqlCommand cmd = new SqlCommand(pQuery, connection);
                    //Execute command
                    cmd.ExecuteScalar();
                }
                catch
                {
                    OpenConnection();
                    //Crea Command
                    SqlCommand cmd = new SqlCommand(pQuery, connection);
                    //Execute command
                    try
                    {
                        cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        HttpContext.Current.Session["Error"] = ex.Message;
                    }
                }
            }
            //Verificar existencia de dato en Base de Datos
            public bool Verify_Query(string pQuery)
            {
                pQuery = Limpiar_Query(pQuery);
                try
                {
                    //Crea Command
                    SqlCommand cmd = new SqlCommand(pQuery, connection);
                    //Ejecuta Command y verifica existencia
                    cmd.ExecuteScalar();
                    return true;
                }
                catch
                {
                    //Abre conexion
                    OpenConnection();
                    //Crea Command
                    SqlCommand cmd = new SqlCommand(pQuery, connection);
                    //Ejecuta Command y verifica existencia
                    try
                    {
                        cmd.ExecuteScalar();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }

            }

            //Verifica conexion
            public string TestConexion()
            {
                bool Test = OpenConnection();
                string resultado;
                if (Test == true)
                { resultado = "Great"; }
                else
                { resultado = "Conexion Fallida"; }

                return resultado;
            }

        }
    }