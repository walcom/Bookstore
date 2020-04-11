using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BookStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null, "",   // url/
                new { Controller = "Book", Action = "ListAll", specilization = (string)null, pageno = 1 }
                );


            routes.MapRoute(            // url/BookListPage2
             name: null,
             url: "BookListPage{pageno}",
             defaults: new { controller = "Book", action = "ListAll", specilization = (string)null, pageno = 1 }
         );


            //                              url/specilization_name
            routes.MapRoute(null, "{specilization}",
              new { Controller = "Book", Action = "List", pageno = 1 }
              );


            //                            url/specilization_name/Page2
            routes.MapRoute(null, "{specilization}/Page{pageno}",
               new { Controller = "Book", Action = "List" }, new { pageno = @"\d+" }
               );



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Book", action = "ListAll", id = UrlParameter.Optional }
            );
        }

    }
}
