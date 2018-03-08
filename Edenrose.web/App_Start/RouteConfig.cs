using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Edenrose.web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
             name: "IndexPage",
             url: "tin-tuc/page/{pageindex}",
             defaults: new { controller = "TinTuc", action = "Index", id = UrlParameter.Optional },
             namespaces: new[] { "Edenrose.web.Controllers" }
             );
            routes.MapRoute(
              name: "Index",
              url: "tin-tuc",
              defaults: new { controller = "TinTuc", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "Edenrose.web.Controllers" }
              );


            routes.MapRoute(
              name: "Article",
              url: "tin-tuc/{url}",
              defaults: new { controller = "TinTuc", action = "Details", id = UrlParameter.Optional },
              namespaces: new[] { "Edenrose.web.Controllers" }
              );

            routes.MapRoute(
                "Default",
                "{controller}/{action}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Edenrose.web.Controllers" }
            );
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
          
          }
    }
}
