using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardiSoft.Areas.Maquinaria.Controllers
{
    public class DiarioNovedadesController : Controller
    {
        // GET: Maquinaria/DiarioNovedades

        GardiSoft.Models.GardiSoftContext db = new Models.GardiSoftContext();

        public ActionResult Index()
        {
            if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" });
            ViewBag.IdUbicacion = new SelectList(db.Ubicacions.OrderBy(x => x.RutaCompleta).ToList(), "Id", "RutaCompleta");
            return View();
        }

        public JsonResult VerOtsDiario(string fechaInicio, DateTime  fechaTermino,string cerrada,int ubicacion)
        {
          var  fechaAjustada = new DateTime(fechaTermino.Year, fechaTermino.Month, fechaTermino.Day, 23, 59, 59);
            DataTable tabla = new Models.Conectar()
           .EjecutarConsultaSelect("sp_uma_mantenimiento_DiarioNovedades",
           CommandType.StoredProcedure,
           new System.Data.SqlClient.SqlParameter("cerrada", cerrada),
           new System.Data.SqlClient.SqlParameter("fechaInicio", fechaInicio),
           new System.Data.SqlClient.SqlParameter("fechaTermino", fechaAjustada),
           new System.Data.SqlClient.SqlParameter("ubicacion", ubicacion));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerOtsDiarioExcel(string fechaInicio, DateTime fechaTermino, string cerrada, int ubicacion )
        {
            var fechaAjustada = new DateTime(fechaTermino.Year, fechaTermino.Month, fechaTermino.Day, 23, 59, 59);
            DataTable tabla = new Models.Conectar()
           .EjecutarConsultaSelect("sp_uma_mantenimiento_DiarioNovedadesExcel",
           CommandType.StoredProcedure,
           //bnbnbnbn
           new System.Data.SqlClient.SqlParameter("fechaInicio", fechaInicio),
           new System.Data.SqlClient.SqlParameter("fechaTermino", fechaAjustada),
           new System.Data.SqlClient.SqlParameter("ubicacion", ubicacion));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerOtsPendientesActividades(int idOt)
        {
            DataTable tabla = new Models.Conectar()
         .EjecutarConsultaSelect("sp_uma_mantenimiento_verActividadesDiarioNovedades",
         CommandType.StoredProcedure,
         new System.Data.SqlClient.SqlParameter("idOt", idOt));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}