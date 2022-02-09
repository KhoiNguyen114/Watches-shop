using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using LuanVan_ShopDongHo.Models;
using Google.Authenticator;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Net;
using PagedList;

namespace LuanVan_ShopDongHo.Controllers
{
    public class KhachHangController : Controller
    {
        // GET: KhachHang
        ShopDongHoDataContext db = new ShopDongHoDataContext();
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult DangNhap()
        {
            var tk = new TaiKhoan();
            return View(tk);
        }

        public string MD5Hash(string text)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap([Bind(Include = "tenDN, matKhau")] TaiKhoan tk)
        {
            if (ModelState.IsValid)
            {
                tk.MATKHAU = MD5Hash(tk.MATKHAU);
                var taiKhoan = db.TAIKHOANs.Where(t => t.TENDN == tk.TENDN && t.MATKHAU == tk.MATKHAU).FirstOrDefault();
                if (taiKhoan != null)
                {
                    if (taiKhoan.userConfirm != true)
                    {
                        ViewBag.error = "Tài khoản chưa được kích hoạt vui lòng vào Email kích hoạt";
                        return RedirectToAction("verifyAccount", "KhachHang", new { tenDN = tk.TENDN });
                    }
                    if(taiKhoan.TRANGTHAI == true)
                    {
                        ViewBag.error = "Tài khoản của bạn đã bị khóa! Xin vui lòng liên hệ với cửa hàng để giải quyết!";
                        return View(tk);
                    }
                    if(taiKhoan.MALOAI == "LTK001")
                    {
                        KHACHHANG kh = db.KHACHHANGs.Where(t => t.TENDN == taiKhoan.TENDN).FirstOrDefault();
                        Session["User"] = taiKhoan;
                        Session["KH"] = kh;
                        return RedirectToAction("Home", "Home");
                    }
                   else
                    {
                        KHACHHANG kh = db.KHACHHANGs.Where(t => t.TENDN == taiKhoan.TENDN).FirstOrDefault();
                        Session["KH"] = kh;
                        Session["Admin"] = taiKhoan;
                        Session["User"] = taiKhoan;
                        return RedirectToAction( "Index","AdminPage");
                    }
                }
                else
                {
                    ViewBag.error = "Tài khoản hoặc mật khẩu không chính xác";
                    return View(tk);
                }
            }
            return View(tk);
        }

        //Đăng xuất
        public ActionResult DangXuat()
        {
            Session["User"] = null;
            Session["KH"] = null;
            return RedirectToAction("Home", "Home");
        }

        [HttpPost]
        public JsonResult CheckEmail(string email)
        {
            using (var db = new ShopDongHoDataContext())
            {
                var user = db.TAIKHOANs.FirstOrDefault(t => t.Email == email.Trim());
                if(user != null)
                {
                    return Json(new { success = false, msg = "Email đã tồn tại" });
                }
                return Json(new { success = true, msg = "Email hợp lệ" });
            }
        }

        public ActionResult DangKiTaiKhoan()
        {
            TaiKhoan k = new TaiKhoan();
            return View(k);
        }

        [HttpPost]
        public ActionResult DangKiTaiKhoan(string email = "", string username = "", string psw = "")
        {
            using (var db = new LuanVan_ShopDongHo.Models.ShopDongHoDataContext())
            {
                string mk = MD5Hash(psw);
                var user = db.TAIKHOANs.FirstOrDefault(t => t.TENDN.Contains(username) || t.Email == email);
                if (user != null)
                {
                    return RedirectToAction("DangNhap", "KhachHang");
                }
              

                TAIKHOAN User_new = new TAIKHOAN
                {
                    TENDN = username,
                    MATKHAU = mk,
                    MALOAI = "LTK001",
                    Email = email
                };
                db.TAIKHOANs.InsertOnSubmit(User_new);
                db.SubmitChanges();
                return RedirectToAction("updateThongTin", "KhachHang", new { username = User_new.TENDN });
            }
        }

        public async Task<JsonResult> sendEmailCode(string userName = "")
        {
            using (var db = new ShopDongHoDataContext())
            {
                try
                {
                    var user = db.TAIKHOANs.FirstOrDefault(t => t.TENDN.Contains(userName.Trim()));

                    TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();

                    string key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
                    SetupCode setup = tfa.GenerateSetupCode("NQN", user.TENDN, key, false, 3);
                    user.key_tfa = key;
                    DateTime date_send = DateTime.Now;
                    user.date_send = date_send;
                    string mailKey = tfa.GetCurrentPIN(user.key_tfa, user.date_send.Value);

                    string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                    token = token.Replace('-', '=').Replace("=", "");

                    user.token = token;

                    var confirmURL = Url.Action("verifyAccount", "KhachHang", new { tendn = user.TENDN, token = token }, protocol: Request.Url.Scheme);

                    string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/template/emailVerify.html"));
                    content = content.Replace("{{customer}}", userName);
                    content = content.Replace("{{confirmCode}}", mailKey);
                    content = content.Replace("{{URL}}", confirmURL);

                    var usermail = user.Email;
                    Mail email = new Mail();
                    await email.sendMail(usermail, "Xác nhận đăng ký thành viên", content);
                    db.SubmitChanges();
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return Json(new { success = false });
                }
            }

        }

        public ActionResult verifyAccount(string tenDN = "", string token = "")
        {
            using (var db = new ShopDongHoDataContext())
            {
                var user = db.TAIKHOANs.FirstOrDefault(t => t.TENDN.Contains(tenDN));
                if (user == null)
                    return RedirectToAction("DangNhap", "KhachHang");
                var email = user.Email;
                Session["User"] = user;
                ViewBag.email = email;
                ViewBag.tenDN = tenDN;
                ViewBag.token = token;
                return View(user);
            }

        }

        [HttpPost]
        public JsonResult confirmCode(string username = "", string code = "")
        {
            try
            {
                using (var db = new ShopDongHoDataContext())
                {
                    var user = db.TAIKHOANs.FirstOrDefault(t => t.TENDN.Contains(username));
                    if (user == null)
                        return Json(new { success = false, msg = "Xác thực thất bại" });
                    TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                    if (tfa.GetCurrentPIN(user.key_tfa, user.date_send.Value) != code)
                        return Json(new { success = false, msg = "Xác thực thất bại" });
                    user.userConfirm = true;
                    db.SubmitChanges();
                    return Json(new { success = true });
                }
            }
            catch (Exception )
            {
                return Json(new { success = false, msg = "Xác thực thất bại" });
            }
        }

        public ActionResult updateThongTin(string username = "")
        {
            var user = db.TAIKHOANs.FirstOrDefault(t => t.TENDN == username);
            if (user == null)
                return RedirectToAction("DangNhap", "KhachHang");
            KhachHang kh = new KhachHang();
            Session["User"] = user;
            ViewBag.username = username;
            return View(kh);
        }

        [HttpPost]
        public async Task<ActionResult> updateThongTin(string username = "", string fullname = "", string gender = "", DateTime? ngaysinh = null, string sdt = "", string address = "", string cmnd = "")
        {
            using (var db = new ShopDongHoDataContext())
            {
                if (Session["User"] == null)
                    return RedirectToAction("DangNhap", "KhachHang");
                var user = Session["User"] as TAIKHOAN;

                int numb = db.KHACHHANGs.Count();
                numb++;
                KHACHHANG kh = db.KHACHHANGs.FirstOrDefault(t => t.TENKH.Trim() == fullname.Trim() && t.SOCMND == cmnd);
                if(kh == null)
                {
                    kh = new KHACHHANG
                    {
                        MAKH = string.Format("KH{0}", numb),
                        TENDN = username,
                        TENKH = fullname,
                        GIOITINH = gender,
                        NGAYSINH = ngaysinh,
                        SDT = sdt,
                        DIACHI = address,
                        SOCMND = cmnd,
                        TRANGTHAI = false
                    };
                    db.KHACHHANGs.InsertOnSubmit(kh);
                }    
                else
                {
                    kh.TENDN = username;
                    kh.TENKH = fullname;
                    kh.GIOITINH = gender;
                    kh.NGAYSINH = ngaysinh;
                    kh.SDT = sdt;
                    kh.DIACHI = address;
                    kh.SOCMND = cmnd;
                    kh.TRANGTHAI = false;
                }    
                Session["User"] = user;
                db.SubmitChanges();
                await sendEmailCode(username);
                return RedirectToAction("verifyAccount", "KhachHang", new { tenDN = username, token = user.token });
            }
        }

        public List<SelectListItem> taoListTrangThai()
        {
            var dsTT = new List<SelectListItem>();
            dsTT.Add(new SelectListItem() { Text = "Có hiệu lực", Value = "False" });
            dsTT.Add(new SelectListItem() { Text = "Đã xóa", Value = "True" });

            return dsTT;
        }

        public List<SelectListItem> taoListGioiTinh()
        {
            List<SelectListItem> ds = new List<SelectListItem>();
            ds.Add(new SelectListItem() { Text = "Nam", Value = "Nam" });
            ds.Add(new SelectListItem() { Text = "Nữ", Value = "Nữ" });
            ds.Add(new SelectListItem() { Text = "Chọn giới tính", Value = "" });
            return ds;
        }

        public ActionResult QuanLyKhachHang(int? page,string tenKH = "",bool? trangThai = null,string gioitinh = "")
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
            int pagesize = 10;
            int pageNumber = (page ?? 1);

            List<KHACHHANG> dsKH = db.KHACHHANGs.Where(t => t.TRANGTHAI == false).ToList();
            List<TAIKHOAN> dsTK = db.TAIKHOANs.Where(t => t.MALOAI == "LTK001").ToList();
            var dsTT = taoListTrangThai();
            var dsGT = taoListGioiTinh();

            if(tenKH != "")
            {
                dsKH = dsKH.Where(t => t.TENKH.Trim() == tenKH.Trim()).ToList();
                ViewBag.tenKH = tenKH;
            }

            if(trangThai != null)
            {
                dsKH = dsKH.Where(t => t.TRANGTHAI == trangThai).ToList();
                ViewBag.trangThai = trangThai;
            }

            if(gioitinh != "")
            {
                dsKH = dsKH.Where(t => t.GIOITINH == gioitinh).ToList();
                ViewBag.GioiTinh = gioitinh;
            }

            var lst = dsKH.ToPagedList(pageNumber, pagesize);

            ViewBag.dsGT = dsGT;
            ViewBag.dsTT = dsTT;
            ViewBag.dsTK = dsTK;
            ViewBag.dsKHLoad = lst;
            ViewBag.pageNum = pageNumber;
            ViewBag.pageSize = pagesize;
            KHACHHANG kh = new KHACHHANG();
            return View(kh);
        }

        
        public JsonResult GetKhachHang(string maKH)
        {
            var kh = (from k in db.KHACHHANGs
                      where k.MAKH.Equals(maKH.Trim())
                      select new
                      {
                          k.MAKH,
                          k.TENKH,
                          k.TRANGTHAI,
                          k.GIOITINH,
                          k.SDT,
                          k.DIACHI,
                          k.NGAYSINH,
                          k.SOCMND,
                      });
            return Json(kh, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SuaKhachHang(KHACHHANG kh)
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

            KHACHHANG khachHang = db.KHACHHANGs.Where(t => t.MAKH == kh.MAKH).FirstOrDefault();
            if (khachHang == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã khách hàng này không tồn tại! Xin vui lòng thử lại",
                }, JsonRequestBehavior.AllowGet);
            }

            KHACHHANG khDN = db.KHACHHANGs.Where(t => t.TENDN == kh.TENDN).FirstOrDefault();
            if (khDN != null && kh.TENDN == khDN.TENDN && khDN.MAKH != khachHang.MAKH)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mỗi khách hàng chỉ có thể dùng 1 tên đăng nhập riêng biệt!",
                }, JsonRequestBehavior.AllowGet);
            }

            khachHang.TENKH = kh.TENKH;
            khachHang.DIACHI = kh.DIACHI;
            khachHang.GIOITINH = kh.GIOITINH;
            khachHang.NGAYSINH = kh.NGAYSINH;
            khachHang.SDT = kh.SDT;
            khachHang.SOCMND = kh.SOCMND;
            khachHang.TRANGTHAI = false;

            NHANVIEN nv = db.NHANVIENs.Where(t => t.MANV == kh.MAKH).FirstOrDefault();
            if(nv != null)
            {
                nv.TENNV = kh.TENKH;
                nv.DIACHI = kh.DIACHI;
                nv.GIOITINH = kh.GIOITINH;
                nv.NGAYSINH = kh.NGAYSINH;
                nv.DIENTHOAI = kh.SDT;
                nv.SOCMND = kh.SOCMND;
                nv.TRANGTHAI = false;
            }

            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Cập nhật khách hàng thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult XoaKhachHang(string maKH)
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

            KHACHHANG khachHang = db.KHACHHANGs.Where(t => t.MAKH == maKH).FirstOrDefault();
            if (khachHang == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã khách hàng này không tồn tại! Xin vui lòng thử lại",
                }, JsonRequestBehavior.AllowGet);
            }           

            khachHang.TRANGTHAI = true;

            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Xóa khách hàng thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult KhoiPhucKhachHang(string maKH)
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

            KHACHHANG khachHang = db.KHACHHANGs.Where(t => t.MAKH == maKH).FirstOrDefault();
            if (khachHang == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã khách hàng này không tồn tại! Xin vui lòng thử lại",
                }, JsonRequestBehavior.AllowGet);
            }

            khachHang.TRANGTHAI = false;

            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Khôi phục khách hàng thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ThongTinTaiKhoanKH()
        {
            TAIKHOAN tk = (TAIKHOAN)Session["User"];
            if (tk == null)
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            KHACHHANG kh = db.KHACHHANGs.Where(t => t.TENDN == tk.TENDN).FirstOrDefault();
            if (kh == null)
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            ViewBag.kh = kh;
            return View();
        }

        public JsonResult DoiMatKhau(string matKhauCu, string matKhauMoi)
        {
            TAIKHOAN tk = (TAIKHOAN)Session["User"];
            if (tk == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Phiên đã hết hạn! Xin vui lòng đăng nhập lại",
                }, JsonRequestBehavior.AllowGet);
            }

            TAIKHOAN taiKhoan = db.TAIKHOANs.FirstOrDefault(t => t.TENDN == tk.TENDN);
            if (taiKhoan == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Tài khoản này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            string mkCu = MD5Hash(matKhauCu);
            if (taiKhoan.MATKHAU != mkCu)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mật khẩu cũ không trùng khớp! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }
            
            string mk = MD5Hash(matKhauMoi);
            taiKhoan.MATKHAU = mk;
            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Đổi mật khẩu thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CapNhatThongTinKH(int idKhachHang)
        {
            TAIKHOAN tk = (TAIKHOAN)Session["User"];
            if (tk == null)
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            KHACHHANG kh = db.KHACHHANGs.FirstOrDefault(t => t.IDKhachHang == idKhachHang);
            if(kh == null)
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            ViewBag.dsGT = taoListGioiTinh();
            return View(kh);
        }

        public JsonResult CapNhatThongTinKhachHang(KHACHHANG kh)
        {
            TAIKHOAN tk = (TAIKHOAN)Session["User"];
            if (tk == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Phiên đã hết hạn! Xin vui lòng đăng nhập lại",
                }, JsonRequestBehavior.AllowGet);
            }

            KHACHHANG khachHang = db.KHACHHANGs.FirstOrDefault(t => t.IDKhachHang == kh.IDKhachHang);
            if (khachHang == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Khách hàng này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            khachHang.TENKH = kh.TENKH;
            khachHang.GIOITINH = kh.GIOITINH;
            khachHang.DIACHI = kh.DIACHI;
            khachHang.SDT = kh.SDT;
            khachHang.SOCMND = kh.SOCMND;
            khachHang.NGAYSINH = kh.NGAYSINH;
            khachHang.TRANGTHAI = false;

            NHANVIEN nv = db.NHANVIENs.Where(t => t.TENDN == khachHang.TENDN).FirstOrDefault();     //First or Default lấy theo khóa chính
            if(nv != null)
            {
                nv.TENNV = kh.TENKH;
                nv.GIOITINH = kh.GIOITINH;
                nv.DIACHI = kh.DIACHI;
                nv.DIENTHOAI = kh.SDT;
                nv.SOCMND = kh.SOCMND;
                nv.NGAYSINH = kh.NGAYSINH;
                nv.TRANGTHAI = false;
            }

            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Cập nhật thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult KhoiPhucMatKhau()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KhoiPhucMatKhau(string tendn, string psw)
        {
            using (var db = new ShopDongHoDataContext())
            {
                string mk = MD5Hash(psw);
                var user = db.TAIKHOANs.FirstOrDefault(t => t.TENDN.Contains(tendn));
                if (user == null)
                {
                    return RedirectToAction("KhoiPhucMatKhau", "KhachHang");
                }
                user.MATKHAU = mk;
                db.SubmitChanges();
                return RedirectToAction("DangNhap", "KhachHang");
            }
        }


    }

}