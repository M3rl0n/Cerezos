using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VistaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace VistaDatos
{
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
                    string query = "select IDUsuario, Nombre, Apellido, Email, Clave, Restablecer, FechaRegistro, Activo from Usuario";
                    SqlCommand cmd = new SqlCommand(query, oconecion);
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
                                    Activo = Convert.ToBoolean(dr["Activo"])
                                    //IDRol = Convert.ToInt32(dr["IDRol"])
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
    }
}
