using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VistaEntidad;
using VistaNegocio;

namespace VistaAdminCerezos.Controllers
{
    [Authorize]
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
            string mensaje = string.Empty;
            bool operacionExitosa = true;
            bool GuardarImagenExito = true;

            ProductosCerezos oProducto = new ProductosCerezos();
            oProducto = JsonConvert.DeserializeObject<ProductosCerezos>(objeto);

            decimal precio;

            if (decimal.TryParse(oProducto.PrecioTexto, NumberStyles.AllowDecimalPoint, new CultureInfo("es-CO"), out precio))
            {
                oProducto.Precio = precio;
            }
            else
            {
                return Json(new { operacionExitosa = false, mensaje = "El formao del precio debe ser ##.##", JsonRequestBehavior.AllowGet });
            }

            if (oProducto.IDProducto == 0)
            {
                int idProductoGenerado = new N_Producto().Insertar(oProducto, out mensaje);
                if (idProductoGenerado != 0)
                {
                    oProducto.IDProducto = idProductoGenerado;
                }
                else
                {
                    operacionExitosa = false;
                }
            }
            else
            {
                operacionExitosa = new N_Producto().Actualizar(oProducto, out mensaje);
            }

            //Logica para guardar la imagen
            if (operacionExitosa)
            {
                if (archivoImagen != null)
                {
                    string RutaGuardar = ConfigurationManager.AppSettings["ServidorFotos"];
                    string extension = Path.GetExtension(archivoImagen.FileName);
                    string nombreimg = string.Concat(oProducto.IDProducto.ToString(), extension);

                    try
                    {
                        archivoImagen.SaveAs(Path.Combine(RutaGuardar, nombreimg));
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                        GuardarImagenExito = false;
                    }

                    if (GuardarImagenExito)
                    {
                        oProducto.RutaImagen = RutaGuardar;
                        oProducto.NombreImagen = nombreimg;
                        bool rspta = new N_Producto().GuardarDatosImagen(oProducto, out mensaje);
                    }
                    else
                    {
                        mensaje = "Se guardo el producto pero hubo problemas con la imagen";
                    }

                }
            }

            return Json(new { operacionExitosa = operacionExitosa, IDGenerado = oProducto.IDProducto, mensaje, JsonRequestBehavior.AllowGet });
        }

        //Metodo para devolver imagnes en Base64

        [HttpPost]
        public JsonResult ImagenProducto(int id)
        {
            bool conversion;
            ProductosCerezos oProducto = new N_Producto().Listar().Where(p => p.IDProducto == id).FirstOrDefault();
            string textoBase64 = N_Recursos.ConvertirBase64(Path.Combine(oProducto.RutaImagen, oProducto.NombreImagen), out conversion);

            return Json(new
            {
                Conversion = conversion,
                textobase64 = textoBase64,
                extension = Path.GetExtension(oProducto.NombreImagen)
            },
            JsonRequestBehavior.AllowGet);
        }

        //Eliminar Producto
        [HttpPost]
        public JsonResult EliminarProducto(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new N_Producto().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }


        #endregion

    }
}