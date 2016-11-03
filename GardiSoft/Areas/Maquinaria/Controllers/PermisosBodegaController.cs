using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardiSoft.Areas.Maquinaria.Controllers
{
    public class PermisosBodegaController : Controller
    {
        Models.GardiSoftContext db = new Models.GardiSoftContext();
        // GET: Maquinaria/PermisosBodega
        public ActionResult Index()
        {
            List<string> roles = new List<string>();
            roles.Add("Mecanico");
            roles.Add("Bodega");

            ViewBag.Rol = new SelectList(roles);

            if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" }); 
            return View();
        }

        //GUARDAR PERMISO DE USUARIO Y BODEGA
        public JsonResult GuardarPermiso(string Usuario, int Bodega,string Rol)
        {
            try
            {
                Entidades.Uma.PermisosBodega p = new Entidades.Uma.PermisosBodega();
                p.Usuario = Usuario;
                p.Bodega = Bodega;
                p.Rol = Rol;
                db.PermisosBodega.Add(p);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" });

            }
            catch (Exception ex)
            {
                return Json(new { Resultado = "Error" });
            }
        }

        //VER PERMISOS USUARIOS BODEGA

        public JsonResult VerPermisos()
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimieto_PermisosBodegas",
              CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        //ELIMINAR PERMISO   
        public JsonResult EliminarPermiso(int Id)
        {
            try
            {
                Entidades.Uma.PermisosBodega a = db.PermisosBodega.First(x => x.Id == Id);
                db.PermisosBodega.Remove(a);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);

            }

        }

        //******************* VER BODEGAS ***********************


        public JsonResult VerBodegas()
        {
            DataTable tabla = new Models.Conectar()
                         .EjecutarConsultaSelect("sp_uma_mantenimiento_Bodegas",
                         CommandType.StoredProcedure);           
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        //******************* VER PERMISOS ***********************


        public JsonResult PermisoUsuario()
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_PermisosBodegaUsuario",
              CommandType.StoredProcedure, new System.Data.SqlClient.SqlParameter("usuario", User.Identity.Name));
            
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

    }
}
