using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        //++++++++++++++++ CATEGORIA +++++++++++++++++++++++++
        #region CATEGORIAPRODUCTOS

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
        #endregion

        //++++++++++++++++ PRODUCTOS +++++++++++++++++++++++++
        #region PRODUCTOS
        //Devuelve lista de prod
        [HttpGet]
        public JsonResult ListarProductos()
        {
            List<ProductosCerezos> oLista = new List<ProductosCerezos>();
            oLista = new N_Producto().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        //Guardar y editar prod
        [HttpPost]
        public JsonResult GuardarProductos(string objeto, HttpPostedFileBase archivoImagen)
        {
            object resultado;
            string mensaje = string.Empty;
            bool operacionExitosa = true;
            bool GuardarImagenExito = true;

            ProductosCerezos oProducto = new ProductosCerezos();
            oProducto = JsonConvert.DeserializeObject<ProductosCerezos>(objeto);

            decimal precio;

            if(decimal.TryParse(oProducto.PrecioTexto, NumberStyles.AllowDecimalPoint, new CultureInfo("es-CO"), out precio))
            {
                oProducto.Precio = precio;
            }
            else
            {
                return Json(new {operacionExitosa = false, mensaje = "El formao del precio debe ser ##.##", JsonRequestBehavior.AllowGet});
            }

            if (oProducto.IDProducto == 0)
            {
                resultado = new N_Producto().Insertar(oProducto, out mensaje);
            }
            else
            {
                resultado = new N_Producto().Actualizar(oProducto, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }



        #endregion
    }
}