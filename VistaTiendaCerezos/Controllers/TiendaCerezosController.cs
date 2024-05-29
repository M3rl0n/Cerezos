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
        private const int IDClienteTest = 1;
        // GET: TiendaCerezos
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DetalleProducto(int IDProducto = 0)
        {

            ProductosCerezos oProducto = new ProductosCerezos();
            bool conversion;

            oProducto = new N_Producto().Listar().Where(p => p.IDProducto == IDProducto).FirstOrDefault();

            if(oProducto != null)
            {
                oProducto.Base64 = N_Recursos.ConvertirBase64(Path.Combine(oProducto.RutaImagen, oProducto.NombreImagen), out conversion);
                oProducto.Extension = Path.GetExtension(oProducto.NombreImagen);
            }
            return View(oProducto);
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

        //Agregar al carrito

        [HttpPost]
        public JsonResult AgregarCarrito(int IDProducto)
        {
            bool existe = new N_CarritoCompras().ExisteCarrito(IDProducto);
            bool respuesta = false;

            string mensaje = string.Empty;

            if (existe)
            {
                mensaje = "El producto ya existe en el carrito";
            }
            else
            {
                respuesta = new N_CarritoCompras().OperacionCarrito(IDProducto, true, out mensaje);
            }

            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        //Devolver cantidad de producto segun el carrito

        [HttpGet]

        public JsonResult CantidadEnCarrito()
        {
            int? IDCliente = null;
            int cantidad = new N_CarritoCompras().CantidadEnCarrito(IDCliente);
            return Json(new {cantidad = cantidad}, JsonRequestBehavior.AllowGet);
        }

        //Devolver lista de prodcutos 

        [HttpPost]
        
        public JsonResult ListarProductosCarrito()
        {
            List<CarritoCerezos> olista = new List<CarritoCerezos>();
            bool conversion;

            olista = new N_CarritoCompras().listarProducto(IDClienteTest).Select(oc => new CarritoCerezos()
            {
                oProducto = new ProductosCerezos()
                {
                    IDProducto = oc.oProducto.IDProducto,
                    Nombre = oc.oProducto.Nombre,
                    oCategoria = oc.oProducto.oCategoria,
                    Precio = oc.oProducto.Precio,
                    RutaImagen = oc.oProducto.RutaImagen,
                    Base64 = N_Recursos.ConvertirBase64(Path.Combine(oc.oProducto.RutaImagen, oc.oProducto.NombreImagen), out conversion),
                    Extension = Path.GetExtension(oc.oProducto.NombreImagen)
                },
                Cantidad = oc.Cantidad
            }).ToList();

            return Json(new { data = olista }, JsonRequestBehavior.AllowGet);

        }


        //Calcular carrito de compras

        [HttpPost]
        public JsonResult OperacionCarrito(int IDProducto, bool sumar)
        {

            bool respuesta = false;

            string mensaje = string.Empty;

            respuesta = new N_CarritoCompras().OperacionCarrito(IDProducto, true, out mensaje);

            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

           
        }

        //eliminar producto dentro del carrito

        [HttpPost]

        public JsonResult EliminarCarrito(int IDProducto)
        {
            bool respuesta = false;

            string mensaje = string.Empty;

            respuesta = new N_CarritoCompras().EliminarCarrito(IDProducto);

            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }

        //Obter departamentos

        [HttpPost]

        public JsonResult ObtenerDepartamentos()
        {
            List<DepartamentoCerezos> oLista = new List<DepartamentoCerezos>();

            oLista = new N_Ubicacion().Departamentos();

            return Json(new {lista = oLista}, JsonRequestBehavior.AllowGet);

        }

        //Obter Ciudades

        [HttpPost]

        public JsonResult ObtenerCiudades()
        {
            List<Ciudad> oLista = new List<Ciudad>();

            oLista = new N_Ubicacion().Ciudades();

            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);

        }

        //vista de carrito

        public ActionResult Carrito()
        {
            return View();
        }

    }
}