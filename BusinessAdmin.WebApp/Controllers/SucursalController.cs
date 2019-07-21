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
    public class SucursalController : Controller
    {
        private BdContexto db = new BdContexto();

        // GET: /Sucursal/
        public ActionResult Index()
        {
            return View(db.Sucursales.ToList());
        }

        // GET: /Sucursal/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sucursal sucursal = db.Sucursales.Find(id);
            if (sucursal == null)
            {
                return HttpNotFound();
            }
            return View(sucursal);
        }

        // GET: /Sucursal/Create
        public ActionResult Create()
        {
            Sucursal sucursal = new Sucursal();
            return View(sucursal);
        }

        // POST: /Sucursal/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="SucursalID,Nombre,Direccion,TelefonoFijo,TelefonoMovil,EsActivo")] Sucursal sucursal)
        {
            // Validar si nombre existe
            bool existeSucursal = false;
            if (db.Sucursales.Any() && !string.IsNullOrEmpty(sucursal.Nombre))
            {
                existeSucursal = db.Sucursales.Where(x => x.Nombre == sucursal.Nombre).FirstOrDefault() == null ? false : true;
                if(existeSucursal)
                {
                    ModelState.AddModelError(string.Empty, "Existe la sucursal con nombre: " + sucursal.Nombre + ", ingrese otro nombre.");
                }
            }
            if (ModelState.IsValid)
            {
                db.Sucursales.Add(sucursal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sucursal);
        }

        // GET: /Sucursal/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sucursal sucursal = db.Sucursales.Find(id);
            if (sucursal == null)
            {
                return HttpNotFound();
            }
            return View(sucursal);
        }

        // POST: /Sucursal/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="SucursalID,Nombre,Direccion,TelefonoFijo,TelefonoMovil,EsActivo")] Sucursal sucursal)
        {
            // Validar si nombre existe
            bool existeSucursal = false;
            if (db.Sucursales.Any() && !string.IsNullOrEmpty(sucursal.Nombre))
            {
                existeSucursal = db.Sucursales.Where(x => x.Nombre == sucursal.Nombre && x.SucursalID != sucursal.SucursalID).FirstOrDefault() == null ? false : true;
                if (existeSucursal)
                {
                    ModelState.AddModelError(string.Empty, "Existe la sucursal con nombre: " + sucursal.Nombre + ", ingrese otro nombre.");
                }
            }
            if (ModelState.IsValid)
            {
                db.Entry(sucursal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sucursal);
        }

        // GET: /Sucursal/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sucursal sucursal = db.Sucursales.Find(id);
            if (sucursal == null)
            {
                return HttpNotFound();
            }
            return View(sucursal);
        }

        // POST: /Sucursal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Sucursal sucursal = db.Sucursales.Find(id);
            // Validar que la sucursal no tenga peluqueros, productos o servicios asociados
            bool sucursalExisteEnRelaciones = false;
            sucursalExisteEnRelaciones = db.Peluqueros.Where(x => x.SucursalID == sucursal.SucursalID).FirstOrDefault() == null ? false :true;
            sucursalExisteEnRelaciones = db.Productos.Where(x => x.SucursalID == sucursal.SucursalID).FirstOrDefault() == null ? false : true;
            sucursalExisteEnRelaciones = db.Servicios.Where(x => x.SucursalID == sucursal.SucursalID).FirstOrDefault() == null ? false : true;
            if (sucursalExisteEnRelaciones)
            {
                ModelState.AddModelError(string.Empty, "La sucursal " + sucursal.Nombre + " esta siendo usado, no se puede eliminar.");
                return View(sucursal);
            }
            else
            {
                //db.Sucursales.Remove(sucursal);
                sucursal.EsActivo = false;
                db.Entry(sucursal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
