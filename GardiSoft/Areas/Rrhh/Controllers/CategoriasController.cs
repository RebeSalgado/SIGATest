using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardiSoft.Areas.Rrhh.Controllers
{
    public class CategoriasController : Controller
    {

        Models.GardiSoftContext db = new Models.GardiSoftContext();
        // GET: Rrhh/Categorias
        public ActionResult Categoria()
        {
            return View();
        }


        // ************************************************** CATEGORÍA ******************************************************

        //GUARDAR CATEGORÍA
        public JsonResult GuardarCategoria(string NombreCategoria, string CCC)
        {
            try
            {
                Entidades.Rrhh.Capacitaciones.Categoria C = new Entidades.Rrhh.Capacitaciones.Categoria();
                C.Nombre = NombreCategoria;
                C.ValorCcc = CCC;
                db.Categorias.Add(C);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" });

            }
            catch (Exception ex)
            {
                return Json(new { Resultado = "Error" });
            }
        }

        //VER CATEGORÍAS

        public JsonResult VerCategorias()
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_rrhh_capacitacion_ver_categorias",
              CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        // MODIFICAR CATEGORÍA
        public JsonResult ModificarCategoria(int Id, string Nombre, string CCC)
        {
            try
            {
                Entidades.Rrhh.Capacitaciones.Categoria a = db.Categorias.First(x => x.Id == Id);
                a.Nombre = Nombre;
                a.ValorCcc = CCC;
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);

            }

        }

        //ELIMINAR CATEGORÍA 
        public JsonResult EliminarCategoria(int Id)
        {
            try
            {
                Entidades.Rrhh.Capacitaciones.Categoria C = db.Categorias.First(x => x.Id == Id);
                db.Categorias.Remove(C);
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