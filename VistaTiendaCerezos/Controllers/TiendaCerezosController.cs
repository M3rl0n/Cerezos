using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VistaEntidad;
using VistaNegocio;

namespace VistaTiendaCerezos.Controllers
{
    public class TiendaCerezosController : Controller
    {
        // GET: TiendaCerezos
        public ActionResult Index()
        {
            return View();
        }

        //Metodo para listar categorias productos

        [HttpGet]

        public JsonResult ListarCategoriasProductos()
        {
            List<CategoriaProductos> lista = new List<CategoriaProductos>();
            lista = new N_CategoriaProd().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        //Listar productos de acuerdo categoria

        [HttpPost]

        public JsonResult ListarProductos(int IDCategoria)
        {
            List<ProductosCerezos> lista = new List<ProductosCerezos>();
            bool conversion;

            lista = new N_Producto().Listar().Select(p => new ProductosCerezos()
            {
                IDProducto = p.IDProducto,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                oCategoria = p.oCategoria,
                Precio = p.Precio,
                Stock = p.Stock,
                RutaImagen = p.RutaImagen,
                Base64 = N_Recursos.ConvertirBase64(Path.Combine(p.RutaImagen, p.NombreImagen), out conversion),
                Extension = Path.GetExtension(p.NombreImagen),
                Activo = p.Activo
            }).Where(p =>
            p.oCategoria.IDCategoria == (IDCategoria == 0 ? p.oCategoria.IDCategoria : IDCategoria) &&
            p.Stock > 0 && p.Activo == true
            ).ToList();

            var jsonresult = Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;

            return jsonresult;

        }


    }
}