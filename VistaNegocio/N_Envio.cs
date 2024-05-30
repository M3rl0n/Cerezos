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
    public class N_EstadosEnvio
    {
        //Accesder a los metodos que tengan la clase D_ENVIOS
        private D_EstadosEnvio objVistaDato = new D_EstadosEnvio();      //Retornar lista de usuarios
        public List<TPEstadosENVCerezos> Listar()
        {
            return objVistaDato.listar();
        }
    }

    public class N_Envio
    {
        //Accesder a los metodos que tengan la clase D_Usuarios
        private D_Envio objVistaDato = new D_Envio();

        //Retornar lista de usuarios
        public List<SeguimientoENVCerezos> Listar()
        {
            return objVistaDato.listar();
        }

        //Llamado de metodo Actualizar, reglas de negocio
        public bool Actualizar(SeguimientoENVCerezos obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            //Validar campo nombre
            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "lA DESCRIPCION NO PUEDE ESTAR VACIA";
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


    }





}
