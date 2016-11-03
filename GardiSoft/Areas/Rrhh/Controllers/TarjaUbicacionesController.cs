using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Entidades.Rrhh.Tarja;
using GardiSoft.Models;

namespace GardiSoft.Areas.Rrhh.Controllers
{
    public class TarjaUbicacionesController : Controller
    {
        private GardiSoftContext db = new GardiSoftContext();

        // GET: Rrhh/TarjaUbicaciones
        public ActionResult Index()
        {
            var tarjaUbicaciones = db.TarjaUbicaciones.Include(t => t.SubProyecto);
            return View(tarjaUbicaciones.ToList());
        }

        // GET: Rrhh/TarjaUbicaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TarjaUbicaciones tarjaUbicaciones = db.TarjaUbicaciones.Find(id);
            if (tarjaUbicaciones == null)
            {
                return HttpNotFound();
            }
            return View(tarjaUbicaciones);
        }

        // GET: Rrhh/TarjaUbicaciones/Create
        public ActionResult Create()
        {
            ViewBag.IdSubproyecto = new SelectList(db.SubProyectoes.Where(x=> x.Visible == true).ToList() , "Id", "Nombre");
            return View();
        }

        // POST: Rrhh/TarjaUbicaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion,IdSubproyecto")] TarjaUbicaciones tarjaUbicaciones)
        {
            if (ModelState.IsValid)
            {
                db.TarjaUbicaciones.Add(tarjaUbicaciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdSubproyecto = new SelectList(db.SubProyectoes, "Id", "Nombre", tarjaUbicaciones.IdSubproyecto);
            return View(tarjaUbicaciones);
        }

        // GET: Rrhh/TarjaUbicaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TarjaUbicaciones tarjaUbicaciones = db.TarjaUbicaciones.Find(id);
            if (tarjaUbicaciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdSubproyecto = new SelectList(db.SubProyectoes, "Id", "Nombre", tarjaUbicaciones.IdSubproyecto);
            return View(tarjaUbicaciones);
        }

        // POST: Rrhh/TarjaUbicaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion,IdSubproyecto")] TarjaUbicaciones tarjaUbicaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tarjaUbicaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdSubproyecto = new SelectList(db.SubProyectoes, "Id", "Nombre", tarjaUbicaciones.IdSubproyecto);
            return View(tarjaUbicaciones);
        }

        // GET: Rrhh/TarjaUbicaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TarjaUbicaciones tarjaUbicaciones = db.TarjaUbicaciones.Find(id);
            if (tarjaUbicaciones == null)
            {
                return HttpNotFound();
            }
            return View(tarjaUbicaciones);
        }

        // POST: Rrhh/TarjaUbicaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TarjaUbicaciones tarjaUbicaciones = db.TarjaUbicaciones.Find(id);
            db.TarjaUbicaciones.Remove(tarjaUbicaciones);
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
