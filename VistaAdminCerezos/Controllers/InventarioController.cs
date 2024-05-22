using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VistaEntidad;
using VistaNegocio;

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

        //Devuelve lista de categorias prod
        [HttpGet]
        public JsonResult ListarCategoriasProd()
        {
            List<CategoriaProductos> oLista = new List<CategoriaProductos>();
            oLista = new N_CategoriaProd().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        //Guardar y editar Categorias prod
        [HttpPost]
        public JsonResult GuardarCategoriaProd(CategoriaProductos objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IDCategoria == 0)
            {
                resultado = new N_CategoriaProd().Insertar(objeto, out mensaje);
            }
            else
            {
                resultado = new N_CategoriaProd().Actualizar(objeto, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        //Eliminar Usuario
        [HttpPost]
        public JsonResult EliminarCategoriaProd(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new N_CategoriaProd().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }
    }
}