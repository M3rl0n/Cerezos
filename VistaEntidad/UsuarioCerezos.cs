using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace VistaEntidad
{
//CREATE TABLE Usuario(
//IDUsuario int primary key identity,
//Nombre varchar(100),
//Apellido varchar(100),
//Email varchar(100),
//Clave varchar(150),
//Restablecer bit default 1,
//FechaRegistro datetime default getdate(),
//Activo bit default 1,
//IDRol int foreign key(IDRol) references Rol(IDRol)
//);
    public class UsuarioCerezos
    {
        //Propiedades de la clase
        public int IDUsuario { get; set; } 
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
        public bool Restablecer { get; set; }
        public string FechaRegistro { get; set; }
        public bool Activo { get; set; }
        public int IDRol { get; set; }  //Duda si llamar rol
    }
}
