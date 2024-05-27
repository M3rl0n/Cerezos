using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VistaDatos;
using VistaEntidad;

namespace VistaNegocio
{
    public class N_CategoriaProd
    {
        //Accesder a los metodos que tengan la clase D_CategoriaProd
        private D_CategoriaProd objVistaDato = new D_CategoriaProd();

        //Retornar lista de categorias
        public List<CategoriaProductos> Listar()
        {
            return objVistaDato.listar();
        }

        //Llamado de metodo insertar, reglas de negocio
        public int Insertar(CategoriaProductos obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            //Validar campo nombre
            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre de la categoria de producto no puede ser vacio";
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
        public bool Actualizar(CategoriaProductos obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            //Validar campo nombre
            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre de la categoria de producto no puede ser vacio";
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

        //Llamado de metodo Eliminar, reglas de negocio
        public bool Eliminar(int id, out string Mensaje)
        {
            return objVistaDato.Eliminar(id, out Mensaje);
        }
    }
}
