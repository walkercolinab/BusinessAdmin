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
    public class PeluqueroController : Controller
    {
        private BdContexto db = new BdContexto();

        // GET: /Peluquero/
        public ActionResult Index()
        {
            var peluqueros = db.Peluqueros.Include(p => p.Sucursal);
            return View(peluqueros.ToList());
        }

        // GET: /Peluquero/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Peluquero peluquero = db.Peluqueros.Find(id);
            if (peluquero == null)
            {
                return HttpNotFound();
            }
            return View(peluquero);
        }

        // GET: /Peluquero/Create
        public ActionResult Create()
        {
            ViewBag.SucursalID = new SelectList(db.Sucursales, "SucursalID", "Nombre");
            return View();
        }

        // POST: /Peluquero/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="PeluqueroID,Nombres,ApellidoPaterno,ApellidoMaterno,CedulaIdentidad,TelefonoFijo,TelefonoMovil,FechaIngreso,ModoContrado,PorcentajeCorte,PorcentajeVentas,PorcentajeSemanal,EsActivo,SucursalID")] Peluquero peluquero)
        {
            if (ModelState.IsValid)
            {
                db.Peluqueros.Add(peluquero);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SucursalID = new SelectList(db.Sucursales, "SucursalID", "Nombre", peluquero.SucursalID);
            return View(peluquero);
        }

        // GET: /Peluquero/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Peluquero peluquero = db.Peluqueros.Find(id);
            if (peluquero == null)
            {
                return HttpNotFound();
            }
            ViewBag.SucursalID = new SelectList(db.Sucursales, "SucursalID", "Nombre", peluquero.SucursalID);
            return View(peluquero);
        }

        // POST: /Peluquero/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="PeluqueroID,Nombres,ApellidoPaterno,ApellidoMaterno,CedulaIdentidad,TelefonoFijo,TelefonoMovil,FechaIngreso,ModoContrado,PorcentajeCorte,PorcentajeVentas,PorcentajeSemanal,EsActivo,SucursalID")] Peluquero peluquero)
        {
            if (ModelState.IsValid)
            {
                db.Entry(peluquero).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SucursalID = new SelectList(db.Sucursales, "SucursalID", "Nombre", peluquero.SucursalID);
            return View(peluquero);
        }

        // GET: /Peluquero/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Peluquero peluquero = db.Peluqueros.Find(id);
            if (peluquero == null)
            {
                return HttpNotFound();
            }
            return View(peluquero);
        }

        // POST: /Peluquero/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Peluquero peluquero = db.Peluqueros.Find(id);
            db.Peluqueros.Remove(peluquero);
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
