using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

            respuesta = new N_CarritoCompras().OperacionCarrito(IDProducto, sumar, out mensaje);

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

        //Boton de ventas
        [HttpPost]

        public async Task<JsonResult> ProcesarPago(List<CarritoCerezos> oListaCarrito, VentaCerezos oVenta)
        {
            decimal total = 0;

            DataTable detalle_venta = new DataTable();
            detalle_venta.Locale = new CultureInfo("en-CO");
            detalle_venta.Columns.Add("IDProducto", typeof(string));
            detalle_venta.Columns.Add("Cantidad", typeof(int));
            detalle_venta.Columns.Add("Total", typeof(decimal));
           
            foreach(CarritoCerezos oCarrito in oListaCarrito) {
                decimal subtotal = Convert.ToDecimal(oCarrito.Cantidad.ToString()) * oCarrito.oProducto.Precio;
                total += subtotal;

                detalle_venta.Rows.Add(new object[]
                {
                    oCarrito.oProducto.IDProducto,
                    oCarrito.Cantidad,
                    subtotal
                });
            }

            oVenta.MontoTotal = total;
            oVenta.IDCliente = IDClienteTest;

            TempData["Venta"]  = oVenta;
            TempData["DetalleVenta"] = detalle_venta;

            return Json(new {Status = true, Link="/TiendaCerezos/PagoEfectuado?IDTransaccion=code0001&status=true"}, JsonRequestBehavior.AllowGet);


        }
        //Vista de pago efecutado
        public async Task<ActionResult> PagoEfectuado()
        {

            string IDTransaccion = Request.QueryString["IDTransaccion"];
            bool status = Convert.ToBoolean(Request.QueryString["status"]);

            ViewData["Status"] = status;

            if (status)
            {
                VentaCerezos oVenta = (VentaCerezos)TempData["Venta"];
                DataTable detalle_venta = (DataTable)TempData["DetalleVenta"];
                oVenta.IDTransaccion = IDTransaccion;


                string mensaje = string.Empty;

                bool respuesta = new N_Venta().Insertar(oVenta, detalle_venta, out mensaje);

                ViewData["IDTransaccion"] = oVenta.IDTransaccion;

            }

            return View();

        }

    }
}