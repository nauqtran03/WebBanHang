using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebBanHang
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                name: "Contact",
                url: "contact",
                defaults: new { controller = "Contact", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "WebBanHang.Controllers" }
            );
            routes.MapRoute(
                name: "Checkout",
                url: "checkout",
                defaults: new { controller = "ShoppingCart", action = "CheckOut", alias = UrlParameter.Optional },
                namespaces: new[] { "WebBanHang.Controllers" }
            );
            routes.MapRoute(
                name: "ShoppingCart",
                url: "shopping-cart",
                defaults: new { controller = "ShoppingCart", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "WebBanHang.Controllers" }
            );
            routes.MapRoute(
                name: "Product",
                url: "list-product",
                defaults: new { controller = "Product", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "WebBanHang.Controllers" }
            );
            routes.MapRoute(
                name: "News",
                url: "list-news",
                defaults: new { controller = "New", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "WebBanHang.Controllers" }
            );
            routes.MapRoute(
                name: "DetailPost",
                url: "post/{alias}",
                defaults: new { controller = "Article", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "WebBanHang.Controllers" }
            );
            routes.MapRoute(
                name: "DetailNews",
                url: "{alias}-n{id}",
                defaults: new { controller = "New", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "WebBanHang.Controllers" }
            );
            routes.MapRoute(
                name: "DetailProduct",
                url: "detail-product/{alias}-p{id}",
                defaults: new { controller = "Product", action = "Detail", alias = UrlParameter.Optional },
                namespaces: new[] { "WebBanHang.Controllers" }
            );
            routes.MapRoute(
                name: "CategoryProduct",
                url: "list-category/{alias}/{id}",
                defaults: new { controller = "Product", action = "ProductCate", id = UrlParameter.Optional },
                namespaces: new[] { "WebBanHang.Controllers" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WebBanHang.Controllers" }
            );
        }
    }
}
