using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TDShop.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*botdetect}", new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
               name: "Liên Hệ",
               url: "lien-he",
               defaults: new { controller = "Contact", action = "Index" },
               namespaces: new[] { "TDShop.Web.Controllers" }
            );


            routes.MapRoute(
               name: "Trang",
               url: "trang/{alias}",
               defaults: new { controller = "Page", action = "Index", alias = UrlParameter.Optional },
               namespaces: new[] { "TDShop.Web.Controllers" }
            );

            routes.MapRoute(
               name: "Login",
               url: "dang-nhap",
               defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
               namespaces: new[] { "TDShop.Web.Controllers" }
            );
            routes.MapRoute(
               name: "Register",
               url: "dang-ky",
               defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional },
               namespaces: new[] { "TDShop.Web.Controllers" }
            );
            #region Admin
            routes.MapRoute(
                name: "Quản trị",
                url: "quan-tri",
                defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "TDShop.Web.Controllers" }
            );
            #endregion
            routes.MapRoute(
                name: "Tìm kiếm",
                url: "tim-kiem",
                defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
                namespaces: new[] { "TDShop.Web.Controllers" }
            );
            routes.MapRoute(
                name: "Thêm vào giỏ",
                url: "them-gio-hang/{id}",
                defaults: new { controller = "Cart", action = "Add", id = UrlParameter.Optional },
                namespaces: new[] { "TDShop.Web.Controllers" }
            );
            routes.MapRoute(
                name: "Chi tiết sản phẩm",
                url: "{alias}.p-{id}",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "TDShop.Web.Controllers" }
            );
            routes.MapRoute(
                 name: "Danh mục sản phẩm",
                 url: "{alias}.pc-{id}",
                 defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
                   namespaces: new string[] { "TDShop.Web.Controllers" }
             );
            routes.MapRoute(
                 name: "Tag sản phẩm",
                 url: "tag/{tagid}",
                 defaults: new { controller = "Product", action = "ListByTag", tagid = UrlParameter.Optional },
                   namespaces: new string[] { "TDShop.Web.Controllers" }
             );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "TDShop.Web.Controllers" }
            );

           


        }
    }
}
