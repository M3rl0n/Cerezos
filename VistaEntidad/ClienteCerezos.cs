using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistaEntidad
{
//CREATE TABLE Cliente(
//IDCliente int primary key identity,
//Nombre varchar (100),
//Apellido varchar(100),
//Email varchar(100)
//);
    public class ClienteCerezos
    {
        //Creamos todas la propiedades nuestra clase (prop)
        public int IDCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
    }
}
