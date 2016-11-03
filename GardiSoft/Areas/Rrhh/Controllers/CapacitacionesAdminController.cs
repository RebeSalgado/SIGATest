using GardiSoft.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardiSoft.Areas.Rrhh.Controllers
{
    [Authorize]
    public class CapacitacionesAdminController : Controller
    {
        private GardiSoftContext db = new GardiSoftContext();

        // GET: Rrhh/CapacitacionesAdmin
        public ActionResult Index()
        {
             if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" }); 
            ViewBag.periodo = new SelectList(db.Dncs.Select(t=> t.anio).Distinct());
           
            return View();
        }


        public ActionResult Buscar(int? periodo = 2016)
        {
             if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" }); 
           var dncs=  db.Dncs.Include("SubProyecto").Where(x => x.anio == periodo).ToList();
            return PartialView("_DncAdmin", dncs);
        }


        public ActionResult VerPresupuesto()
        {

           var a =  new Models.Conectar().QueryExcel("sp_rrhh_presupuestoDnc",Server.MapPath("~/ArchivosExcel/rrhh/"),"presupuestoDnc" ,null);
            return Json(new  { ruta = a },JsonRequestBehavior.AllowGet);
          

        }

        // GET: Rrhh/CapacitacionesAdmin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Rrhh/CapacitacionesAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rrhh/CapacitacionesAdmin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Rrhh/CapacitacionesAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Rrhh/CapacitacionesAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Rrhh/CapacitacionesAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Rrhh/CapacitacionesAdmin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
