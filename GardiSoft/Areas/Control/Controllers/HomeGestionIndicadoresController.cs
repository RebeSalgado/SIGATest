using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Entidades.Control;
using GardiSoft.Models;

namespace GardiSoft.Areas.Control.Controllers
{
    public class HomeGestionIndicadoresController : Controller
    {
        private GardiSoftContext db = new GardiSoftContext();
        // GET: Control/HomeGestionIndicadores
        public ActionResult Index()
        {
            return View();
        }
    


    /*------------------------"Controlador Areas"---------------------------------------------*/

    public ActionResult ListarAreas()
    {
        return PartialView(db.Areas.ToList());
    }

    // GET: Control/Areas/Details/5
    public ActionResult DetallesArea(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Area area = db.Areas.Find(id);
        if (area == null)
        {
            return HttpNotFound();
        }
        return View(area);
    }

    // GET: Control/Areas/Create
    public ActionResult CrearAreas()
    {
        return PartialView();
    }

    // POST: Control/Areas/Create
    // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
    // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult CrearAreas([Bind(Include = "Id_Area,Nombre")] Area area)
    {
        if (ModelState.IsValid)
        {
            db.Areas.Add(area);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(area);
    }

    // GET: Control/Areas/Edit/5
    public ActionResult EditarAreas(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Area area = db.Areas.Find(id);
        if (area == null)
        {
            return HttpNotFound();
        }
        return View(area);
    }

    // POST: Control/Areas/Edit/5
    // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
    // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult EditarAreas([Bind(Include = "Id_Area,Nombre")] Area area)
    {
        if (ModelState.IsValid)
        {
            db.Entry(area).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(area);
    }

    // GET: Control/Areas/Delete/5
    public ActionResult BorrarAreas(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Area area = db.Areas.Find(id);
        if (area == null)
        {
            return HttpNotFound();
        }
        return View(area);
    }

    // POST: Control/Areas/Delete/5
    [HttpPost, ActionName("BorrarAreas")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmedAreas(int id)
    {
        Area area = db.Areas.Find(id);
        db.Areas.Remove(area);
        db.SaveChanges();
        return RedirectToAction("Index");
    }
        /*-------------------------Metodo Utilizado por todos los controladores--------------------------------------*/
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            db.Dispose();
        }
        base.Dispose(disposing);
    }

        /*--------------------------------------Fin Controlador areas----------------------------------------*/

        /*-------------------------------------- Controlador Turno----------------------------------------*/

        public ActionResult ListarTurno()
        {
            return PartialView(db.Turno.ToList());
        }

        // GET: Control/Turno/Details/5
        public ActionResult DetallesTurno(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turno turno = db.Turno.Find(id);
            if (turno == null)
            {
                return HttpNotFound();
            }
            return View(turno);
        }

        // GET: Control/Turno/Create
        public ActionResult CrearTurno()
        {
            return View();
        }

        // POST: Control/Turno/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearTurno([Bind(Include = "Id_Turno,Nombre")] Turno turno)
        {
            if (ModelState.IsValid)
            {
                db.Turno.Add(turno);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(turno);
        }

        // GET: Control/Turno/Edit/5
        public ActionResult EditarTurno(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turno turno = db.Turno.Find(id);
            if (turno == null)
            {
                return HttpNotFound();
            }
            return View(turno);
        }

        // POST: Control/Turno/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarTurno([Bind(Include = "Id_Turno,Nombre")] Turno turno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(turno).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(turno);
        }

        // GET: Control/Turno/Delete/5
        public ActionResult BorrarTurno(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turno turno = db.Turno.Find(id);
            if (turno == null)
            {
                return HttpNotFound();
            }
            return View(turno);
        }

        // POST: Control/Turno/Delete/5
        [HttpPost, ActionName("BorrarTurno")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedTurno(int id)
        {
            Turno turno = db.Turno.Find(id);
            db.Turno.Remove(turno);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /*--------------------------------------Fin Controlador Turno----------------------------------------*/

        /*-------------------------------------- Controlador Nivel----------------------------------------*/

        public ActionResult ListarNivel()
        {
            return PartialView(db.Nivel_Control.ToList());
        }

        // GET: Control/Nivel_Control/Details/5
        public ActionResult DetallesNivel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nivel_Control nivel_Control = db.Nivel_Control.Find(id);
            if (nivel_Control == null)
            {
                return HttpNotFound();
            }
            return View(nivel_Control);
        }

        // GET: Control/Nivel_Control/Create
        public ActionResult CrearNivel()
        {
            return View();
        }

        // POST: Control/Nivel_Control/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearNivel([Bind(Include = "Id_Nivel,Nombre")] Nivel_Control nivel_Control)
        {
            if (ModelState.IsValid)
            {
                db.Nivel_Control.Add(nivel_Control);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nivel_Control);
        }

        // GET: Control/Nivel_Control/Edit/5
        public ActionResult EditarNivel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nivel_Control nivel_Control = db.Nivel_Control.Find(id);
            if (nivel_Control == null)
            {
                return HttpNotFound();
            }
            return View(nivel_Control);
        }

        // POST: Control/Nivel_Control/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarNivel([Bind(Include = "Id_Nivel,Nombre")] Nivel_Control nivel_Control)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nivel_Control).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nivel_Control);
        }

        // GET: Control/Nivel_Control/Delete/5
        public ActionResult BorarNivel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nivel_Control nivel_Control = db.Nivel_Control.Find(id);
            if (nivel_Control == null)
            {
                return HttpNotFound();
            }
            return View(nivel_Control);
        }

        // POST: Control/Nivel_Control/Delete/5
        [HttpPost, ActionName("BorarNivel")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedNivel(int id)
        {
            Nivel_Control nivel_Control = db.Nivel_Control.Find(id);
            db.Nivel_Control.Remove(nivel_Control);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /*--------------------------------------Fin Controlador Nivel----------------------------------------*/

        /*-------------------------------------- Controlador Proyecto----------------------------------------*/

        public ActionResult ListarProyecto()
        {
            return PartialView(db.Proyectoes.ToList());
        }

        // GET: Control/Proyecto/Details/5
        public ActionResult DetallesProyecto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proyecto proyecto = db.Proyectoes.Find(id);
            if (proyecto == null)
            {
                return HttpNotFound();
            }
            return View(proyecto);
        }

        // GET: Control/Proyecto/Create
        public ActionResult CrearProyecto()
        {
            return View();
        }

        // POST: Control/Proyecto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearProyecto([Bind(Include = "Id_Proyecto,Nombre,Ubicacion")] Proyecto proyecto)
        {
            if (ModelState.IsValid)
            {
                db.Proyectoes.Add(proyecto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(proyecto);
        }

        // GET: Control/Proyecto/Edit/5
        public ActionResult EditarProyecto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proyecto proyecto = db.Proyectoes.Find(id);
            if (proyecto == null)
            {
                return HttpNotFound();
            }
            return View(proyecto);
        }

        // POST: Control/Proyecto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarProyecto([Bind(Include = "Id_Proyecto,Nombre,Ubicacion")] Proyecto proyecto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proyecto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(proyecto);
        }

        // GET: Control/Proyecto/Delete/5
        public ActionResult BorrarProyecto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proyecto proyecto = db.Proyectoes.Find(id);
            if (proyecto == null)
            {
                return HttpNotFound();
            }
            return View(proyecto);
        }

        // POST: Control/Proyecto/Delete/5
        [HttpPost, ActionName("BorrarProyecto")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedProyecto(int id)
        {
            Proyecto proyecto = db.Proyectoes.Find(id);
            db.Proyectoes.Remove(proyecto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}

