using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistaEntidad
{
//CREATE TABLE Detalle_Venta(
//IDDetalle_Venta int primary key identity,
//Cantidad int,
//Total money,
//IDVenta int foreign key (IDVenta) references Venta(IDVenta),
//IDProducto int foreign key (IDProducto) references Productos(IDProducto),
//);
    public class DetalleVentaCerezos
    {
        public int IDDetalle_Venta { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public int IDVenta { get; set; } //Duda si llamar clase venta
        public ProductosCerezos oProducto { get; set; } //objProducto?
        public int IDTransaccion { get; set; } //id de transaccion
    }
}
