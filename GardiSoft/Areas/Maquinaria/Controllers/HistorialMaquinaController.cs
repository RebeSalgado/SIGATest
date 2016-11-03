using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using Newtonsoft.Json;
using System.IO;

namespace GardiSoft.Areas.Maquinaria.Controllers
{
    public class HistorialMaquinaController : Controller
    {
        private GardiSoft.Models.GardiSoftContext db = new Models.GardiSoftContext();
        // GET: Maquinaria/HistorialMaquina
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult VerComponentesVigentes(string codMaquina )
        {
            DataTable tabla = new Models.Conectar()
          .EjecutarConsultaSelect("sp_uma_mantenimiento_VerComponentesVigentes",
        CommandType.StoredProcedure,
          new System.Data.SqlClient.SqlParameter("codMaquina", codMaquina));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);

        }


        public JsonResult VerComponentesAnteriores(string codMaquina)
        {
            DataTable tabla = new Models.Conectar()
          .EjecutarConsultaSelect("sp_uma_mantenimiento_VerComponentesAnteriores",
        CommandType.StoredProcedure,
          new System.Data.SqlClient.SqlParameter("codMaquina", codMaquina));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json( json, JsonRequestBehavior.AllowGet);

        }

    }
}