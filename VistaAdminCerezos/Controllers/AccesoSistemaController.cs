using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using VistaEntidad;
using VistaNegocio;

namespace VistaAdminCerezos.Controllers
{
    public class AccesoSistemaController : Controller
    {
        // GET: AccesoSistema
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CambiarClave()
        {
            return View();
        }

        public ActionResult RestablecerClave()
        {
            return View();
        }

        //Realizar login
        [HttpPost]
        public ActionResult Index(string Email, string Clave)
        {
            UsuarioCerezos oUsuario = new UsuarioCerezos();
            oUsuario = new N_Usuarios().Listar().Where(u => u.Email == Email && u.Clave == N_Recursos.ConvertirSHA256(Clave)).FirstOrDefault();

            if(oUsuario == null)
            {
                ViewBag.Error = "Correo o contraseña invalidos";
                return View();
            }
            else
            {
                if(oUsuario.Restablecer) {

                    TempData["IDUsuario"] = oUsuario.IDUsuario;
                    return RedirectToAction("CambiarClave");

                }
                ViewBag.Error = null;
                return RedirectToAction("Index","Home");
            }
        }

        //Configurar cambios de clave primer inicio de sesion

        [HttpPost]
        public ActionResult CambiarClave(string IDUsuario, string ClaveActual, string NuevaClave, string ConfirmarClave)
        {
            UsuarioCerezos oUsuario = new UsuarioCerezos();
            oUsuario = new N_Usuarios().Listar().Where(u => u.IDUsuario == int.Parse(IDUsuario)).FirstOrDefault();

            if(oUsuario.Clave != N_Recursos.ConvertirSHA256(ClaveActual))
            {
                TempData["IDUsuario"] = IDUsuario;
                ViewData["vclave"] = "";
                ViewBag.Error = "La constraseña actual no es correcta";
                return View();
            }
            else if (NuevaClave != ConfirmarClave)
            {
                TempData["IDUsuario"] = IDUsuario;
                ViewData["vclave"] = ClaveActual;
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }


            ViewData["vclave"] = "";
            NuevaClave = N_Recursos.ConvertirSHA256(NuevaClave);

            string mensaje = string.Empty;

            bool respuesta = new N_Usuarios().CambiarClave(int.Parse(IDUsuario), NuevaClave, out mensaje);

            if(respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["IDUsuario"] = IDUsuario;
                ViewBag.Error = mensaje;
                return View();
            }
        }

    }
}