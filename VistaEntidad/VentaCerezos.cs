using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistaEntidad
{
//CREATE TABLE Venta(
//IDVenta int primary key identity,
//FechaVenta datetime default getdate(),
//TotalProducto int,
//MontoTotal money,
//Telefono varchar(50),
//Direccion varchar(50),
//IDCiudad int foreign key(IDCiudad) references Ciudad(IDCiudad),
//IDTransaccion varchar(50),
//IDCliente int foreign key(IDCliente) references Cliente(IDCliente)
//);
    public class VentaCerezos
    {
        //Creamos todas la propiedades nuestra clase (prop)
        public int IDVenta { get; set; }
        public string FechaVenta { get; set; } //FechaTexto?
        public int TotalProducto { get; set; }
        public decimal MontoTotal { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public int IDCiudad { get; set; } //No referencia?
        public string IDTransaccion { get; set; }
        public int IDCliente { get; set; } //No referencia?
        public List<DetalleVentaCerezos> oDetalleVenta { get; set; }

    }
}
