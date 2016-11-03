using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardiSoft.Areas.Maquinaria.Controllers
{
    public class RepuestosOriginalesController : Controller
    {

        Models.GardiSoftContext db = new Models.GardiSoftContext();

        // GET: Maquinaria/RepuestosOriginales
        public ActionResult Index()
        {
             if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" }); 
            return View();
        }

        //GUARDA NUEVOS REPUESTOS ORIGINALES 
        public JsonResult GuardarRepuesto(string CodigoOriginal)
        {
            try
            {
                Entidades.Uma.RepuestosOriginales R = new Entidades.Uma.RepuestosOriginales();
                R.CodigoOriginal = CodigoOriginal;
                db.RepuestosOriginales.Add(R);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" });

            }
            catch (Exception ex)
            {
                return Json(new { Resultado = "Error" });
            }
        }
        
        //CARGA LOS REPUESTOS ORIGINALES  
        public JsonResult VerRepuestos()
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_RepuestosOriginales",
              CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        //MODIFICA REPUESTO ORIGINAL
        public JsonResult ModificarRepuesto(int Id, string CodigoOriginal)
        {
            try
            {
                Entidades.Uma.RepuestosOriginales a = db.RepuestosOriginales.First(x => x.Id == Id);
                a.CodigoOriginal = CodigoOriginal;
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);

            }

        }

        //ELIMINAR REPUESTO ORIGINAL
        public JsonResult EliminarRepuesto(int Id)
        {
            try
            {
                Entidades.Uma.RepuestosOriginales a = db.RepuestosOriginales.First(x => x.Id == Id);
                db.RepuestosOriginales.Remove(a);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);

            }

        }


        //ADMINISTRAR REPUESTOS ALTERNATIVOS 

        public ActionResult GestionarAlternativos(int Id = 0)
        {
            ViewBag.Id = Id;
            return View();
        }
        
        // VER REPUESTOS ALTERNATIVOS 
        public JsonResult VerRepuestosAlternativos(int idOriginal = 0)
        {
           DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_AdministrarRepuestoAlternativos",
              CommandType.StoredProcedure,
              new System.Data.SqlClient.SqlParameter("idOriginal", idOriginal));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        //AGREGAR NUEVO REPUESTO ALTERNATIVO
        public JsonResult AgregarRepuestoAlternativo(string CodigoAlternativo, int idOriginal)
        {
            try
            {
                Entidades.Uma.RepuestosAlternativos R = new Entidades.Uma.RepuestosAlternativos();
                R.CodigoAlternativo = CodigoAlternativo;
                R.IdOriginal = idOriginal;
                db.RepuestosAlternativos.Add(R);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" });

            }
            catch (Exception ex)
            {
                return Json(new { Resultado = "Error" });
            }
        }


        //ELIMINAR REPUESTO ALTERNATIVO 

        public JsonResult EliminarRepuestoAlternativo(int Id)
        {
            try
            {
                Entidades.Uma.RepuestosAlternativos a = db.RepuestosAlternativos.First(x => x.Id == Id);
                db.RepuestosAlternativos.Remove(a);
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