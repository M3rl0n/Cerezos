using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistaEntidad.Paypal
{
    public class Response_Paypal<T>
    {
        //Clase generica permite response capture y response cheackout en una sola clase
        public bool Status { get; set; }
        public T Response { get; set; }
    }

}
