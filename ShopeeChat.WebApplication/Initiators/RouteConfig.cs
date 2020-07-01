using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopeeChat.WebApplication.Initiators
{
    public class RouteConfig
    {
        public static void RegisterRoutes(IRouteBuilder routes)
        {
            routes.MapRoute(
                "dang-xuat",
                "dang-xuat",
                new { controller = "Account", action = "Logout" }
            );
            routes.MapRoute(
                "dang-nhap",
                "dang-nhap",
                new { controller = "Account", action = "Login" }
            );
            routes.MapRoute(
               "default?",
               "{controller}/{action}/{id?}",
               new { controller = "Home", action = "Index" }
           );
        }
    }
}
