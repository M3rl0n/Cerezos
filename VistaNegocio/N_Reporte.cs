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
        public DashboardCerezos VerDashboard()
        {
            return objVistaDato.VerDashboard ();
        }
    }
}
