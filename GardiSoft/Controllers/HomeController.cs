using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using Entidades.Uma;

namespace GardiSoft.Controllers
{
    public class HomeController : Controller
    {
        GardiSoft.Models.GardiSoftContext db = new Models.GardiSoftContext();

        public ActionResult Index(int? id = 0)
        {
            ViewBag.Modulo = id;
            var modulos = db.Modulo.ToList();
            return View(modulos);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
           
           ViewBag.Message = "Your contact page.";

            return View();
        }



        public ActionResult Menu(int? id=0)
        {
            DataTable tabla = new Models.Conectar().EjecutarConsultaSelect("sp_sys_verPermisosPorUSuario", System.Data.CommandType.StoredProcedure,
              new System.Data.SqlClient.SqlParameter("usuario", User.Identity.Name ?? "noname")
              , new System.Data.SqlClient.SqlParameter("idModulo", id));


            var menu = tabla.AsEnumerable().Select(x => new
            {
                AliasModulo = x["Alias"].ToString(),
                NombreModulo = x["nombreModulo"].ToString(),
                IdMenu = int.Parse(x["id"].ToString()),
                NombreMenu = x["Nombre"].ToString(),
                idSubmenu = int.Parse(x["Idsb"].ToString()),
                TituloSubMenu = x["titulo"].ToString(),
                ActionSubmenu = x["NombreAction"].ToString(),
                NombreControladorSubMenu = x["NombreControlador"].ToString(),
                Color = x["color"].ToString()


            })
             .GroupBy(x => new { x.AliasModulo, x.IdMenu, x.NombreMenu, x.NombreModulo,x.Color })
             .Select(x => new Entidades.Sys.Menu
             {
                 Id = x.Key.IdMenu,
                 Nombre = x.Key.NombreMenu,
                 Modulo = new Entidades.Sys.Modulo { Alias = x.Key.AliasModulo, Nombre = x.Key.NombreModulo,Color = x.Key.Color },
                 Submenu = x.Select(y => new Entidades.Sys.SubMenu { Id = y.idSubmenu, Titulo = y.TituloSubMenu, NombreAction = y.ActionSubmenu, NombreControlador = y.NombreControladorSubMenu }).ToList()

             });
           // var menu = db.Menu.Include(x => x.Submenu).Include(x=> x.Modulo).Where(x => x.Modulo.Id == id).ToList();            
            return PartialView("_Menu",menu);
        }

        public ActionResult Submenu(int? id = 0)
        {


           // var menu = db.Menu.Include(x => x.Submenu).Include(x => x.Modulo).Where(x => x.Modulo.Id == id).ToList();

            DataTable tabla = new Models.Conectar().EjecutarConsultaSelect("sp_sys_verPermisosPorUSuario", System.Data.CommandType.StoredProcedure, 
                new System.Data.SqlClient.SqlParameter("usuario", User.Identity.Name??"noname")
                , new System.Data.SqlClient.SqlParameter("idModulo", id));


           var menu =  tabla.AsEnumerable().Select(x => new
            {
                AliasModulo = x["Alias"].ToString(),
               NombreModulo = x["nombreModulo"].ToString(),
               IdMenu = int.Parse(x["id"].ToString()),
                NombreMenu = x["Nombre"].ToString(),
                idSubmenu = int.Parse(x["Idsb"].ToString()),
                TituloSubMenu = x["titulo"].ToString(),
                ActionSubmenu = x["NombreAction"].ToString(),
                NombreControladorSubMenu = x["NombreControlador"].ToString(),
               Color = x["color"].ToString()


           })
            .GroupBy(x => new { x.AliasModulo, x.IdMenu, x.NombreMenu,x.NombreModulo,x.Color })
            .Select(x => new Entidades.Sys.Menu
            {
                Id = x.Key.IdMenu,
                Nombre = x.Key.NombreMenu,
                Modulo = new Entidades.Sys.Modulo { Alias = x.Key.AliasModulo,Nombre = x.Key.NombreModulo,Color = x.Key.Color },
                Submenu = x.Select(y => new Entidades.Sys.SubMenu { Id = y.idSubmenu, Titulo = y.TituloSubMenu, NombreAction = y.ActionSubmenu, NombreControlador = y.NombreControladorSubMenu }).ToList()

            });




            ViewBag.Id = id;
            return PartialView("_Submenu", menu);
        }


        public ActionResult AccesoDenegado()
        {
            return View();
        }
    }
}