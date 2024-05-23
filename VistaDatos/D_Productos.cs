using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VistaEntidad;
using System.Globalization;

namespace VistaDatos
{
    public class D_Productos
    {
        //Listar productos
        public List<ProductosCerezos> listar()
        {
            List<ProductosCerezos> lista = new List<ProductosCerezos>();

            try
            {
                using (SqlConnection oconecion = new SqlConnection(Conexion.cn))
                {
                    //Consultar a la bd
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("select p.IDProducto, p.Nombre, p.Descripcion, p.Precio, p.Stock, p.RutaImagen, p.NombreImagen, p.Activo,");
                    sb.AppendLine("cp.IDCategoria, cp.Nombre[NomCategoria]");
                    sb.AppendLine("Productos p");
                    sb.AppendLine("inner join Categoria_Productos cp on cp.IDCategoria = p.IDCategoria");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconecion);
                    cmd.CommandType = CommandType.Text;
                    oconecion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //Almacenar cada registro en la lista
                        while (dr.Read())
                        {
                            lista.Add(new ProductosCerezos()
                                {
                                    IDProducto = Convert.ToInt32(dr["IDProducto"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-CO")),
                                    Stock = Convert.ToInt32(dr["Stock"]),
                                    RutaImagen = dr["RutaImagen"].ToString(),
                                    NombreImagen = dr["NombreImagen"].ToString(),
                                    Activo = Convert.ToBoolean(dr["Activo"]),
                                    oCategoria = new CategoriaProductos() { IDCategoria = Convert.ToInt32(dr["IDCategoria"]), Nombre = dr["NomCategoria"].ToString() }
                                });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<ProductosCerezos>();
            }

            return lista;
        }
        //INSETAR PRODUCTOS
        public int Insertar(ProductosCerezos obj, out string Mensaje)
        {
            int idautogenerado = 0;

            Mensaje = string.Empty;
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarProducto", oconexion);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.AddWithValue("IDCategoria", obj.oCategoria.IDCategoria);
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

        //ACTUALIZAR PRODUCTOS
        public bool Actualizar(ProductosCerezos obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarProducto", oconexion);
                    cmd.Parameters.AddWithValue("IDProducto", obj.IDProducto);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
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

        //Guardar imagen
        public bool GuardarDatosImagen(ProductosCerezos obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "update Productos set RutaImagen = @rutaimagen, NombreImagen = @nombreimagen where IDProducto = @IDProducto";
                    SqlCommand cmd = new SqlCommand("sp_RegistrarProducto", oconexion);
                    cmd.Parameters.AddWithValue("@rutaimagen", obj.RutaImagen);
                    cmd.Parameters.AddWithValue("@nombreimagen", obj.NombreImagen);
                    cmd.Parameters.AddWithValue("@IDProducto", obj.IDProducto);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        resultado=true;
                    }else {
                        Mensaje = "No se pudo actualizar imagen";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }





        //ELIMINAR PRODUCTO
        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarProducto", oconexion);
                    cmd.Parameters.AddWithValue("IDProducto", id);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    //Validacion si elimino o no
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
