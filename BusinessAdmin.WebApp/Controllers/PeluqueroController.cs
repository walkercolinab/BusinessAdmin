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
            ViewBag.SucursalID = new SelectList(db.Sucursales.Where(x => x.EsActivo), "SucursalID", "Nombre");
            Peluquero peluquero = new Peluquero();
            return View(peluquero);
        }

        // POST: /Peluquero/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="PeluqueroID,Nombres,ApellidoPaterno,ApellidoMaterno,CedulaIdentidad,TelefonoFijo,TelefonoMovil,FechaIngreso,ModoContrado,PorcentajeCorte,PorcentajeVentas,PorcentajeSemanal,EsActivo,SucursalID")] Peluquero peluquero)
        {
            // Validar si ci existe
            bool existeCi = false;
            if (db.Peluqueros.Any())
            {
                existeCi = db.Peluqueros.Where(x => x.CedulaIdentidad == peluquero.CedulaIdentidad).FirstOrDefault() == null ? false : true;
                if (existeCi)
                {
                    ModelState.AddModelError(string.Empty, "Existe el peluquero con cedula de identidad: " + peluquero.CedulaIdentidad + ", ingrese otra cedula de identidad.");
                }
            }
            if (ModelState.IsValid)
            {
                db.Peluqueros.Add(peluquero);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SucursalID = new SelectList(db.Sucursales.Where(x => x.EsActivo), "SucursalID", "Nombre", peluquero.SucursalID);
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
            ViewBag.SucursalID = new SelectList(db.Sucursales.Where(x => x.EsActivo), "SucursalID", "Nombre", peluquero.SucursalID);
            return View(peluquero);
        }

        // POST: /Peluquero/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="PeluqueroID,Nombres,ApellidoPaterno,ApellidoMaterno,CedulaIdentidad,TelefonoFijo,TelefonoMovil,FechaIngreso,ModoContrado,PorcentajeCorte,PorcentajeVentas,PorcentajeSemanal,EsActivo,SucursalID")] Peluquero peluquero)
        {
            // Validar si ci existe
            bool existeCi = false;
            if (db.Peluqueros.Any())
            {
                existeCi = db.Peluqueros.Where(x => x.CedulaIdentidad == peluquero.CedulaIdentidad && x.PeluqueroID != peluquero.PeluqueroID).FirstOrDefault() == null ? false : true;
                if (existeCi)
                {
                    ModelState.AddModelError(string.Empty, "Existe el peluquero con cedula de identidad: " + peluquero.CedulaIdentidad + ", ingrese otra cedula de identidad.");
                }
            }
            if (ModelState.IsValid)
            {
                db.Entry(peluquero).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SucursalID = new SelectList(db.Sucursales.Where(x => x.EsActivo), "SucursalID", "Nombre", peluquero.SucursalID);
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
            // Validar que la sucursal no tenga peluqueros, productos o servicios asociados
            bool peluqueroExisteEnRelaciones = false;
            //peluqueroExisteEnRelaciones = db.Pagos.Where(x => x.pe == sucursal.SucursalID).FirstOrDefault() == null ? false : true;
            peluqueroExisteEnRelaciones = db.CobroProductos.Where(x => x.PeluqueroID == peluquero.PeluqueroID).FirstOrDefault() == null ? false : true;
            peluqueroExisteEnRelaciones = db.CobroServicios.Where(x => x.PeluqueroID == peluquero.PeluqueroID).FirstOrDefault() == null ? false : true;
            if (peluqueroExisteEnRelaciones)
            {
                ModelState.AddModelError(string.Empty, "El peluquero " + peluquero.Nombres + " " + peluquero.ApellidoPaterno + " esta siendo usado, no se puede eliminar.");
                return View(peluquero);
            }
            else
            {
                //db.Peluqueros.Remove(peluquero);
                peluquero.EsActivo = false;
                db.Entry(peluquero).State = EntityState.Modified;
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
