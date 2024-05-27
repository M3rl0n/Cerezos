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


        //Llamado de metodo insertar, reglas de negocio
        public int Insertar(ClienteCerezos obj, out string Mensaje)
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

            return 0;

        }



        //!!!!SE COMENTA PORQUE SE HACE REFERENCIA A CLASES QUE TODAVIA NO HAN SIDO CREADAS EN VISTAADMIN!!!!
        //public int Insertar(ClienteCerezos obj, out string Mensaje)
        //{
        //    Mensaje = string.Empty;

        //    if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
        //    {
        //        Mensaje = "El nombre del usuario no puede ser vacio";
        //    }
        //    else if (string.IsNullOrEmpty(obj.Apellido) || string.IsNullOrWhiteSpace(obj.Apellido))
        //    {
        //        Mensaje = "El apellido del usuario no puede ser vacio";
        //    }
        //    else if (string.IsNullOrEmpty(obj.Email) || string.IsNullOrWhiteSpace(obj.Email))
        //    {
        //        Mensaje = "El correo del usuario no puede ser vacio";
        //    }

        //    if (string.IsNullOrEmpty(Mensaje))
        //    {
        //        Clave que no ha sido creada
        //        string clave = N_Recursos.GenerarClave();

        //        string asunto = "Creación de cuenta";
        //        string mensaje_email = "<h3>Su cuenta fue creada correctamente</h3></br><p> Su contraseña para acceder es: !clave";
        //        mensaje_email = mensaje_email.Replace("!clave!", clave);

        //        bool respuesta = N_Recursos.EnviarEmail(obj.Email, asunto, mensaje_email);
        //        if (respuesta)
        //        {
        //            obj.Clave = N_Recursos.ConvertirSha256(clave);
        //            return objVistaDato.Registrar(obj, out Mensaje);

        //        }
        //        else
        //        {
        //            Mensaje = "No se puede enviar el email";
        //            return 0;
        //        }
        //    }
        //}
        //public bool CambiarClave(int IDCliente, string nuevaclave, out string Mensaje)
        //{
        //    return objVistaDatos.CambiarClave(IDCliente, nuevaclave, out Mensaje);
        //}
    }
}
