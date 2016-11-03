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

namespace GardiSoft.Areas.Rrhh.Controllers
{
    [Authorize]
    public class CapacitacionesInternaController : Controller
    {
        private GardiSoftContext db = new GardiSoftContext();

        // GET: Rrhh/CapacitacionesInterna
        public ActionResult Index()
        {
            // var internas = db.Internas.Include(i => i.TipoInterno);
            if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" });
            ViewBag.idTipoEfc = new SelectList(db.TipoEfc, "Id", "Nombre");
            ViewBag.idTipoCurso = new SelectList(db.TipoCapacitacion, "id", "Nombre");
            ViewBag.IdSubproyecto = new SelectList(db.SubProyectoes.Where(x => x.Visible == true).ToList(), "id", "Nombre");
            new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag);
            return View();
        }


        public ActionResult IndexAdmin()
        {
            // var internas = db.Internas.Include(i => i.TipoInterno);
            if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" });
            ViewBag.idTipoEfc = new SelectList(db.TipoEfc, "Id", "Nombre");
            ViewBag.idTipoCurso = new SelectList(db.TipoCapacitacion, "id", "Nombre");
            ViewBag.IdSubproyecto = new SelectList(db.SubProyectoes.Where(x => x.Visible == true).ToList(), "id", "Nombre");
            new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag);
            return View();
        }



        public JsonResult AvisarARrhh(int id = 0)
        {

            var externa = db.Capacitacion.FirstOrDefault(x => x.Id == id);
            externa.Estado = "En Espera";
            new Conectar().EjecutarConsultaSelect("sp_rrhh_capacitaciones_InternaSolicitud", CommandType.StoredProcedure,
                new System.Data.SqlClient.SqlParameter("id", id));
            db.SaveChanges();

            return Json(new { Resultado = "guardado" });
        }

        public ActionResult AgregarTrabajadores(int id)
        {
            Session["idSolicitudInterna"] = id;
            ViewBag.Estado = db.Capacitacion.FirstOrDefault(x => x.Id == id).Estado;
            new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag);
            return View();
        }

        public ActionResult AgregarTrabajadoresAdmin(int id)
        {
            Session["idSolicitudInterna"] = id;
            ViewBag.Id = id;
            ViewBag.Estado = db.Capacitacion.FirstOrDefault(x => x.Id == id).Estado;
            new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag);
            return View();
        }

        public PartialViewResult TrabajadoresInscritos()
        {
            var id = int.Parse(Session["idSolicitudInterna"].ToString());
            ViewBag.Id = id;
            return PartialView("_TrabajadoresInscritos", db.CursoTrabajadores.Where(x => x.idCursoInterno == id).ToList());
        }

        public JsonResult GuardarAsistio(int id, bool asistio)
        {
            var trabajador = db.CursoTrabajadores.First(x => x.Id == id);
            trabajador.Realizada = asistio;
            db.SaveChanges();
            return Json(new { Resultado = "Guardado" });
        }

        public ActionResult AgregaTrabajador([Bind(Include = "rutTrabajador,realizada,NombreTrabajador")] CursoTrabajadores trabajador)
        {
            if (ModelState.IsValid)
            {
                trabajador.idCursoInterno = int.Parse(Session["idSolicitudInterna"].ToString());
               CursoTrabajadores c = db.CursoTrabajadores.FirstOrDefault(x => x.idCursoInterno == trabajador.idCursoInterno && x.RutTrabajador == trabajador.RutTrabajador);
                if (c == null)
                {
                    db.CursoTrabajadores.Add(trabajador);
                    db.SaveChanges();
                }
                return Json(new { Id = "agregado" });
            }
            else
            {
                return View();
            }

        }

        public ActionResult Solicitud()
        {
            var lista = db.Capacitacion.Include(t => t.TipoEfc).Where(x => x.Usuario == User.Identity.Name).ToList();
            return PartialView("_Solicitudes", lista);
        }

        public ActionResult SolicitudAdmin()
        {
            var lista = db.Capacitacion.Include(t => t.TipoEfc).ToList();
            return PartialView("_SolicitudAdmin", lista);
        }

        public JsonResult ValidarCapacitacion(int id = 0)
        {

            var capacitacion = db.Capacitacion.FirstOrDefault(x => x.Id == id);
            capacitacion.Estado = "Aprobada";
            db.SaveChanges();
            return Json(new { Resultado = "Guardado" });

        }

        [HttpPost]
       
        public JsonResult GuardarListaAsistencia(int id=0)
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    
                    var path = Server.MapPath("~/ArchivosUpload/rrhh/CapacitacionesInterna/a.jpg");
                    file.SaveAs(path);
                    var interna = db.Capacitacion.FirstOrDefault(x => x.Id == id);
                    interna.Estado = "Realizada";
                    db.SaveChanges();
                }
            }

            return Json(new { Resultado = "Aguardado" });
        }


        public ActionResult VerR21()
        {
            ViewBag.idSubproyecto = new SelectList(db.SubProyectoes.ToList(),"Id","Nombre");
            return View();
        }

        [HttpPost]
        public JsonResult GenerarR21(int idSubproyecto, string FechaInicio, string FechaTermino)
        {
             DataTable tabla = new Models.Conectar()
               .EjecutarConsultaSelect("sp_rrhh_capacitacionesR21",
               CommandType.StoredProcedure,
               new System.Data.SqlClient.SqlParameter("IdSubproyecto", idSubproyecto),
               new System.Data.SqlClient.SqlParameter("Inicio", FechaInicio),
               new System.Data.SqlClient.SqlParameter("Termino", FechaTermino)               );
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        // GET: Rrhh/CapacitacionesInterna/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Capacitacion interna = db.Capacitacion.Find(id);
            if (interna == null)
            {
                return HttpNotFound();
            }
            return View(interna);
        }

        // GET: Rrhh/CapacitacionesInterna/Create
        public ActionResult Create()
        {
            ViewBag.IdTipoefc = new SelectList(db.TipoEfc, "Id", "Nombre");
            ViewBag.IdTipoCursoInterno = new SelectList(db.TipoEfc, "Id", "Nombre");
            return View();
        }

        // POST: Rrhh/CapacitacionesInterna/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Capacitacion interna)
        {
            if (ModelState.IsValid)
            {
                interna.Usuario = User.Identity.Name;
                interna.Estado = "Ingresado";
                db.Capacitacion.Add(interna);
                db.SaveChanges();

                return Json(new { Id = "agregado" });
            }

            ViewBag.IdTipoCursoInterno = new SelectList(db.TipoEfc, "Id", "Nombre", interna.idTipoEFC);
            return View(interna);
        }

        // GET: Rrhh/CapacitacionesInterna/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Capacitacion interna = db.Capacitacion.Find(id);
            if (interna == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdTipoCursoInterno = new SelectList(db.TipoEfc, "Id", "Nombre", interna.idTipoEFC);
            return View(interna);
        }

        // POST: Rrhh/CapacitacionesInterna/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CodSense,NombreOtec,IdTipoCursoInterno,ValorCurso,CantidadHoras,FechaInicio,fechaTermino,ContactoOtec,MailContacto")] Capacitacion interna)
        {
            if (ModelState.IsValid)
            {
                db.Entry(interna).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdTipoCursoInterno = new SelectList(db.TipoEfc, "Id", "Nombre", interna.idTipoEFC);
            return View(interna);
        }

        // GET: Rrhh/CapacitacionesInterna/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Capacitacion interna = db.Capacitacion.Find(id);
            if (interna == null)
            {
                return HttpNotFound();
            }
            return View(interna);
        }

        // POST: Rrhh/CapacitacionesInterna/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Capacitacion interna = db.Capacitacion.Find(id);
            db.Capacitacion.Remove(interna);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        public ActionResult VerBuscarCapacitacion()
        {
            var meses = new GardiSoft.Models.Helper.HtmlHelper().Meses();
            ViewBag.Meses = new SelectList(meses, "Valor", "Nombre", meses.First());
            var años = new GardiSoft.Models.Helper.HtmlHelper().Año();
            ViewBag.Año = new SelectList(años);
            return View();
        }
        [AllowAnonymous]
        public ActionResult BuscarCapacitacion(int meses, int año)
        {

            var capacitaciones = db.Capacitacion.Where(x => x.FechaInicio.Year == año && x.FechaInicio.Month == meses).Select(x => x).ToList();
            return PartialView("_BuscarCapacitacion", capacitaciones);
        }
        [AllowAnonymous]
        public ActionResult VerLista(int id)
        {
            var asistentes = db.CursoTrabajadores.Where(x => x.idCursoInterno == id).ToList();
            return View("VerLista", asistentes);


        }
        [AllowAnonymous]
        public ActionResult Presente(int id)
        {
            var participante = db.CursoTrabajadores.FirstOrDefault(x => x.Id == id);
            participante.Realizada = !participante.Realizada;
            db.SaveChanges();
            return Json(new { Resultado = "actualizado" }, JsonRequestBehavior.AllowGet);
        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        // CARGAR LAS CAPACITACIONES MEDIANTE SP
        public JsonResult VerCapacitaciones()
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_rrhh_VerCapactiacionesInternas",
              CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}
