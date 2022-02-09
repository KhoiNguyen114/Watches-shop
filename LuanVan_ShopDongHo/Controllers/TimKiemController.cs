using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuanVan_ShopDongHo.Models;
using PagedList;

namespace LuanVan_ShopDongHo.Controllers
{
    public class TimKiemController : Controller
    {
        ShopDongHoDataContext db = new ShopDongHoDataContext();
        // GET: TimKiem
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadGoiY(string tuKhoa)
        {
            bool kq = true;
            if (String.IsNullOrEmpty(tuKhoa))
            {
                kq = false;
            }
            IQueryable dsSP = (from k in db.SANPHAMs where k.TENSP.Contains(tuKhoa) || k.MASP == tuKhoa select new { k.MASP, k.TENSP, k.HINHANH, k.DONGIA });
            return Json(new
            {
                success = kq,
                data = dsSP
            }, JsonRequestBehavior.AllowGet);
        }

        
    }
}