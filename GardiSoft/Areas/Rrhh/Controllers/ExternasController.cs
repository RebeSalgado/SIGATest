using System;
using System.Collections;
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
    public class ExternasController : Controller
    {


        private GardiSoftContext db = new GardiSoftContext();

        public IEnumerable Comuna { get; private set; }

        // GET: Rrhh/Externas
        public ActionResult Index()
        {
            var externas = db.Externas.Include(e => e.Categoria).Include(e => e.SubProyecto);
            ViewBag.IdSubproyecto = new SelectList(db.SubProyectoes.Where(x => x.Visible == true).ToList(), "Id", "Nombre");
            ViewBag.IdCategoria = new SelectList(db.Categorias.ToList(), "Id", "Nombre");
            new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag);
            return View();
        }

        
        public PartialViewResult VerListaCapacitaciones() {

            var lista = db.Externas.Where(x => x.Usuario == User.Identity.Name)
                .OrderByDescending(x=> x.FechaInicio)
                .Include(x=> x.Categoria)
                .Include(x=> x.SubProyecto)                
                .Select(x => x).ToList();
            return PartialView("_VerListaCapacitaciones",lista);

        }


        public ActionResult AgregarTrabajador(int Id = 0)
        {
            var sexo = new[] { new { Id = "F", Valor = "Femenino" }, new { Id = "M", Valor = "Masculino" } };
            ViewBag.Sexo = new SelectList(sexo, "Id", "Valor");
            ViewBag.IdComuna = new SelectList(db.Comunas.ToList(), "Id", "Comuna");
            ViewBag.IdEscolaridad = new SelectList(db.Escolaridad.ToList(), "Id", "Descripcion");
            ViewBag.IdNivel = new SelectList(db.Nivel.ToList(), "Id", "Nombre");
            ViewBag.IdCapacitacionExterna = Id;
            ViewBag.Estado = db.Externas.FirstOrDefault(x => x.Id == Id).Estado;
            new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag);
            return View();
        }

        public JsonResult AvisarARrhh(int id =0)
        {

            var externa = db.Externas.FirstOrDefault(x => x.Id == id);
            externa.Estado = "En Espera";
            new Conectar().EjecutarConsultaSelect("sp_rrhh_capacitaciones_ExternaSolicitud", CommandType.StoredProcedure,
                new System.Data.SqlClient.SqlParameter("id", id));
            db.SaveChanges();

            return Json(new { Resultado = "guardado" });
        }

        public JsonResult AvisarARrhhAprobacion(int id = 0)
        {
            var externa = db.Externas.FirstOrDefault(x => x.Id == id);
            externa.Estado = "Aprobada";
            new Conectar().EjecutarConsultaSelect("sp_rrhh_capacitaciones_Externaaprobacion", CommandType.StoredProcedure,
                new System.Data.SqlClient.SqlParameter("id", id));
            db.SaveChanges();

            return Json(new { Resultado = "guardado" });
        }

        public ActionResult IndexAdmin()
        {
            var externas = db.Externas.Include(e => e.Categoria).Include(e => e.SubProyecto).ToList();
            ViewBag.IdSubproyecto = new SelectList(db.SubProyectoes.Where(x => x.Visible == true).ToList(), "Id", "Nombre");
            ViewBag.IdCategoria = new SelectList(db.Categorias.ToList(), "Id", "Nombre");
            new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag);
            return View(externas);
        }

        public ActionResult VerTrabajadoresAdmin(int id=0)
        {
            ViewBag.Id = id;
            var externa = db.Externas.Include(e => e.Categoria).Include(e => e.SubProyecto).FirstOrDefault(x => x.Id == id);
            new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag);
            ViewBag.IdSubproyecto = new SelectList(db.SubProyectoes.Where(x => x.Visible == true).ToList(), "Id", "Nombre",externa.IdSubproyecto);
            ViewBag.IdCategoria = new SelectList(db.Categorias.ToList(), "Id", "Nombre",externa.IdCategoria);
            return View(externa);
            
        }

        public JsonResult VerTrabajadoresAdminFormatoOtic(int id = 0)
        {            
            DataTable tabla = new Models.Conectar()
                .EjecutarConsultaSelect("sp_rrhh_capacitaciones_externaFormatoOtic",
                CommandType.StoredProcedure,
                new System.Data.SqlClient.SqlParameter("idExterna", id));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


      

        public JsonResult GuardarTrabajador(ExternaTrabajadores Trabajadores)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.ExternaTrabajadores.Add(Trabajadores);
                    db.SaveChanges();
                }

                return Json(new { Resultado = "Guardado" });
            }
            catch (Exception ex)
            {
                return Json(new { Resultado = "error" });
            }

        }

        public PartialViewResult VerTrabajadores(int id= 0)
        {
            var trabajadores = db.ExternaTrabajadores.Where(x => x.IdCapacitacionExterna == id)
                .OrderBy(x => x.NombreTrabajador)
                .Include(x => x.Comunas)
                .Include(x => x.Escolaridad)
                .Include(x => x.Nivel)
                .ToList();
            return PartialView("_VerTrabajadores", trabajadores);
        }

        //public ActionResult VerTrabajadoresAdmin(int id = 0)
        //{
        //    var trabajadores = db.ExternaTrabajadores.Where(x => x.IdCapacitacionExterna == id)
        //        .OrderBy(x => x.NombreTrabajador)
        //        .Include(x => x.Comunas)
        //        .Include(x => x.Escolaridad)
        //        .Include(x => x.Nivel)
        //        .ToList();
        //    return PartialView(trabajadores);
        //}

        // GET: Rrhh/Externas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Externa externa = db.Externas.Find(id);
            if (externa == null)
            {
                return HttpNotFound();
            }
            return View(externa);
        }

        // GET: Rrhh/Externas/Create
        public ActionResult Create()
        {
            ViewBag.IdCategoria = new SelectList(db.Categorias, "Id", "Nombre");
            ViewBag.IdSubproyecto = new SelectList(db.SubProyectoes, "Id", "Nombre");
            return View();
        }

        // POST: Rrhh/Externas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create([Bind(Include = "Id,IdSubproyecto,Otec,NombreCurso,CantidadHoras,FechaInicio,IdCategoria,ContactoOtec,Usuario,Estado")] Externa externa)
        {
            if (ModelState.IsValid)
            {
                externa.Estado = "Ingresado";
                externa.Usuario = User.Identity.Name;
                db.Externas.Add(externa);
                db.SaveChanges();
               // return Json(new { Resultado = "Guardado" });
            }

            ViewBag.IdCategoria = new SelectList(db.Categorias, "Id", "Nombre", externa.IdCategoria);
            ViewBag.IdSubproyecto = new SelectList(db.SubProyectoes, "Id", "Nombre", externa.IdSubproyecto);
            return Json(new { Resultado = "Guardado" });
        }


        

        // GET: Rrhh/Externas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Externa externa = db.Externas.Find(id);
            if (externa == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCategoria = new SelectList(db.Categorias, "Id", "Nombre", externa.IdCategoria);
            ViewBag.IdSubproyecto = new SelectList(db.SubProyectoes, "Id", "Nombre", externa.IdSubproyecto);
            return View(externa);
        }

        // POST: Rrhh/Externas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdSubproyecto,Otec,NombreCurso,CantidadHoras,FechaInicio,IdCategoria,ContactoOtec")] Externa externa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(externa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCategoria = new SelectList(db.Categorias, "Id", "Nombre", externa.IdCategoria);
            ViewBag.IdSubproyecto = new SelectList(db.SubProyectoes, "Id", "Nombre", externa.IdSubproyecto);
            return View(externa);
        }

        // GET: Rrhh/Externas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Externa externa = db.Externas.Find(id);
            if (externa == null)
            {
                return HttpNotFound();
            }
            return View(externa);
        }

        // POST: Rrhh/Externas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Externa externa = db.Externas.Find(id);
            db.Externas.Remove(externa);
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
