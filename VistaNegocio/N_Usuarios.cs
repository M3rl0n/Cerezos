using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VistaDatos;
using VistaEntidad;

namespace VistaNegocio
{
    //Listar roles
    public class N_Roles
    {
        //Accesder a los metodos que tengan la clase D_Usuarios
        private D_Rol objVistaDato = new D_Rol();      //Retornar lista de usuarios
        public List<RolCerezos> Listar()
        {
            return objVistaDato.listar();
        }
    }
    //Ejecutar metodos Usuarios
    public class N_Usuarios
    {
        //Accesder a los metodos que tengan la clase D_Usuarios
        private D_Usuarios objVistaDato =new D_Usuarios();

        //Retornar lista de usuarios
        public List<UsuarioCerezos> Listar()
        {
            return objVistaDato.listar();
        }

        //Llamado de metodo insertar, reglas de negocio
        public int Insertar(UsuarioCerezos obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            //Validar campo nombre
            if(string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del usuario no puede ser vacio";
            }
            //Validar campo Apellido
            if (string.IsNullOrEmpty(obj.Apellido) || string.IsNullOrWhiteSpace(obj.Apellido))
            {
                Mensaje = "El apellido del usuario no puede ser vacio";
            }
            //Validar campo Correo
            if (string.IsNullOrEmpty(obj.Email) || string.IsNullOrWhiteSpace(obj.Email))
            {
                Mensaje = "El email del usuario no puede ser vacio";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                //Llamar clase recursos + metodo
                string clave = "Prueba123";
                obj.Clave = N_Recursos.ConvertirSHA256(clave);

                return objVistaDato.Insertar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }
            
        }

        //Llamado de metodo Actualizar, reglas de negocio
        public bool Actualizar(UsuarioCerezos obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            //Validar campo nombre
            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del usuario no puede ser vacio";
            }
            //Validar campo Apellido
            if (string.IsNullOrEmpty(obj.Apellido) || string.IsNullOrWhiteSpace(obj.Apellido))
            {
                Mensaje = "El apellido del usuario no puede ser vacio";
            }
            //Validar campo Correo
            if (string.IsNullOrEmpty(obj.Email) || string.IsNullOrWhiteSpace(obj.Email))
            {
                Mensaje = "El email del usuario no puede ser vacio";
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
