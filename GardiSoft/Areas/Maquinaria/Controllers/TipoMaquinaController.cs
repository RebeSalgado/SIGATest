using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardiSoft.Areas.Maquinaria.Controllers
{
    public class TipoMaquinaController : Controller
    {
        Models.GardiSoftContext db = new Models.GardiSoftContext();
        // GET: Maquinaria/TipoMaquina
        public ActionResult Index()
        {
             if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" }); 
            return View();
        }

        //VER TIPOS DE MAQUINA
        public JsonResult VerTipoMaquina()
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_TipoMaquina",
              CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        //GUARDAR TIPO MAQUINA
        public JsonResult GuardarTipoMaquina(string Nombre, int Componente)
        {
            try
            {
                Entidades.Uma.TipoMaquina m = new Entidades.Uma.TipoMaquina();
                m.Nombre = Nombre;
                m.EsComponente = (Componente == 0) ? false : true;
                db.TipoMaquinas.Add(m);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" });

            }
            catch (Exception ex)
            {
                return Json(new { Resultado = "Error" });
            }
        }


        //MODIFICAR TIPO MAQUINA
        public JsonResult ModificarMaquina(string Nombre, int Componente, int Id)
        {
            try
            {
                Entidades.Uma.TipoMaquina a = db.TipoMaquinas.First(x => x.Id == Id);
                a.Nombre = Nombre;
                a.EsComponente = (Componente == 0) ? false : true;
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);

            }

        }


        //ELIMINAR TIPO MAQUINA
        public JsonResult EliminarMaquina(int Id)
        {
            try
            {
                Entidades.Uma.TipoMaquina a = db.TipoMaquinas.First(x => x.Id == Id);
                db.TipoMaquinas.Remove(a);
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