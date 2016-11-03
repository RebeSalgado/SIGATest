using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using Newtonsoft.Json;
using System.IO;

namespace GardiSoft.Areas.Control.Controllers
{
    public class KpiController : Controller
    {

        Models.GardiSoftContext db = new Models.GardiSoftContext();
        // GET: Control/Kpi
        public ActionResult Index()
        {
            ViewBag.IdSubproyecto = new SelectList(db.SubProyectoes.Where(x => x.Visible == true).ToList(), "Id", "Nombre");
            ViewBag.IdIndicador = new SelectList(db.Indicadores.ToList(), "Id", "Descripcion");
            return View(new Entidades.Control.Kpi { Activo = true, FechaInicioMedicion = DateTime.Now });
        }

        public JsonResult GuardarKpi(Entidades.Control.Kpi kpi)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    kpi.CreadoPor = User.Identity.Name;
                    db.Kpi.Add(kpi);
                    db.SaveChanges();

                         DataTable tabla = new Models.Conectar()
                         .EjecutarConsultaSelect("sp_control_kpi_crearCalendarioKpi",
                         CommandType.StoredProcedure,
                         new System.Data.SqlClient.SqlParameter("id",kpi.Id));
                  

                    return Json(new { Resultado = "Guardado" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);
            }

        }



        public JsonResult VerKpis()
        {

            //uma_mantenimiento_calendarioPreventivo
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_ver_kpis",
              CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);

        }

        public JsonResult EliminarKpi(int id)
        {
            try
            {
                var eliminar = db.Kpi.First(x => x.Id == id);
                db.Kpi.Remove(eliminar);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);
            }

        }

    }
}