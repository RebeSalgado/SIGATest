using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Entidades.Sys;
using GardiSoft.Models;

namespace GardiSoft.Areas.Systema.Controllers
{
    public class PermisosController : Controller
    {
        private GardiSoftContext db = new GardiSoftContext();

        // GET: Systema/Permisos
        public ActionResult Index()
        {
            var permisos = db.Permisos.Include(p => p.SubMenu.Menu.Modulo).Include(p => p.Usuario).OrderByDescending(x=> new { Modulo = x.SubMenu.Menu.Modulo.Nombre, x.SubMenu.Menu.Nombre, x.SubMenu.Titulo }).ToList();
            return View(permisos.ToList());
        }

        public string VerNombreReal(string UserName)
        {
            var perfil = db.Perfil.FirstOrDefault(x => x.UserName == UserName);
            if(perfil != null)
            {
                return perfil.NombreReal;
            }
            else
            {
                return UserName;
            }

        }

        // GET: Systema/Permisos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permisos permisos = db.Permisos.Find(id);
            if (permisos == null)
            {
                return HttpNotFound();
            }
            return View(permisos);
        }

        // GET: Systema/Permisos/Create
        public ActionResult Create()
        {
            ViewBag.IdSubMenu = new SelectList(db.SubMenu, "Id", "Titulo");
            ViewBag.IdUsuario = new SelectList(db.Usuario, "Id", "Email");
            return View();
        }

        // POST: Systema/Permisos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdUsuario,IdSubMenu")] Permisos permisos)
        {
            if (ModelState.IsValid)
            {
                db.Permisos.Add(permisos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdSubMenu = new SelectList(db.SubMenu, "Id", "NombreAction", permisos.IdSubMenu);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "Id", "Email", permisos.IdUsuario);
            return View(permisos);
        }

        // GET: Systema/Permisos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permisos permisos = db.Permisos.Find(id);
            if (permisos == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdSubMenu = new SelectList(db.SubMenu, "Id", "NombreAction", permisos.IdSubMenu);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "Id", "Email", permisos.IdUsuario);
            return View(permisos);
        }

        // POST: Systema/Permisos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdUsuario,IdSubMenu")] Permisos permisos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(permisos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdSubMenu = new SelectList(db.SubMenu, "Id", "NombreAction", permisos.IdSubMenu);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "Id", "Email", permisos.IdUsuario);
            return View(permisos);
        }

        // GET: Systema/Permisos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permisos permisos = db.Permisos.Find(id);
            if (permisos == null)
            {
                return HttpNotFound();
            }
            return View(permisos);
        }

        // POST: Systema/Permisos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Permisos permisos = db.Permisos.Find(id);
            db.Permisos.Remove(permisos);
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
