using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuanVan_ShopDongHo.Models;
using PagedList;

namespace LuanVan_ShopDongHo.Controllers
{
    public class NhanVienController : Controller
    {
        ShopDongHoDataContext db = new ShopDongHoDataContext();
        QuanLyController ql = new QuanLyController();
        List<NHANVIEN> dsNV;
        // GET: NhanVien
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

        public ActionResult QuanLyNhanVien(int? page,bool? trangThai = null)
        {
            TAIKHOAN tk = (TAIKHOAN)Session["Admin"];
            if (tk == null)
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            if (page == null)
            {
                page = 1;
            }

            bool temp = trangThai ?? false;
            ViewBag.trangThai = temp;

            int pagesize = 10; 
            int pageNumber = (page ?? 1);
            List<NHANVIEN> ds = db.NHANVIENs.Where(t=> t.TRANGTHAI == temp).ToList();

            List<TAIKHOAN> dsTK = db.TAIKHOANs.Where(t => t.MALOAI == "LTK002").ToList();
            dsNV = ds;
            var lst = ds.ToPagedList(pageNumber, pagesize);
            var dsTT = taoListTrangThai();

            ViewBag.dsTT = dsTT;
            ViewBag.dsNV = lst;
            ViewBag.dsTK = dsTK;
            ViewBag.pageNum = pageNumber;
            ViewBag.pageSize = pagesize;
            NHANVIEN nv = new NHANVIEN();
            return View(nv);
        }

        public JsonResult ThemNhanVien(NHANVIEN nv)
        {
            NHANVIEN nhanVien = db.NHANVIENs.Where(t => t.MANV == nv.MANV).FirstOrDefault();
            if(nhanVien != null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã nhân viên này đã tồn tại vui lòng chọn mã nhân viên khác!",
                }, JsonRequestBehavior.AllowGet);
            }

            NHANVIEN nvDN = db.NHANVIENs.Where(t => t.TENDN == nv.TENDN).FirstOrDefault();
            if(nvDN != null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mỗi nhân viên chỉ có thể dùng 1 tên đăng nhập riêng biệt!",
                }, JsonRequestBehavior.AllowGet);
            }

            NHANVIEN a = new NHANVIEN();
            a.MANV = nv.MANV;
            a.TENNV = nv.TENNV;
            a.TENDN = nv.TENDN;
            a.DIACHI = nv.DIACHI;
            a.GIOITINH = nv.GIOITINH;
            a.NGAYSINH = nv.NGAYSINH;
            a.DIENTHOAI = nv.DIENTHOAI;
            a.SOCMND = nv.SOCMND;
            a.TRANGTHAI = false;

            db.NHANVIENs.InsertOnSubmit(a);

            KHACHHANG kh = new KHACHHANG();
            kh.MAKH = nv.MANV;
            kh.TENKH = nv.TENNV;
            kh.TENDN = nv.TENDN;
            kh.DIACHI = nv.DIACHI;
            kh.GIOITINH = nv.GIOITINH;
            kh.NGAYSINH = nv.NGAYSINH;
            kh.SDT = nv.DIENTHOAI;
            kh.SOCMND = nv.SOCMND;
            kh.TRANGTHAI = false;

            db.KHACHHANGs.InsertOnSubmit(kh);

            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Thêm nhân viên mới thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNhanVien(string maNV)
        {
            var nv = (from k in db.NHANVIENs
                      where k.MANV.Equals(maNV.Trim())
                      select new
                      {
                          k.MANV,
                          k.TENNV,
                          k.TENDN,
                          k.TRANGTHAI,
                          k.GIOITINH,
                          k.DIENTHOAI,
                          k.DIACHI,
                          k.NGAYSINH,
                          k.SOCMND,
                      });
            return Json(nv, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SuaNhanVien(NHANVIEN nv)
        {
            NHANVIEN nhanVien = db.NHANVIENs.Where(t => t.MANV == nv.MANV).FirstOrDefault();
            if (nhanVien == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã nhân viên này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            NHANVIEN nvDN = db.NHANVIENs.Where(t => t.TENDN == nv.TENDN).FirstOrDefault();
            if (nvDN != null && nv.TENDN == nvDN.TENDN && nvDN.MANV != nhanVien.MANV)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mỗi nhân viên chỉ có thể dùng 1 tên đăng nhập riêng biệt!",
                }, JsonRequestBehavior.AllowGet);
            }

            KHACHHANG khDN = db.KHACHHANGs.Where(t => t.TENDN == nv.TENDN).FirstOrDefault();
            if (khDN != null && nv.TENDN == khDN.TENDN && khDN.MAKH != nhanVien.MANV)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mỗi nhân viên chỉ có thể dùng 1 tên đăng nhập riêng biệt!",
                }, JsonRequestBehavior.AllowGet);
            }

            nhanVien.TENNV = nv.TENNV;
            nhanVien.TENDN = nv.TENDN;
            nhanVien.DIACHI = nv.DIACHI;
            nhanVien.GIOITINH = nv.GIOITINH;
            nhanVien.NGAYSINH = nv.NGAYSINH;
            nhanVien.DIENTHOAI = nv.DIENTHOAI;
            nhanVien.SOCMND = nv.SOCMND;
            nhanVien.TRANGTHAI = false;

            KHACHHANG khachHang = db.KHACHHANGs.Where(t => t.MAKH == nv.MANV).FirstOrDefault();
            if(khachHang != null)
            {
                khachHang.TENKH = nv.TENNV;
                khachHang.TENDN = nv.TENDN;
                khachHang.DIACHI = nv.DIACHI;
                khachHang.GIOITINH = nv.GIOITINH;
                khachHang.NGAYSINH = nv.NGAYSINH;
                khachHang.SDT = nv.DIENTHOAI;
                khachHang.SOCMND = nv.SOCMND;
                khachHang.TRANGTHAI = false;
            }           

            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Cập nhật nhân viên thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult XoaNhanVien(string maNV)
        {
            NHANVIEN nhanVien = db.NHANVIENs.Where(t => t.MANV == maNV).FirstOrDefault();
            if (nhanVien == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã nhân viên này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            nhanVien.TRANGTHAI = true;
            db.SubmitChanges();

            KHACHHANG khachHang = db.KHACHHANGs.Where(t => t.MAKH == maNV).FirstOrDefault();
            if (khachHang == null)
            {

                return Json(new
                {
                    kq = true,
                    message = "Xóa nhân viên thành công!",
                }, JsonRequestBehavior.AllowGet);
            }
            khachHang.TRANGTHAI = true;

            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Xóa nhân viên thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult KhoiPhucNhanVien(string maNV)
        {
            NHANVIEN nhanVien = db.NHANVIENs.Where(t => t.MANV == maNV).FirstOrDefault();
            if (nhanVien == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã nhân viên này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            nhanVien.TRANGTHAI = false;
            db.SubmitChanges();
            KHACHHANG khachHang = db.KHACHHANGs.Where(t => t.MAKH == maNV).FirstOrDefault();
            if (khachHang == null)
            {

                return Json(new
                {
                    kq = true,
                    message = "Khôi phục nhân viên thành công!",
                }, JsonRequestBehavior.AllowGet);
            }
            khachHang.TRANGTHAI = false;

            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Khôi phục nhân viên thành công!",
            }, JsonRequestBehavior.AllowGet);
        }
    }
}