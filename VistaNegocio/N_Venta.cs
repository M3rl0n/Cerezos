using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VistaDatos;
using VistaEntidad;

namespace VistaNegocio
{
    public class N_Venta
    {
        private D_Ventas objVistaDato = new D_Ventas();
        //LLmar metodo venta
        public bool Insertar(VentaCerezos obj, DataTable DetalleVenta, out string Mensaje)
        {
            return objVistaDato.Insertar(obj, DetalleVenta, out  Mensaje);
        }



    }
}
