using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using Newtonsoft.Json;

namespace GardiSoft.Areas.Systema.Controllers
{
    public class SoporteController : Controller
    {

        GardiSoft.Models.GardiSoftContext db = new Models.GardiSoftContext();
        // GET: Systema/Soporte
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ControlDeVersion()
        {
            ViewBag.IdAplicacion = new SelectList(db.Aplicaciones.ToList(),"Id","Aplicacion");
            ViewBag.IdTipoSoporte = new SelectList(db.TipoSoporte.ToList(),"Id","Descripcion");
            return View(new Entidades.Sys.ControlDeCambios());
        }

        public JsonResult GuardarControlDeVersion(Entidades.Sys.ControlDeCambios cambio,string Version)
        {
            try
            {
                cambio.Version = decimal.Parse(Version.Replace('.', ','));
                //if (ModelState.IsValid)
                //{
                db.ControlDeCambios.Add(cambio);
                db.SaveChanges();
                //}
            }
            catch
            {
                return Json(new { Resultado = "Error" });
            } 

            return Json(new { Resultado = "Guardado" });

        }
        
        public PartialViewResult VerCambios()
        {
            return PartialView(db.ControlDeCambios.Include(x => x.TipoSoporte)
                .Include(x => x.Aplicacion)
                .ToList().OrderByDescending(x=> x.Fecha));
        }

        public JsonResult VerUltimaVersion(int idAplicacion)
        {
            decimal? version = db.ControlDeCambios.Where(x => x.IdAplicacion == idAplicacion)
                .DefaultIfEmpty()
                .Max(x => ((decimal?)x.Version??0));
            return Json(version);
        }

        public ActionResult VerNovedadesParaTi(int? id=0)
        {
            
            var cambio = db.ControlDeCambios.Include(x=> x.Aplicacion).FirstOrDefault(x => x.Id == id);
            if (cambio != null)
            {
                ViewBag.Version = cambio.Version;
                ViewBag.Software = cambio.Aplicacion.Aplicacion;
                ViewBag.Descripcion = cambio.CambioRealizado;
                if (System.IO.File.Exists(Server.MapPath("~/ArchivosUpload/soporte/" + id + ".PDF")))
                {
                    ViewBag.Pdf = id + ".pdf";
                }
                else
                {
                    ViewBag.Pdf = "#";
                }
            }
           
            return View();
        }


        public JsonResult VerCambiosRealizados()
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_sys_verCambios",
              CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }







    }
}