using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using System.Data;
using Newtonsoft.Json;

namespace GardiSoft.Areas.Maquinaria.Controllers
{
    public class UbicacionesController : Controller
    {
        // GET: Maquinaria/Ubicaciones
        GardiSoft.Models.GardiSoftContext db = new Models.GardiSoftContext();

        public ActionResult Index()
        {
             if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" }); 
            return View();
        }


        public JsonResult VerUbicaciones()
        {
            return Json(db.Ubicacions.OrderBy(x => x.RutaCompleta).ToList(), JsonRequestBehavior.AllowGet);
        } 


        public JsonResult Ubicaciones()
        {
             DataTable tabla = new Models.Conectar()
               .EjecutarConsultaSelect("sp_uma_mantenimiento_verUbicaciones",
               CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public JsonResult CargarCentros()
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_verCentros",
              CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AsociarCentroResponsabilidad(int id, int creId, string creNombre)
        {
            try
            {
                var ubicacion = db.Ubicacions.First(x => x.Id == id);
                ubicacion.creId = creId;
                ubicacion.creNombre = creNombre;
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" });
            }
            catch (Exception)
            {
                
                return Json(new { Resultado = "Error" });
            }
        }


        public JsonResult Guardar(Entidades.Uma.Ubicacion ubicacion)
        {
            try
            {
                db.Ubicacions.Add(ubicacion);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" }, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json(new { Resultado = "Error" });
            }
           
        }

        public JsonResult Eliminar(int id)
        {
            try
            {
               var ubi =  db.Ubicacions.First(x => x.Id == id);
                db.Ubicacions.Remove(ubi);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" },JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}