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
    public class D_Ubicacion
    {

        //lISTAR DEPARTAMENTOS
        public List<DepartamentoCerezos> Departamentos()
        {
            List<DepartamentoCerezos> lista = new List<DepartamentoCerezos>();

            try
            {
                using (SqlConnection oconecion = new SqlConnection(Conexion.cn))
                {
                    //Consultar a la bd
                    string query = "select * from Departamento";

                    SqlCommand cmd = new SqlCommand(query, oconecion);
                    cmd.CommandType = CommandType.Text;
                    oconecion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //Almacenar cada registro en la lista
                        while (dr.Read())
                        {
                            lista.Add(
                                new DepartamentoCerezos()
                                {
                                    IDDepartamento = Convert.ToInt32(dr["IDDepartamento"]),
                                    Nombre = dr["Nombre"].ToString(),

                                }
                                );
                        }
                    }
                }
            }
            catch
            {
                lista = new List<DepartamentoCerezos>();
            }

            return lista;
        }


        //lISTAR CIUDAD
        public List<Ciudad> Ciudades()
        {
            List<Ciudad> lista = new List<Ciudad>();

            try
            {
                using (SqlConnection oconecion = new SqlConnection(Conexion.cn))
                {
                    //Consultar a la bd
                    string query = "select * from Ciudad where IDDepartamento = 2";

                    SqlCommand cmd = new SqlCommand(query, oconecion);
                    cmd.CommandType = CommandType.Text;
                    oconecion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //Almacenar cada registro en la lista
                        while (dr.Read())
                        {
                            lista.Add(
                                new Ciudad()
                                {
                                    IDCiudad = Convert.ToInt32(dr["IDCiudad"]),
                                    Nombre = dr["Nombre"].ToString(),

                                }
                                );
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Ciudad>();
            }

            return lista;
        }


    }
}
