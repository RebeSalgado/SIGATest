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
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace GardiSoft.Areas.Rrhh.Controllers
{
    [Authorize]
    public class CapacitacionesDncController : Controller
    {
        private GardiSoftContext db = new GardiSoftContext();

        // GET: Rrhh/CapacitacionesDnc
        public ActionResult Index()
        {
            //var dncs = db.Dncs.Include(d => d.SubProyecto);
            //return View(dncs.ToList());
            if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" });
            ViewBag.IdSubproyecto = new SelectList(db.SubProyectoes.Where(x => x.Visible == true).ToList(), "Id", "Nombre");
            ViewBag.Dncs = db.Dncs.Include("SubProyecto").Where(x => x.anio == 2016 && x.usuarioCreador == User.Identity.Name).ToList();
            return View();
        }


        // GET: Rrhh/CapacitacionesDnc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dnc dnc = db.Dncs.Find(id);
            if (dnc == null)
            {
                return HttpNotFound();
            }
            return View(dnc);
        }

        // GET: Rrhh/CapacitacionesDnc/Create
        public ActionResult Create()
        {
            if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" });
            ViewBag.IdSubproyecto = new SelectList(db.SubProyectoes, "Id", "Nombre");
            return View();
        }

        // POST: Rrhh/CapacitacionesDnc/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdSubproyecto,CreCodigo,usuarioCreador,anio,Cerrada")] Dnc dnc)
        {

            if (ModelState.IsValid)
            {
                var encontrado = db.Dncs.FirstOrDefault(t => t.usuarioCreador == User.Identity.Name && t.anio == dnc.anio && t.IdSubproyecto == dnc.IdSubproyecto);

                if (encontrado == null)//no existe
                {
                    dnc.usuarioCreador = User.Identity.Name;
                    db.Dncs.Add(dnc);
                    db.SaveChanges();

                }
                else
                {
                    dnc.Id = encontrado.Id;

                }

            }

            ViewBag.IdSubproyecto = new SelectList(db.SubProyectoes, "Id", "Nombre", dnc.IdSubproyecto);
            ViewBag.Dncs = db.Dncs.Include("SubProyecto").Where(x => x.anio == 2016 && x.usuarioCreador == User.Identity.Name).ToList();
            return RedirectToAction("Index", "CapacitacionTrabajadores", new { Area = "Rrhh", id = dnc.Id });
        }



        public JsonResult VerCursosExternos(string rut)
        {

            var cursos = db.HistoricoOtic.Where(x => x.RutTrabajador == rut).Select(x => new { x, x.Fecha.Year }).ToList();
            return Json(cursos, JsonRequestBehavior.AllowGet);
        }

        // GET: Rrhh/CapacitacionesDnc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dnc dnc = db.Dncs.Find(id);
            if (dnc == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdSubproyecto = new SelectList(db.SubProyectoes, "Id", "Nombre", dnc.IdSubproyecto);
            return View();
        }

        // POST: Rrhh/CapacitacionesDnc/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdSubproyecto,CreCodigo,usuarioCreador,anio,Cerrada")] Dnc dnc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dnc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdSubproyecto = new SelectList(db.SubProyectoes, "Id", "Nombre", dnc.IdSubproyecto);
            return View(dnc);
        }

        // GET: Rrhh/CapacitacionesDnc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dnc dnc = db.Dncs.Find(id);
            if (dnc == null)
            {
                return HttpNotFound();
            }
            return View(dnc);
        }

        // POST: Rrhh/CapacitacionesDnc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dnc dnc = db.Dncs.Find(id);
            db.Dncs.Remove(dnc);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult VerAvanceDNC()
        {
            if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" });
            return View();
        }

        [HttpPost]
        public JsonResult VerAvanceDnc(int anio=0)
        {
             
            DataTable tabla = new Models.Conectar()
             .EjecutarConsultaSelect("sp_rrhh_dnc_verDncCompletadas",
             CommandType.StoredProcedure,
             new System.Data.SqlClient.SqlParameter("anio", anio));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult VerEstadoDnc(int anio = 0)
        {

            var dnc = db.DncEstados.First(x => x.Año == anio);
            return Json(dnc , JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CambiarFechaCierre(int idCierre, DateTime fechacierre)
        {
            try
            {
                var r = db.DncEstados.FirstOrDefault(x => x.Id == idCierre);
                r.FechaCierre = fechacierre;
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" });
            }
            catch (Exception)
            {

                return Json(new { Resultado = "Error" });
            }
           

        }

        public  JsonResult VerPlanificacion()
        {
              DataTable tabla = new Models.Conectar()
             .EjecutarConsultaSelect("sp_rrhh_dnc_verPlanificacion",
             CommandType.StoredProcedure);
           return Json(new Models.Excel2().ExportarSp(tabla, Server.MapPath("~/ArchivosExcel/")));
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
