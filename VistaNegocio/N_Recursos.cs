using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace VistaNegocio
{
    public class N_Recursos
    {
        //Encriptar texto en SHA256 

        public static string ConvertirSHA256(String text)
        {
            StringBuilder Sb = new StringBuilder();
            //Usar referencia de "system.security.Cryptography"
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(text));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }


    }
}
