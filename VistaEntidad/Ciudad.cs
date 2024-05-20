using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistaEntidad
{
//CREATE TABLE Ciudad(
//IDCiudad int primary key identity not null,
//Nombre varchar(100) not null,
//IDDepartamento int foreign key(IDDepartamento) references Departamento(IDDepartamento),
//);


    public class Ciudad
    {
        //Propiedades de clase
        public int IDCiudad { get; set; }
        public string Nombre { get; set; }
        public DepartamentoCerezos oDepartamento { get; set; } //Duda si llamar clase departamento
    }
}
