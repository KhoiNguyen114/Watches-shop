using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuanVan_ShopDongHo.Models;
using PagedList;

namespace LuanVan_ShopDongHo.Controllers
{
    public class TaiKhoanController : Controller
    {
        ShopDongHoDataContext db = new ShopDongHoDataContext();
        KhachHangController khC = new KhachHangController();

        public List<TaiKhoanModel> dsTK;
        // GET: TaiKhoan
        public ActionResult Index()
        {
            return View();
        }

        public List<SelectListItem> taoListTrangThai()
        {
            var dsTT = new List<SelectListItem>();
            dsTT.Add(new SelectListItem() { Text = "Có hiệu lực", Value = "False" });
            dsTT.Add(new SelectListItem() { Text = "Đã khóa", Value = "True" });

            return dsTT;
        }

        public ActionResult QuanLyTaiKhoan(int? page, bool? trangThai = null)
        {
            TAIKHOAN tkCheck = (TAIKHOAN)Session["Admin"];
            if (tkCheck == null)
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            if (page == null)
            {
                page = 1;
            }
            int pagesize = 10;
            int pageNumber = (page ?? 1);

            bool temp = trangThai ?? false;

            List<TaiKhoanModel> ds = (from k in db.TAIKHOANs
                                      join ltk in db.LOAITKs on k.MALOAI equals ltk.MALOAI
                                      where k.TRANGTHAI == temp
                                      select new TaiKhoanModel
                                      {
                                          tenDN = k.TENDN,
                                          matKhau = k.MATKHAU,
                                          tenLoai = ltk.TENLOAI,
                                          email = k.Email,
                                          trangThai = k.TRANGTHAI,
                                      }).ToList();
            
            if(trangThai == null)
            {
                ds = ds.Where(t => t.trangThai == false).ToList();
            }
            else if(trangThai != null )
            {
                ds = ds.Where(t => t.trangThai == trangThai).ToList();
                ViewBag.trangThai = trangThai;
            }

            var lst = ds.ToPagedList(pageNumber, pagesize);
            var dsTT = taoListTrangThai();



            dsTK = ds;
            ViewBag.dsTK = lst;
            ViewBag.dsTT = dsTT;
            ViewBag.pageNum = pageNumber;
            ViewBag.pageSize = pagesize;

            TAIKHOAN tk = new TAIKHOAN();
            return View(tk);
        }

        public JsonResult TaoTaiKhoan(TAIKHOAN tk)
        {
            TAIKHOAN taiKhoan = db.TAIKHOANs.Where(t => t.TENDN == tk.TENDN).FirstOrDefault();
            if (taiKhoan != null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Tên đăng nhập này đã tồn tại vui lòng chọn tên đăng nhập khác!",
                }, JsonRequestBehavior.AllowGet);
            }
            TAIKHOAN taiK = new TAIKHOAN();
            string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            token = token.Replace('-', '=').Replace("=", "");
            DateTime date_send = DateTime.Now;
            string key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
            bool user_confrim = true;

            string mk = khC.MD5Hash(tk.MATKHAU);

            taiK.TENDN = tk.TENDN;
            taiK.MATKHAU = mk;
            taiK.MALOAI = "LTK002";
            taiK.token = token;
            taiK.date_send = date_send;
            taiK.key_tfa = key;
            taiK.userConfirm = user_confrim;
            taiK.Email = tk.Email;
            taiK.TRANGTHAI = false;

            db.TAIKHOANs.InsertOnSubmit(taiK);
            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Tạo tài khoản mới thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTaiKhoan(string tenDN)
        {
            var sp = (from k in db.TAIKHOANs
                      where k.TENDN.Equals(tenDN.Trim())
                      select new
                      {
                          k.TENDN,
                          k.MATKHAU,
                          k.Email,
                          k.TRANGTHAI,
                      });
            return Json(sp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CapNhatTaiKhoan(TAIKHOAN tk)
        {
            TAIKHOAN taiKhoan = db.TAIKHOANs.Where(t => t.TENDN == tk.TENDN).FirstOrDefault();
            if (taiKhoan == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Tên đăng nhập này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            if(!tk.MATKHAU.Equals(taiKhoan.MATKHAU))
            {
                string mk = khC.MD5Hash(tk.MATKHAU);
                taiKhoan.MATKHAU = mk;
            }
            else
            {
                taiKhoan.MATKHAU = tk.MATKHAU;
            }           
            taiKhoan.TENDN = tk.TENDN;
            taiKhoan.Email = tk.Email;
            taiKhoan.TRANGTHAI = false;

            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Cập nhật tài khoản thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BanTaiKhoan(string tenDN)
        {
            TAIKHOAN taiKhoan = db.TAIKHOANs.Where(t => t.TENDN == tenDN).FirstOrDefault();
            if (taiKhoan == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Tên đăng nhập này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            taiKhoan.TRANGTHAI = true;
            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Khóa tài khoản thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult KhoiPhucTaiKhoan(string tenDN)
        {
            TAIKHOAN taiKhoan = db.TAIKHOANs.Where(t => t.TENDN == tenDN).FirstOrDefault();
            if (taiKhoan == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Tên đăng nhập này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            taiKhoan.TRANGTHAI = false;
            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Khôi phục tài khoản thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ThongTinTaiKhoan()
        {
            TAIKHOAN tk = (TAIKHOAN)Session["Admin"];
            if (tk == null)
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            NHANVIEN nv = db.NHANVIENs.Where(t => t.TENDN == tk.TENDN).FirstOrDefault();
            if (nv == null)
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            ViewBag.nv = nv;
            return View(tk);
        }

        public ActionResult DangXuatAdmin()
        {
            Session["Admin"] = null;
            return RedirectToAction("DangNhap", "KhachHang");
        }

        public JsonResult DoiMatKhau(string matKhauCu, string matKhauMoi)
        {
            TAIKHOAN tk = (TAIKHOAN)Session["Admin"];
            if (tk == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Phiên đã hết hạn! Xin vui lòng đăng nhập lại",
                }, JsonRequestBehavior.AllowGet);
            }
            string mkCu = khC.MD5Hash(matKhauCu);
            if (tk.MATKHAU != mkCu)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mật khẩu cũ không trùng khớp! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            string mk = khC.MD5Hash(matKhauMoi);
            tk.MATKHAU = mk;
            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Đổi mật khẩu thành công!",
            }, JsonRequestBehavior.AllowGet);
        }
    }
}