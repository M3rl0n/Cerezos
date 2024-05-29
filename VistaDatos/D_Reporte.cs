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
    public class D_Reporte
    {

        //Reporte de ventas
        public List<ReporteVentas> Ventas(string fechainicio, string fechafin, string idtransaccion)
        {
            List<ReporteVentas> lista = new List<ReporteVentas>();

            try
            {
                using (SqlConnection oconecion = new SqlConnection(Conexion.cn))
                {

                    //Ejecutar procedimiento almacenado
                    SqlCommand cmd = new SqlCommand("sp_ReporteVentas", oconecion);
                    cmd.Parameters.AddWithValue("fechainicio", fechainicio);
                    cmd.Parameters.AddWithValue("fechafin", fechainicio);
                    cmd.Parameters.AddWithValue("idtransaccion", idtransaccion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconecion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //Almacenar cada registro en la lista
                        while (dr.Read())
                        {
                            lista.Add(
                                new ReporteVentas()
                                {
                                    FechaVenta = dr["FechaVenta"].ToString(),
                                    Cliente = dr["Cliente"].ToString(),
                                    Producto = dr["Producto"].ToString(),
                                    Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-CO")),
                                    Cantidad = Convert.ToInt32(dr["Cantidad"]),
                                    Total = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-CO")),
                                    IDTransaccion = dr["IDTransaccion"].ToString()
                                }
                                );
                        }
                    }
                }
            }
            catch
            {
                lista = new List<ReporteVentas>();
            }
            return lista;
        }


        //Reporte del dashboard
        public DashboardCerezos VerDashboard()
        {
            DashboardCerezos objeto = new DashboardCerezos();

            try
            {
                using (SqlConnection oconecion = new SqlConnection(Conexion.cn))
                {
                    //Ejecutar procedimiento almacenado
                
                    SqlCommand cmd = new SqlCommand("sp_ReporteDashboard", oconecion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconecion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //Almacenar cada registro en la lista
                        while (dr.Read())
                        {
                            objeto = new DashboardCerezos()
                            {
                                TotalClientes = Convert.ToInt32(dr["TotalClientes"]),
                                TotalSolicitudes = Convert.ToInt32(dr["TotalSolicitudes"]),
                                TotalProductos = Convert.ToInt32(dr["TotalProductos"]),
                                TotalVenta = Convert.ToInt32(dr["TotalVenta"]),
                            };
                        }
                    }
                }
            }
            catch
            {
                objeto = new DashboardCerezos();
            }
            return objeto;
        }


    }
}
