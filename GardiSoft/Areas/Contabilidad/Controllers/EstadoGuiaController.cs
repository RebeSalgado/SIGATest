using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Entidades.Contabilidad.GuiaDespacho;

namespace GardiSoft.Areas.Contabilidad.Controllers
{
   [Authorize]
    public class EstadoGuiaController : Controller
    {

        GardiSoft.Models.GardiSoftContext db = new Models.GardiSoftContext();

        // GET: Contabilidad/EstadoGuia
        public ActionResult Index(int? id = 0)
        {
             if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" }); 
            var eventos = db.Evento.Include(x => x.Situacion).Where(x => x.GuiaDespacho.FolioDocumento == id.ToString()).OrderBy(x=> x.Fecha).ToList();
            return View(eventos);
        }



        [Authorize]
        public ActionResult IngresarEvento(string mensaje)
        {
             if (new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag, User)) return RedirectToAction("AccesoDenegado", "Home", new { Area = "" }); 
            ViewBag.Mensaje = mensaje;
            ViewBag.IdSituacion = new SelectList(db.Situacions.ToList(), "Id", "Nombre");
            return View();
        }

        [Authorize]
        public ActionResult GuardarEvento(Evento evento,string folio)
        {

            try
            {
                var id = new Models.Helper.HtmlHelper().Permiso(this.Request, this.ViewBag);
                ViewBag.IdSituacion = new SelectList(db.Situacions.ToList(), "Id", "Nombre");
                evento.ContieneImagen = false;
                evento.Fecha = DateTime.Now;
                evento.Usuario = User.Identity.Name;
                var guiaId = db.GuiaDespachoes.FirstOrDefault(x => x.FolioDocumento == folio).Id;
                evento.IdCabecera = guiaId;
                db.Evento.Add(evento);
                db.SaveChanges();

                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        // var fileName = Path.GetFileName(file.FileName);
                        var path = Server.MapPath("~/ArchivosUpload/Contabilidad/GuiaDespacho/" + evento.Id + ".jpg");
                        file.SaveAs(path);
                        evento.ContieneImagen = true;
                        db.SaveChanges();
                    }
                }


                return RedirectToAction("Index", "Home", new { area = "", Id = id });
            }
            catch (Exception)
            {

                return RedirectToAction("IngresarEvento", "EstadoGuia", new { area = "Contabilidad", mensaje = "error"  });


            }
           
        }
    }
}