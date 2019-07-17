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
    public class CobroProductoController : Controller
    {
        private BdContexto db = new BdContexto();

        // GET: /CobroProducto/
        public ActionResult Index()
        {
            var cobroproductos = db.CobroProductos.Include(c => c.Usuario).Include(c => c.Producto).Include(c => c.Peluquero);
            return View(cobroproductos.ToList());
        }

        // GET: /CobroProducto/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CobroProducto cobroproducto = db.CobroProductos.Find(id);
            if (cobroproducto == null)
            {
                return HttpNotFound();
            }
            return View(cobroproducto);
        }

        // GET: /CobroProducto/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombres");
            ViewBag.ProductoID = new SelectList(db.Productos, "ProductoID", "Nombre");
            ViewBag.PeluqueroID = new SelectList(db.Peluqueros, "PeluqueroID", "Nombres");
            return View();
        }

        // POST: /CobroProducto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CobroProductoID,Monto,TipoPago,Ingreso,Cambio,FechaCobro,EsActivo,UsuarioID,ProductoID,PeluqueroID")] CobroProducto cobroproducto)
        {
            if (ModelState.IsValid)
            {
                db.CobroProductos.Add(cobroproducto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombres", cobroproducto.UsuarioID);
            ViewBag.ProductoID = new SelectList(db.Productos, "ProductoID", "Nombre", cobroproducto.ProductoID);
            ViewBag.PeluqueroID = new SelectList(db.Peluqueros, "PeluqueroID", "Nombres", cobroproducto.PeluqueroID);
            return View(cobroproducto);
        }

        // GET: /CobroProducto/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CobroProducto cobroproducto = db.CobroProductos.Find(id);
            if (cobroproducto == null)
            {
                return HttpNotFound();
            }
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombres", cobroproducto.UsuarioID);
            ViewBag.ProductoID = new SelectList(db.Productos, "ProductoID", "Nombre", cobroproducto.ProductoID);
            ViewBag.PeluqueroID = new SelectList(db.Peluqueros, "PeluqueroID", "Nombres", cobroproducto.PeluqueroID);
            return View(cobroproducto);
        }

        // POST: /CobroProducto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CobroProductoID,Monto,TipoPago,Ingreso,Cambio,FechaCobro,EsActivo,UsuarioID,ProductoID,PeluqueroID")] CobroProducto cobroproducto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cobroproducto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombres", cobroproducto.UsuarioID);
            ViewBag.ProductoID = new SelectList(db.Productos, "ProductoID", "Nombre", cobroproducto.ProductoID);
            ViewBag.PeluqueroID = new SelectList(db.Peluqueros, "PeluqueroID", "Nombres", cobroproducto.PeluqueroID);
            return View(cobroproducto);
        }

        // GET: /CobroProducto/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CobroProducto cobroproducto = db.CobroProductos.Find(id);
            if (cobroproducto == null)
            {
                return HttpNotFound();
            }
            return View(cobroproducto);
        }

        // POST: /CobroProducto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CobroProducto cobroproducto = db.CobroProductos.Find(id);
            db.CobroProductos.Remove(cobroproducto);
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
