using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VistaDatos;
using VistaEntidad;

namespace VistaNegocio
{
    public class N_Cliente
    {
        //Accesder a los metodos que tengan la clase D_Clientes
        private D_Cliente objVistaDato = new D_Cliente();

        //Retornar lista de Clientes
        public List<ClienteCerezos> Listar()
        {
            return objVistaDato.listar();
        }

        //Lllamar metodo de actualizar
        //Llamado de metodo Actualizar, reglas de negocio
        public bool Actualizar(ClienteCerezos obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            //Validar campo nombre
            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del cliente no puede ser vacio";
            }
            //Validar campo Apelldio
            if (string.IsNullOrEmpty(obj.Apellido) || string.IsNullOrWhiteSpace(obj.Apellido))
            {
                Mensaje = "El nombre del cliente no puede ser vacio";
            }
            //Validar campo Correo
            if (string.IsNullOrEmpty(obj.Email) || string.IsNullOrWhiteSpace(obj.Email))
            {
                Mensaje = "El nombre del cliente no puede ser vacio";
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
