using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistaEntidad
{
//CREATE TABLE Solicitud(
//IDSolicitud int primary key identity,
//Descripcion Varchar(200),
//IDTipo_Solicitud int foreign key(IDTipo_Solicitud) references Tipo_Solicitud(IDTipo_Solicitud),
//IDCliente int foreign key(IDCliente) references Cliente(IDCliente),
//);
    public class SolicitudCerezos
    {
        //Propiedades de clases
        public int IDSolicitud { get; set; }
        public string Descripcion { get; set; }
        public int IDTipo_Solicitud { get; set; } //Duda si llamar tipo_Solicitud
        public ClienteCerezos OCliente { get; set; } //Duda si llamar cliente Cerezos
    }
}
