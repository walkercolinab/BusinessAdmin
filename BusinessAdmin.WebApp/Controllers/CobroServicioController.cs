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
    public class CobroServicioController : Controller
    {
        private BdContexto db = new BdContexto();

        // GET: /CobroServicio/
        public ActionResult Index()
        {
            var cobroservicios = db.CobroServicios.Include(c => c.Usuario).Include(c => c.Servicio).Include(c => c.Peluquero);
            return View(cobroservicios.ToList());
        }

        // GET: /CobroServicio/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CobroServicio cobroservicio = db.CobroServicios.Find(id);
            if (cobroservicio == null)
            {
                return HttpNotFound();
            }
            return View(cobroservicio);
        }

        // GET: /CobroServicio/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombres");
            ViewBag.ServicioID = new SelectList(db.Servicios, "ServicioID", "Nombre");
            ViewBag.PeluqueroID = new SelectList(db.Peluqueros, "PeluqueroID", "Nombres");
            return View();
        }

        // POST: /CobroServicio/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CobroServicioID,Monto,TipoPago,Ingreso,Cambio,FechaCobro,EsActivo,UsuarioID,ServicioID,PeluqueroID")] CobroServicio cobroservicio)
        {
            if (ModelState.IsValid)
            {
                db.CobroServicios.Add(cobroservicio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombres", cobroservicio.UsuarioID);
            ViewBag.ServicioID = new SelectList(db.Servicios, "ServicioID", "Nombre", cobroservicio.ServicioID);
            ViewBag.PeluqueroID = new SelectList(db.Peluqueros, "PeluqueroID", "Nombres", cobroservicio.PeluqueroID);
            return View(cobroservicio);
        }

        // GET: /CobroServicio/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CobroServicio cobroservicio = db.CobroServicios.Find(id);
            if (cobroservicio == null)
            {
                return HttpNotFound();
            }
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombres", cobroservicio.UsuarioID);
            ViewBag.ServicioID = new SelectList(db.Servicios, "ServicioID", "Nombre", cobroservicio.ServicioID);
            ViewBag.PeluqueroID = new SelectList(db.Peluqueros, "PeluqueroID", "Nombres", cobroservicio.PeluqueroID);
            return View(cobroservicio);
        }

        // POST: /CobroServicio/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CobroServicioID,Monto,TipoPago,Ingreso,Cambio,FechaCobro,EsActivo,UsuarioID,ServicioID,PeluqueroID")] CobroServicio cobroservicio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cobroservicio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Nombres", cobroservicio.UsuarioID);
            ViewBag.ServicioID = new SelectList(db.Servicios, "ServicioID", "Nombre", cobroservicio.ServicioID);
            ViewBag.PeluqueroID = new SelectList(db.Peluqueros, "PeluqueroID", "Nombres", cobroservicio.PeluqueroID);
            return View(cobroservicio);
        }

        // GET: /CobroServicio/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CobroServicio cobroservicio = db.CobroServicios.Find(id);
            if (cobroservicio == null)
            {
                return HttpNotFound();
            }
            return View(cobroservicio);
        }

        // POST: /CobroServicio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CobroServicio cobroservicio = db.CobroServicios.Find(id);
            db.CobroServicios.Remove(cobroservicio);
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
