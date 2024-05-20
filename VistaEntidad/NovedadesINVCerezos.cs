using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistaEntidad
{
//CREATE TABLE Novedades_Inventario(
//IDNovedad int primary key identity,
//FechaNovedad datetime default getdate(),
//Descripcion Varchar (250),
//IDProducto int foreign key(IDProducto) references Productos(IDProducto)
//);
    public class NovedadesINVCerezos
    {
        //Propiedades de clase
        public int IDNovedad { get; set; }
        public string FechaNovedad { get; set; } //Fecha texto?
        public string Descripcion { get; set; }
        public ProductosCerezos OProducto { get; set; } //objprodcuto?
    }
}
