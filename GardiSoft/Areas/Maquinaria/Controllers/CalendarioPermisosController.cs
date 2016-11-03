using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using Newtonsoft;
using System.Data;
using Newtonsoft.Json;

namespace GardiSoft.Areas.Maquinaria
{
    public class CalendarioPermisosController : Controller
    {
        Models.GardiSoftContext db = new Models.GardiSoftContext();

        // GET: Maquinaria/CalendarioPermisos
        public ActionResult Index()
        {
             if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" }); 
            return View();
        }

        //VER USUARIOS CON PERMISOS
        public JsonResult VerUsuarios()
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_CalendarioOperacionesPermisos",
              CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        //AGREGAR NUEVO USUARIO
        public JsonResult AgregarUsuario(string Usuario)
        {
            try
            {
                Entidades.Uma.CalendarioOperacionesPermisos C = new Entidades.Uma.CalendarioOperacionesPermisos();
                C.Usuario = Usuario;
                C.GenerarOt = true;
                db.CalendarioOperacionesPermisos.Add(C);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" });

            }
            catch (Exception ex)
            {
                return Json(new { Resultado = "Error" });
            }
        }

        //ELIMINAR USUARIO
        public JsonResult EliminarUsuario(int Id)
        {
            try
            {
                Entidades.Uma.CalendarioOperacionesPermisos a = db.CalendarioOperacionesPermisos.First(x => x.Id == Id);
                db.CalendarioOperacionesPermisos.Remove(a);
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