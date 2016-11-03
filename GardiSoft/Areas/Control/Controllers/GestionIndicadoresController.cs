using Entidades.Control;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace GardiSoft.Areas.Control.Controllers
{
    public class GestionIndicadoresController : Controller
    {
        Models.GardiSoftContext db = new Models.GardiSoftContext();
        // GET: Control/GestionIndicadores

        public List<Area> Arealist = new List<Area>();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GuardarDatosGestion( string nombre_area, string nombre_nivel, string nombre_turno, string nombre_usuario)
        {
            /*Conexion con el precedure, guarda datos del fgestion1*/
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_GestionIndicadores_mantenedor",
              CommandType.StoredProcedure,
              new System.Data.SqlClient.SqlParameter("nombre_area", nombre_area),
              new System.Data.SqlClient.SqlParameter("nombre_nivel", nombre_nivel));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarDatosGestion2(string nombre_Proyecto, int Id_Area, int Id_Nivel , int Id_Turno, int Id_Indicador , int Id_Usuario)
        {
            /*Conexion con el precedure, guarda datos del fgestion2*/
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_GestionIndicadores_mantenedorNuevo",
              CommandType.StoredProcedure,
              new System.Data.SqlClient.SqlParameter("nombre_Proyecto", nombre_Proyecto),
              new System.Data.SqlClient.SqlParameter("Id_Area", Id_Area),
              new System.Data.SqlClient.SqlParameter("Id_Nivel", Id_Nivel),
              new System.Data.SqlClient.SqlParameter("Id_Turno", Id_Turno),
              new System.Data.SqlClient.SqlParameter("Id_Indicador", Id_Indicador),
              new System.Data.SqlClient.SqlParameter("Id_Usuario", Id_Usuario));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            db.SaveChanges();
            return Json(json, JsonRequestBehavior.AllowGet);
            
        }

        public void GuardaBBdd()
        {
             db.SaveChanges();
        }
        public JsonResult TraeArea()
        {
             
            /*Conexion con el precedure, trae los datos de area, para llenar el combo*/
            DataTable tabla = new Models.Conectar()
            .EjecutarConsultaSelect("sp_GestionIndicadores_TraeArea",
            CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TraeNivel()
        {
            /*Conexion con el precedure, trae los datos de nivel, para llenar el combo*/
            DataTable tabla = new Models.Conectar()
            .EjecutarConsultaSelect("sp_GestionIndicadores_TraeNivel",
            CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TraeTurno()
        {
            /*Conexion con el precedure, trae los datos de nivel, para llenar el combo*/
            DataTable tabla = new Models.Conectar()
            .EjecutarConsultaSelect("sp_GestionIndicadores_TraeTurno",
            CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TraeIndicador()
        {
            /*Conexion con el precedure, trae los datos de nivel, para llenar el combo*/
            DataTable tabla = new Models.Conectar()
            .EjecutarConsultaSelect("sp_GestionIndicadores_TraeIndicador",
            CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TraeUsuario()
        {
            /*Conexion con el precedure, trae los datos de nivel, para llenar el combo*/
            DataTable tabla = new Models.Conectar()
            .EjecutarConsultaSelect("sp_GestionIndicadores_TraeUsuario",
            CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }




    }
}
