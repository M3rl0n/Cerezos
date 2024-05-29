using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VistaEntidad;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace VistaDatos
{
    public class D_CarritoCompras
    {

        //Carrito de compras
        private const int IDClienteTest = 1;
        public bool ExisteCarrito(int IDProducto)
        {
            bool resultado = true;

            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ExisteCarrito", oconexion);
                    cmd.Parameters.AddWithValue("IDCliente", IDClienteTest);
                    cmd.Parameters.AddWithValue("IDProducto", IDProducto);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    //Ultimo id generado con la bd
                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
            }
            catch (Exception ex)
            {
                resultado = false;
            }
            return resultado;
        }


        //OPERACION CARRITO SUMAR/RESTAR PRODUCTOS
        public bool OperacionCarrito(int IDProducto, bool sumar, out string Mensaje)
        {
            bool resultado = true;

            Mensaje = string.Empty;
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_OperacionCarrito", oconexion);
                    cmd.Parameters.AddWithValue("IDCliente", IDClienteTest);
                    cmd.Parameters.AddWithValue("IDProducto", IDProducto);
                    cmd.Parameters.AddWithValue("sumar", sumar);
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

        //CANTIDAD EN CARRITO
        public int CantidadEnCarrito(int? IDCliente = null)
        {
            int resultado = 0;
            int ClienteID = IDCliente ?? IDClienteTest;
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("select COUNT(*) from Carrito where IDCliente = @IDCliente", oconexion);
                    cmd.Parameters.AddWithValue("@IDCliente", ClienteID);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    resultado = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
            }
            return resultado;
        }

        //Listar carrito de compras
        public List<CarritoCerezos> listarProducto(int IDCliente)
        {
            List<CarritoCerezos> lista = new List<CarritoCerezos>();

            try
            {
                using (SqlConnection oconecion = new SqlConnection(Conexion.cn))
                {
                    string query = "select * from fn_obternerCarritoCliente(@IDCliente)";

                    SqlCommand cmd = new SqlCommand(query, oconecion);
                    cmd.Parameters.AddWithValue("@IDCliente", IDClienteTest);
                    cmd.CommandType = CommandType.Text;
                    oconecion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //Almacenar cada registro en la lista
                        while (dr.Read())
                        {
                            lista.Add(new CarritoCerezos()
                            {

                                oProducto = new ProductosCerezos()
                                {
                                    IDProducto = Convert.ToInt32(dr["IDProducto"]),
                                    Nombre = dr["NombreProducto"].ToString(),
                                    Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-CO")),
                                    RutaImagen = dr["RutaImagen"].ToString(),
                                    NombreImagen = dr["NombreImagen"].ToString(),
                                    oCategoria = new CategoriaProductos() { Nombre = dr["CategoriaProducto"].ToString() }
                                },
                                Cantidad = Convert.ToInt32(dr["Cantidad"])
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<CarritoCerezos>();
            }

            return lista;
        }


        //ELIMINAR CARRITO

        public bool EliminarCarrito(int IDProducto)
        {
            bool resultado = true;

            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarCarrito", oconexion);
                    cmd.Parameters.AddWithValue("IDCliente", IDClienteTest);
                    cmd.Parameters.AddWithValue("IDProducto", IDProducto);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    //Ultimo id generado con la bd
                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
            }
            catch (Exception ex)
            {
                resultado = false;
            }
            return resultado;
        }



    }
}
