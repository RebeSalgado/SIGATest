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
    public class AdministrarActividadController : Controller
    {

        Models.GardiSoftContext db = new Models.GardiSoftContext();
        // GET: Maquinaria/AdministrarActividad



        public ActionResult Index(int Id = 0)
        {
             if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" }); 
            ViewBag.Id = Id;
            ViewBag.PlanParte = db.PlanPartes.First(x => x.id == Id).Nombre;
            return View();
        }

        //VER ACTIVIDAD

        public JsonResult VerActividad(int idactividad = 0)
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_AdministrarActividadesPlanPartes",
              CommandType.StoredProcedure,
            new System.Data.SqlClient.SqlParameter("idactividad", idactividad));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarActividad(int idparte = 0, string Descripcion = null, int Frecuencia = 0, int MinutosEstimados = 0)
        {
            try
            {
                Entidades.Uma.Actividades p = new Entidades.Uma.Actividades();
                p.idPartes = idparte;
                p.Descripcion= Descripcion;
                p.Frecuencia = Frecuencia;
                p.MinutosEstimados = MinutosEstimados;
                db.Actividades.Add(p);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" });

            }
            catch (Exception ex)
            {
                return Json(new { Resultado = "Error" });
            }
        }


        //ELIMINAR ACTIVIDAD 
        public JsonResult EliminarActividad(int Id)
        {
            try
            {
                Entidades.Uma.Actividades a = db.Actividades.First(x => x.Id == Id);
                db.Actividades.Remove(a);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);

            }

        }



        public ActionResult IndexMaster(int Id = 0)
        {
           // if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" });
            ViewBag.Id = Id;
            ViewBag.PlanParte = db.PlanPartesMaster.First(x => x.id == Id).Nombre;
            return View();
        }

        //VER ACTIVIDAD

        public JsonResult VerActividadMaster(int idactividad = 0)
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_AdministrarActividadesPlanPartesMaster",
              CommandType.StoredProcedure,
            new System.Data.SqlClient.SqlParameter("idactividad", idactividad));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarActividadMaster(int idparte = 0, string Descripcion = null, int Frecuencia = 0, int MinutosEstimados = 0)
        {
            try
            {
                Entidades.Uma.ActividadesMaster p = new Entidades.Uma.ActividadesMaster();
                p.idPartes = idparte;
                p.Descripcion = Descripcion;
                p.Frecuencia = Frecuencia;
                p.MinutosEstimados = MinutosEstimados;
                db.ActividadesMaster.Add(p);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" });

            }
            catch (Exception ex)
            {
                return Json(new { Resultado = "Error" });
            }
        }

        public JsonResult GuardarActividadAux(int idparte = 0, string Descripcion = null, int Frecuencia = 0, int MinutosEstimados = 0)
        {
            try
            {
                Entidades.Uma.Actividades p = new Entidades.Uma.Actividades();
                p.idPartes = idparte;
                p.Descripcion = Descripcion;
                p.Frecuencia = Frecuencia;
                p.MinutosEstimados = MinutosEstimados;
                db.Actividades.Add(p);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" });

            }
            catch (Exception ex)
            {
                return Json(new { Resultado = "Error" });
            }
        }

        public JsonResult ModificarActividadMaster(Entidades.Uma.ActividadesMaster actividad)
        {
            try
            {
                Entidades.Uma.ActividadesMaster a = db.ActividadesMaster.First(x => x.Id == actividad.Id);
                a.MinutosEstimados = actividad.MinutosEstimados;
                a.Descripcion = actividad.Descripcion;
                a.Frecuencia = actividad.Frecuencia;
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);

            }

        }


        public JsonResult ModificarActividadAux(Entidades.Uma.Actividades actividad)
        {
            try
            {
                Entidades.Uma.Actividades a = db.Actividades.First(x => x.Id == actividad.Id);
                a.MinutosEstimados = actividad.MinutosEstimados;
                a.Descripcion = actividad.Descripcion;
                a.Frecuencia = actividad.Frecuencia;
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);

            }

        }

        //ELIMINAR ACTIVIDAD 
        public JsonResult EliminarActividadMaster(int Id)
        {
            try
            {
                Entidades.Uma.ActividadesMaster a = db.ActividadesMaster.First(x => x.Id == Id);
                db.ActividadesMaster.Remove(a);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);

            }

        }

        public JsonResult EliminarActividadAux(int Id)
        {
            try
            {
                Entidades.Uma.Actividades a = db.Actividades.First(x => x.Id == Id);
                db.Actividades.Remove(a);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);

            }

        }

        public JsonResult EliminarRepuestoMaster(int id)
        {

            try
            {
                Entidades.Uma.RepuestosActividadesMaster a = db.RepuestosActividadesMaster.First(x => x.iD == id);
                db.RepuestosActividadesMaster.Remove(a);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);

            }

        }


        public JsonResult EliminarRepuestoAux(int id)
        {

            try
            {
                Entidades.Uma.RepuestosActividades a = db.RepuestosActividades.First(x => x.iD == id);
                db.RepuestosActividades.Remove(a);
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