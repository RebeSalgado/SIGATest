using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using Ionic.Zip;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Threading;
using System.Xml.Linq;

namespace GardiSoft.Areas.Contabilidad.Controllers
{
    [Authorize]
    public class GuiaDespachoIngresoController : Controller
    {
        // GET: Contabilidad/GuiaDespachoIngreso

        GardiSoft.Models.GardiSoftContext db = new Models.GardiSoftContext();

        public ActionResult VerGuias(int? id = 0)
        {

            new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag);

            var guia = db.GuiaDespachoes.Include(x => x.Referencias).Include(x => x.Detalles).Where(x => x.Id == id).FirstOrDefault();

         

            List<object> TipoDespacho = new List<object>();
            TipoDespacho.Add(new { Nombre = "Despacho por cuenta del receptor", Value = 1 });
            TipoDespacho.Add(new { Nombre = "Despacho por cuenta emisor a instalaciones del cliente ", Value = 2 });
            TipoDespacho.Add(new { Nombre = "Despacho por cuenta emisor a otras instalaciones ", Value = 3 });
            ViewBag.TipoDespacho = new SelectList(TipoDespacho, "Value", "Nombre", (guia == null) ? "3" : guia.TipoDespacho);

            List<object> FormaDePago = new List<object>();
            FormaDePago.Add(new { Nombre = "Contado", Value = 1 });
            FormaDePago.Add(new { Nombre = "Crédito", Value = 2 });
            ViewBag.FormaDePago = new SelectList(FormaDePago, "Value", "Nombre", (guia == null) ? "1" : guia.FormaDePago);

            List<object> TipoDocumento = new List<object>();
            TipoDocumento.Add(new { Nombre = "Guía Despacho", Value = 52 });
            ViewBag.TipoDocumento = new SelectList(TipoDocumento, "Value", "Nombre");

            List<object> TipoTraslado = new List<object>();
            TipoTraslado.Add(new { Nombre = "Operación constituye venta $$$", Value = "1" });
            TipoTraslado.Add(new { Nombre = " Traslados internos", Value = "5" });
            TipoTraslado.Add(new { Nombre = "Traslado No venta", Value = "6" });
            TipoTraslado.Add(new { Nombre = "Devolución", Value = "7" });
            ViewBag.IndicadorTipoTraslado = new SelectList(TipoTraslado, "Value", "Nombre", (guia == null) ? "5" : guia.IndicadorTipoTraslado);

            List<object> TipoReferencia = new List<object>();
            TipoReferencia.Add(new { Nombre = "Ninguno", Value = "0" });
            TipoReferencia.Add(new { Nombre = "30 Factura", Value = "30" });
            TipoReferencia.Add(new { Nombre = "32 Factura de bienes y serv. Exentos", Value = "32" });
            TipoReferencia.Add(new { Nombre = "33 Factura electronica", Value = "33" });
            TipoReferencia.Add(new { Nombre = "34 Factura elec. exenta", Value = "34" });
            TipoReferencia.Add(new { Nombre = "50 guía despacho ", Value = "50" });
            TipoReferencia.Add(new { Nombre = "61 Nota de crédito electronica", Value = "61" });
            TipoReferencia.Add(new { Nombre = "801 órden de compra", Value = "801" });
            TipoReferencia.Add(new { Nombre = "802 Nota de pedido", Value = "802" });
            TipoReferencia.Add(new { Nombre = "803 Contrato", Value = "803" });
            ViewBag.TipoReferencia = new SelectList(TipoReferencia, "Value", "Nombre");

            List<object> TipoDetalle = new List<object>();
            TipoDetalle.Add(new { Value = "1", Nombre = "Exento de IVA" });
            TipoDetalle.Add(new { Value = "2", Nombre = "Afecto de IVA" });
            TipoDetalle.Add(new { Value = "4", Nombre = "Observaciones" });
            ViewBag.TipoDetalleData = TipoDetalle;
            ViewBag.TipoDetalle = new SelectList(TipoDetalle, "Value", "Nombre");


            if (guia == null)
            {
                guia = new Entidades.Contabilidad.GuiaDespacho.GuiaDespacho();
                guia.MontoExento = "0";
                guia.MontoIva = "0";
                guia.MontoRetencion = "0";
                guia.MontoTotal = "0";
                guia.PorcentajeRetencion = "0";
                guia.Subtotal = "0";
                guia.TotalAPagar = "0";
                guia.TotalDescRec = "0";
                guia.totalNeto = "0";

            }
            return View(guia);
        }

        public JsonResult Create(Entidades.Contabilidad.GuiaDespacho.GuiaDespacho g, bool? aviso = false)
        {
            // var t = db.Database.BeginTransaction();
            string resultado = "exito";
            try
            {
                g.Usuario = User.Identity.Name;
                g.rutEmisor = "79538350-6";
                g.RazonSocial = "CONSTRUCTORA GARDILCIC LTDA.";
                g.GiroNegocio = "Extracción de cobre";
                g.CodigoActividadEconomica = "133000";
                g.CodigoSucursalSII = "";
                g.TotalAPagar = g.MontoTotal;
                g.PorcentajeRetencion = "0";
                g.MontoRetencion = "0";
                g.Fecha = DateTime.Now;
                                             

               if(aviso == true)
                {
                    g.Estado = "En Espera";
                }



                if (g.Referencias != null)
                    foreach (Entidades.Contabilidad.GuiaDespacho.Referencia item in g.Referencias)
                    {
                        db.Entry(item).State = item.Id == 0 ? EntityState.Added : EntityState.Modified;

                    }

                if (g.Detalles != null)
                    foreach (Entidades.Contabilidad.GuiaDespacho.Detalle item in g.Detalles)
                    {
                        db.Entry(item).State = item.Id == 0 ? EntityState.Added : EntityState.Modified;

                    }

                db.Entry(g).State = g.Id == 0 ? EntityState.Added : EntityState.Modified;



                db.SaveChanges();

                if (aviso??false)
                {
                    new Models.Conectar().EjecutarConsultaSelect("sp_uma_mantenimiento_emailNuevaOt", System.Data.CommandType.StoredProcedure, new SqlParameter("id", g.Id));
                }
                // t.Commit();
            }
            catch (Exception ex)
            {
                //t.Rollback();
                resultado = "error";

            }


            return Json(new { result = resultado }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult CreateAdmin(Entidades.Contabilidad.GuiaDespacho.GuiaDespacho g,string mensaje)
        {


            // var t = db.Database.BeginTransaction();
            string resultado = "exito";
            try
            {
                g.rutEmisor = "";
                g.RazonSocial = "";
                g.GiroNegocio = "";
                g.CodigoActividadEconomica = "133000";
                g.CodigoSucursalSII = "";
                g.TotalAPagar = g.MontoTotal;
                g.PorcentajeRetencion = "0";
                g.MontoRetencion = "0";
                g.Fecha = DateTime.Now;
             

                if (g.Referencias != null)
                    foreach (Entidades.Contabilidad.GuiaDespacho.Referencia item in g.Referencias)
                    {
                        db.Entry(item).State = item.Id == 0 ? EntityState.Added : EntityState.Modified;

                    }

                if (g.Detalles != null)
                    foreach (Entidades.Contabilidad.GuiaDespacho.Detalle item in g.Detalles)
                    {
                        db.Entry(item).State = item.Id == 0 ? EntityState.Added : EntityState.Modified;

                    }

                db.Entry(g).State = g.Id == 0 ? EntityState.Added : EntityState.Modified;

               
                db.SaveChanges();
                if (g.Estado == "Ingresado") //la volvieron a este estado para avisar al usuario, así que se notifica por correo
                {

                    new Models.Conectar().EjecutarConsultaSelect("sp_uma_mantenimiento_emailCambioEstadoGuiaNotificacion", System.Data.CommandType.StoredProcedure, new SqlParameter("id", g.Id), new SqlParameter("mensaje", "Esperando las siguientes correcciones" + ": " + mensaje));
                }
                if(g.Estado == "Rechazado")
                {
                    new Models.Conectar().EjecutarConsultaSelect("sp_uma_mantenimiento_emailCambioEstadoGuiaNotificacion", System.Data.CommandType.StoredProcedure, new SqlParameter("id", g.Id),new SqlParameter("mensaje",g.Estado + ": " + mensaje));
                }
                // t.Commit();
            }
            catch (Exception ex)
            {
                //t.Rollback();
                resultado = "error";

            }
            if (g.Estado == "Aprobada")
            {
                var ruta = GenerarArchivo(g.Id);

                return Json( ruta, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = "" }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Index()
        {

             if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" }); 
            var r = db.GuiaDespachoes.OrderByDescending(x=> x.Fecha).ToList();        

            return View(r);

        }

        public object GenerarArchivo(int id)
        {

            
            var objeto = db.GuiaDespachoes.Include(x => x.Referencias).Include(x => x.Detalles).FirstOrDefault(x => x.Id == id);

            //correccion del archivo según formato SII
            if (objeto.Referencias != null)
            {
                foreach (var item in objeto.Referencias) //si la referencia es cero, todo es cero
                {
                    if (item.TipoReferencia == "0")
                    {
                        item.Folio = "0";
                        item.Fecha = "";
                    }

                }
            }
            if(objeto.Detalles != null)
            {
                foreach (var item in objeto.Detalles) //si el detalle es cero no lleva valores
                {
                    if (item.IndicadorExencion == "4")
                    {
                        item.CodigoInternoItem = string.IsNullOrWhiteSpace(item.CodigoInternoItem)?"1":item.CodigoInternoItem;
                        item.UnidadMedida = string.IsNullOrWhiteSpace(item.UnidadMedida)?"1":item.UnidadMedida.Substring(0, item.UnidadMedida.Length > 4 ? 4 : item.UnidadMedida.Length); //máximo 4 caracteres
                        item.DescuentoEnPesos = "";
                        item.DescuentoEnPorcentaje = "";
                        item.MontoItem = "0";
                      
                        item.PrecioUnitarioItem = "";
                        item.RecargoPesos = "";
                        item.RecargoPorcentaje = "";
                        

                    }

                    if(item.IndicadorExencion == "1" || item.IndicadorExencion == "2") //van los valores, pero no el inidicador, extraño pero funciona
                    {
                        item.IndicadorExencion = "";
                    }

                }
            }

            var fechaEmision = DateTime.Parse(objeto.FechaEmision);

            string encabezado = "52" + "|" + objeto.FolioDocumento.Trim() + '|' +  objeto.FechaEmision.Trim() + '|' + objeto.TipoDespacho.Trim() + '|'
                 + objeto.FormaDePago.Trim() + '|' + objeto.FechaVencimiento.Trim() + '|' + objeto.IndicadorTipoTraslado.Trim() + '|' + ((objeto.Observaciones == null)? "": objeto.Observaciones.Trim()) + '|' + objeto.FechaEmision + "T00:00:00|}";

            string emisor = "79538350-6|CONSTRUCTORA GARDILCIC LTDA.|Extraccion de cobre|133000||" + objeto.DireccionOrigen.Trim() + '|' + objeto.ComunaOrigen.Trim() + '|' + objeto.CiudadOrigen.Trim() + "||}";
            string receptor = objeto.RutReceptor.Trim() + '|' + ((objeto.CodigoInternoReceptor == null)?"":objeto.CodigoInternoReceptor.Trim()) + '|' + objeto.GiroReceptor.Trim() + '|' + objeto.GiroReceptor.Trim() + '|' + objeto.DireccionReceptor.Trim() + '|' + objeto.ComunaReceptor.Trim() + '|' + objeto.CiudadReceptor.Trim() + '|' + ((objeto.emalReceptor== null)?"":objeto.emalReceptor.Trim()) + "|}";

            string totales = objeto.Subtotal.Trim() + '|' + objeto.TotalDescRec.Trim() + '|' + objeto.totalNeto.Trim() + '|' + objeto.MontoExento.Trim() + '|' + "19" + '|' + objeto.MontoIva.Trim() + '|' + objeto.MontoTotal.Trim() + "|"+ new Models.Helper.NumerosHelper().enletras(objeto.TotalAPagar) + "|" + objeto.PorcentajeRetencion.Trim() + '|' + objeto.MontoRetencion.Trim() + '|' + objeto.TotalAPagar.Trim() + "|}~";

            List<string> lista = new List<string>();

            lista.Add(encabezado);

            lista.Add(emisor);

            lista.Add(receptor);

            lista.Add(totales);
            int i = 0;
            foreach (var item in objeto.Detalles)
            {
              //  lista.Add( item.IndicadorExencion + '|' + item.CodigoInternoItem + '|' + item.NombreItem + "||" + item.CantidadItem + '|' + item.UnidadMedida + '|' + item.PrecioUnitarioItem + '|' + item.DescuentoEnPorcentaje + '|' + item.DescuentoEnPesos + '|' + item.RecargoPorcentaje + '|' + item.RecargoPesos + '|' + item.MontoItem + "|}"+((i + 1) == objeto.Detalles.Count ? "~" : ""));
                lista.Add(item.IndicadorExencion.Trim() + '|' + item.CodigoInternoItem.Trim() + '|' + item.NombreItem.Trim() + "||" + item.CantidadItem.Trim() + '|' + item.UnidadMedida.Trim() + '|' +  item.PrecioUnitarioItem.Trim() + '|' + item.DescuentoEnPorcentaje.Trim() + '|' + item.DescuentoEnPesos.Trim() + '|' +  item.RecargoPorcentaje.Trim() + '|' +  item.RecargoPesos.Trim() + '|' + item.MontoItem.Trim() + "|}" + ((i + 1) == objeto.Detalles.Count ? "~" : ""));
                i++;
            }
           

            i = 0;
            foreach (var item in objeto.Referencias)
            {
                lista.Add(item.TipoReferencia + '|' + (i + 1) + '|' + item.Folio + '|' + item.Fecha + "||}" + ((i+1)== objeto.Referencias.Count ?"~":"")  );
                i++;
            }
           

         
        

            string patente = objeto.Patente.Trim() + '|' + objeto.RutTransportista.Trim() + '|' + objeto.DireccionDestino.Trim() + '|' + objeto.ComunaDestino.Trim() + '|' + objeto.CiudadDestino.Trim() + "|}}~!";

            lista.Add(patente);
            var archivo = objeto.FolioDocumento + "-" + Guid.NewGuid() + ".txt";
            var ruta = Server.MapPath("~/ArchivosExcel/contabilidad/") + archivo;

            
            System.IO.File.WriteAllLines(ruta, lista, new System.Text.UTF8Encoding(false));
            System.IO.File.Copy(ruta, @"\\10.20.24.90\c$\AceptaService\Download\" + archivo); //ruta disponible en máquina virtual

            Thread.Sleep(5000);


            var file = new DirectoryInfo(@"\\10.20.24.90\c$\AceptaService\gardilcic\var\ca4xml\output\dump")
               .GetFiles(objeto.FolioDocumento.ToString()+"*.*", System.IO.SearchOption.AllDirectories)
               .OrderByDescending(x => x.LastWriteTime).FirstOrDefault();


            var a = XElement.Load(file.FullName);
            var n1 = a.Element("output-doc").Element("Document").Element("Log").Element("Process");
            var uri = n1.Elements("item").Where(x => x.Attribute("name").Value == "custodium-uri").FirstOrDefault();
            var errores = a.Element("error-manager").HasElements;          
            var r = uri.Value;


                                   
            return new { Error = errores, Resultado = (errores)? a.Element("error-manager").Value : r, } ;

        }

        public ActionResult IndexAdmin()
        {
            if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" });
            var r = db.GuiaDespachoes.OrderByDescending(x => x.Fecha).ToList();
            return View(r);

        }

        public ActionResult VerGuiasAdmin(int? id = 0)
        {
           
            new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag);

            var guia = db.GuiaDespachoes.Include(x => x.Referencias).Include(x => x.Detalles).Where(x => x.Id == id).FirstOrDefault();
            

            List<object> TipoDespacho = new List<object>();
            TipoDespacho.Add(new { Nombre = "Despacho por cuenta del receptor", Value = 1 });
            TipoDespacho.Add(new { Nombre = "Despacho por cuenta emisor a instalaciones del cliente ", Value = 2 });
            TipoDespacho.Add(new { Nombre = "Despacho por cuenta emisor a otras instalaciones ", Value = 3 });
            ViewBag.TipoDespacho = new SelectList(TipoDespacho, "Value", "Nombre",(guia == null)? "3":guia.TipoDespacho);

            List<object> FormaDePago = new List<object>();
            FormaDePago.Add(new { Nombre = "Contado", Value = 1 });
            FormaDePago.Add(new { Nombre = "Crédito", Value = 2 });
            ViewBag.FormaDePago = new SelectList(FormaDePago, "Value", "Nombre", (guia == null) ? "1" : guia.FormaDePago);

            List<object> TipoDocumento = new List<object>();
            TipoDocumento.Add(new { Nombre = "Guía Despacho", Value = 52 });
            ViewBag.TipoDocumento = new SelectList(TipoDocumento, "Value", "Nombre");

            List<object> TipoTraslado = new List<object>();
            TipoTraslado.Add(new { Nombre = "Operación constituye venta $$$", Value = "1" });
            TipoTraslado.Add(new { Nombre = " Traslados internos", Value = "5" });
            TipoTraslado.Add(new { Nombre = "Traslado No venta", Value = "6" });
            TipoTraslado.Add(new { Nombre = "Devolución", Value = "7" });
            ViewBag.IndicadorTipoTraslado = new SelectList(TipoTraslado, "Value", "Nombre", (guia == null) ? "5" : guia.IndicadorTipoTraslado);

            List<object> TipoReferencia = new List<object>();
            TipoReferencia.Add(new { Nombre = "Ninguno", Value = "0" });
            TipoReferencia.Add(new { Nombre = "30 Factura", Value = "30" });
            TipoReferencia.Add(new { Nombre = "32 Factura de bienes y serv. Exentos", Value = "32" });
            TipoReferencia.Add(new { Nombre = "33 Factura electronica", Value = "33" });
            TipoReferencia.Add(new { Nombre = "34 Factura elec. exenta", Value = "34" });
            TipoReferencia.Add(new { Nombre = "50 guía despacho ", Value = "50" });
            TipoReferencia.Add(new { Nombre = "61 Nota de crédito electronica", Value = "61" });
            TipoReferencia.Add(new { Nombre = "801 órden de compra", Value = "801" });
            TipoReferencia.Add(new { Nombre = "802 Nota de pedido", Value = "802" });
            TipoReferencia.Add(new { Nombre = "803 Contrato", Value = "803" });
            ViewBag.TipoReferencia = new SelectList(TipoReferencia, "Value", "Nombre");

            List<object> TipoDetalle = new List<object>();
            TipoDetalle.Add(new { Value = "1", Nombre = "Exento de IVA" });
            TipoDetalle.Add(new { Value = "2", Nombre = "Afecto de IVA" });
            TipoDetalle.Add(new { Value = "4", Nombre = "Observaciones" });
            ViewBag.TipoDetalle = new SelectList(TipoDetalle, "Value", "Nombre");


            if (guia == null)
            {
                guia = new Entidades.Contabilidad.GuiaDespacho.GuiaDespacho();
                guia.MontoExento = "0";
                guia.MontoIva = "0";
                guia.MontoRetencion = "0";
                guia.MontoTotal = "0";
                guia.PorcentajeRetencion = "0";
                guia.Subtotal = "0";
                guia.TotalAPagar = "0";
                guia.TotalDescRec = "0";
                guia.totalNeto = "0";

            }
            else //le asigna el folio si corresponde
            {
                if(string.IsNullOrWhiteSpace(guia.FolioDocumento))
                {
                    var folio = db.Folio.Where(x => x.Utilizado == false).OrderBy(x => x.Folio).FirstOrDefault();
                    guia.FolioDocumento = folio == null? null :folio.Folio.ToString();
                    if(folio != null)
                    {
                        folio.Utilizado = true;
                        db.SaveChanges();
                    }

                }


                
            }

            return View(guia);

        }

        public JsonResult EliminaReferencia(int? id= 0)
        {
            var referencia = db.Referencia.FirstOrDefault(x => x.Id == id);
            if (referencia != null)
            {
                db.Entry(referencia).State = EntityState.Deleted;
                db.SaveChanges();
            }
            return Json(new { Resultado = "exito" });
        }
        
        public JsonResult EliminaDetalle(int? id = 0)
        {
            var detalle = db.Detalle.FirstOrDefault(x => x.Id == id);
            if (detalle != null)
            {
                db.Entry(detalle).State = EntityState.Deleted;
                db.SaveChanges();
            }
            return Json(new { Resultado = "exito" });
        }

        public string VerDireccionesReceptor(string rut)
        {
           var table =  new Models.Conectar().EjecutarConsultaSelect("sp_cont_guia_despacho_receptorDirecciones",System.Data.CommandType.StoredProcedure,new SqlParameter("rut",rut));
            return JsonConvert.SerializeObject(table, Formatting.Indented);
        }



        
    }
}