using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardiSoft.Areas.Control.Controllers
{
   
    public class IndicadoresController : Controller
    {
        Models.GardiSoftContext db = new Models.GardiSoftContext();
        // GET: Control/Indicadores
        public ActionResult Index()
        {
            return View();
        }


        // ************************************************** PLAN ******************************************************

        //GUARDAR KPI


         
        public JsonResult GuardarKPI(Entidades.Control.Indicadores indicador)
        {
            try
            {
               
                db.Indicadores.Add(indicador);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" });

            }
            catch (Exception ex)
            {
                return Json(new { Resultado = "Error" });
            }
        }

        //VER KPI

        public JsonResult CargarKPI()
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_control_kpi_VerKPI",
              CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }



        //VER UNIDADES

        public JsonResult CargarUnidades()
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_control_Kpi_VerUnidades",
              CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        //ELIMINAR INDICADORES 
        public JsonResult EliminarIndicador(int Id)
        {
            try
            {
                Entidades.Control.Indicadores I = db.Indicadores.First(x => x.Id == Id);
                db.Indicadores.Remove(I);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);

            }
        }


        // MODIFICAR INDICADORES
        public JsonResult ModificarIndicadores(Entidades.Control.Indicadores indicador)
        {
            try
            {
                Entidades.Control.Indicadores I = db.Indicadores.First(x => x.Id == indicador.Id);
                I.Descripcion = indicador.Descripcion;
                I.Unidad = indicador.Unidad;
                I.Nivel = indicador.Nivel;
                I.Tolerancia1 = indicador.Tolerancia1;
                I.Tolerancia2 = indicador.Tolerancia2;
                I.Estado = indicador.Estado;
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);

            }

        }



    }
}