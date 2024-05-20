using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistaEntidad
{
//CREATE TABLE Seguimiento_Envio(
//IDSeguimiento int primary key identity,
//Descripcion VARCHAR(200),
//IDTipo_Estado_Envio int foreign key(IDTipo_Estado_Envio) references Tipo_Estados_Envio(IDTipo_Estado_Envio),
//IDCliente int foreign key(IDCliente) references Cliente(IDCliente)
//);
    public class SeguimientoENVCerezos
    {
        //Propiedades de clase
        public int IDSeguimiento { get; set; }
        public int Descripcion { get; set; }
        public int IDTipo_Estado_Envio { get; set; } //Duda si llanar clase de estado envio
        public ClienteCerezos OCliente { get; set; } //Duda si llamar Clase cliente

    }
}
