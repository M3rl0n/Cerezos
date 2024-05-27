using VistaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace VistaDatos
{
    public class D_Cliente
    {

        //Listar Clientes Cerezos
        public List<ClienteCerezos> listar()
        {
            List<ClienteCerezos> lista = new List<ClienteCerezos>();

            try
            {
                using (SqlConnection oconecion = new SqlConnection(Conexion.cn))
                {
                    //Consultar a la bd
                    string query = "select IDCliente, Nombre, Apellido, Email from Cliente";
                    SqlCommand cmd = new SqlCommand(query, oconecion);
                    cmd.CommandType = CommandType.Text;
                    oconecion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //Almacenar cada registro en la lista
                        while (dr.Read())
                        {
                            lista.Add(
                                new ClienteCerezos()
                                {
                                    IDCliente = Convert.ToInt32(dr["IDCliente"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Apellido = dr["Apellido"].ToString(),
                                    Email = dr["Email"].ToString(),
                                }
                                );
                        }
                    }
                }
            }
            catch
            {
                lista = new List<ClienteCerezos>();
            }

            return lista;
        }
       
        //Insertar Clientes en la BD
        public int Insertar(ClienteCerezos obj,out string Mensaje)
        {
            int idautogenerado = 0;

            Mensaje = string.Empty;
            try
            {
                using(SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCliente", oconexion);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", obj.Nombre);
                    cmd.Parameters.AddWithValue("Email", obj.Nombre);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                Mensaje = ex.Message;
            }
            return idautogenerado;
        }

        //Actualizar Clientes en la BD




        ////No va esto
        //public bool ReestablecerClave(int IDCliente, string clave, out string Mensaje)
        //{
        //    bool resultado = false;
        //    Mensaje = string.Empty;
        //    try
        //    {
        //        using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
        //        {
        //            SqlCommand cmd = new SqlCommand("Update cliente set clave = @nuevaclave, reestablecer = 1 where IDCliente = @id", oconexion);
        //            cmd.Parameters.AddWithValue("@IDCliente", IDCliente);
        //            cmd.Parameters.AddWithValue("@nuevaclave", clave);
        //            cmd.CommandType = CommandType.Text;
        //            oconexion.Open();
        //            resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        resultado = false;
        //        Mensaje = ex.Message;
        //    }
        //    return resultado;
        //}
        //public bool CambiarClave(int IDCliente, string nuevaClave,out string Mensaje)
        //{
        //    bool resultado = false;
        //    Mensaje = string.Empty;
        //    try
        //    {
        //        using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
        //        {
        //            SqlCommand cmd = new SqlCommand("Update cliente set clave = @nuevaclave, reestablecer = 0 where IDCliente = @id",oconexion);
        //            cmd.Parameters.AddWithValue("@IDCliente", IDCliente);
        //            cmd.Parameters.AddWithValue("@nuevaclave", nuevaClave);
        //            cmd.CommandType = CommandType.Text;
        //            oconexion.Open();
        //            resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        resultado = false;
        //        Mensaje = ex.Message;
        //    }
        //    return resultado;
        //}

    }
}
