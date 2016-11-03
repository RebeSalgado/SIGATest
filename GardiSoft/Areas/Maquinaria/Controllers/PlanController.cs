using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using Newtonsoft;
using System.Data;
using Newtonsoft.Json;

namespace GardiSoft.Areas.Maquinaria.Controllers
{
    
    public class PlanController : Controller
    {

        Models.GardiSoftContext db = new Models.GardiSoftContext();

        // GET: Maquinaria/Plan
        public ActionResult Index()
        {
             if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" }); 
            return View("IndexHistorico");
        }

        // ************************************************** PLAN ******************************************************

        //GUARDAR PLAN
        public JsonResult GuardarPlan(string nombrePlan)
        {
            try
            {
                Entidades.Uma.Plan p = new Entidades.Uma.Plan();
                p.Nombre = nombrePlan;
                db.Plans.Add(p);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" });

            }
            catch (Exception ex)
            {
                return Json(new { Resultado = "Error" });
            }
        }

        //VER PLAN

        public JsonResult VerPlanes()
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_VerPlanes",
              CommandType.StoredProcedure );
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        // MODIFICAR PLAN
        public JsonResult ModificarPlan(int Id , string Nombre)
        {
            try
            {
                Entidades.Uma.Plan a = db.Plans.First(x => x.Id == Id);
                a.Nombre = Nombre;
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);

            }

        }

        //ELIMINAR PLAN 
        public JsonResult EliminarPlan(int Id)
        {
            try
            {
                Entidades.Uma.Plan a = db.Plans.First(x => x.Id == Id);
                db.Plans.Remove(a);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);

            }

        }

        // ***************************** PLAN PARTES  *****************************************

        //ADMINISTRAR PLAN PARTES

        public ActionResult PlanPartes(int Id = 0)
        {

             if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" }); 
            ViewBag.Id = Id;
            ViewBag.Plan = db.Plans.First(x => x.Id == Id).Nombre;
            return View();
        }

        // VER PLAN PARTES 
        public JsonResult VerPlanPartes(int idPlan = 0)
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_VerPlanPartes",
              CommandType.StoredProcedure,
            new System.Data.SqlClient.SqlParameter("idPlan", idPlan));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        //GUARDAR PLAN PARTES 
        public JsonResult GuardarPlanPartes(string nombrePlan, int idPlan = 0)
        {
            try
            {
                Entidades.Uma.PlanPartes p = new Entidades.Uma.PlanPartes();
                p.Nombre = nombrePlan;
                p.idPlan = idPlan;
                db.PlanPartes.Add(p);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" });

            }
            catch (Exception ex)
            {
                return Json(new { Resultado = "Error" });
            }
        }

        // ELIMINAR PLAN PARTES 
        public JsonResult EliminarPlanPartes(int id)
        {
            try
            {
                Entidades.Uma.PlanPartes a = db.PlanPartes.First(x => x.id == id);
                db.PlanPartes.Remove(a);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);

            }

        }





        //********************************** Plan Master ********************************************************

        // GET: Maquinaria/Plan
        public ActionResult IndexMaster()
        {
          //  if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" });
            return View();
        }

        // ************************************************** PLAN ******************************************************

       
        public JsonResult GuardarPlanMaster(string nombrePlan)
        {
            try
            {
                Entidades.Uma.PlanMaster p = new Entidades.Uma.PlanMaster();
                p.Nombre = nombrePlan;
                db.PlanMaster.Add(p);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" });

            }
            catch (Exception ex)
            {
                return Json(new { Resultado = "Error" });
            }
        }

      

        public JsonResult VerPlanesMaster()
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_VerPlanesMaster",
              CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult ModificarPlanMaster(int Id, string Nombre)
        {
            try
            {
                Entidades.Uma.PlanMaster a = db.PlanMaster.First(x => x.Id == Id);
                a.Nombre = Nombre;
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);

            }

        }
       
        public JsonResult EliminarPlanMaster(int Id)
        {
            try
            {
                Entidades.Uma.PlanMaster a = db.PlanMaster.First(x => x.Id == Id);
                db.PlanMaster.Remove(a);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);

            }

        }

        public ActionResult PlanPartesMaster(int Id = 0)
        {

            // if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" });
            ViewBag.Id = Id;
            ViewBag.Plan = db.PlanMaster.First(x => x.Id == Id).Nombre;
            return View();
        }

        public JsonResult VerPlanPartesMaster(int idPlan = 0)
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_VerPlanPartesMaster",
              CommandType.StoredProcedure,
            new System.Data.SqlClient.SqlParameter("idPlan", idPlan));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }



        public JsonResult VerPartesActividad(int idParte)
        {
            DataTable tabla = new Models.Conectar()
             .EjecutarConsultaSelect("sp_uma_mantenimiento_VerActividadesPartesMaster",
             CommandType.StoredProcedure,
           new System.Data.SqlClient.SqlParameter("idparte", idParte));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerRepuestosPorActividadMaster(int idActividad)
        {
            DataTable tabla = new Models.Conectar()
             .EjecutarConsultaSelect("sp_uma_mantenimiento_VerRepuestosPorActividadMaster",
             CommandType.StoredProcedure,
           new System.Data.SqlClient.SqlParameter("idActividad", idActividad));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        //GUARDAR PLAN PARTES 
        public JsonResult GuardarPlanPartesMaster(string nombrePlan, int idPlan = 0)
        {
            try
            {
                Entidades.Uma.PlanPartesMaster p = new Entidades.Uma.PlanPartesMaster();
                p.Nombre = nombrePlan;
                p.idPlan = idPlan;
                db.PlanPartesMaster.Add(p);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" });

            }
            catch (Exception ex)
            {
                return Json(new { Resultado = "Error" });
            }
        }
        // ELIMINAR PLAN PARTES 
        public JsonResult EliminarPlanPartesMaster(int id)
        {
            try
            {
                Entidades.Uma.PlanPartesMaster a = db.PlanPartesMaster.First(x => x.id == id);
                db.PlanPartesMaster.Remove(a);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);

            }

        }






        //********************************** Plan Master ********************************************************

        // GET: Maquinaria/Plan
        public ActionResult IndexHistorico()
        {
            //  if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" });
            return View();
        }

        // ************************************************** PLAN ******************************************************


        public JsonResult GuardarPlanAux(string nombrePlan)
        {
            try
            {
                Entidades.Uma.Plan p = new Entidades.Uma.Plan();
                p.Nombre = nombrePlan;
                db.Plans.Add(p);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" });

            }
            catch (Exception ex)
            {
                return Json(new { Resultado = "Error" });
            }
        }



        public JsonResult VerPlanesAux()
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_VerPlanesaux",
              CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModificarPlanAux(int Id, string Nombre)
        {
            try
            {
                Entidades.Uma.Plan a = db.Plans.First(x => x.Id == Id);
                a.Nombre = Nombre;
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);

            }

        }

        public JsonResult EliminarPlanAux(int Id)
        {
            try
            {
                Entidades.Uma.Plan a = db.Plans.First(x => x.Id == Id);
                db.Plans.Remove(a);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);

            }

        }

        public ActionResult PlanPartesAux(int Id = 0)
        {

            // if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" });
            ViewBag.Id = Id;
            ViewBag.Plan = db.Plans.First(x => x.Id == Id).Nombre;
            return View();
        }

        public JsonResult VerPlanPartesAux(int idPlan = 0)
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_VerPlanPartesAux",
              CommandType.StoredProcedure,
            new System.Data.SqlClient.SqlParameter("idPlan", idPlan));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }



        public JsonResult VerPartesActividadAux(int idParte)
        {
            DataTable tabla = new Models.Conectar()
             .EjecutarConsultaSelect("sp_uma_mantenimiento_VerActividadesPartesAux",
             CommandType.StoredProcedure,
           new System.Data.SqlClient.SqlParameter("idparte", idParte));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerRepuestosPorActividadAux(int idActividad)
        {
            DataTable tabla = new Models.Conectar()
             .EjecutarConsultaSelect("sp_uma_mantenimiento_VerRepuestosPorActividadAux",
             CommandType.StoredProcedure,
           new System.Data.SqlClient.SqlParameter("idActividad", idActividad));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        //GUARDAR PLAN PARTES 
        public JsonResult GuardarPlanPartesAux(string nombrePlan, int idPlan = 0)
        {
            try
            {
                Entidades.Uma.PlanPartes p = new Entidades.Uma.PlanPartes();
                p.Nombre = nombrePlan;
                p.idPlan = idPlan;
                db.PlanPartes.Add(p);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" });

            }
            catch (Exception ex)
            {
                return Json(new { Resultado = "Error" });
            }
        }
        // ELIMINAR PLAN PARTES 
        public JsonResult EliminarPlanPartesAux(int id)
        {
            try
            {
                Entidades.Uma.PlanPartes a = db.PlanPartes.First(x => x.id == id);
                db.PlanPartes.Remove(a);
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