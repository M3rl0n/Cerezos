using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistaEntidad
{

//CREATE TABLE Productos(
//IDProducto int primary key identity,
//Nombre varchar(500),
//Descripcion varchar(500),
//Precio money default 0,
//Stock int,
//FechaRegistro datetime default getdate(),
//RutaImagen varchar(100),
//NombreImagen varchar(100),
//Activo bit default 1,
//IDCategoria int foreign key(IDCategoria) references Categoria_Productos(IDCategoria)
//);

    public class ProductosCerezos
        
    {
        //Creamos todas la propiedades nuestra clase (prop)
        public int IDProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }

        public string PrecioTexto { get; set; }
        public int Stock { get; set; }
        public string FechaRegistro { get; set; } //Data time? duda
        public string RutaImagen { get; set; }
        public string NombreImagen { get; set; }
        public bool Activo { get; set; }
        public CategoriaProductos oCategoria { get; set; }  //Llamamos la clase CategoriaProducto
        public string Base64 { get; set; } //Formato para mostrar las imagens
        public string Extension { get; set; } // ext de imagen

    }
}
