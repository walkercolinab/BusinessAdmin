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
    public class TipoGastoController : Controller
    {
        private BdContexto db = new BdContexto();

        // GET: /TipoGasto/
        public ActionResult Index()
        {
            return View(db.TipoGastos.ToList());
        }

        // GET: /TipoGasto/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoGasto tipogasto = db.TipoGastos.Find(id);
            if (tipogasto == null)
            {
                return HttpNotFound();
            }
            return View(tipogasto);
        }

        // GET: /TipoGasto/Create
        public ActionResult Create()
        {
            TipoGasto tipoGasto = new TipoGasto();
            return View(tipoGasto);
        }

        // POST: /TipoGasto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TipoGastoID,Nombre,EsActivo")] TipoGasto tipogasto)
        {
            // Validar si nombre existe
            bool existeTipoGasto = false;
            if (db.TipoGastos.Any() && !string.IsNullOrEmpty(tipogasto.Nombre))
            {
                existeTipoGasto = db.TipoGastos.Where(x => x.Nombre == tipogasto.Nombre).FirstOrDefault() == null ? false : true;
                if (existeTipoGasto)
                {
                    ModelState.AddModelError(string.Empty, "Existe el tipo de gasto con nombre: " + tipogasto.Nombre + ", ingrese otro nombre.");
                }
            }
            if (ModelState.IsValid)
            {
                db.TipoGastos.Add(tipogasto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipogasto);
        }

        // GET: /TipoGasto/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoGasto tipogasto = db.TipoGastos.Find(id);
            if (tipogasto == null)
            {
                return HttpNotFound();
            }
            return View(tipogasto);
        }

        // POST: /TipoGasto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TipoGastoID,Nombre,EsActivo")] TipoGasto tipogasto)
        {
            // Validar si nombre existe
            bool existeTipoGasto = false;
            if (db.TipoGastos.Any() && !string.IsNullOrEmpty(tipogasto.Nombre))
            {
                existeTipoGasto = db.TipoGastos.Where(x => x.Nombre == tipogasto.Nombre && x.TipoGastoID != tipogasto.TipoGastoID).FirstOrDefault() == null ? false : true;
                if (existeTipoGasto)
                {
                    ModelState.AddModelError(string.Empty, "Existe el tipo de gasto con nombre: " + tipogasto.Nombre + ", ingrese otro nombre.");
                }
            }
            if (ModelState.IsValid)
            {
                db.Entry(tipogasto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipogasto);
        }

        // GET: /TipoGasto/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoGasto tipogasto = db.TipoGastos.Find(id);
            if (tipogasto == null)
            {
                return HttpNotFound();
            }
            return View(tipogasto);
        }

        // POST: /TipoGasto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            TipoGasto tipogasto = db.TipoGastos.Find(id);
            // Validar que el pago no tenga el tipo de gasto asociado
            bool tipoGastoExisteEnRelaciones = false;
            tipoGastoExisteEnRelaciones = db.Pagos.Where(x => x.TipoGastoID == tipogasto.TipoGastoID).FirstOrDefault() == null ? false :true;
            if (tipoGastoExisteEnRelaciones)
            {
                ModelState.AddModelError(string.Empty, "El tipo de gasto " + tipogasto.Nombre + " esta siendo usado, no se puede eliminar.");
                return View(tipogasto);
            }
            else
            {
                //db.TipoGastos.Remove(tipogasto);
                tipogasto.EsActivo = false;
                db.Entry(tipogasto).State = EntityState.Modified;
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
