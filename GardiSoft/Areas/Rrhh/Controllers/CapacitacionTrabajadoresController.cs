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
    [Authorize]
    public class CapacitacionTrabajadoresController : Controller
    {
        private GardiSoftContext db = new GardiSoftContext();

        // GET: Rrhh/CapacitacionTrabajadores
        public ActionResult Index(int? id=0)
        {
            //var trabajadores = db.Trabajadores.Include(t => t.Aspecto).Include(t => t.Dnc).Include(t => t.Tipo);
            ViewBag.IdAspecto = new SelectList(db.Aspecto, "Id", "Nombre");
            ViewBag.IdDnc = new SelectList(db.Dncs, "Id", "usuarioCreador");
            ViewBag.IdTipo = new SelectList(db.Tipo, "Id", "Nombre");
            ViewBag.Id = id;
            var dnc =  db.Dncs.First(x => x.Id == id);
            ViewBag.Abierta = DateTime.Now <= db.DncEstados.First(x => x.Año == dnc.anio).FechaCierre ? true : false;
            new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag);
            return View();
        }


        public JsonResult VerCursosExternos(string rut)
        {

            var cursos = db.HistoricoOtic.Where(x => x.RutTrabajador == rut).Select(x => new { x, x.Fecha.Year }).ToList();
            return Json(cursos, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Eliminartrabjador(int id=0)
        {

            var trabajador = db.Trabajadores.FirstOrDefault(x => x.Id == id);
            if(trabajador != null)
            {
                db.Entry(trabajador).State = EntityState.Deleted;
                db.SaveChanges();
                return Json(new { Resultado = "exito" },JsonRequestBehavior.AllowGet);

            }
            return Json(new { Resultado = "error" },JsonRequestBehavior.AllowGet);
        }


        // GET: Rrhh/CapacitacionTrabajadores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trabajadores trabajadores = db.Trabajadores.Find(id);
            if (trabajadores == null)
            {
                return HttpNotFound();
            }
            return View(trabajadores);
        }

        // GET: Rrhh/CapacitacionTrabajadores/Create
        public ActionResult Create()
        {
            ViewBag.IdAspecto = new SelectList(db.Aspecto, "Id", "Nombre");
            ViewBag.IdDnc = new SelectList(db.Dncs, "Id", "usuarioCreador");
            ViewBag.IdTipo = new SelectList(db.Tipo, "Id", "Nombre");
            return View();
        }

        // POST: Rrhh/CapacitacionTrabajadores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.

        
     
        public JsonResult AgregarTrabajador(Trabajadores trabajadores)
        {


            if (ModelState.IsValid)

            {
                db.Trabajadores.Add(trabajadores);
                db.SaveChanges();
                var a = new { Id = "agregado" };
                return Json(new { Resultado = "exito" }, JsonRequestBehavior.AllowGet);
                //return PartialView("_trabajadores",db.Trabajadores.ToList());

            }
          
            return Json(new { Resultado = "error" }, JsonRequestBehavior.AllowGet);
          
        }

        public ActionResult VerTrabajadores(int? id=0)
        {
            int idDnc = 0;
            try
            {
                idDnc = id??0;
            }
            catch (Exception)
            {

                //si no hay DNC pasa por la ventana anterior

                idDnc = 0;

            }

            return PartialView("_trabajadores", db.Trabajadores.Include(t => t.Aspecto).Include(t => t.Tipo).Where(t=> t.IdDnc == idDnc ).ToList());
        }

        // GET: Rrhh/CapacitacionTrabajadores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trabajadores trabajadores = db.Trabajadores.Find(id);
            if (trabajadores == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAspecto = new SelectList(db.Aspecto, "Id", "Nombre", trabajadores.IdAspecto);
            ViewBag.IdDnc = new SelectList(db.Dncs, "Id", "usuarioCreador", trabajadores.IdDnc);
            ViewBag.IdTipo = new SelectList(db.Tipo, "Id", "Nombre", trabajadores.IdTipo);
            return View(trabajadores);
        }

        // POST: Rrhh/CapacitacionTrabajadores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RutTrabajador,Trimestre,Objetivo,IdTipo,IdAspecto,IdDnc,NombreCapacitacion,Realizada,ValorCurso,CantidadDeHoras,SubsidioSense,ValorHoraSense,FechaEjecucionCurso")] Trabajadores trabajadores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trabajadores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAspecto = new SelectList(db.Aspecto, "Id", "Nombre", trabajadores.IdAspecto);
            ViewBag.IdDnc = new SelectList(db.Dncs, "Id", "usuarioCreador", trabajadores.IdDnc);
            ViewBag.IdTipo = new SelectList(db.Tipo, "Id", "Nombre", trabajadores.IdTipo);
            return View(trabajadores);
          
        }

        // GET: Rrhh/CapacitacionTrabajadores/Delete/5
        public ActionResult Delete(int? id)
        {
            Trabajadores trabajadores = db.Trabajadores.Find(id);
            db.Trabajadores.Remove(trabajadores);
            db.SaveChanges();
            return RedirectToAction("Index");
            //return View(trabajadores);
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
