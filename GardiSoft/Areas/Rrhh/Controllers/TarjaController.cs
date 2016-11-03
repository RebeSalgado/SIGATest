using Entidades.Rrhh.Tarja;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace GardiSoft.Areas.Rrhh.Controllers
{
    [Authorize]
    public class TarjaController : Controller
    {


        GardiSoft.Models.GardiSoftContext db = new Models.GardiSoftContext();

        // GET: Rrhh/Tarja
        public ActionResult Index()
        {
            ViewBag.Tarjas = db.Tarja.Where(x => x.Usuario == User.Identity.Name).ToList();
            return View();
        }

        public ActionResult Crear(Tarja tarja)
        {
            if (ModelState.IsValid)
            {
                tarja.Usuario = User.Identity.Name;
                db.Tarja.Add(tarja);
                db.SaveChanges();
             }

            return RedirectToAction("Index");

        }

        public ActionResult Asistencia(int? id=0)
        {
            var tabla = new Models.Conectar().EjecutarConsultaSelect("sp_rrhh_tarja_verTarja"
                 , System.Data.CommandType.StoredProcedure
                 , new System.Data.SqlClient.SqlParameter("idTarja", id));
            ViewBag.tabla = tabla;

            var columnas = tabla.Columns.Cast<DataColumn>()
                               .Where(x => x.ColumnName != "rut")
                                .Select(x => x.ColumnName)
                                .ToArray();

            ViewBag.Encabezados = "['RUT','" + string.Join("','", columnas) + "']";

            List<string> filas = new List<string>();
            foreach (System.Data.DataRow item in tabla.Rows)
            {

                var celdas = item.ItemArray;


                filas.Add("['" + string.Join("','", celdas) + "'],");
            }

            ViewBag.Data = filas;
            ViewBag.Ubicacion = new SelectList(db.TarjaUbicaciones, "Id", "Descripcion");
        
            ViewBag.Id = id;
            return View();

        }

       
        public JsonResult GuardaAsistencia(TarjaTrabajadores trabajador)
        {
            try
            {
                var asistencia = db.TarjaTrabajadores.FirstOrDefault(x => x.Fecha == trabajador.Fecha && x.IdTarja == trabajador.IdTarja && x.Rut == trabajador.Rut);
                asistencia.Estado = trabajador.Estado;
                db.SaveChanges();

                return Json(new { Resultado = "Exito" });
            }
            catch {
                return Json(new { Resultado = "error",Mensaje = "El estado no existe" });
            }
        }


        public ActionResult AgregarTrabajador(TarjaTrabajadores trabajador)
        {
            int fecha = db.Tarja.FirstOrDefault(x => x.Id == trabajador.IdTarja).Fecha;

            new Models.Conectar().EjecutarConsultaSelect("sp_rrhh_tarja_guarda_trabajador",CommandType.StoredProcedure,
                new System.Data.SqlClient.SqlParameter("fecha", fecha),
                new System.Data.SqlClient.SqlParameter("rut", trabajador.Rut),
                new System.Data.SqlClient.SqlParameter("ubicacion", trabajador.Ubicacion),
                new System.Data.SqlClient.SqlParameter("sindicato", trabajador.Sindicato),
                new System.Data.SqlClient.SqlParameter("idTarja", trabajador.IdTarja),
                new System.Data.SqlClient.SqlParameter("nombre", trabajador.Nombre));


            return RedirectToAction("Asistencia", new { id = trabajador.IdTarja });
        }
    }
}