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
    public class IndicadorController : Controller
    {
        private GardiSoftContext db = new GardiSoftContext();

        // GET: Control/Indicador

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListarIndicador()
        {
            var indicadors = db.Indicadors.Include(i => i.Area).Include(i => i.Nivel_Control).Include(i => i.Proyecto).Include(i => i.Turno);
            return PartialView(indicadors.ToList());
        }

        // GET: Control/Indicador/Details/5
        public ActionResult DetallesIndicador(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indicador indicador = db.Indicadors.Find(id);
            if (indicador == null)
            {
                return HttpNotFound();
            }
            return View(indicador);
        }

        // GET: Control/Indicador/Create
        public ActionResult CrearIndicador()
        {
            ViewBag.IdArea = new SelectList(db.Areas, "Id_Area", "Nombre");
            ViewBag.IdNivel = new SelectList(db.Nivel_Control, "Id_Nivel", "Nombre");
            ViewBag.IdProyecto = new SelectList(db.Proyectoes, "Id_Proyecto", "Nombre");
            ViewBag.IdTurnos = new SelectList(db.Turno, "Id_Turno", "Nombre");
            return PartialView();
        }

        // POST: Control/Indicador/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearIndicador([Bind(Include = "Id_Indicador,Nombre,IdArea,IdNivel,IdTurnos,Frecuencia,IdProyecto")] Indicador indicador)
        {
            if (ModelState.IsValid)
            {
                db.Indicadors.Add(indicador);
                db.SaveChanges();
                
            }

            ViewBag.IdArea = new SelectList(db.Areas, "Id_Area", "Nombre", indicador.IdArea);
            ViewBag.IdNivel = new SelectList(db.Nivel_Control, "Id_Nivel", "Nombre", indicador.IdNivel);
            ViewBag.IdProyecto = new SelectList(db.Proyectoes, "Id_Proyecto", "Nombre", indicador.IdProyecto);
            ViewBag.IdTurnos = new SelectList(db.Turno, "Id_Turno", "Nombre", indicador.IdTurnos);
            return PartialView(indicador);
        }

        // GET: Control/Indicador/Edit/5
        public ActionResult EditarIndicador(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indicador indicador = db.Indicadors.Find(id);
            if (indicador == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdArea = new SelectList(db.Areas, "Id_Area", "Nombre", indicador.IdArea);
            ViewBag.IdNivel = new SelectList(db.Nivel_Control, "Id_Nivel", "Nombre", indicador.IdNivel);
            ViewBag.IdProyecto = new SelectList(db.Proyectoes, "Id_Proyecto", "Nombre", indicador.IdProyecto);
            ViewBag.IdTurnos = new SelectList(db.Turno, "Id_Turno", "Nombre", indicador.IdTurnos);
            return View(indicador);
        }

        // POST: Control/Indicador/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarIndicador([Bind(Include = "Id_Indicador,Nombre,IdArea,IdNivel,IdTurnos,Frecuencia,IdProyecto")] Indicador indicador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(indicador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdArea = new SelectList(db.Areas, "Id_Area", "Nombre", indicador.IdArea);
            ViewBag.IdNivel = new SelectList(db.Nivel_Control, "Id_Nivel", "Nombre", indicador.IdNivel);
            ViewBag.IdProyecto = new SelectList(db.Proyectoes, "Id_Proyecto", "Nombre", indicador.IdProyecto);
            ViewBag.IdTurnos = new SelectList(db.Turno, "Id_Turno", "Nombre", indicador.IdTurnos);
            return View(indicador);
        }

        // GET: Control/Indicador/Delete/5
        public ActionResult BorrarIndicador(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indicador indicador = db.Indicadors.Find(id);
            if (indicador == null)
            {
                return HttpNotFound();
            }
            return View(indicador);
        }

        // POST: Control/Indicador/Delete/5
        [HttpPost, ActionName("BorrarIndicador")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Indicador indicador = db.Indicadors.Find(id);
            db.Indicadors.Remove(indicador);
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
