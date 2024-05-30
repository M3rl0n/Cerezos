using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VistaEntidad;
using VistaNegocio;

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

        //Devuelve listar clientes
        [HttpGet]
        public JsonResult ListarClientes()
        {
            List<ClienteCerezos> oLista = new List<ClienteCerezos>();
            oLista = new N_Cliente().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        //Editar clientes
        [HttpPost]
        public JsonResult GuardarCliente(ClienteCerezos objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IDCliente != 0)
            {
                resultado = new N_Cliente().Actualizar(objeto, out mensaje);
            }
            else
            {
                resultado = 0;
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        //Eliminar categoria
        [HttpPost]
        public JsonResult EliminarCliente(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new N_Cliente().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }



    }
}