using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VistaEntidad;

namespace VistaDatos
{

    //LISTAR Estados de envio
    public class D_EstadosEnvio
    {
        public List<TPEstadosENVCerezos> listar()
        {
            List<TPEstadosENVCerezos> lista = new List<TPEstadosENVCerezos>();

            try
            {
                using (SqlConnection oconecion = new SqlConnection(Conexion.cn))
                {
                    //Consultar a la bd
                    string query = "select * from Tipo_Estados_Envio\r\n";


                    SqlCommand cmd = new SqlCommand(query, oconecion);
                    cmd.CommandType = CommandType.Text;
                    oconecion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //Almacenar cada registro en la lista
                        while (dr.Read())
                        {
                            lista.Add(
                                new TPEstadosENVCerezos()
                                {
                                    IDTipo_Estado_Envio = Convert.ToInt32(dr["IDTipo_Estado_Envio"]),
                                    NombreEstado = dr["NombreEstado"].ToString()
                                }
                                );
                        }
                    }
                }
            }
            catch
            {
                lista = new List<TPEstadosENVCerezos>();
            }
            return lista;
        }
    }
    public class D_Envio
    {
        //Listar envios

        public List<SeguimientoENVCerezos> listar()
        {
            List<SeguimientoENVCerezos> lista = new List<SeguimientoENVCerezos>();

            try
            {
                using (SqlConnection oconecion = new SqlConnection(Conexion.cn))
                {
                    //Consultar a la bd
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("select s.IDSeguimiento, s.Descripcion, s.IDTipo_Estado_Envio, s.IDCliente,");
                    sb.AppendLine("te.NombreEstado [EstadoEnvio],");
                    sb.AppendLine("FROM Seguimiento_Envio s");
                    sb.AppendLine("inner join Tipo_Estados_Envio te on te.IDTipo_Estado_Envio = s.IDTipo_Estado_Envio");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconecion);
                    cmd.CommandType = CommandType.Text;
                    oconecion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //Almacenar cada registro en la lista
                        while (dr.Read())
                        {
                            lista.Add(
                                new SeguimientoENVCerezos()
                                {
                                    IDSeguimiento = Convert.ToInt32(dr["IDSeguimiento"]),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    IDCliente = Convert.ToInt32(dr["IDCliente"]),
                                    oEstadoEnvio = new TPEstadosENVCerezos() { IDTipo_Estado_Envio = Convert.ToInt32(dr["IDTipo_Estado_Envio"]), NombreEstado = dr["NombreEstado"].ToString() }

                                }
                                );
                        }
                    }
                }
            }
            catch
            {
                lista = new List<SeguimientoENVCerezos>();
            }

            return lista;
        }



        //ACTUALIZAR eNVIOS
        public bool Actualizar(SeguimientoENVCerezos obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarSeguimientoEnvio", oconexion);
                    cmd.Parameters.AddWithValue("IDSeguimiento", obj.IDSeguimiento);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("IDCliente", obj.IDCliente);
                    cmd.Parameters.AddWithValue("IDTipo_Estado_Envio", obj.oEstadoEnvio.IDTipo_Estado_Envio);
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

    }


}
