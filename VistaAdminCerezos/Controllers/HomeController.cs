using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VistaEntidad;
using VistaNegocio;

namespace VistaAdminCerezos.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Usuarios()
        {
            return View();
        }

        //Devuelve lista de usuarios
        [HttpGet]
        public JsonResult ListarUsuarios()
        {
            List<UsuarioCerezos> oLista= new List<UsuarioCerezos>();
            oLista = new N_Usuarios().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }

        //Devuelve lista de roles
        [HttpGet]
        public JsonResult ListarRoles()
        {
            List<RolCerezos> oLista = new List<RolCerezos>();
            oLista = new N_Roles().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }

        //Guardar y editar usuario
        [HttpPost]
        public JsonResult GuardarUsuarios(UsuarioCerezos objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IDUsuario == 0) {
                resultado = new N_Usuarios().Insertar(objeto, out mensaje);
            }
            else
            {
                resultado = new N_Usuarios().Actualizar(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);


        }

        //Eliminar Usuario
        [HttpPost]
        public JsonResult EliminarUsuario(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new N_Usuarios().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }
    }
}