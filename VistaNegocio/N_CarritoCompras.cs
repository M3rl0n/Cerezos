using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VistaEntidad;
using VistaDatos;

namespace VistaNegocio
{
    public class N_CarritoCompras
    {
        //Acceder a los metodos que tengan la clase D_Carrito
        private D_CarritoCompras objVistaDato = new D_CarritoCompras();

        //Llamar metodo existe carrito
        public bool ExisteCarrito(int IDProducto)
        {
            return objVistaDato.ExisteCarrito(IDProducto);
        }

        //Llamar metodo OPERACION carrito
        public bool OperacionCarrito(int IDProducto, bool sumar, out string Mensaje)
        {
            return objVistaDato.OperacionCarrito(IDProducto, sumar, out Mensaje);
        }


        //Llamar metodo CANTIDAD carrito
        public int CantidadEnCarrito(int? IDCliente = null)
        {
            return objVistaDato.CantidadEnCarrito(IDCliente);
        }

        //Llamar lista de productos en carrito
        public List<CarritoCerezos> listarProducto(int IDCliente)
        {
            return objVistaDato.listarProducto(IDCliente);
        }

        //Llamar eleiminar carrito
        public bool EliminarCarrito(int IDProducto)
        {
            return objVistaDato.EliminarCarrito(IDProducto);
        }





    }
}
