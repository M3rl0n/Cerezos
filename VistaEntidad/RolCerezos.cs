using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistaEntidad
{
//CREATE TABLE Rol(
//IDRol int primary key identity,
//NombreRol Varchar (100),
//FechaCreacion datetime default getdate()
//);
    public class RolCerezos
    {
        //Propiedades de la clase
        public int IDRol { get; set; }
        public string NombreRol { get; set; }
        //public string FechaCreacion { get; set; } ya que la bd la pone automaticamente
    }
}
