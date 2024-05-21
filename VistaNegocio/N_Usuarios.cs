using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VistaDatos;
using VistaEntidad;

namespace VistaNegocio
{
    public class N_Usuarios
    {
        //Accesder a los metodos que tengan la clase D_Usuarios
        private D_Usuarios objVistaDato =new D_Usuarios();

        //Retornar lista de usuarios
        public List<UsuarioCerezos> Listar()
        {
            return objVistaDato.listar();
        }
    }

    public class N_Roles
    {
        //Accesder a los metodos que tengan la clase D_Usuarios
        private D_Rol objVistaDato = new D_Rol();      //Retornar lista de usuarios
        public List<RolCerezos> Listar()
        {
            return objVistaDato.listar();
        }
    }
}
