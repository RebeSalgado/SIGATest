using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;

namespace GardiSoft.Areas.Maquinaria.Controllers
{
    public class GestionRepuestosController : Controller
    {
        GardiSoft.Models.GardiSoftContext db = new GardiSoft.Models.GardiSoftContext();

        // GET: Maquinaria/GestionRepuestos
        public ActionResult Index(int id=0)
        {
            if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" });
            ViewBag.Id = id;
            ViewBag.IdOrigenDeFalla = new SelectList(db.OrigenDeFalla, "Id", "Descripcion");
          
            return View();
        }

        public JsonResult VerRepuestosSolicitadosCorrectivo(int idBodega = 0,int idOt = 0)
        {
             DataTable tabla = new Models.Conectar()
               .EjecutarConsultaSelect("sp_uma_mantenimiento_verSolicitudMecanicoCorrectivo",
               CommandType.StoredProcedure,
               new System.Data.SqlClient.SqlParameter("idBodega", idBodega),
               new System.Data.SqlClient.SqlParameter("idOt", idOt));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public JsonResult EnviarEmailAprobador(int id, string correo)
        {
            DataTable tabla = new Models.Conectar()
               .EjecutarConsultaSelect("sp_uma_mantenimiento_emailAprobador",
               CommandType.StoredProcedure,
               new System.Data.SqlClient.SqlParameter("id", id),
               new System.Data.SqlClient.SqlParameter("usuario", correo));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public JsonResult VerRepuestosSolicitadosPreventivos(int idBodega = 0, int idOt = 0)
        {
             DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_verSolicitudMecanicoPreventivo",
               CommandType.StoredProcedure,
               new System.Data.SqlClient.SqlParameter("idBodega", idBodega),
               new System.Data.SqlClient.SqlParameter("idOt", idOt));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


       
        public JsonResult VerOt(int idOt= 0)
        {
            try
            {
                Entidades.Uma.OT ot = db.Ot.FirstOrDefault(x => x.Id == idOt);
                var m = db.Maquinas.FirstOrDefault(x => x.Id == ot.IdMaquina);
                var m2 = db.Maquinas.FirstOrDefault(x => x.Id == ot.IdMaquinaPadre);
                m.Padre = null;
                m2.Padre = null;
                ot.Maquina = m;
                ot.MaquinaPadre = m;
                //var ot = db.Ot.Include(x => x.Maquina).Include(x => x.MaquinaPadre).FirstOrDefault(x => x.Id == idOt);
                //ot.MaquinaPadre = db.Maquinas.FirstOrDefault(x => x.Id == ot.IdMaquinaPadre);
                JsonResult r = Json(ot, JsonRequestBehavior.AllowGet);
                return r;
            }
            catch (Exception ex)
            {
                return Json("");
            }
           
        }
      


        public ActionResult IndexAprobador(int? id= 0)
        {

             if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" });
            ViewBag.idOt = id;
            return View();
        }

        public JsonResult VerRepuestosSolicitadosAprobacion(int idBodega = 0, int idOt = 0)
            {
                DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_verSolicitudAprobacion",
            CommandType.StoredProcedure,
              new System.Data.SqlClient.SqlParameter("idBodega", idBodega),
              new System.Data.SqlClient.SqlParameter("idOt", idOt));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);

        }

      




        public JsonResult QuitarRecursoOtCorrectivo(int IdProducto, int IdDetalle, int Cantidad, bool aprobado)
        {
            
            //if(db.OtDetalleCorrectivo.Include(x=> x.Cabecera).FirstOrDefault(x=> x.Id == IdDetalle).Cabecera.Cerrada == true)
            //{
            //    return Json(new { Resultado = "Error" });

            //}

            try
            {
                DataTable tabla = new Models.Conectar()
            .EjecutarConsultaSelect("sp_uma_mantenimiento_QuitarRepuesto",
            CommandType.StoredProcedure,
            new System.Data.SqlClient.SqlParameter("@iddetalle", IdDetalle),
            new System.Data.SqlClient.SqlParameter("@idrecurso", IdProducto),
            new System.Data.SqlClient.SqlParameter("@cantidad", Cantidad),
            new System.Data.SqlClient.SqlParameter("@estado", aprobado),
            new System.Data.SqlClient.SqlParameter("@usuario", User.Identity.Name)
            );

                return Json(new { Resultado = "Guardado" });
            }
            catch (Exception ex)
            {

                return Json(new { Resultado = "Error" });
            }

        }


        public JsonResult QuitarRecursoOtPreventivo(int IdProducto, int IdDetalle, int Cantidad, bool aprobado)
        {

            

            try
            {
                DataTable tabla = new Models.Conectar()
            .EjecutarConsultaSelect("sp_uma_mantenimiento_QuitarRepuestoPreventivo",
            CommandType.StoredProcedure,
            new System.Data.SqlClient.SqlParameter("@iddetalle", IdDetalle),
            new System.Data.SqlClient.SqlParameter("@idrecurso", IdProducto),
            new System.Data.SqlClient.SqlParameter("@cantidad", Cantidad),
            new System.Data.SqlClient.SqlParameter("@estado", aprobado),
            new System.Data.SqlClient.SqlParameter("@usuario", User.Identity.Name)
            );

                return Json(new { Resultado = "Guardado" });
            }
            catch (Exception ex)
            {

                return Json(new { Resultado = "Error" });
            }

        }


     

        public JsonResult VerRepuestosSolicitadosAprobacionPreventivos(int idBodega = 0, int idOt = 0)
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_verSolicitudAprobacionPreventivo",
              CommandType.StoredProcedure,
              new System.Data.SqlClient.SqlParameter("idBodega", idBodega),
              new System.Data.SqlClient.SqlParameter("idOt", idOt));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerOtAprobacionPreventivo(int idOt = 0)
        {
            var ot = db.Ot.Include(x => x.Maquina).Include(x => x.MaquinaPadre).FirstOrDefault(x => x.Id == idOt);
            return Json(ot);
        }

        
        public JsonResult AprobarSolicitudCorrectiva(int idSolicitud = 0, int cantidad = 0)
        {
            try
            {
                Entidades.Uma.RepuestosCorrectivosSolicitados a = db.RepuestosCorrectivosSolicitados.First(x => x.Id == idSolicitud);
                a.Cantidad = cantidad;
                a.Aprobado = true;
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);

            }

        }




     

        public JsonResult AprobarSolicitud(int idotdetalle, int idrecurso , int cantidad = 0)
        {
            try
            {
                Entidades.Uma.OtRepuestosSolicitados a = db.OtRepuestosSolicitados.First(x => x.IdOtDetalle == idotdetalle && x.IdRecurso == idrecurso && x.Aprobado == false);
                a.Cantidad = cantidad;
                a.Aprobado = true;
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);

            }

        }


        public JsonResult AvisarBodegero(int idOt,int bodega,string productos)
        {
            DataTable tabla = new Models.Conectar()
             .EjecutarConsultaSelect("sp_uma_enviarMailBodegeros",
             CommandType.StoredProcedure,
             new System.Data.SqlClient.SqlParameter("idot",idOt),
             new System.Data.SqlClient.SqlParameter("bodega", bodega),
             new System.Data.SqlClient.SqlParameter("productos", productos));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
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
    }
}