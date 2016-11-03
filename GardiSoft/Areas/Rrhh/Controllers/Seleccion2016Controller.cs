using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardiSoft.Areas.Rrhh.Controllers
{
    public class Seleccion2016Controller : Controller
    {
        // GET: Rrhh/Seleccion2016
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult VerSeleccion()
        {
            DataTable tabla = new Models.Conectar()
             .EjecutarConsultaSelect("sp_rrhh_CargarColaboradores",
              CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public JsonResult FiltrarBusqueda(string Edad1 , string Edad2  , string Comuna, string Region, string NombreOficio , string NombreInstituto,  string CargoPostula)
{
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_rrhh_CargarColaboradores",
              CommandType.StoredProcedure,
            new System.Data.SqlClient.SqlParameter("@Edad1", Edad1 ),
            new System.Data.SqlClient.SqlParameter("@Edad2", Edad2 ),
            new System.Data.SqlClient.SqlParameter("@Comuna", Comuna ),
            new System.Data.SqlClient.SqlParameter("@Region", Region ),
            new System.Data.SqlClient.SqlParameter("@NombreOficio", NombreOficio ),
            new System.Data.SqlClient.SqlParameter("@NombreInstituto", NombreInstituto ),
            //new System.Data.SqlClient.SqlParameter("@NombreTitulo", NombreTitulo),
            new System.Data.SqlClient.SqlParameter("@CargoPostula", CargoPostula ));
    string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

    }
}