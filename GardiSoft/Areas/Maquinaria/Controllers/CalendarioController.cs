using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardiSoft.Areas.Maquinaria.Controllers
{
    [Authorize]
    public class CalendarioController : Controller
    {
        GardiSoft.Models.GardiSoftContext db = new Models.GardiSoftContext();
        // GET: Maquinaria/Calendario
        public ActionResult Index()
        {
            if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" }); 
            ViewBag.IdUbicacion = new SelectList(db.Ubicacions.ToList(), "Id", "Nombre");
            return View();
        }

        [HttpPost]
        public JsonResult CalendarioMantenimiento(int IdUbicacion)
        {
            //uma_mantenimiento_calendarioPreventivo
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("uma_mantenimiento_calendarioPreventivo",
              CommandType.StoredProcedure,
              new System.Data.SqlClient.SqlParameter("IdUbicacion", IdUbicacion));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GenerarOtPropuesta(int idMaquina, int Frecuencia,string fecha,int idcconsumo, int idcresp,string cconsumo,string cresp,int idPadre=0)
        {
            try
            {
               DataTable tabla = new Models.Conectar()
               .EjecutarConsultaSelect("sp_uma_mantenimiento_GenerarOt",
               CommandType.StoredProcedure,
               new System.Data.SqlClient.SqlParameter("idMaquina", idMaquina),
               new System.Data.SqlClient.SqlParameter("Frecuencia", Frecuencia),
               new System.Data.SqlClient.SqlParameter("Usuario", User.Identity.Name),
               new System.Data.SqlClient.SqlParameter("fechaInicio", fecha),
               new System.Data.SqlClient.SqlParameter("idcconsumo", idcconsumo),
               new System.Data.SqlClient.SqlParameter("idcresp", idcresp),
                new System.Data.SqlClient.SqlParameter("cconsumo", cconsumo),
               new System.Data.SqlClient.SqlParameter("cresp", cresp),
               new System.Data.SqlClient.SqlParameter("idPadre",idPadre));


                string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Resultado = 0 }, JsonRequestBehavior.AllowGet);
            }           
        }
        
        [HttpPost]
        public JsonResult EliminarOtPropuesta(int idMaquina, int Frecuencia)
        {
            try
            {
                DataTable tabla = new Models.Conectar()
                .EjecutarConsultaSelect("sp_uma_mantenimiento_cancelarOtPropuesta",
                CommandType.StoredProcedure,
                new System.Data.SqlClient.SqlParameter("idMaquina", idMaquina),
                new System.Data.SqlClient.SqlParameter("Frecuencia", Frecuencia)
                
                );
                string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Resultado = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
        
        [HttpPost]
        public JsonResult ConfirmarOtPropuesta(int idMaquina, int Frecuencia,string fecha, int idcconsumo, int idcresp, string cconsumo, string cresp)
        {
            try
            {
                DataTable tabla = new Models.Conectar()
                .EjecutarConsultaSelect("sp_uma_mantenimiento_ConfirmarPropuesta",
                CommandType.StoredProcedure,
                new System.Data.SqlClient.SqlParameter("idMaquina", idMaquina),
                new System.Data.SqlClient.SqlParameter("Frecuencia", Frecuencia),
                new System.Data.SqlClient.SqlParameter("fecha", fecha));
               
                return Json(new{Resultado = "Guardado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Resultado = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CalendarioOperaciones()
        {
             if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" });
            var nombre = User.Identity.Name;
            var permisos = db.CalendarioOperacionesPermisos.FirstOrDefault(x => x.Usuario == nombre);
            ViewBag.IdUbicacion = new SelectList(db.Ubicacions.ToList(), "Id", "Nombre");
            if (permisos != null)
            {
                ViewBag.permiso = "si";
            }
           
            return View();

        }

        public JsonResult EventosCalendarioOperaciones(int idUbicacion = 0)
        {
             DataTable tabla = new Models.Conectar()
               .EjecutarConsultaSelect("uma_mantenimiento_ver_calendarioOperaciones",
               CommandType.StoredProcedure
               ,new System.Data.SqlClient.SqlParameter("idUbicacion", idUbicacion));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EsSiguiente(DateTime fecha,int idMaquina)
        {            
                DataTable tabla = new Models.Conectar()
               .EjecutarConsultaSelect("sp_uma_mantenimiento_comprobarSiguienteMantenimiento",
               CommandType.StoredProcedure
               , new System.Data.SqlClient.SqlParameter("idMaquina", idMaquina),
                new System.Data.SqlClient.SqlParameter("fecha", fecha));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);

        }



    }
}