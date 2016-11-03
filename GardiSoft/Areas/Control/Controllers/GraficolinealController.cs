using GardiSoft.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardiSoft.Areas.Control.Controllers
{
    public class GraficolinealController : Controller
    {
        private GardiSoftContext db = new GardiSoftContext();
        // GET: Control/Graficolineal
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult MetaxReal(string IdIndicador)
        {
           
              DataTable tabla = new Models.Conectar()
             .EjecutarConsultaSelect("sp_GestionIndicadores_GraficoSeleccionaMetaxReal",
              CommandType.StoredProcedure,
              new System.Data.SqlClient.SqlParameter("IdIndicador", IdIndicador));

            var json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TraeIndicador()
        {

            /*Conexion con el precedure, trae los datos de Indicador, para llenar el combo*/
            DataTable tabla = new Models.Conectar()
            .EjecutarConsultaSelect("sp_GestionIndicadores_TraeIndicador",
            CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public JsonResult TraeFechar()
        {

            /*Conexion con el precedure, trae los datos de Fecha, para llenar el combo*/
            DataTable tabla = new Models.Conectar()
            .EjecutarConsultaSelect("sp_GestionIndicadores_GraficoFecha",
            CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}