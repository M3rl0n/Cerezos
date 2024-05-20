using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistaEntidad
{
//CREATE TABLE Categoria_Productos(
//IDCategoria int primary key identity,
//Nombre Varchar (100),
//Activo bit default 1,
//FechaRegistro date default getdate()
//);
    public class CategoriaProductos
    {
        //Creamos todas la propiedades nuestra clase (prop)
        public int IDCategoria { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        //public string FechaRegistro { get; set; } //Duda

    }
}
