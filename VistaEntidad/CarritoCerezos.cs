using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistaEntidad
{
//CREATE TABLE Carrito(
//IDCarrito int primary key identity,
//Cantidad int,
//IDCliente int foreign key (IDCliente) references Cliente(IDCliente),
//IDProducto int foreign key (IDProducto) references Productos(IDProducto)
//);
    public class CarritoCerezos
    {
        //Creamos todas la propiedades de nuestra clase (prop)
        public int IDCarrito { get; set; }
        public int Cantidad { get; set; }
        public ClienteCerezos oCliente { get; set; } //Llamar clase ClienteCerezos crear obj
        public ProductosCerezos oProducto { get; set; } //LLamar Clase ProductoCerezos crear obj
    }
}
