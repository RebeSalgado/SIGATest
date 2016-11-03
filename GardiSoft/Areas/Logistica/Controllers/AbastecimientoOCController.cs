using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardiSoft.Areas.Logistica.Controllers
{
    public class AbastecimientoOCController : Controller
    {
        // GET: Logistica/AbastecimientoOC
        public ActionResult Index()
        {

            

            if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" });
            return View();

        }

        [HttpPost]
        public JsonResult VerOc(string FechaInicio, string FechaTermino)
        {
            DataTable tabla = new Models.Conectar()
             .EjecutarConsultaSelect("sp_logi_OC_historicas",
             CommandType.StoredProcedure,
              new SqlParameter("fechai", FechaInicio),
              new SqlParameter("fechaf", FechaTermino));
            return Json(new { Ruta = new Models.Excel2().ExportarSp(tabla, Server.MapPath("~/ArchivosExcel/")) });
            //string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            //return Json(json, JsonRequestBehavior.AllowGet);
        }


        public ActionResult HistoricoPrecios()
        {
            if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" });
            return View();
        }

        public JsonResult BuscarProductos(string busqueda)
        {
            DataTable tabla = new Models.Conectar()
           .EjecutarConsultaSelect("sp_logistica_ver_productos",
           CommandType.StoredProcedure,
            new SqlParameter("busqueda", busqueda));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public JsonResult VerProductos(string busqueda)
        {
            DataTable tabla = new Models.Conectar()
           .EjecutarConsultaSelect("sp_logi_abastecimiento_verHistoricoOc",
           CommandType.StoredProcedure,
            new SqlParameter("busqueda", busqueda));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public JsonResult VerProveedores(int producto)
        {
            DataTable tabla = new Models.Conectar()
           .EjecutarConsultaSelect("sp_logi_abastecimientoProveedores",
           CommandType.StoredProcedure,
            new SqlParameter("productoId", producto));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerPrecios(int producto)
        {
            DataTable tabla = new Models.Conectar()
           .EjecutarConsultaSelect("sp_logi_abastecimientoHistoricoPrecios",
           CommandType.StoredProcedure,
            new SqlParameter("productoId", producto));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


    }
}