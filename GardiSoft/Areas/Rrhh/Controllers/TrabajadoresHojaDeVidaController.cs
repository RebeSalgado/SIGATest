using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardiSoft.Areas.Rrhh.Controllers
{
    [Authorize]
    public class TrabajadoresHojaDeVidaController : Controller
    {

        private GardiSoft.Models.GardiSoftContext db = new Models.GardiSoftContext();

        // GET: Rrhh/TrabajadoresHojaDeVida
        public ActionResult Index(string rut)
        {
             if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" }); 
            ViewBag.rut = rut;
            return View();
        }


        public ActionResult VerHojaDeVidaResumen()
        {
             if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" }); 
            return View();
        }

        public JsonResult VerTrabajadoresHistorialDeCargos()
        {
            var tabla = new Models.Conectar().EjecutarConsultaSelect("sp_rrhh_fin700_hojaVidaTrabajadorResumen",
                System.Data.CommandType.StoredProcedure);
            var obj = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(obj, JsonRequestBehavior.AllowGet);

        }

        public JsonResult VerDatosTrabajador(string rut)
        {
            
            var trabajador = db.Trabajador.FirstOrDefault(x => x.Rut == rut);
            return Json(trabajador,JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerHistorialDeCargos(string rut)
        {
            var tabla = new Models.Conectar().EjecutarConsultaSelect("sp_rrhh_fin700_historial_cargos",
                System.Data.CommandType.StoredProcedure, new System.Data.SqlClient.SqlParameter("rut", rut));
           var obj = JsonConvert.SerializeObject(tabla, Formatting.Indented);
          
           return Json(obj, JsonRequestBehavior.AllowGet);
            
        }

        public JsonResult VerLicenciasMedicas(string rut)
        {
            var tabla = new Models.Conectar().EjecutarConsultaSelect("sp_rrhh_fin700_licencias_Medicas_rut",
                 System.Data.CommandType.StoredProcedure, new System.Data.SqlClient.SqlParameter("rut", rut));
            var obj = JsonConvert.SerializeObject(tabla, Formatting.Indented);

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerCursosExternos(string rut)
        {

            var cursos = db.HistoricoOtic.Where(x => x.RutTrabajador == rut).Select(x=> new { x,x.Fecha.Year }).ToList();
            return Json(cursos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerCursosInternos(string rut)
        {
             var tabla = new Models.Conectar().EjecutarConsultaSelect("sp_rrhh_dnc_Cursos_Interno",
                 System.Data.CommandType.StoredProcedure, new System.Data.SqlClient.SqlParameter("rut", rut));
            var obj = JsonConvert.SerializeObject(tabla, Formatting.Indented);

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerExcepciones(string rut)
        {
            var tabla = new Models.Conectar().EjecutarConsultaSelect("sp_rrhh_fin700_excepcionesTrabajador",
                System.Data.CommandType.StoredProcedure, new System.Data.SqlClient.SqlParameter("rut", rut));
            var obj = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        public JsonResult VerEDD(string rut)
        {

            var tabla = new Models.Conectar().EjecutarConsultaSelect("sp_rrhh_fin700_EDD",
                System.Data.CommandType.StoredProcedure, new System.Data.SqlClient.SqlParameter("rut", rut));
            var obj = JsonConvert.SerializeObject(tabla, Formatting.Indented);

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}