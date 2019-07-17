﻿using System;
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
    public class ServicioController : Controller
    {
        private BdContexto db = new BdContexto();

        // GET: /Servicio/
        public ActionResult Index()
        {
            var servicios = db.Servicios.Include(s => s.Sucursal);
            return View(servicios.ToList());
        }

        // GET: /Servicio/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servicio servicio = db.Servicios.Find(id);
            if (servicio == null)
            {
                return HttpNotFound();
            }
            return View(servicio);
        }

        // GET: /Servicio/Create
        public ActionResult Create()
        {
            ViewBag.SucursalID = new SelectList(db.Sucursales, "SucursalID", "Nombre");
            return View();
        }

        // POST: /Servicio/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ServicioID,Nombre,PrecioBase,EsActivo,SucursalID")] Servicio servicio)
        {
            if (ModelState.IsValid)
            {
                db.Servicios.Add(servicio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SucursalID = new SelectList(db.Sucursales, "SucursalID", "Nombre", servicio.SucursalID);
            return View(servicio);
        }

        // GET: /Servicio/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servicio servicio = db.Servicios.Find(id);
            if (servicio == null)
            {
                return HttpNotFound();
            }
            ViewBag.SucursalID = new SelectList(db.Sucursales, "SucursalID", "Nombre", servicio.SucursalID);
            return View(servicio);
        }

        // POST: /Servicio/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ServicioID,Nombre,PrecioBase,EsActivo,SucursalID")] Servicio servicio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servicio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SucursalID = new SelectList(db.Sucursales, "SucursalID", "Nombre", servicio.SucursalID);
            return View(servicio);
        }

        // GET: /Servicio/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servicio servicio = db.Servicios.Find(id);
            if (servicio == null)
            {
                return HttpNotFound();
            }
            return View(servicio);
        }

        // POST: /Servicio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Servicio servicio = db.Servicios.Find(id);
            db.Servicios.Remove(servicio);
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
