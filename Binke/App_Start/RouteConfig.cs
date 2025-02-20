﻿using System.Web.Mvc;
using System.Web.Routing;

namespace Binke
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();


            // This Is Search Details Url route
            routes.MapRoute(
                "Search",                                           // Route name
                "Search/{type}/{search}",                            // URL with parameters
                new { controller = "Search", action = "Index", type = UrlParameter.Optional, search = UrlParameter.Optional },// Parameter defaults
                new string[] { "Binke.Controllers.Search" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
