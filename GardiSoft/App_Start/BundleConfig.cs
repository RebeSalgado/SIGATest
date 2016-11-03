using System.Web;
using System.Web.Optimization;

namespace GardiSoft
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            //Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            //preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));


            bundles.Add(new ScriptBundle("~/bundles/graficos").Include(
                "~/Scripts/Fusioncharts/fusioncharts.js",
                "~/Scripts/Fusioncharts/fusioncharts.charts.js",
                "~/Scripts/Fusioncharts/themes/fusioncharts.theme.fint.js"));

            bundles.Add(new ScriptBundle("~/bundles/calendario").Include(
                 "~/Scripts/FullCalendar/moment.min.js",
                 "~/Scripts/FullCalendar/fullcalendar.js",
                        "~/Scripts/FullCalendar/fullcalendar.min.js",
                   "~/Scripts/FullCalendar/lang-all.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/handsomeTable").Include
                ("~/Scripts/handsontable.full.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/switch").Include(
                 "~/Scripts/BootstrapSwitch/bootstrap-switch.min.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/imprimir").Include(
                "~/Scripts/print/jQuery.print.js"
              ));


            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                   //  "~/Scripts/FullCalendar/moment.min.js",
              //  "~/Scripts/FullCalendar/jquery.min.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-typeahead.js",
                      "~/Scripts/jquery.validate.min.js",
                      "~/Scripts/jquery.unobtrusive-ajax.min.js",
                      "~/Scripts/jquery.validate.unobtrusive.min.js",
                      "~/Scripts/pnotify.custom.js",                     
                      "~/Scripts/knockout-2.2.1.js",
                      "~/Scripts/globalize.js",
                     // "~/Scripts/handsontable.full.min.js",
                      "~/Scripts/datepicker/bootstrap-datepicker.min.js",
                      "~/Scripts/datepicker/bootstrap-datepicker.es.min.js",
                      "~/Scripts/utilitarios.js",
                      "~/Scripts/datatables.min.js",
                      "~/Scripts/jszip.min.js",
                      "~/Scripts/buttons.html5.min.js",
                      "~/Scripts/jquery.dataTables.columnFilter.js"

                     // "~/Scripts/BootstrapSwitch/bootstrap-switch.min.js"
                    //  "~/Scripts/FullCalendar/fullcalendar.js",
                    //  "~/Scripts/FullCalendar/fullcalendar.min.js",
                    //  "~/Scripts/print/jQuery.print.js"
                    //  "~/Scripts/FullCalendar/lang-all.js"
                      //"~/Scripts/Fusioncharts/fusioncharts.js",
                      // "~/Scripts/Fusioncharts/themes/fusioncharts.theme.fint.js"
                       ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/bootstrap.css",
                 "~/Content/site.css",
                  "~/Content/pnotify.custom.css",
                  "~/Content/datatables.min.css",
                      "~/Content/datePicker/bootstrap-datepicker3.standalone.min.css",
                      "~/Content/handsontable.full.min.css",
                      "~/Content/font-awesome/css/font-awesome.css",
                      "~/Content/font-awesome/css/font-awesome.min.css",
                      "~/Content/bootstrapSwitch/bootstrap-switch.css",
                      "~/Content/fullcalendar.css",
                      "~/Content/FullCalendar/fullcalendar.min.css"
                      //,
                      //"~/Content/FullCalendar/fullcalendar.print.css"
                      ));

           

        }
    }
}
