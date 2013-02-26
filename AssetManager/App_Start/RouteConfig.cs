using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace AssetManager
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            routes.MapRoute(
                "ControllerOnly",
                "{controller}",
                new { language = "en", controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                "ControllerWithAction",
                "{controller}/{action}",
                new { language = "en", controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                "Default",
                "{language}/{controller}/{action}",
                new { controller = "Home", action = "Index"}
            );

            /*
            routes.MapRoute("WithParam",
             "{controller}/{action}/{*id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });*/

        }

    }
}