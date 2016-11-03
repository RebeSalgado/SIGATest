using System.Web.Mvc;

namespace GardiSoft.Areas.Rrhh
{
    public class RrhhAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Rrhh";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Rrhh_default",
                "Rrhh/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}