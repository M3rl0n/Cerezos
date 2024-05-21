using System;
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

        //Devuelve Datos
        [HttpGet]
        public JsonResult ListarUsuarios()
        {
            List<UsuarioCerezos> oLista= new List<UsuarioCerezos>();
            oLista = new N_Usuarios().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }


    }
}