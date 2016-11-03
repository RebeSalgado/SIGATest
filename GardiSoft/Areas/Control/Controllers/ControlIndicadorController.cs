using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Entidades.Control;
using GardiSoft.Models;
using Newtonsoft.Json;

namespace GardiSoft.Areas.Control.Controllers
{
    public class ControlIndicadorController : Controller
    {
        private GardiSoftContext db = new GardiSoftContext();
        
        // GET: Control/ControlIndicador

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult ListarControl()
        {
            var control_Registro = db.Control_Registro.Include(c => c.Indicador).Include(c => c.Usuario);
            return PartialView(control_Registro.ToList());
        }

        // GET: Control/ControlIndicador/Details/5
        public ActionResult DetallesControl(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ControlIndicador controlIndicador = db.Control_Registro.Find(id);
            if (controlIndicador == null)
            {
                return HttpNotFound();
            }
            return View(controlIndicador);
        }

        // GET: Control/ControlIndicador/Create
        
        public ActionResult CrearControl()
        {
            ViewBag.IdIndicador = new SelectList(db.Indicadors, "Id_Indicador", "Nombre");
            ViewBag.IdUsuario = new SelectList(db.Usuario, "Id", "UserName");
            return PartialView();
        }

        // POST: Control/ControlIndicador/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearControl([Bind(Include = "Id_Control,IdIndicador,Nreal,Desviacion,Fecha,Meta,IdUsuario")] ControlIndicador controlIndicador)
        {
            if (ModelState.IsValid)
            {
                db.Control_Registro.Add(controlIndicador);
                db.SaveChanges();
                
            }

            ViewBag.IdIndicador = new SelectList(db.Indicadors, "Id_Indicador", "Nombre", controlIndicador.IdIndicador);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "Id", "UserName", controlIndicador.IdUsuario);
            return PartialView(controlIndicador);
        }

        // GET: Control/ControlIndicador/Edit/5
        public ActionResult EditarControl(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ControlIndicador controlIndicador = db.Control_Registro.Find(id);
            if (controlIndicador == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdIndicador = new SelectList(db.Indicadors, "Id_Indicador", "Nombre", controlIndicador.IdIndicador);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "Id", "UserName", controlIndicador.IdUsuario);
            return View(controlIndicador);
        }

        // POST: Control/ControlIndicador/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarControl([Bind(Include = "Id_Control,IdIndicador,Nreal,Desviacion,Fecha,Meta,IdUsuario")] ControlIndicador controlIndicador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(controlIndicador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdIndicador = new SelectList(db.Indicadors, "Id_Indicador", "Nombre", controlIndicador.IdIndicador);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "Id", "UserName", controlIndicador.IdUsuario);
            return View(controlIndicador);
        }

        // GET: Control/ControlIndicador/Delete/5
        public ActionResult BorrarControl(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ControlIndicador controlIndicador = db.Control_Registro.Find(id);
            if (controlIndicador == null)
            {
                return HttpNotFound();
            }
            return View(controlIndicador);
        }

        // POST: Control/ControlIndicador/Delete/5
        [HttpPost, ActionName("BorrarControl")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ControlIndicador controlIndicador = db.Control_Registro.Find(id);
            db.Control_Registro.Remove(controlIndicador);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        /*Metodo Linq para listar los datos por nombre del indicador*/
        public ActionResult ListaPorIndicador(string IdIndicador, string fecha)
        {

            ViewBag.fecha = new SelectList(db.Control_Registro , "Id_Control", "fecha");

            IEnumerable<ControlIndicador> lista = from c in db.Control_Registro
                                                  join i in db.Indicadors
                                                  on c.IdIndicador equals i.Id_Indicador
                                                  where i.Nombre == IdIndicador
                                                  select c;

            if (!string.IsNullOrEmpty(fecha))
            {
                int f = Convert.ToInt32(fecha);
                lista = from c in db.Control_Registro
                        where c.Id_Control == f
                        select c;



                return View(lista.ToList());

            }
            else
            {
                return View(lista.ToList());
            }

        }
    }
}