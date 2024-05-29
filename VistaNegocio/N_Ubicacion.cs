using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VistaDatos;
using VistaEntidad;

namespace VistaNegocio
{
    public class N_Ubicacion
    {

        private D_Ubicacion objVistaDato = new D_Ubicacion();

        //Llamar departamentos
        public List<DepartamentoCerezos> Departamentos()
        {
            return objVistaDato.Departamentos();
        }

        //Llamar ciudades por departamento

        public List<Ciudad> Ciudades()
        {
            return objVistaDato.Ciudades();
        }

    }
}
