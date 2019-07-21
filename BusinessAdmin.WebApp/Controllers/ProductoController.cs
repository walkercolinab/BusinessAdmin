using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BusinessAdmin.WebApp.Models;
using BusinessAdmin.WebApp.DAL;

namespace BusinessAdmin.WebApp.Controllers
{
    public class ProductoController : Controller
    {
        private BdContexto db = new BdContexto();

        // GET: /Producto/
        public ActionResult Index()
        {
            var productos = db.Productos.Include(p => p.Sucursal);
            return View(productos.ToList());
        }

        // GET: /Producto/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: /Producto/Create
        public ActionResult Create()
        {
            Producto producto = new Producto();
            ViewBag.SucursalID = new SelectList(db.Sucursales.Where(x => x.EsActivo), "SucursalID", "Nombre");
            return View(producto);
        }

        // POST: /Producto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ProductoID,Nombre,PrecioCompra,PrecioVenta,Stock,EsActivo,SucursalID")] Producto producto)
        {
            // Validar si nombre existe
            bool existeProducto = false;
            if (db.Productos.Any() && !string.IsNullOrEmpty(producto.Nombre))
            {
                existeProducto = db.Productos.Where(x => x.Nombre == producto.Nombre).FirstOrDefault() == null ? false : true;
                if (existeProducto)
                {
                    ModelState.AddModelError(string.Empty, "Existe el producto con nombre: " + producto.Nombre + ", ingrese otro nombre.");
                }
            }
            // Validar que precio de venta es mayor a cero
            if (producto.PrecioVenta <= 0)
            {
                ModelState.AddModelError(string.Empty, "Precio de venta del producto debe ser mayor a cero.");
            }
            // Validar que precio de venta es mayor a cero
            if (producto.PrecioCompra <= 0)
            {
                ModelState.AddModelError(string.Empty, "Precio de compra del producto debe ser mayor a cero.");
            }
            if (ModelState.IsValid)
            {
                db.Productos.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SucursalID = new SelectList(db.Sucursales.Where(x => x.EsActivo), "SucursalID", "Nombre", producto.SucursalID);
            return View(producto);
        }

        // GET: /Producto/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.SucursalID = new SelectList(db.Sucursales.Where(x => x.EsActivo), "SucursalID", "Nombre", producto.SucursalID);
            return View(producto);
        }

        // POST: /Producto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ProductoID,Nombre,PrecioCompra,PrecioVenta,Stock,EsActivo,SucursalID")] Producto producto)
        {
            // Validar si nombre existe
            bool existeProducto = false;
            if (db.Productos.Any() && !string.IsNullOrEmpty(producto.Nombre))
            {
                existeProducto = db.Productos.Where(x => x.Nombre == producto.Nombre && x.ProductoID != producto.ProductoID).FirstOrDefault() == null ? false : true;
                if (existeProducto)
                {
                    ModelState.AddModelError(string.Empty, "Existe el producto con nombre: " + producto.Nombre + ", ingrese otro nombre.");
                }
            }
            // Validar que precio de venta es mayor a cero
            if (producto.PrecioVenta <= 0)
            {
                ModelState.AddModelError(string.Empty, "Precio de venta del producto debe ser mayor a cero.");
            }
            // Validar que precio de venta es mayor a cero
            if (producto.PrecioCompra <= 0)
            {
                ModelState.AddModelError(string.Empty, "Precio de compra del producto debe ser mayor a cero.");
            }
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SucursalID = new SelectList(db.Sucursales.Where(x => x.EsActivo), "SucursalID", "Nombre", producto.SucursalID);
            return View(producto);
        }

        // GET: /Producto/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: /Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Producto producto = db.Productos.Find(id);
            //db.Productos.Remove(producto);
            producto.EsActivo = false;
            db.Entry(producto).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
