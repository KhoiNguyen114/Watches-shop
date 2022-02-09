using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using LuanVan_ShopDongHo.Models;

namespace LuanVan_ShopDongHo.Controllers
{
    public class PhanQuyenController : Controller
    {
        // GET: PhanQuyen

        ShopDongHoDataContext db = new ShopDongHoDataContext();
        public ActionResult Index()
        {
            return View();
        }

        public List<SelectListItem> taoListTrangThai()
        {
            var dsTT = new List<SelectListItem>();
            dsTT.Add(new SelectListItem() { Text = "Đang làm", Value = "False" });
            dsTT.Add(new SelectListItem() { Text = "Đã xóa", Value = "True" });

            return dsTT;
        }

        public ActionResult QuanLyPhanQuyen()
        {
            TAIKHOAN tk = (TAIKHOAN)Session["Admin"];
            if (tk == null)
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            List<TAIKHOAN> dsTK = db.TAIKHOANs.Where(t => t.MALOAI == "LTK002" && t.TRANGTHAI == false).ToList();
            var ds = from k in db.TAIKHOANs
                     join h in db.GROUPPERMISSIONs on k.TENDN equals h.TENDN
                     where k.MALOAI == "LTK002" && k.TRANGTHAI == false
                     select k;
            var dsExcept = dsTK.Except(ds);

            List<PhanQuyen> dsTKDaPQ = (from k in db.GROUPPERMISSIONs
                                        join h in db.GROUPNAMEs on k.IDGROUP equals h.IDGROUP
                                        where k.TRANGTHAI == false
                                        select new PhanQuyen
                                        {
                                            tenDN = k.TENDN,
                                            tenNhomNguoiDung = h.NAME,
                                        }).ToList();

            List<GROUPNAME> dsGroupName = db.GROUPNAMEs.Where(t => t.TRANGTHAI == false).ToList();
            List<GROUPPERMISSION> dsGroupPermissions = db.GROUPPERMISSIONs.ToList();
            var dsTT = taoListTrangThai();

            ViewBag.dsTT = dsTT;
            ViewBag.dsTK = dsExcept;
            ViewBag.dsGroupName = dsGroupName;
            ViewBag.dsGroupPermissions = dsGroupPermissions;
            ViewBag.dsTKDaPQ = dsTKDaPQ;
            ViewBag.groupNameCount = dsGroupName.Count;
            GROUPNAME groupName = new GROUPNAME();
            return View(groupName);
        }

        public JsonResult GetPhanQuyenThemVaoNhom(string tenDN)
        {
            TAIKHOAN tk = (TAIKHOAN)Session["Admin"];
            if (tk == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Phiên đã hết hạn! Xin vui lòng đăng nhập lại!",
                }, JsonRequestBehavior.AllowGet);
            }
            var groupPer = db.GROUPPERMISSIONs.Where(t => t.TENDN == tenDN).FirstOrDefault();
            if (groupPer == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Tên đăng nhập này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            var gp = (from k in db.GROUPPERMISSIONs
                      join h in db.GROUPNAMEs on k.IDGROUP equals h.IDGROUP
                      where k.TENDN.Equals(tenDN.Trim())
                      select new
                      {
                          k.TENDN,
                          k.IDGROUP,
                      });

            return Json(new
            {
                kq = true,
                data = gp,
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ThemGroupPermission(GROUPPERMISSION gp)
        {
            TAIKHOAN tk = (TAIKHOAN)Session["Admin"];
            if (tk == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Phiên đã hết hạn! Xin vui lòng đăng nhập lại!",
                }, JsonRequestBehavior.AllowGet);
            }
            var groupPer = db.GROUPPERMISSIONs.Where(t => t.TENDN == gp.TENDN).FirstOrDefault();
            if (groupPer != null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Tài khoản này đã được phân quyền! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            GROUPPERMISSION groupPermission = new GROUPPERMISSION();
            groupPermission.TENDN = gp.TENDN;
            groupPermission.IDGROUP = gp.IDGROUP;
            groupPermission.TRANGTHAI = false;

            db.GROUPPERMISSIONs.InsertOnSubmit(groupPermission);
            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Thêm tài khoản vào nhóm người dùng thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CapNhatGroupPermission(GROUPPERMISSION gp)
        {
            TAIKHOAN tk = (TAIKHOAN)Session["Admin"];
            if (tk == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Phiên đã hết hạn! Xin vui lòng đăng nhập lại!",
                }, JsonRequestBehavior.AllowGet);
            }
            var groupPer = db.GROUPPERMISSIONs.Where(t => t.TENDN == gp.TENDN).FirstOrDefault();
            if (groupPer == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Tài khoản này chưa được phân quyền hoặc không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            groupPer.TENDN = gp.TENDN;
            groupPer.IDGROUP = gp.IDGROUP;
            groupPer.TRANGTHAI = false;

            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Cập nhật tài khoản vào nhóm người dùng thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PhanQuyen()
        {
            TAIKHOAN tk = (TAIKHOAN)Session["Admin"];
            if (tk == null)
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            List<GROUPNAME> dsGroupName = db.GROUPNAMEs.Where(t => t.TRANGTHAI == false).ToList();
            List<PERMISSION> dsPermission = db.PERMISSIONs.ToList();
            PERMISSION permission = new PERMISSION();
            List<PhanQuyenLoad> dsPQLoad = (from k in db.PERMISSIONs
                                            join h in db.GROUPNAMEs on k.IDGROUP equals h.IDGROUP
                                            select new PhanQuyenLoad
                                            {
                                                idNND = k.IDGROUP,
                                                tenNND = h.NAME,
                                                quyenBanHang = k.PERMISSION_SALE,
                                                quyenBaoCao = k.PERMISSION_REPORT,
                                                quyenQuanLyKho = k.PERMISSION_STOCKWARE,
                                                quyenChinhSuaNguoiDung = k.PERMISSION_EDITUSER,
                                                quyenQuanLyKhuyenMai = k.PERMISSION_EDITSALESOFF,
                                            }).ToList();

            ViewBag.dsPQLoad = dsPQLoad;
            ViewBag.dsGroupName = dsGroupName;
            ViewBag.dsPermission = dsPermission;
            ViewBag.permission = permission;
            return View();
        }

        public JsonResult LuuPhanQuyen(PhanQuyenModel dsPQ)
        {
            TAIKHOAN tk = (TAIKHOAN)Session["Admin"];
            if (tk == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Phiên đã hết hạn! Xin vui lòng đăng nhập lại!",
                }, JsonRequestBehavior.AllowGet);
            }
            string[] mangID = dsPQ.tenNND.Split('|');
            string[] mangValue = dsPQ.quyen.Split('|');

            if (mangID.Length != mangValue.Length)
            {
                return Json(new
                {
                    kq = false,
                    message = "Có lỗi xảy ra trong quá trình xử lí!",
                }, JsonRequestBehavior.AllowGet);
            }

            List<PhanQuyenModel> dsPhanQuyen = new List<PhanQuyenModel>();
            for (int i = 0; i < mangID.Length; i++)
            {
                PhanQuyenModel pq = new PhanQuyenModel();
                pq.tenNND = mangID[i];
                pq.quyen = mangValue[i];
                dsPhanQuyen.Add(pq);
            }

            List<PERMISSION> dsPermission = db.PERMISSIONs.ToList();
            for (int i = 0; i < dsPhanQuyen.Count; i++)
            {
                PERMISSION p = db.PERMISSIONs.Where(t => t.IDGROUP == int.Parse(dsPhanQuyen[i].tenNND)).FirstOrDefault();
                switch (dsPhanQuyen[i].quyen)
                {
                    case "Bán hàng":
                        p.PERMISSION_SALE = true;
                        break;
                    case "Chỉnh sửa người dùng":
                        p.PERMISSION_EDITUSER = true;
                        break;
                    case "Báo cáo":
                        p.PERMISSION_REPORT = true;
                        break;
                    case "Quản lý kho":
                        p.PERMISSION_STOCKWARE = true;
                        break;
                    case "Quản lý khuyến mãi":
                        p.PERMISSION_EDITSALESOFF = true;
                        break;
                }
                db.SubmitChanges();
            }

            return Json(new
            {
                kq = true,
                message = "Phân quyền thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ThemNhomNguoiDung(GROUPNAME gn)
        {
            TAIKHOAN tk = (TAIKHOAN)Session["Admin"];
            if (tk == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Phiên đã hết hạn! Xin vui lòng đăng nhập lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            GROUPNAME groupName = db.GROUPNAMEs.Where(t => t.NAME.ToLower().Equals(gn.NAME.ToLower())).FirstOrDefault();
            if (groupName != null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Nhóm người dùng này đã tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            GROUPNAME group = new GROUPNAME();
            group.NAME = gn.NAME;
            group.TRANGTHAI = false;
            db.GROUPNAMEs.InsertOnSubmit(group);
            db.SubmitChanges();

            GROUPNAME gName = db.GROUPNAMEs.Where(t => t.NAME.ToLower().Equals(group.NAME.ToLower())).FirstOrDefault();
            PERMISSION p = new PERMISSION();
            p.IDGROUP = gName.IDGROUP;
            p.PERMISSION_EDITSALESOFF = false;
            p.PERMISSION_EDITUSER = false;
            p.PERMISSION_REPORT = false;
            p.PERMISSION_STOCKWARE = false;
            p.PERMISSION_SALE = false;
            db.PERMISSIONs.InsertOnSubmit(p);
            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Thêm nhóm người dùng mới thành công!",
            }, JsonRequestBehavior.AllowGet);
        }
    }
}