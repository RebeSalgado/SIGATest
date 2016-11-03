using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardiSoft.Areas.Control.Controllers
{

    public class ControlTiempoInteriorMinaController : Controller
    {
        Models.GardiSoftContext db = new Models.GardiSoftContext();
        // GET: Control/ControlTiempoInteriorMina
        public ActionResult Index()
        {

            return View();
        }

        public JsonResult CargarCheck(DateTime Fecha ,int IdNivel = 0, int IdArea = 0, int IdTurno = 0)
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_Control_Kpi_IngresarValoresCheckProcesos",
              CommandType.StoredProcedure,
              new System.Data.SqlClient.SqlParameter("Fecha", Fecha),
              new System.Data.SqlClient.SqlParameter("idNivel", IdNivel),
              new System.Data.SqlClient.SqlParameter("IdArea", IdArea),
              new System.Data.SqlClient.SqlParameter("IdTurno", IdTurno));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerNivel()
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_control_verNivel",
              CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerArea()
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_control_verArea",
              CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public JsonResult VerTurno()
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_control_verTurno",
              CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GuardarValorKpi(Entidades.Control.Valores v)
        {
            var r = db.Valores.FirstOrDefault(x => x.IdControl == v.IdControl && x.FechaIngreso == v.FechaIngreso);
            if (r == null)

            {
                try
                {
                    //v.IngresadoPor = User.Identity.Name; -- 
                    v.IngresadoPor = "tu";
                    db.Valores.Add(v);
                    db.SaveChanges();
                    return Json(new { Resultado = "Guardado" });

                }
                catch (Exception ex)
                {
                    return Json(new { Resultado = "Error" });
                }

                
               
            }

            else
            {
                r.Valor = v.Valor;
                r.Observacion = v.Observacion;
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" });
            }
        }


        public ActionResult MantenedorActividades()
        {

            return View();
        }



    }
}