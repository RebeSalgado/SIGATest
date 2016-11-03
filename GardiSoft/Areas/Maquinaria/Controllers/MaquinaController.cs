using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using Newtonsoft.Json;
using System.IO;

namespace GardiSoft.Areas.Maquinaria.Controllers
{
    [Authorize]
    public class MaquinaController : Controller
    {

        private GardiSoft.Models.GardiSoftContext db = new Models.GardiSoftContext();
        // GET: Maquinaria/Maquina
        public ActionResult Maquinas()
        {
            if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" });
            var maquinas = db.Maquinas.OrderBy(x => x.Codigo).Where(x => x.Id < 1000000).ToList();
            maquinas.Insert(0, new Entidades.Uma.Maquina { Codigo = "Ninguna", Id = null });
            ViewBag.IdParent = new SelectList(maquinas, "Id", "Codigo");
            ViewBag.IdPlan = new SelectList(db.Plans.OrderBy(x => x.Nombre).ToList(), "Id", "Nombre");
            ViewBag.IdUbicacion = new SelectList(db.Ubicacions.OrderBy(x => x.RutaCompleta).ToList(), "Id", "RutaCompleta");
            ViewBag.IdTipoMaquina = new SelectList(db.TipoMaquinas.ToList(), "Id", "Nombre");
            return View();
        }
        
        public JsonResult GuardarMaquina(Entidades.Uma.Maquina maquina)
        {
            try
            {
                db.Maquinas.Add(maquina);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" });
            }
            catch
            {
                return Json(new { Resultado = "Error" });
            }
        }

        public JsonResult EliminarMaquina(int idMaquina)
        {
            try
            {
                var maquina = db.Maquinas.FirstOrDefault(x => x.Id == idMaquina);
                db.Maquinas.Remove(maquina);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" });
            }
            catch
            {
                return Json(new { Resultado = "Problema" });
            }

        }

        public PartialViewResult verMaquinas()
        {
            var maquinas = db.Maquinas
                .Include(x => x.TipoMaquina)
                .Include(x => x.Plan)
                .Include(x => x.Ubicacion)
                .Include(x => x.Padre)
                .Where(x => x.TipoMaquina.Id != 0)//sin inmubles
                .ToList();

            return PartialView(maquinas ?? new List<Entidades.Uma.Maquina>());
        }

        public JsonResult VerMaquinasSeleccion()
        {
            DataTable tabla = new Models.Conectar()
             .EjecutarConsultaSelect("sp_uma_mantenimiento_VerMaquinaSeleccionar",
             CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Editar(int id = 0)
        {
            if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" });
            var maquina = db.Maquinas.FirstOrDefault(x => x.Id == id);

            var padre = db.Maquinas.ToList();
            padre.Insert(0, new Entidades.Uma.Maquina { Id = -1, Codigo = "Ninguno" });
            ViewBag.IdParent = new SelectList(padre, "Id", "Codigo", maquina.IdParent);
            ViewBag.IdPlan = new SelectList(db.Plans.ToList(), "Id", "Nombre", maquina.IdPlan);
            ViewBag.IdUbicacion = new SelectList(db.Ubicacions.ToList(), "Id", "RutaCompleta", maquina.IdUbicacion);
            ViewBag.IdTipoMaquina = new SelectList(db.TipoMaquinas.ToList(), "Id", "Nombre", maquina.IdTipoMaquina);
            return View(maquina);
        }

        public ActionResult ConfigurarPlan(int idPlan, int idMaquina)
        {
            if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" });
            ViewBag.Maquinas = new SelectList(db.Maquinas.ToList(), "Id", "Codigo", idMaquina);
            ViewBag.Plan = new SelectList(db.Plans.ToList(), "Id", "Nombre");
            var maquina = db.Maquinas.Where(x => x.Id == idMaquina).First();
            ViewBag.LecturaTotal = maquina.LecturaActual + maquina.LecturaBase;
            ViewBag.LecturaActual = maquina.LecturaActual;
            ViewBag.LecturaBase = maquina.LecturaBase;
            ViewBag.Promedio = maquina.Promedio;
            ViewBag.Codigo = db.Maquinas.FirstOrDefault(x => x.Id == idMaquina).Codigo;
            ViewBag.IdPlan = idPlan;
            ViewBag.IdMaquina = idMaquina;
            return View();
        }

        [HttpPost]
        public JsonResult VerPlanConfiguracion(int idPlan)
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_ConfigurarPlan",
              CommandType.StoredProcedure,
              new System.Data.SqlClient.SqlParameter("idPlan", idPlan));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerIntervalosPlan(int idPlan = 0)
        {
            var intervalos = db.Actividades.Where(x => x.PlanPartes.idPlan == idPlan)
                .Where(x => x.Frecuencia >= 125)
                .GroupBy(x => new { Frecuencia = x.Frecuencia })
                .Select(x => new { Frecuencia = x.Key }).ToList();

            return Json(intervalos);

        }

        public JsonResult GuardaIntervaloPlan()
        {
            // InputStream contains the JSON object you've sent
            string jsonString = new StreamReader(this.Request.InputStream).ReadToEnd();

            var a = new
            {
                IdPlan = 0,
                IdMaquina = 0,
                Configuraciones = new[]  {

                    new
                    {
                        Frecuencia = 0,
                        UltimaMantencion = 0

                    }
                }
            };

            // Deserialize it to a dictionary
            var dic = Newtonsoft.Json
                .JsonConvert.DeserializeAnonymousType(jsonString, a);

            var plan = db.Actividades.Where(x => x.PlanPartes.idPlan == dic.IdPlan).ToList();

            //borra la configuracion actual
            db.MaquinaActividad.RemoveRange(db.MaquinaActividad.Where(x => x.IdMaquina == dic.IdMaquina));
            foreach (var item in dic.Configuraciones)
            {
                //obtiene todas las actividades con esa frecuencia
                var actividades = plan.Where(x => x.Frecuencia == item.Frecuencia);

                //va agregando las actividades consu horometro
                foreach (var act in actividades)
                {
                    db.MaquinaActividad.Add(new Entidades.Uma.MaquinaActividad
                    {
                        IdActividad = act.Id,
                        IdMaquina = dic.IdMaquina,
                        UltimoHorometroMantenimiento = item.UltimaMantencion,
                        UltimaFechaMantenimiento = null
                    });
                }


            }

            db.SaveChanges();


            return Json("");
        }

        public JsonResult GuardaEditar(Entidades.Uma.Maquina maquina)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (maquina.IdParent == -1)
                    {
                        maquina.IdParent = null;
                    }
                    db.Entry(maquina).State = EntityState.Modified;
                    db.SaveChanges();
                    DataTable tabla = new Models.Conectar()
            .EjecutarConsultaSelect("sp_mantenimiento_regularizarHorometrosActualizados",
            CommandType.StoredProcedure,
              new System.Data.SqlClient.SqlParameter("idMaquinaEditada", maquina.Id));
                    string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
        

                }

                return Json(new { Resultado = "Guardado" });
            }
            catch (Exception ex)

            {

                return Json(new { Resultado = "Error" });
            }


        }

        public ActionResult VerComponentes(int idMaquina = 0)
        {

            if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" });
            if (idMaquina == 0) // no deberia estar aquí
            {
                return RedirectToAction("Maquinas");
            }
            var Maquina = db.Maquinas.Include(x => x.TipoMaquina).FirstOrDefault(x => x.Id == idMaquina);
            ViewBag.Maquina = Maquina;
            ViewBag.IdComponente = new SelectList(db.Maquinas.ToList(), "Id", "Codigo");
            ViewBag.Ubicacion = new SelectList(new List<string> { "Motor", "Izquierda", "Derecha" });
            ViewBag.DebeAsignar = !Maquina.TipoMaquina.EsComponente; //parche, si es componente no se dibuja el formulario de asignacion
            return View();
        }

        [HttpPost]
        public JsonResult VerListaComponentes(int idMaquina)
        {

            DataTable tabla = new Models.Conectar()
          .EjecutarConsultaSelect("sp_uma_mantenimiento_VerComponentesVigentes",
        CommandType.StoredProcedure,
          new System.Data.SqlClient.SqlParameter("codMaquina", db.Maquinas.FirstOrDefault(x=> x.Id == idMaquina).Codigo));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult VerListaComponentesAnteriores(int idMaquina)
        {
            DataTable tabla = new Models.Conectar()
         .EjecutarConsultaSelect("sp_uma_mantenimiento_VerComponentesAnteriores",
       CommandType.StoredProcedure,
         new System.Data.SqlClient.SqlParameter("codMaquina", db.Maquinas.FirstOrDefault(x => x.Id == idMaquina).Codigo));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        //si el componente esta asociado a otra maquina envia una advertencia
        public JsonResult EvaluaCambioDeComponente(int idComponente)
        {
            var padre = db.HistoricoComponente.Include(x => x.Maquina)
                .Where(x => x.IdComponente == idComponente && x.FechaDesignacion == null)
                .FirstOrDefault();

            return Json(new { Padre = (padre == null ? "ninguno" : padre.Maquina.Codigo) });
        }

        public JsonResult DesaociarComponente(int idComponente)
        {
            try
            {
                var componente = db.Maquinas.FirstOrDefault(x => x.Id == idComponente);
                componente.Promedio = 0;
                componente.IdParent = idComponente;
                var componenteHistorico = db.HistoricoComponente.FirstOrDefault(x => x.IdComponente == idComponente && x.FechaDesignacion == null);
                if (componenteHistorico != null)
                {
                    componenteHistorico.FechaDesignacion = DateTime.Now;
                }
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" });
            }
            catch (Exception)
            {

                return Json(new { Resultado = "Error" });
            }


        }
        
        public JsonResult AgregaComponente(Entidades.Uma.HistoricoComponentes componente)
        {
            //busca el componente en esa máquina
            try
            {

                DataTable tabla = new Models.Conectar()
             .EjecutarConsultaSelect("sp_uma_mantenimiento_asignarComponente",
             CommandType.StoredProcedure,
               new System.Data.SqlClient.SqlParameter("idMaquinaPadre", componente.IdMaquinaPadre),
               new System.Data.SqlClient.SqlParameter("idComponente", componente.IdComponente),
               new System.Data.SqlClient.SqlParameter("ubicacion", componente.Ubicacion));
                string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
                return Json(new { Resultado = "Guardado" });
            }
            catch (Exception ex)
            {

                return Json(new { Resultado = "Error" });
            }
        }

        // ¿ADMINISTRAR MAQUINAS
        public ActionResult Index()
        {
            if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" });
            return View();
        }

        //VER MAQUINAS 
        public JsonResult AdministrarMaquinas()
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_AdministrarMaquina",
              CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerListaComponentesmdl()
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_VerComponentesMaquina",
              CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        //Nueva Ubicacion
        public ActionResult CambiarUbicacion(int idMaquina = 0)
        {
            ViewBag.IdMaquina = idMaquina;
            ViewBag.IdUbicacion = (db.Ubicacions.OrderBy(x => x.RutaCompleta).ToList());
            return View();
        }

        public JsonResult CambiarUbicacionMaquina(int idMaquina = 0, int idUbicacion = 0, int creId= 0)
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_CambiarUbicacionMaquina",
              CommandType.StoredProcedure,
            new System.Data.SqlClient.SqlParameter("idMaquina", idMaquina),
            new System.Data.SqlClient.SqlParameter("idUbicacion", idUbicacion),
            new System.Data.SqlClient.SqlParameter("creId", creId));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        // MODIFICAR HOROMETRO
        public JsonResult CambiarHorometro(int idmaquina = 0, int Horometro = 0)
        {
            try
            {
                Entidades.Uma.Maquina m = db.Maquinas.First(x => x.Id == idmaquina);
                m.LecturaBase = Horometro;
                db.SaveChanges();
                return Json(new { Resultado = "Guardado", Actual = m.LecturaActual, Base = m.LecturaBase, LecturaTotal = m.LecturaBase + m.LecturaActual }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);

            }

        }

        // MODIFICAR HOROMETRO
        public JsonResult CambiarPromedio(int idmaquina = 0, int promedio = 0)
        {
            try
            {
                Entidades.Uma.Maquina m = db.Maquinas.First(x => x.Id == idmaquina);
                m.Promedio = promedio;
                db.SaveChanges();
                return Json(new { Resultado = "Guardado", Actual = m.LecturaActual, Base = m.LecturaBase, LecturaTotal = m.LecturaBase + m.LecturaActual }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);

            }

        }


      

    }
}