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
    public class D_Cliente
    {
        //public int Registrar(ClienteCerezos obj,out string Mensaje) 
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


    }
}
