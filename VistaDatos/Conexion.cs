using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace VistaDatos
{
    public class Conexion
    {
        //Conectarnos varias veces a la BD
        public static string cn = ConfigurationManager.ConnectionStrings["cadena"].ToString();
    }
}
