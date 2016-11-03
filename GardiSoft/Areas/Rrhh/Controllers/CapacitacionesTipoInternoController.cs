using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Entidades.Rrhh.Capacitaciones;
using GardiSoft.Models;

namespace GardiSoft.Areas.Rrhh.Controllers
{
    public class CapacitacionesTipoInternoController : Controller
    {
        private GardiSoftContext db = new GardiSoftContext();

        // GET: Rrhh/CapacitacionesTipoInterno
        public ActionResult Index()
        {
            return View(db.TipoEfc.ToList());
        }

        // GET: Rrhh/CapacitacionesTipoInterno/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoEfc tipoInterno = db.TipoEfc.Find(id);
            if (tipoInterno == null)
            {
                return HttpNotFound();
            }
            return View(tipoInterno);
        }

        // GET: Rrhh/CapacitacionesTipoInterno/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rrhh/CapacitacionesTipoInterno/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre")] TipoEfc tipoInterno)
        {
            if (ModelState.IsValid)
            {
                db.TipoEfc.Add(tipoInterno);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoInterno);
        }

        // GET: Rrhh/CapacitacionesTipoInterno/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoEfc tipoInterno = db.TipoEfc.Find(id);
            if (tipoInterno == null)
            {
                return HttpNotFound();
            }
            return View(tipoInterno);
        }

        // POST: Rrhh/CapacitacionesTipoInterno/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre")] TipoEfc tipoInterno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoInterno).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoInterno);
        }

        // GET: Rrhh/CapacitacionesTipoInterno/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoEfc tipoInterno = db.TipoEfc.Find(id);
            if (tipoInterno == null)
            {
                return HttpNotFound();
            }
            return View(tipoInterno);
        }

        // POST: Rrhh/CapacitacionesTipoInterno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoEfc tipoInterno = db.TipoEfc.Find(id);
            db.TipoEfc.Remove(tipoInterno);
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
