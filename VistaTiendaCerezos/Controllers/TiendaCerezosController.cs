using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VistaTiendaCerezos.Controllers
{
    public class TiendaCerezosController : Controller
    {
        // GET: TiendaCerezos
        public ActionResult Index()
        {
            return View();
        }
    }
}