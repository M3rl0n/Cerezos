using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VistaEntidad;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Security.Claims;

namespace VistaDatos
{
    //LISTAR ROLES
    public class D_Rol
    {
        public List<RolCerezos> listar()
        {
            List<RolCerezos> lista = new List<RolCerezos>();

            try
            {
                using (SqlConnection oconecion = new SqlConnection(Conexion.cn))
                {
                    //Consultar a la bd
                    string query = "select IDRol, NombreRol from Rol";


                    SqlCommand cmd = new SqlCommand(query, oconecion);
                    cmd.CommandType = CommandType.Text;
                    oconecion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //Almacenar cada registro en la lista
                        while (dr.Read())
                        {
                            lista.Add(
                                new RolCerezos()
                                {
                                    IDRol = Convert.ToInt32(dr["IDRol"]),
                                    NombreRol = dr["NombreRol"].ToString()
                                }
                                );
                        }
                    }
                }
            }
            catch
            {
                lista = new List<RolCerezos>();
            }
            return lista;
        }
    }
    public class D_Usuarios
    {
        public List<UsuarioCerezos> listar()
        {
            List<UsuarioCerezos> lista = new List<UsuarioCerezos>();

            try
            {
                using (SqlConnection oconecion = new SqlConnection(Conexion.cn))
                {
                    //Consultar a la bd
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("SELECT u.IDUsuario, u.Nombre, u.Apellido, u.Email, u.Clave, u.Restablecer, u.FechaRegistro, u.Activo,");
                    sb.AppendLine("r.IDRol, r.NombreRol");
                    sb.AppendLine("from Usuario u");
                    sb.AppendLine("inner join Rol r on r.IDRol = u.IDRol");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconecion);
                    cmd.CommandType = CommandType.Text;
                    oconecion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //Almacenar cada registro en la lista
                        while (dr.Read())
                        {
                            lista.Add(
                                new UsuarioCerezos()
                                {
                                    IDUsuario = Convert.ToInt32(dr["IDUsuario"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Apellido = dr["Apellido"].ToString(),
                                    Email = dr["Email"].ToString(),
                                    Clave = dr["Clave"].ToString(),
                                    Restablecer = Convert.ToBoolean(dr["Restablecer"]),
                                    FechaRegistro = dr["FechaRegistro"].ToString(),
                                    Activo = Convert.ToBoolean(dr["Activo"]),
                                    oRol = new RolCerezos() {IDRol = Convert.ToInt32(dr["IDRol"]), NombreRol = dr["NombreRol"].ToString() }
                                
                                }
                                );
                        }
                    }
                }
            }
            catch
            {
                lista = new List<UsuarioCerezos>();
            }

            return lista;
        }

        //INSETAR USUARIOS
        public int Insertar(UsuarioCerezos obj, out string Mensaje)
        {
            int idautogenerado = 0;

            Mensaje = string.Empty;
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", oconexion);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", obj.Apellido);
                    cmd.Parameters.AddWithValue("Email", obj.Email);
                    cmd.Parameters.AddWithValue("Clave", obj.Clave);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.AddWithValue("IDRol", obj.oRol.IDRol);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    //Ultimo id generado con la bd
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

        //ACTUALIZAR USUARIOS
        public bool Actualizar(UsuarioCerezos obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarUsuario", oconexion);
                    cmd.Parameters.AddWithValue("IDUsuario", obj.IDUsuario);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", obj.Apellido);
                    cmd.Parameters.AddWithValue("Email", obj.Email);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.AddWithValue("IDRol", obj.oRol.IDRol);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    //Ultimo id generado con la bd
                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado; 
        }

        //ELIMINAR USUARIOS
        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("delete top (1) from Usuario where IDUsuario = @id", oconexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        //Cambiar clave de usuario

        public bool CambiarClave(int IDUsuario, string NuevaClave ,out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("update Usuario set Clave = @NuevaClave, Restablecer = 0 where IDUsuario = @id ", oconexion);
                    cmd.Parameters.AddWithValue("@id", IDUsuario);
                    cmd.Parameters.AddWithValue("@NuevaClave", NuevaClave);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        //Restablecer Clave de usuario

        public bool RestablecerClave(int IDUsuario, string Clave, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("\"update Usuario set Clave = @Clave, Restablecer = 1 where IDUsuario = @id", oconexion);
                    cmd.Parameters.AddWithValue("@id", IDUsuario);
                    cmd.Parameters.AddWithValue("@Clave", Clave);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }


    }
}
