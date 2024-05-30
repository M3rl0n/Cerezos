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
        public String Descripcion { get; set; }
        public TPEstadosENVCerezos oEstadoEnvio { get; set; } //Duda si llanar clase de estado envio
        public int IDCliente { get; set; } //Duda si llamar Clase cliente

    }
}
