using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardiSoft.Areas.Maquinaria.Controllers
{
    public class OrigenFallaController : Controller
    {
        Models.GardiSoftContext db = new Models.GardiSoftContext();
        // GET: Maquinaria/OrigenFalla
        public ActionResult Index()
        {
             if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" }); 
            return View();
        }

        //GUARDAR FALLA
        public JsonResult GuardarFalla(string Descripcion)
        {
            try
            {
                Entidades.Uma.OrigenDeFalla of = new Entidades.Uma.OrigenDeFalla();
                of.Descripcion = Descripcion;
                db.OrigenDeFalla.Add(of);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" });

            }
            catch (Exception ex)
            {
                return Json(new { Resultado = "Error" });
            }
        }

        //VER FALLAS

        public JsonResult VerFallas()
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_OrigenFalla",
              CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        //ELIMINAR FALLA 
        public JsonResult EliminarFalla(int Id)
        {
            try
            {
                Entidades.Uma.OrigenDeFalla of = db.OrigenDeFalla.First(x => x.Id == Id);
                db.OrigenDeFalla.Remove(of);
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