using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VistaAdminCerezos.Controllers
{
    public class GestClientesController : Controller
    {
        // GET: GestClientes
        public ActionResult Clientes()
        {
            return View();
        }
        public ActionResult Pedidos()
        {
            return View();
        }
        public ActionResult Solicitudes()
        {
            return View();
        }
    }
}