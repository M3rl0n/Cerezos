﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VistaAdminCerezos.Controllers
{
    public class InventarioController : Controller
    {
        // GET: Inventario
        public ActionResult Productos()
        {
            return View();
        }
        public ActionResult Categorias()
        {
            return View();
        }
    }
}