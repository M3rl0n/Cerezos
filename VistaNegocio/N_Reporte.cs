using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VistaDatos;
using VistaEntidad;


namespace VistaNegocio
{
    public class N_Reporte
    {
        //Accesder a los metodos que tengan la clase D_Reporte
        private D_Reporte objVistaDato = new D_Reporte();      //Retornar reporte

        //Reporte de ventas
        public List<ReporteVentas> Ventas(string fechainicio, string fechafin, string idtransaccion)
        {
            return objVistaDato.Ventas(fechainicio, fechafin, idtransaccion);
        }


        public DashboardCerezos VerDashboard()
        {
            return objVistaDato.VerDashboard ();
        }
    }
}
