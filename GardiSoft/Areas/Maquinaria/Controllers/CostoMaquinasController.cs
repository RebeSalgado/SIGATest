using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardiSoft.Areas.Maquinaria.Controllers
{
    public class CostoMaquinasController : Controller
    {
        // GET: Maquinaria/CostoMaquinas
        public ActionResult Index()
        {
            return View();
        }

       
        public JsonResult Calcularcostos(string codMaquina, DateTime fechaInicio, DateTime fechaTermino)
        {
            try
            {
                DataTable tabla = new Models.Conectar()
                .EjecutarConsultaSelect("ver_consumosMaquinaPorFecha",
                CommandType.StoredProcedure,
                new System.Data.SqlClient.SqlParameter("idMaquina", codMaquina),
                new System.Data.SqlClient.SqlParameter("fechaInicio", fechaInicio),
                new System.Data.SqlClient.SqlParameter("fechaTermino", fechaTermino));  
                string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Resultado = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}