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
                string clave = N_Recursos.GenerarClave();
                string asunto = "Creacion de cuenta";
                string mensajeCorreo = "<h3>Su cuenta feu creada con exito!</h3><br><p>Su contraseña para acceder es: !clave!</p>";
                mensajeCorreo = mensajeCorreo.Replace("!clave!", clave);

                //Enviar correo
                bool respuesta = N_Recursos.EnviarCorreo(obj.Email, asunto, mensajeCorreo);
                if (respuesta)
                {
                    //Encriptar
                    obj.Clave = N_Recursos.ConvertirSHA256(clave);
                    return objVistaDato.Insertar(obj, out Mensaje);
                }
                else
                {
                    Mensaje = "No se puede enviar el correo";
                    return 0;
                }
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
