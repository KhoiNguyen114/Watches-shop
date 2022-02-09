using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LuanVan_ShopDongHo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            /*
            routes.MapRoute(
                name:"DangKy",
                url:"{controller}/{action}",
                defaults: new {controller = "KhachHang", action = "DangKiTaiKhoan" }
                );*/
            /*
            routes.MapRoute(
                name: "Order",
                url : "{controller}/{action}",
                defaults: new {controller ="DatHang",action = "Order"}
                );*/
            //routes.MapRoute(
            //    name: "TrangLocSP",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Loc", action = "MainFilter", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Home", id = UrlParameter.Optional }
            );

            routes.MapRoute(
    name: "QuanLySanPham",
    url: "{controller}/{action}/{id}",
    defaults: new { controller = "QuanLy", action = "QuanLySanPham", id = UrlParameter.Optional }
);

            routes.MapRoute(
            name: "MainFilter",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Loc", action = "MainFilter", id = UrlParameter.Optional }
            );

            
        }
    }
}
