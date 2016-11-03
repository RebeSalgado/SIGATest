using Entidades.Uma;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardiSoft.Areas.Maquinaria.Controllers
{
    public class AdministrarRepuestosController : Controller
    {

        Models.GardiSoftContext db = new Models.GardiSoftContext();
        // GET: Maquinaria/AdministrarRepuestos
        public ActionResult Index()
        {
             if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" }); 
            return View();
        }

        //VER REPUESTOS ACTIVIDADES 
        public JsonResult VerRepuestosActividades()
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_AdministrarRepuestosActividades",
              CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        //VER ACTIVIDADES 

        public JsonResult VerActividades()
        {
            DataTable tabla = new Models.Conectar()
           .EjecutarConsultaSelect("sp_uma_mantenimiento_VerActividadRepuesto",
           CommandType.StoredProcedure);
           //new System.Data.SqlClient.SqlParameter("idacividad", idactividad));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        // VER REPUESTOS 

        public JsonResult VerProductos(string busqueda)
        {
            DataTable tabla = new Models.Conectar()
           .EjecutarConsultaSelect("sp_uma_mantenimiento_SelecionarRepuestosActividades",
           CommandType.StoredProcedure,
            new SqlParameter("busqueda", busqueda));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        


       public JsonResult AsociarRepuestoActividad(int idActividad = 0, int idRecurso = 0, int Cantidad = 0)
        {
            try
            {
                Entidades.Uma.RepuestosActividadesMaster p = new Entidades.Uma.RepuestosActividadesMaster();
                p.idActividad = idActividad;
                p.idRecurso = idRecurso;
                p.Cantidad = Cantidad;
                db.RepuestosActividadesMaster.Add(p);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" });

            }
            catch (Exception ex)
            {
                return Json(new { Resultado = "Error" });
            }
        }

        public JsonResult AsociarRepuestoActividadAux(int idActividad = 0, int idRecurso = 0, int Cantidad = 0)
        {
            try
            {
                Entidades.Uma.RepuestosActividades p = new Entidades.Uma.RepuestosActividades();
                p.idActividad = idActividad;
                p.idRecurso = idRecurso;
                p.Cantidad = Cantidad;
                db.RepuestosActividades.Add(p);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" });

            }
            catch (Exception ex)
            {
                return Json(new { Resultado = "Error" });
            }
        }

    }
}