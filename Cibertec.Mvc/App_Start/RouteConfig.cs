using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Cibertec.Mvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "SociosDeNegocio",
                url: "SociosDeNegocio/{id}/{action}",
                defaults: new { controller = "Customer", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "OrdenDeNegocio",
                url: "OrdenDeNegocio/{id}/{action}",
                defaults: new { controller = "Orders", action = "Index", id = UrlParameter.Optional }
            );

            //MapRoute de CRUD
            routes.MapRoute(
                name: "DefaultCRUD",
                url: "{controller}/{id}/{action}"
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
