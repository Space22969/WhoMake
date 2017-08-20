using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WhoMake
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
            name: "Tasks",
            url: "{action}/{id}",
            defaults: new { controller = "Main", action = "Tasks",
                category_id = UrlParameter.Optional,
                service_id = UrlParameter.Optional,
                name = UrlParameter.Optional }
        );


            routes.MapRoute(
                name: "Default",
                url: "{action}/{id}",
                defaults: new { controller = "Main", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
