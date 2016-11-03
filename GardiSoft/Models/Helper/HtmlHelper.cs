using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace GardiSoft.Models.Helper
{

    

    public class HtmlHelper
    {

        GardiSoft.Models.GardiSoftContext db = new GardiSoftContext();

        public List<object> Meses()
        {

            List<object> meses = new List<object>();
            meses.Add(new { Valor = 1, Nombre = "Enero" });
            meses.Add(new { Valor = 2, Nombre = "Febrero" });
            meses.Add(new { Valor = 3, Nombre = "Marzo" });
            meses.Add(new { Valor = 4, Nombre = "Abril" });
            meses.Add(new { Valor = 5, Nombre = "Mayo" });
            meses.Add(new { Valor = 6, Nombre = "Junio" });
            meses.Add(new { Valor = 7, Nombre = "Julio" });
            meses.Add(new { Valor = 8, Nombre = "Agosto" });
            meses.Add(new { Valor = 9, Nombre = "Septiembre" });
            meses.Add(new { Valor = 10, Nombre = "Octubre" });
            meses.Add(new { Valor = 11, Nombre = "Noviembre" });
            meses.Add(new { Valor = 12, Nombre = "Diciembre" });


            return meses;


        }

      

        public List<object> Año()
        {
            List<object> año = new List<object>();
            año.Add((DateTime.Now.Year - 1).ToString());
            año.Add((DateTime.Now.Year ).ToString());
            año.Add((DateTime.Now.Year + 1).ToString());
            return año;
            
        }

        internal bool Permiso(HttpRequestBase request, dynamic viewBag, IPrincipal user)
        {
            //carga el Id del módulo para poder mostrar el menú
            string area = request.RequestContext.RouteData.DataTokens["area"].ToString();
            var modulo = db.Modulo.Where(x => x.Nombre == area).FirstOrDefault();
            viewBag.Modulo = (modulo == null) ? 0 : modulo.Id;

            //revisa si tiene los permisos correspondientes
            string actionName = request.RequestContext.RouteData.Values["action"].ToString();
            string controllerName = request.RequestContext.RouteData.Values["controller"].ToString();
            var usuario = db.Usuario.FirstOrDefault(x => x.Email == user.Identity.Name);
            var idUsuarioLogin = (usuario == null) ? " " : usuario.Id;
            var permisos = db.Permisos.Where(x => x.IdUsuario == idUsuarioLogin && x.SubMenu.NombreAction == actionName && x.SubMenu.NombreControlador == controllerName).FirstOrDefault();

            return (permisos == null) ? true : false;
        }

        internal int Permiso(HttpRequestBase request, dynamic viewBag)
        {
            //carga el Id del módulo para poder mostrar el menú
            string area = request.RequestContext.RouteData.DataTokens["area"].ToString();         
            var modulo = db.Modulo.Where(x => x.Nombre == area).FirstOrDefault();            
            viewBag.Modulo = (modulo == null) ? 0: modulo.Id;
            
            //revisa si tiene los permisos correspondientes
            string actionName = request.RequestContext.RouteData.Values["action"].ToString();
            string controllerName = request.RequestContext.RouteData.Values["controller"].ToString();

          //  var permisos = db.Permisos.Where(x=> x.IdUsuario ==  )
            
            return (modulo == null) ? 0 : modulo.Id;
        }

        /// <summary>
        /// Comprueba si el usuario tiene permiso para acceder a esa vista
        /// </summary>
        /// <returns></returns>
       
    }
}