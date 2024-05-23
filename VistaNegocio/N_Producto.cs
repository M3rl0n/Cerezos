using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VistaDatos;
using VistaEntidad;

namespace VistaNegocio
{
    //Listar categorias
    //public class N_Roles
    //{
    //    //Accesder a los metodos que tengan la clase D_Usuarios
    //    private D_Rol objVistaDato = new D_Rol();      //Retornar lista de usuarios
    //    public List<RolCerezos> Listar()
    //    {
    //        return objVistaDato.listar();
    //    }
    //}
    public class N_Producto
    {
        //Ejecutar metodos Productos

        //Accesder a los metodos que tengan la clase D_Productos
        private D_Productos objVistaDato = new D_Productos();

        //Retornar lista de usuarios
        public List<ProductosCerezos> Listar()
        {
            return objVistaDato.listar();
        }

        //Llamado de metodo insertar, reglas de negocio
        public int Insertar(ProductosCerezos obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            //Validar campo nombre
            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del producto no puede ser vacio";
            }
            //Validar campo Desciprcion
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripcion del producto no puede ser vacio";
            }
            //Validar campo Categoria
            else if (obj.oCategoria.IDCategoria == 0)
            {
                Mensaje = "Debe seleccionar una categoria";
            }
            //Validar campo Precio
            else if (obj.Precio == 0)
            {
                Mensaje = "Debe ingresar el precio del producto";
            }
            //Validar campo Precio
            else if (obj.Stock == 0)
            {
                Mensaje = "Debe ingresar el stock del producto";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objVistaDato.Insertar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }

        }

        //Llamado de metodo Actualizar, reglas de negocio
        public bool Actualizar(ProductosCerezos obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            //Validar campo nombre
            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del producto no puede ser vacio";
            }
            //Validar campo Desciprcion
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripcion del producto no puede ser vacio";
            }
            //Validar campo Categoria
            else if (obj.oCategoria.IDCategoria == 0)
            {
                Mensaje = "Debe seleccionar una categoria";
            }
            //Validar campo Precio
            else if (obj.Precio == 0)
            {
                Mensaje = "Debe ingresar el precio del producto";
            }
            //Validar campo Precio
            else if (obj.Stock == 0)
            {
                Mensaje = "Debe ingresar el stock del producto";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objVistaDato.Actualizar(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }

        //Llamar metodo guardar imagen
        public bool GuardarDatosImagen(ProductosCerezos obj, out string Mensaje)
        {
            return objVistaDato.GuardarDatosImagen(obj, out Mensaje);
        }



        //Llamado de metodo Eliminar, reglas de negocio
        public bool Eliminar(int id, out string Mensaje)
        {
            return objVistaDato.Eliminar(id, out Mensaje);
        }
    }
}
