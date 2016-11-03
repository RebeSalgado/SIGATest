using System.Web.Mvc;

namespace GardiSoft.Areas.Maquinaria
{
    public class MaquinariaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Maquinaria";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Maquinaria_default",
                "Maquinaria/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}