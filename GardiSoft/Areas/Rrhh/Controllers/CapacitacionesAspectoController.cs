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
    public class CapacitacionesAspectoController : Controller
    {
        private GardiSoftContext db = new GardiSoftContext();

        // GET: Rrhh/CapacitacionesAspecto
        public ActionResult Index()
        {

            return View(db.Aspecto.ToList());
        }

        // GET: Rrhh/CapacitacionesAspecto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aspecto aspecto = db.Aspecto.Find(id);
            if (aspecto == null)
            {
                return HttpNotFound();
            }
            return View(aspecto);
        }

        // GET: Rrhh/CapacitacionesAspecto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rrhh/CapacitacionesAspecto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre")] Aspecto aspecto)
        {
            if (ModelState.IsValid)
            {
                db.Aspecto.Add(aspecto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aspecto);
        }

        // GET: Rrhh/CapacitacionesAspecto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aspecto aspecto = db.Aspecto.Find(id);
            if (aspecto == null)
            {
                return HttpNotFound();
            }
            return View(aspecto);
        }

        // POST: Rrhh/CapacitacionesAspecto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre")] Aspecto aspecto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspecto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aspecto);
        }

        // GET: Rrhh/CapacitacionesAspecto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aspecto aspecto = db.Aspecto.Find(id);
            if (aspecto == null)
            {
                return HttpNotFound();
            }
            return View(aspecto);
        }

        // POST: Rrhh/CapacitacionesAspecto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Aspecto aspecto = db.Aspecto.Find(id);
            db.Aspecto.Remove(aspecto);
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
