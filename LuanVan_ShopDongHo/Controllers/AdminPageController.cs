using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuanVan_ShopDongHo.Models;
using PagedList;
using PagedList.Mvc;
using Newtonsoft.Json;


namespace LuanVan_ShopDongHo.Controllers
{
    public class AdminPageController : Controller
    {
        ShopDongHoDataContext db = new ShopDongHoDataContext();
        // GET: AdminPage
        CommonApiController commonApi = new CommonApiController();
        public ActionResult Index()
        {
            TAIKHOAN taiKhoan = (TAIKHOAN)Session["Admin"];
            if (taiKhoan == null)
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            TAIKHOAN tk = Session["User"] as TAIKHOAN;
            if (tk == null)
                return RedirectToAction("DangNhap", "KhachHang");
            Session["Admin"] = tk;
            return View(taiKhoan);
        }       

        public ActionResult MenuAdmin()
        {
            TAIKHOAN taiKhoan = (TAIKHOAN)Session["Admin"];
            if (taiKhoan == null)
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            GROUPPERMISSION gp = db.GROUPPERMISSIONs.Where(t => t.TENDN == taiKhoan.TENDN).FirstOrDefault();
            if (gp == null)
            {
                ViewBag.kpq = "Tài khoản chưa được phân quyền xin vui lòng liên hệ với quản lý để được phân quyền";
            }
            else
            {
                GROUPNAME gn = db.GROUPNAMEs.Where(t => t.IDGROUP == gp.IDGROUP).FirstOrDefault();
                PERMISSION p = db.PERMISSIONs.Where(t => t.IDGROUP == gn.IDGROUP).FirstOrDefault();
                if (p.PERMISSION_EDITUSER == true)
                {
                    ViewBag.editUser = true;
                }
                else
                {
                    ViewBag.editUser = false;
                }

                if (p.PERMISSION_SALE == true)
                {
                    ViewBag.sale = true;
                }
                else
                {
                    ViewBag.sale = false;
                }

                if (p.PERMISSION_REPORT == true)
                {
                    ViewBag.report = true;
                }
                else
                {
                    ViewBag.report = false;
                }

                if (p.PERMISSION_STOCKWARE == true)
                {
                    ViewBag.stockWare = true;
                }
                else
                {
                    ViewBag.stockWare = false;
                }

                if (p.PERMISSION_EDITSALESOFF == true)
                {
                    ViewBag.editSaleOff = true;
                }

                else
                {
                    ViewBag.editSaleOff = false;
                }
            }
            return PartialView("MenuAdmin");
        }
    }
}
