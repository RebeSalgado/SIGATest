using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardiSoft.Areas.Maquinaria.Controllers
{
    public class TipoFallaController : Controller
    {
        Models.GardiSoftContext db = new Models.GardiSoftContext();
        // GET: Maquinaria/TipoFalla
        public ActionResult Index()
        {
             if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" }); 
            return View();
        }


        //VER TIPOS DE FALLA

        public JsonResult VerFallas()
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_TiposDeFalla",
              CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        //VER TIPOS DE MAQUINA
        public JsonResult VerMaquinas()
        {
            DataTable tabla = new Models.Conectar()
                         .EjecutarConsultaSelect("sp_uma_mantenimiento_ListaTipoMaquina",
                         CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        //GUARDAR FALLA
        public JsonResult GuardarFalla(string Descripcion, int IdTipoMaquina)
        {
            try
            {
                Entidades.Uma.TiposDeFalla tf = new Entidades.Uma.TiposDeFalla();
                tf.DescripcionFalla = Descripcion;
                tf.IdTipoMaquina = IdTipoMaquina;
                db.TiposDeFalla.Add(tf);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" });

            }
            catch (Exception ex)
            {
                return Json(new { Resultado = "Error" });
            }
        }



        //ELIMINAR FALLA   
        public JsonResult EliminarFalla(int Id)
        {
            try
            {
                Entidades.Uma.TiposDeFalla tf = db.TiposDeFalla.First(x => x.Id == Id);
                db.TiposDeFalla.Remove(tf);
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