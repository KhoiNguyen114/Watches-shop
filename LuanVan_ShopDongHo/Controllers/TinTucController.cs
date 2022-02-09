using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using LuanVan_ShopDongHo.Models;
using PagedList;

namespace LuanVan_ShopDongHo.Controllers
{
    public class TinTucController : Controller
    {
        ShopDongHoDataContext db = new ShopDongHoDataContext();
        // GET: TinTuc
        public ActionResult Index()
        {
            return View();
        }

        public List<SelectListItem> taoListTrangThai()
        {
            var dsTT = new List<SelectListItem>();
            dsTT.Add(new SelectListItem() { Text = "Có hiệu lực", Value = "False" });
            dsTT.Add(new SelectListItem() { Text = "Đã xóa", Value = "True" });

            return dsTT;
        }

        public List<TinTuc> daoThuTuList(List<TinTuc> ds)
        {
            List<TinTuc> dsTT = new List<TinTuc>();
            for(int i=ds.Count-1; i>=0; i--)
            {
                dsTT.Add(ds[i]);
            }
            return dsTT;
        }

        public ActionResult QuanLyTinTuc(int? page,DateTime? ngayTao = null,bool? trangThai = null)
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
            int pagesize = 20; 
            int pageNumber = (page ?? 1);

            bool temp = trangThai ?? false;

            List<TinTuc> dsTT = (from k in db.TINTUCs
                                 join h in db.NHANVIENs on k.MANV equals h.MANV
                                 where k.TRANGTHAI == temp
                                 select new TinTuc
                                 {
                                     tieuDe = k.TIEUDE,
                                     tomTat = k.TOMTAT,
                                     tenNV = h.TENNV,
                                     noiDung = k.NOIDUNG,
                                     maTinTuc = k.IDTINTUC,
                                     ngayDang = k.NGAYDANG,
                                     trangThai = k.TRANGTHAI,
                                     hinhDaiDien = k.HINHDAIDIEN,
                                 }).ToList();

            if(ngayTao != null)
            {
                dsTT = dsTT.Where(t => t.ngayDang.Value == ngayTao.Value).ToList();
                ViewBag.ngayTao = ngayTao;
            }
            if(trangThai != null)
            {
                dsTT = dsTT.Where(t => t.trangThai == trangThai).ToList();
                ViewBag.trangThai = trangThai;
            }

            var lst = dsTT.ToPagedList(pageNumber, pagesize);
            var dsTrangThai = taoListTrangThai();

            ViewBag.dsTrangThai = dsTrangThai;
            ViewBag.dsTTLoad = lst;
            ViewBag.pageNum = pageNumber;
            ViewBag.pageSize = pagesize;
            return View();
        }

        [HttpGet]
        public ActionResult ThemTinTuc()
        {
            TAIKHOAN tk = (TAIKHOAN)Session["Admin"];
            if (tk == null)
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            TINTUC tt = new TINTUC();
            return View(tt);
        }

        public ActionResult CapNhatTinTuc(int idTinTuc)
        {
            TAIKHOAN tk = (TAIKHOAN)Session["Admin"];
            if (tk == null)
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            TINTUC tt = db.TINTUCs.Where(t => t.IDTINTUC == idTinTuc).FirstOrDefault();
            if(tt == null)
            {
                return RedirectToAction("QuanLyTinTuc", "TinTuc");
            }
            NHANVIEN nv = db.NHANVIENs.Where(t => t.MANV == tt.MANV).FirstOrDefault();
            if (nv == null)
            {
                return RedirectToAction("QuanLyTinTuc", "TinTuc");
            }
            ViewBag.tenNV = nv.TENNV;
            return View(tt);
        }

        public ActionResult ChiTietTinTucAdmin(int idTinTuc)
        {
            TAIKHOAN tk = (TAIKHOAN)Session["Admin"];
            if (tk == null)
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            TINTUC tt = db.TINTUCs.Where(t => t.IDTINTUC == idTinTuc).FirstOrDefault();
            if (tt == null)
            {
                return RedirectToAction("QuanLyTinTuc", "TinTuc");
            }
            NHANVIEN nv = db.NHANVIENs.Where(t => t.MANV == tt.MANV).FirstOrDefault();
            if (nv == null)
            {
                return RedirectToAction("QuanLyTinTuc", "TinTuc");
            }
            ViewBag.tenNV = nv.TENNV;
            return View(tt);
        }

        public ActionResult ChiTietTinTucKhachHang(int idTinTuc)
        {            
            TINTUC tt = db.TINTUCs.Where(t => t.IDTINTUC == idTinTuc).FirstOrDefault();
            if (tt == null)
            {
                return RedirectToAction("Home", "Home");
            }
            
            return View(tt);
        }

        public ActionResult LoadTinTucKhachHang(int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            int pagesize = 5;
            int pageNumber = (page ?? 1);
            List<TinTuc> dsTT = (from k in db.TINTUCs
                                 join h in db.NHANVIENs on k.MANV equals h.MANV
                                 where k.TRANGTHAI == false
                                 select new TinTuc
                                 {
                                     tieuDe = k.TIEUDE,
                                     tomTat = k.TOMTAT,
                                     tenNV = h.TENNV,
                                     noiDung = k.NOIDUNG,
                                     maTinTuc = k.IDTINTUC,
                                     ngayDang = k.NGAYDANG,
                                     trangThai = k.TRANGTHAI,
                                     hinhDaiDien = k.HINHDAIDIEN,
                                 }).ToList();
            var daoThuTu = daoThuTuList(dsTT);            
            var lst = daoThuTu.ToPagedList(pageNumber, pagesize);

            List<TinTuc> dsTTMoiNhat = (from k in db.TINTUCs
                                 join h in db.NHANVIENs on k.MANV equals h.MANV
                                 where k.TRANGTHAI == false
                                 orderby k.NGAYDANG descending
                                 select new TinTuc
                                 {
                                     tieuDe = k.TIEUDE,
                                     tomTat = k.TOMTAT,
                                     tenNV = h.TENNV,
                                     noiDung = k.NOIDUNG,
                                     maTinTuc = k.IDTINTUC,
                                     ngayDang = k.NGAYDANG,
                                     trangThai = k.TRANGTHAI,
                                     hinhDaiDien = k.HINHDAIDIEN,
                                 }).Take(10).ToList();

            ViewBag.dsTTMoiNhat = dsTTMoiNhat;
            ViewBag.dsTTLoad = lst;
            ViewBag.pageNum = pageNumber;
            ViewBag.pageSize = pagesize;
            return View();
        }

        [HttpPost]
        public JsonResult ThemTinTucMoi(TINTUC tt)
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
            NHANVIEN nv = db.NHANVIENs.Where(t => t.TENDN == tk.TENDN).FirstOrDefault();
            if (nv == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Nhân viên này không tồn tại nên không thể tạo tin tức! Xin vui lòng đăng nhập lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            string[] mang = tt.HINHDAIDIEN.Split('\\');
            string hinh = mang[mang.Length - 1];

            TINTUC tintuc = new TINTUC();
            tintuc.TIEUDE = tt.TIEUDE;
            tintuc.TOMTAT = tt.TOMTAT;
            tintuc.NOIDUNG = tt.NOIDUNG;
            tintuc.NGAYDANG = DateTime.Now;
            tintuc.HINHDAIDIEN = hinh;
            tintuc.MANV = nv.MANV;
            tintuc.TRANGTHAI = false;
            db.TINTUCs.InsertOnSubmit(tintuc);
            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Thêm tin tức mới thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SuaTinTuc(TINTUC tt)
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
            NHANVIEN nv = db.NHANVIENs.Where(t => t.TENDN == tk.TENDN).FirstOrDefault();
            if (nv == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Nhân viên này không tồn tại nên không thể tạo tin tức! Xin vui lòng đăng nhập lại!",
                }, JsonRequestBehavior.AllowGet);
            }
            TINTUC tintuc = db.TINTUCs.Where(t=> t.IDTINTUC == tt.IDTINTUC).FirstOrDefault();
            if(tintuc == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Tin tức này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            if(tt.HINHDAIDIEN != null)
            {
                string[] mang = tt.HINHDAIDIEN.Split('\\');
                string hinh = mang[mang.Length - 1];
                tintuc.HINHDAIDIEN = hinh;
            }

            tintuc.TIEUDE = tt.TIEUDE;
            tintuc.TOMTAT = tt.TOMTAT;
            tintuc.NOIDUNG = tt.NOIDUNG;
            tintuc.MANV = nv.MANV;
            tintuc.TRANGTHAI = false;
            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Cập nhật tin tức thành công!",
            }, JsonRequestBehavior.AllowGet);
        }        

        public JsonResult XoaTinTuc(int idTinTuc)
        {
            TINTUC tintuc = db.TINTUCs.Where(t => t.IDTINTUC == idTinTuc).FirstOrDefault();
            if (tintuc == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Tin tức này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            tintuc.TRANGTHAI = true;
            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Xóa tin tức thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult KhoiPhucTinTuc(int idTinTuc)
        {
            TINTUC tintuc = db.TINTUCs.Where(t => t.IDTINTUC == idTinTuc).FirstOrDefault();
            if (tintuc == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Tin tức này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            tintuc.TRANGTHAI = false;
            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Khôi phục tin tức thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult loadAnhSummernote(HttpPostedFileBase fupload)
        {
            if (Request.Files.Count != 0)
            {
                var file = Request.Files[0];
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Images/TinTuc/"), fileName);
                WebImage webImage = new WebImage(file.InputStream);
                webImage.Resize(1110, 624);
                webImage.Save(path);
                //file.SaveAs(path);
                string duongDan = Convert.ToString(path);
                return Json(new
                {
                    kq = true,
                    data = fileName,
                }, JsonRequestBehavior.AllowGet);

            }
            return Json(new
            {
                kq = false,
            }, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult XoaAnhSummernote(string filePath)
        {
            string[] mang = filePath.Split('/');
            var fileName = mang[mang.Length - 1];
            var path = Path.Combine(Server.MapPath("~/Content/Images/TinTuc/"), fileName);
            FileInfo fileA = new FileInfo(path);
            if (fileA.Exists)
            {
                fileA.Delete();
            }
            return Json(new
            {
                kq = true,
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpLoadHinhDaiDienTinTuc()
        {
            if (Request.Files.Count != 0)
            {
                var file = Request.Files[0];
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Images/TinTuc/HinhDaiDienTinTuc/"), fileName);
                WebImage webImage = new WebImage(file.InputStream);
                webImage.Resize(983, 553);
                webImage.Save(path);
                //file.SaveAs(path);
                return Json(new
                {
                    kq = true,
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    kq = false,
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}