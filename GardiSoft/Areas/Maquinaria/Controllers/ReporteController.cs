using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardiSoft.Areas.Maquinaria.Controllers
{
    public class ReporteController : Controller
    {
        // GET: Maquinaria/Reporte
        GardiSoft.Models.GardiSoftContext db = new Models.GardiSoftContext();

        public ActionResult Index()
        {
            ViewBag.IdUbicacion = new SelectList(db.Ubicacions.ToList(), "Id", "Nombre");
            return View();
        }

        public JsonResult VerReporte(string fechaInicio, string fechaTermino, int IdUbicacion)
        {
            DataTable tabla = new Models.Conectar()
             .EjecutarConsultaSelect("sp_uma_mantenimiento_VerReporte",
              CommandType.StoredProcedure,
              new System.Data.SqlClient.SqlParameter("fechaInicio", fechaInicio),
              new System.Data.SqlClient.SqlParameter("fechaTermino", fechaTermino),
              new System.Data.SqlClient.SqlParameter("ubicacion", IdUbicacion));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DashBoardUma()
        {
            ViewBag.IdMaquina = new SelectList(db.Maquinas, "Id", "Codigo");
            return View();
        }

        //------------------- Grafico Cantidad por Tipo OT ----------------
        public JsonResult GraficoCantidadOtPorTipo(int crespid,string inicio,string termino)
        {
            DataTable tabla = new Models.Conectar()
           .EjecutarConsultaSelect("sp_uma_mantenimiento_grafico_CantidadOtPorTipo",
            CommandType.StoredProcedure
            , new System.Data.SqlClient.SqlParameter("crespid", crespid)
            , new System.Data.SqlClient.SqlParameter("inicio", inicio)
            , new System.Data.SqlClient.SqlParameter("termino", termino));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GraficoCantidadPorTiposDeFallaMaquinas()
        {
            DataTable tabla = new Models.Conectar()
           .EjecutarConsultaSelect("sp_uma_mantenimiento_reporteTipoFallasMaquinas",
            CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GraficoCantidadPorTiposDeFalla(int crespid, string inicio, string termino, int idTipo)
        {
            DataTable tabla = new Models.Conectar()
           .EjecutarConsultaSelect("sp_uma_mantenimiento_grafico_CantidadPorTiposDeFalla",
            CommandType.StoredProcedure,
            new System.Data.SqlClient.SqlParameter("crespid", crespid),
            new System.Data.SqlClient.SqlParameter("inicio", inicio),
            new System.Data.SqlClient.SqlParameter("termino", termino),
            new System.Data.SqlClient.SqlParameter("idTipo", idTipo));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GraficoCantidadTrabajoPendiente(int crespid, string inicio, string termino)
        {
            DataTable tabla = new Models.Conectar()
           .EjecutarConsultaSelect("sp_uma_mantenimiento_grafico_CantidadTrabajoPendiente",
            CommandType.StoredProcedure,
             new System.Data.SqlClient.SqlParameter("crespid", crespid),
            new System.Data.SqlClient.SqlParameter("inicio", inicio),
            new System.Data.SqlClient.SqlParameter("termino", termino)
           );
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GraficoCantidadRRHHPendientePreventivo(int crespid, string inicio, string termino)
        {
            DataTable tabla = new Models.Conectar()
           .EjecutarConsultaSelect("sp_uma_mantenimiento_grafico_CantidadRRHHPendientePreventivo",
            CommandType.StoredProcedure,
              new System.Data.SqlClient.SqlParameter("crespid", crespid),
            new System.Data.SqlClient.SqlParameter("inicio", inicio),
            new System.Data.SqlClient.SqlParameter("termino", termino));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GraficoCostoPorTipoMantenimiento()
        {
            DataTable tabla = new Models.Conectar()
           .EjecutarConsultaSelect("sp_uma_mantenimiento_grafico_costoPorTipoMantenimiento",
            CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GraficoConsumoPorMaquina(int idMaquina, string inicio, string termino)
        {
            DataTable tabla = new Models.Conectar()
           .EjecutarConsultaSelect("sp_uma_mantenimiento_reporteConsumoPorMaquina",
            CommandType.StoredProcedure,
             new System.Data.SqlClient.SqlParameter("idMaquina", idMaquina),
            new System.Data.SqlClient.SqlParameter("fechainicio", inicio),
            new System.Data.SqlClient.SqlParameter("fechatermino", termino));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

       

        public JsonResult CentrosResponsabilidad()
        {
            DataTable tabla = new Models.Conectar()
           .EjecutarConsultaSelect("sp_uma_mantenimiento_reporteCentrosResponsabilidad",
            CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

    }
}