using Entidades.Uma;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GardiSoft.Areas.Maquinaria.Controllers
{
    [Authorize]
    public class OTController : Controller
    {
        GardiSoft.Models.GardiSoftContext db = new Models.GardiSoftContext();
        // GET: Maquinaria/OT
        public ActionResult Index(string id)
        {
            if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" });
            ViewBag.IdUbicacion = new SelectList(db.Ubicacions.OrderBy(x => x.RutaCompleta).ToList(), "Id", "RutaCompleta");
            ViewBag.IdOrigenDeFalla = new SelectList(db.OrigenDeFalla, "Id", "Descripcion");

            return View();
        }

        public JsonResult VerCalendarioOt(int IdUbicacion = 0)
        {
            //uma_mantenimiento_calendarioPreventivo
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_VerCalendarioOt",
              CommandType.StoredProcedure,
            new System.Data.SqlClient.SqlParameter("IdUbicacion", IdUbicacion)
              );
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerFallasOt(int id)
        {
            var fallas = db.FallasOt.FirstOrDefault(x => x.IdOt == id);
            var ot = db.Ot.FirstOrDefault(x => x.Id == id);
            return Json(new { IdOrigen = fallas.IdOrigen, IdFalla = fallas.IdFalla, FechaAvisoOperaciones = ot.FechaInicio.ToString("yyyy-MM-dd"), HoraAvisoOperaciones = ot.FechaInicio.ToString("hh:mm"), FechaIntervencionMaquina = ot.FechaInicio.ToString("yyyy-MM-dd"), HoraIntervencionMaquina = ot.FechaInicio.ToString("hh:mm") });
        }

        public ActionResult IndexGestionOT(int id = 0)
        {
            if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" });
            if (id == 0)
            {
                return RedirectToAction("Index");

            }
            DataTable tabla = new Models.Conectar()
          .EjecutarConsultaSelect("sp_uma_mantenimiento_verRepuesto",
          CommandType.StoredProcedure);

            ViewBag.Productos = tabla;

            if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" });
            var ot = db.Ot.Include(x => x.Tipo).FirstOrDefault(x => x.Id == id);
            return View(ot);
        }

        public JsonResult VerActividadesOT(int id = 0)
        {
            DataTable tabla = new Models.Conectar()
             .EjecutarConsultaSelect("sp_uma_mantenimiento_verProgresoActividadesOt",
             CommandType.StoredProcedure,
             new System.Data.SqlClient.SqlParameter("idOt", id)
             );
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerActividadesCorrectivasOT(int id = 0)
        {
            DataTable tabla = new Models.Conectar()
             .EjecutarConsultaSelect("sp_uma_mantenimiento_verOtCorrectivaActividadesRecursos",
             CommandType.StoredProcedure,
             new System.Data.SqlClient.SqlParameter("idOt", id)
             );
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerTrabajadores(int id = 0)
        {
            DataTable tabla = new Models.Conectar()
             .EjecutarConsultaSelect("sp_uma_mantenciones_verManoDeObra",
             CommandType.StoredProcedure
             );
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ActualizarAvanceOt(int id = 0, int estado = 0, int idTipo = 0)
        {

            if (idTipo == 6 || idTipo == 2)
            {
                return ActualizarAvancePreventivoOt(id, estado);
            }
            else
            {
                return ActualizarAvanceCorrectivoOt(id, estado);
            }


        }

        private JsonResult ActualizarAvancePreventivoOt(int id = 0, int estado = 0)
        {

            try
            {
                if (db.DetalleOt.Include(x => x.Cabecera).First(x => x.Id == id).Cabecera.Cerrada == false)
                {
                    DataTable tabla = new Models.Conectar()
            .EjecutarConsultaSelect("sp_uma_mantenimiento_ActualizarAvanceOt",
            CommandType.StoredProcedure,
            new System.Data.SqlClient.SqlParameter("estado", estado),
            new System.Data.SqlClient.SqlParameter("id", id));
                    string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
                    return Json(json, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Resultado = "Cerrada" });
                }


            }
            catch (Exception ex)
            {
                return Json(new { Resultado = "error" });

            }

        }

        public JsonResult VerTipoDeFalla(int idMaquina)
        {
            Maquina m = db.Maquinas.First(x => x.Id == idMaquina);
            //m.IdTipoMaquina
            List<TiposDeFalla> tipos = db.TiposDeFalla.Where(x => x.IdTipoMaquina == m.IdTipoMaquina).ToList();
            return Json(tipos);
        }

        private JsonResult ActualizarAvanceCorrectivoOt(int id = 0, int estado = 0)
        {

            try
            {
                if (db.OtDetalleCorrectivo.Include(x => x.Cabecera).First(x => x.Id == id).Cabecera.Cerrada == false)
                {
                    DataTable tabla = new Models.Conectar()
            .EjecutarConsultaSelect("sp_uma_mantenimiento_ActualizarAvanceCorrectivoOt",
            CommandType.StoredProcedure,
            new System.Data.SqlClient.SqlParameter("estado", estado),
            new System.Data.SqlClient.SqlParameter("id", id));
                    string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
                    return Json(json, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Resultado = "Cerrada" });
                }


            }
            catch (Exception ex)
            {
                return Json(new { Resultado = "error" });

            }

        }

        public JsonResult VerSeleccionarRepuesto()
        {
            DataTable tabla = new Models.Conectar()
           .EjecutarConsultaSelect("sp_uma_mantenimiento_verRepuesto",
           CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult verRepuestoscorrectivosSolicitados(int idot = 0)
        {
            DataTable tabla = new Models.Conectar()
           .EjecutarConsultaSelect("sp_uma_mantenimiento_verRepuestoscorrectivosSolicitados",
           CommandType.StoredProcedure,
           new System.Data.SqlClient.SqlParameter("idot", idot));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerAsociarRepuestoActividad(int idOt)
        {
            DataTable tabla = new Models.Conectar()
           .EjecutarConsultaSelect("sp_uma_mantenimiento_verAsociarRepuestoActividad",
           CommandType.StoredProcedure,
           new System.Data.SqlClient.SqlParameter("idOt", idOt));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgregarManoDeObra(int idManoDeObra, DateTime fechaInicio, DateTime fechaTermino, int valor, int idOt)
        {
            try
            {
                if (db.Ot.First(x => x.Id == idOt).Cerrada == false)
                {
                    db.OtManoDeObra.Add(new Entidades.Uma.OtManoDeObra { IdOt = idOt, IdPersonal = idManoDeObra, HoraInicio = fechaInicio, horaTermino = fechaTermino, Valor = valor, Tiempo = (int)fechaTermino.Subtract(fechaInicio).TotalMinutes });
                    db.SaveChanges();
                    return Json(new { Resultado = "Agregado" });
                }
                else
                {
                    return Json(new { Resultado = "Cerrada" });
                }
            }
            catch (Exception ex)
            {

                return Json(new { Resultado = "Error" });
            }

        }

        public JsonResult CerrarOt(int id)
        {
            try
            {

                DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_cerrarOt",
              CommandType.StoredProcedure,
              new System.Data.SqlClient.SqlParameter("idot", id));

                return Json(new { Resultado = "Guardado" });
            }
            catch (Exception ex)
            {
                return Json(new { Resultado = "error" });
            }
        }

        public JsonResult GuardarQuedoOperativa(int id,bool operativa)
        {
            try
            {
                var ot = db.Ot.FirstOrDefault(x => x.Id == id);
                ot.QuedoOperativa = operativa;
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" });
            }
            catch (Exception ex)
            {
                return Json(new { Resultado = "error" });
            }
        }

        public JsonResult GuardarHoraFinalizacionIntervencion(int id,DateTime fecha)
        {
            try
            {
                var ot = db.Ot.FirstOrDefault(x => x.Id == id);
                ot.FechaTermino = DateTime.Now;
                ot.FechaTerminoIntervencion = fecha;
                db.SaveChanges();
                return Json(new { Resultado="Guardado"});
            }
            catch (Exception ex)
            {
                return Json(new { Resultado = "error" });
            }
        }


        public JsonResult VerRepuestosSolicitados(int id = 0)
        {
            DataTable tabla = new Models.Conectar()
            .EjecutarConsultaSelect("sp_uma_mantenimiento_verRepuestosPlan",
            CommandType.StoredProcedure,
            new System.Data.SqlClient.SqlParameter("idOt", id)
            );
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);

        }

        public JsonResult VerTrabajadoresSolicitados(int id = 0)
        {
            DataTable tabla = new Models.Conectar()
            .EjecutarConsultaSelect("sp_uma_mantenimiento_verOtTrabajadores",
            CommandType.StoredProcedure,
            new System.Data.SqlClient.SqlParameter("idOt", id)
            );
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);

        }

        public ActionResult RecursosOt()
        {
            if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" });
            return View();
        }

        public JsonResult VerRecursosOt()
        {
            DataTable tabla = new Models.Conectar()
           .EjecutarConsultaSelect("sp_uma_verRecursosOt",
           CommandType.StoredProcedure

           );
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OtCorrectiva()
        {
            if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" });
            var idTipo = db.Maquinas.First().IdTipoMaquina;
            ViewBag.IdMaquina = new SelectList(db.Maquinas, "Id", "Codigo");
            ViewBag.IdTipoOt = new SelectList(db.tipoOt.Where(x => x.Id != 2 && x.Id != 6), "Id", "Nombre");
            ViewBag.IdOrigenDeFalla = new SelectList(db.OrigenDeFalla, "Id", "Descripcion");
            ViewBag.IdTipoDeFalla = new SelectList(db.TiposDeFalla.Where(x => x.IdTipoMaquina == idTipo), "Id", "DescripcionFalla");
            ViewBag.LugarOt = new SelectList(db.Ubicacions.OrderBy(x => x.RutaCompleta).ToList(), "Id", "RutaCompleta");
            return View(new Entidades.Uma.OT());
        }


        public JsonResult VerMaquinasSeleccion()
        {
            DataTable tabla = new Models.Conectar()
             .EjecutarConsultaSelect("sp_uma_mantenimiento_VerMaquinaSeleccionar",
             CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerInformacionEntrega(int idMaquina = 0)
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_Fin700MaquinaCentroConsumoPorMaquina",
              CommandType.StoredProcedure,
              new System.Data.SqlClient.SqlParameter("idMaquina", idMaquina));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public JsonResult VerInformacionPadre(int idMaquina = 0)
        {
            DataTable tabla = new Models.Conectar()
            .EjecutarConsultaSelect("sp_uma_mantenimiento_VerMaquinaPadre",
            CommandType.StoredProcedure,
            new System.Data.SqlClient.SqlParameter("idMaquina", idMaquina));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarOtCorrectiva(Entidades.Uma.OT ot, int idOrigenDefalla, int IdTipoDeFalla, int idPadre)
        {
            try
            {

                ot.FechaCreacion = DateTime.Now;
                ot.Cerrada = false;
                ot.IdMaquinaPadre = idPadre;
                ot.PorcentajeFinalizado = 0;
                ot.Usuario = User.Identity.Name;
                ot.FechaTermino = DateTime.Now;
                ot.FechaInicio = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Ot.Add(ot);
                    db.SaveChanges();

                    OtDetalleCorrectivo t = new OtDetalleCorrectivo
                    {
                        IdOt = ot.Id,
                        Progreso = 0,
                        NombreActividad = ot.Descripcion,
                        Usuario = User.Identity.Name,
                        FechaCreacion = DateTime.Now

                    };

                    db.OtDetalleCorrectivo.Add(t);

                    FallasOt f = new FallasOt();
                    f.IdFalla = IdTipoDeFalla;
                    f.IdOrigen = idOrigenDefalla;
                    f.IdOt = ot.Id;
                    db.FallasOt.Add(f);
                    db.SaveChanges();


                    return Json(new { Resultado = "Guardado", Id = ot.Id });
                }
                else
                {
                    return Json(new { Resultado = "Error" });
                }
            }
            catch (Exception ex)
            {

                return Json(new { Resultado = "Error" });
            }

        }

        public JsonResult VerOtCorrectiva()
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_verOtCorrectiva",
              CommandType.StoredProcedure);
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarRecursoOtCorrectivo(int IdProducto, int IdDetalle, int Cantidad, bool aprobado)
        {
            try
            {
                DataTable tabla = new Models.Conectar()
            .EjecutarConsultaSelect("sp_uma_mantenimiento_agregarRepuesto",
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

        public JsonResult GuardarRecursoOtPreventivo(int IdProducto, int IdDetalle, int Cantidad, bool aprobado)
        {
            try
            {
                DataTable tabla = new Models.Conectar()
            .EjecutarConsultaSelect("sp_uma_mantenimiento_agregarRepuestoPreventivo",
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

        public JsonResult EliminarSolicitudDeRepuesto(int id)
        {
            try
            {
                RepuestosCorrectivosSolicitados a;
                a = db.RepuestosCorrectivosSolicitados.First(x => x.Id == id);
                db.RepuestosCorrectivosSolicitados.Remove(a);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" });
            }
            catch (Exception ex)
            {

                return Json(new { Resultado = "Error" });
            }

        }

        public JsonResult VerProductos(string busqueda, int bodegaId = 0)
        {
            DataTable tabla = new Models.Conectar()
           .EjecutarConsultaSelect("sp_uma_mantenimiento_verProductosPorBodega",
           CommandType.StoredProcedure,
            new SqlParameter("busqueda", busqueda),
            new SqlParameter("bodegaId", bodegaId));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarActividadesMasiva(List<string> actividades, int idot)
        {


            foreach (var item in actividades)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    db.OtDetalleCorrectivo.Add(new Entidades.Uma.OtDetalleCorrectivo
                    {
                        IdOt = idot,
                        NombreActividad = item,
                        Progreso = 0,
                        Usuario = User.Identity.Name,
                        FechaCreacion = DateTime.Now

                    });
                }
            }
            db.SaveChanges();

            return Json("");
        }





        public ActionResult OtCerrada(string id)
        {
            if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" });
            ViewBag.IdUbicacion = new SelectList(db.Ubicacions.ToList(), "Id", "RutaCompleta");
            return View();
        }



        public JsonResult VerCalendarioOtCerrada(int IdUbicacion = 0)
        {
            //uma_mantenimiento_calendarioPreventivo
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_VerCalendarioOtCerrada",
              CommandType.StoredProcedure,
            new System.Data.SqlClient.SqlParameter("IdUbicacion", IdUbicacion)
              );
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public JsonResult VolverAbrirOT(int idOT = 0)
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_CambiarEstadoOT",
              CommandType.StoredProcedure,
            new System.Data.SqlClient.SqlParameter("idOT", idOT));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }



        public JsonResult VistaImpresion(int idOT = 0)
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_VistaImpresion",
              CommandType.StoredProcedure,
            new System.Data.SqlClient.SqlParameter("idOT", idOT));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public JsonResult CancelarOt(int id = 0)
        {
            DataTable tabla = new Models.Conectar()
            .EjecutarConsultaSelect("sp_uma_mantenimiento_eliminarOt ",
            CommandType.StoredProcedure,
            new System.Data.SqlClient.SqlParameter("idOt", id));
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);

        }

        public JsonResult CerrarActividadesOT(int id)
        {
            try
            {
                DataTable tabla = new Models.Conectar()
             .EjecutarConsultaSelect("sp_uma_cerrarActividadesOt",
             CommandType.StoredProcedure,
             new System.Data.SqlClient.SqlParameter("id", id));
                return Json(new { Resultado = "Guardado" });
            }
            catch (Exception ex)
            {
                return Json(new { Resultado = "Error" });
            }
        }


        public JsonResult CerrarOtOperaciones(DateTime horaAvisoOperaciones, DateTime horaIntervencionMaquina, string observaciones, int id, int idOrigen, int IdFalla)
        {
            try
            {
                var ot = db.Ot.FirstOrDefault(x => x.Id == id);
                ot.HoraAvisoOperaciones = horaAvisoOperaciones;
                ot.HoraIntervencionMaquina = horaIntervencionMaquina;
                ot.MinutosDeIntervencion = (int)horaIntervencionMaquina.Subtract(horaAvisoOperaciones).TotalMinutes;
                //ot.QuedoOperativa = quedoOperativa;
                ot.Observaciones = observaciones;
                // ot.Cerrada = true;

                //sp_uma_cerrarActividadesOt

                //var detalles = db.DetalleOt.Where(x => x.IdOt == id).ToList();

                //if (detalles != null)
                //{
                //    foreach (var d in detalles.ToList())
                //    {
                //        d.Progreso = 100;
                //    }
                //}

                var falla = db.FallasOt.FirstOrDefault(x => x.IdOt == id);
                falla.IdOrigen = idOrigen;
                falla.IdFalla = IdFalla;
                db.SaveChanges();

                //   DataTable tabla = new Models.Conectar()
                //.EjecutarConsultaSelect("sp_uma_cerrarActividadesOt",
                //CommandType.StoredProcedure,
                //new System.Data.SqlClient.SqlParameter("id", id));

                return Json(new { Resultado = "Guardado" });
            }
            catch (Exception ex)
            {
                return Json(new { Resultado = "Error" });
            }



        }

        #region servicios
        //################################### SERVICIOS #######################################


        public JsonResult BuscarPedidos(int IdPedido = 0, int idOt = 0)
        {
            DataTable tabla = new Models.Conectar()
            .EjecutarConsultaSelect("sp_uma_mantenimiento_verOcporPedidosDeServicios",
            CommandType.StoredProcedure,
            new System.Data.SqlClient.SqlParameter("idpedido", IdPedido),
            new System.Data.SqlClient.SqlParameter("idOt", idOt)
            );
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);

        }

        //GUARDAR SERVICIO
        public JsonResult GuardarServicios(int idOT, string RazonSocial, int OcDetId, string Glosa, int Cantidad, int Valor, DateTime Fecha)
        {
            try
            {
                Entidades.Uma.Servicios S = new Entidades.Uma.Servicios();
                S.IdOt = idOT;
                S.RazonSocial = RazonSocial;
                S.OcDetId = OcDetId;
                S.Glosa = Glosa;
                S.Cantidad = Cantidad;
                S.Valor = Valor;
                S.Fecha = Fecha;
                db.Servicios.Add(S);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" });

            }
            catch (Exception ex)
            {
                return Json(new { Resultado = "Error" });
            }
        }


        //ELIMINAR SERVICIO   
        public JsonResult EliminarServicio(int OcDetId)
        {
            try
            {
                Entidades.Uma.Servicios S = db.Servicios.First(x => x.Id == OcDetId);
                db.Servicios.Remove(S);
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);

            }

        }


        public JsonResult ModificarServicio(int id, int valor)
        {
            try
            {
                Entidades.Uma.Servicios S = db.Servicios.First(x => x.Id == id);
                S.Valor = valor;
                db.SaveChanges();
                return Json(new { Resultado = "Guardado" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Resultado = "Error" }, JsonRequestBehavior.AllowGet);

            }

        }

        //VER SERVICIOS

        public JsonResult CargarServiciosAgregados(int idOT)
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_VerServicios",
              CommandType.StoredProcedure,
              new System.Data.SqlClient.SqlParameter("idOT", idOT)
              );
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        //################################### VALORES SERVICIOS ########################################

        //****************** VALOR RRHH **********************
        public JsonResult ValorRRHH(int idOT)
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_SumaRRHH",
              CommandType.StoredProcedure,
              new System.Data.SqlClient.SqlParameter("idOT", idOT)
              );
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        #endregion

        //****************** VALOR REPUESTOS **********************
        public JsonResult ValorRepuestos(int idOT)
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_SumaRepuestos",
              CommandType.StoredProcedure,
              new System.Data.SqlClient.SqlParameter("idot", idOT)
              );
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        //****************** VALOR SERVICIOS **********************
        public JsonResult ValorServicio(int idOT)
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_SumaServicios",
              CommandType.StoredProcedure,
              new System.Data.SqlClient.SqlParameter("idOT", idOT)
              );
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        #region Finalizar Turno

        public ActionResult FinalizarTurno()
        {
            if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" });
            ViewBag.IdOrigenDeFalla = new SelectList(db.OrigenDeFalla, "Id", "Descripcion");
            ViewBag.IdUbicacion = new SelectList(db.Ubicacions.OrderBy(x => x.RutaCompleta).ToList(), "Id", "RutaCompleta");
            return View();
        }

        public JsonResult verOtFinalizarTurno(int verTodo=0,string idUbicacion = "1")
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_verOtFinalizarTurno",
              CommandType.StoredProcedure,
              new System.Data.SqlClient.SqlParameter("usuario", User.Identity.Name ?? "")
              , new System.Data.SqlClient.SqlParameter("todas", verTodo)
              , new System.Data.SqlClient.SqlParameter("idUbicacion", idUbicacion)

              );
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public JsonResult enviarComprobante(int idot,string usuarios)
        {
            DataTable tabla = new Models.Conectar()
              .EjecutarConsultaSelect("sp_uma_mantenimiento_enviarComprobante",
              CommandType.StoredProcedure,
              new System.Data.SqlClient.SqlParameter("id", idot),
              new System.Data.SqlClient.SqlParameter("usuarios", usuarios)
              );
            string json = JsonConvert.SerializeObject(tabla, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}