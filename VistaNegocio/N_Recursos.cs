using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace VistaNegocio
{
    public class N_Recursos
    {
        //Metodo para genera clave automatica
        public static string GenerarClave()
        {
            string clave = Guid.NewGuid().ToString("N").Substring(0, 6);
            return clave;
        }

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

        //Metodo para enviar correo
        public static bool EnviarCorreo(string correo, string asunto, string mensaje)
        {
            bool resultado = false;
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(correo);
                mail.From = new MailAddress("mailcerezos@gmail.com");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;

                //Servidor cliente para enviar correos
                var smtp = new SmtpClient()
                {
                    Credentials = new NetworkCredential("mailcerezos@gmail.com", "usqmbhuxhrflajze"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true
                };
                smtp.Send(mail);
                resultado = true;
            }
            catch (Exception ex)
            {
                resultado = false;
            }

            return resultado;
        }

        //Convertir cadena de texto (ruta imagen) en base 64

        public static string ConvertirBase64(string ruta, out bool conversion)
        {
            string textobase64 = string.Empty;
            conversion = true;
            try
            {
                byte[] bytes = File.ReadAllBytes(ruta);
                textobase64 = Convert.ToBase64String(bytes);
            }
            catch
            {
                conversion = false;
            }

            return textobase64;

        }
    }
}
