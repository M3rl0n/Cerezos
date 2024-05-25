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
    public class D_Reporte
    {
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
