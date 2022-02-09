using LuanVan_ShopDongHo.Models;
using Newtonsoft.Json;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LuanVan_ShopDongHo.Controllers
{
    public class QuanLyDonHangController : Controller
    {
        int pageSize = 20;

        Dictionary<int, string> TrangThaiDonHang = new Dictionary<int, string>
        {
            {0,"Tất cả trạng thái"},
            {1,"Đang chờ xử lý"},
            {2,"Đang vận chuyển"},
            {3,"Đơn hàng đã thanh toán"},
            {4,"Đơn hàng đã hủy"}
        };

        // GET: QuanLyDonHang
        public ActionResult Index(string TENDN = "", int? page = null, string search = "", string createDate = "", string tenNV = "", int trangThaiSearch = 0, int hinhThucSearch = 0)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("DangNhap", "KhachHang");
            if (Session["User"] == null)
                return RedirectToAction("DangNhap", "KhachHang");

            TAIKHOAN u = Session["Admin"] as TAIKHOAN;
            if (u.TENDN != TENDN)
                return RedirectToAction("DangNhap", "KhachHang");
            
            using (var db = new ShopDongHoDataContext())
            {
                int pageNum = page ?? 1;

                // load hoa don
                var HoaDon = db.HOADONs.OrderByDescending(t => t.NGAYLAP).ToList();

                if (search != "")
                {
                    var idKhach = db.KHACHHANGs.FirstOrDefault(t => t.TENKH.Trim() == search.Trim()).IDKhachHang;
                    HoaDon = HoaDon.Where(t => t.IDKhachHang == idKhach).ToList();
                    
                }
                if (createDate != "")
                {
                    DateTime date = Convert.ToDateTime(createDate);
                    HoaDon = HoaDon.Where(t => t.NGAYLAP == date).ToList();
                }

                if (tenNV != "0" && tenNV != "")
                {
                    var MaNV = db.NHANVIENs.FirstOrDefault(t => t.TENNV.Trim() == tenNV.Trim()).MANV;
                    HoaDon = HoaDon.Where(t => t.MANV == MaNV).ToList();
                }

                if (trangThaiSearch != 0)
                {
                    HoaDon = HoaDon.Where(t => t.TINHTRANG == trangThaiSearch).ToList();
                }

                if (hinhThucSearch != 0)
                {
                    HoaDon = HoaDon.Where(t => t.MAHINHTHUCTHANHTOAN == hinhThucSearch).ToList();
                }

                var HoaDons = HoaDon.ToPagedList(pageNum, pageSize);

                // load hinhthuc
                var HinhThucs = db.HINHTHUCs.ToList();

                // load trang thai
                var TrangThais = TrangThaiDonHang.Select(t => new { value = t.Key, text = t.Value }).ToList();

                //load nhan vien
                var NhanViens = db.NHANVIENs.Select(t => new { value = t.MANV, text = t.TENNV }).ToList();
                NhanViens.Insert(0, new { value = "0", text = "- Nhân viên -" });

                //load danh sach hinh thuc
                var hinhthucs = db.HINHTHUCs.Select(t => new { value = t.MAHINHTHUC, text = t.TENHINHTHUC }).ToList();
                hinhthucs.Insert(0, new { value = 0, text = "- Hình Thức -" });



                ViewBag.search = search;
                ViewBag.createDate = createDate;
                ViewBag.tenNV = tenNV;
                ViewBag.trangThaiSearch = trangThaiSearch;
                ViewBag.hinhThucSearch = hinhThucSearch;

                ViewBag.NhanViens = NhanViens;
                ViewBag.TrangThais = TrangThais;
                ViewBag.hinhthucs = hinhthucs;
                ViewBag.trangThai = TrangThaiDonHang;
                ViewBag.hinhThuc = HinhThucs;
                ViewBag.tendn = TENDN;

                Session["Admin"] = u;
                Session["User"] = u;
                return View(HoaDons);
            }
        }

        public ActionResult ThemDH(string TENDN = "")
        {
            using (var db = new ShopDongHoDataContext())
            {
                if (Session["Admin"] == null)
                    return RedirectToAction("DangNhap", "KhachHang");
                if (Session["User"] == null)
                    return RedirectToAction("DangNhap", "KhachHang");
                TAIKHOAN u = Session["User"] as TAIKHOAN;
                
                var SanPhams = db.SANPHAMs.Select(t => new { value = t.MASP, text = t.TENSP }).ToList();
                SanPhams.Insert(0, new { value = "0", text = "-Tất cả sản phẩm-" });
                ViewBag.SanPhams = SanPhams;
                ViewBag.TENDN = TENDN;
                return View();
            }

        }

        [HttpPost]
        public JsonResult AddDH(int idKH = 0, string total = "", string TENDN = "")
        {
            try
            {
                using (var db = new ShopDongHoDataContext())
                {
                    var tk = db.TAIKHOANs.FirstOrDefault(t => t.TENDN == TENDN);
                    Session["Admin"] = tk;
                    Session["User"] = tk;

                    DateTime now = DateTime.Now;
                    var count = db.HOADONs.Count();
                    count++;
                    string MAHD = "HD" + now.Day + now.Month + now.Year + count;
                    HOADON hd = new HOADON
                    {
                        MAHD = MAHD,
                        IDKhachHang = idKH,
                        MAHINHTHUCTHANHTOAN = 1,
                        NGAYLAP = DateTime.Now,
                        TONGTIEN = int.Parse(total),
                        GHICHU = "Đơn hàng thanh toán có giá trị " + long.Parse(total),
                        TRANGTHAI = true,
                        TINHTRANG = 1,
                    };
                    db.HOADONs.InsertOnSubmit(hd);
                    db.SubmitChanges();

                    var idHD = db.HOADONs.Where(t => t.MAHD.Equals(MAHD)).FirstOrDefault().IDHoaDon;

                    if (idHD > 0)
                    {
                        GioHang gh = Session["GHK"] as GioHang;
                        foreach (var item in gh.dsSP)
                        {
                            var chitiets = new CHITIETHD
                            {
                                IDHoaDon = idHD,
                                MASP = item.maSP,
                                SOLUONG = item.soLuong,
                                DONGIA = item.donGia,
                                THANHTIEN = item.thanhTien,
                                KHUYENMAI = item.khuyenMai
                            };
                            db.CHITIETHDs.InsertOnSubmit(chitiets);
                        }
                    }
                    db.SubmitChanges();
                    GioHang gh_new = new GioHang();
                    Session["GHK"] = gh_new;
                    return Json(new { success = true, msg = "Thêm hóa đơn thành công" });
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return Json(new { success = false, msg = "Thêm hóa đơn không thành công" });
            }

        }

        [HttpPost]
        public JsonResult addSP(string maSP = "", int countSP = 0)
        {
            using (var db = new ShopDongHoDataContext())
            {

                GioHang ghk = Session["GHK"] as GioHang;
                if (ghk == null || countSP == 0)
                    ghk = new GioHang();
                if(ghk.dsSP.Exists(t=> t.maSP == maSP))
                {
                    return Json(new { success = false, msg = "Sản phẩm đã có trong giỏ hàng" });
                }
                ghk.themMatHang(maSP);

                Session["GHK"] = ghk;
                var soluong = ghk.dsSP.FirstOrDefault(t => t.maSP == maSP).soLuong;
                var tensp = ghk.dsSP.FirstOrDefault(t => t.maSP == maSP).tenSP;
                var dongia = ghk.dsSP.FirstOrDefault(t => t.maSP == maSP).donGia;
                var km = ghk.dsSP.FirstOrDefault(t => t.maSP == maSP).khuyenMai;
                var soLuongHienCo = ghk.dsSP.FirstOrDefault(t => t.maSP == maSP).soLuongHienCo;
                var tongTien = ghk.SumALL();
                var thanhTien = ghk.tongThanhTien();
                countSP++;

                return Json(new { success = true, msg = "Thêm mặt hàng thành công", maSP = maSP, tenSP = tensp, soluong = soluong, dongia = dongia, km = km, soluonghienco = soLuongHienCo, tongTien = tongTien, thanhTien = thanhTien, countSP = countSP });
            }
        }


        [HttpPost]
        public JsonResult ReloadTongTien(string maSP, string soLuong)
        {
            using (var db = new ShopDongHoDataContext())
            {
                GioHang ghk = Session["GHK"] as GioHang;
                if (ghk == null)
                    ghk = new GioHang();
                CartItem a = ghk.dsSP.Where(t => t.maSP == maSP).FirstOrDefault();
                var khuyenmai = (from ctk in db.CHITIETKHUYENMAIs
                                 join k in db.KHUYENMAIs on ctk.MAKM equals k.MAKM
                                 where ctk.MASP == maSP && k.TINHTRANG == "Đang Diễn Ra"
                                 select k).FirstOrDefault();
                var km = khuyenmai == null ? 0 : khuyenmai.KHUYENMAI1;
                int temp;
                if (!int.TryParse(soLuong, out temp))
                {

                    return Json(new
                    {
                        success = false,
                    }, JsonRequestBehavior.AllowGet);
                }
                long? tienKM = a.donGia * (km) / 100;
                a.soLuong = int.Parse(soLuong);
                a.khuyenMai = tienKM;
                Session["GHK"] = ghk;
                long? tongTien = ghk.SumALL();
                long? thanhTien = ghk.tongThanhTien();
                long? tongKM = ghk.dsSP.Sum(t => t.khuyenMai);
                return Json(new { success = true, tongTien = tongTien, thanhTien = thanhTien, tongKM = tongKM }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult XoaMatHang(string masp, int countSP)
        {
            bool kq = true;
            GioHang gio = (GioHang)Session["GHK"];
            CartItem a = gio.dsSP.Where(t => t.maSP == masp).FirstOrDefault();
            if (a == null)
            {
                kq = false;
                return Json(new { success = kq });
            }
            gio.dsSP.Remove(a);
            var tongTien = gio.SumALL();
            var thanhTien = gio.tongThanhTien();
            var tongKM = gio.dsSP.Sum(t => t.khuyenMai);
            Session["GHK"] = gio;
            return Json(new { success = kq, countSP = countSP, maSP = masp, tongTien = tongTien, tongKM = tongKM, thanhTien = thanhTien }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddKH(string FullName = "", string GioiTinh = "", string NgaySinh = "", string SDT = "", string DiaChi = "", string CMND = "")
        {
            try
            {
                using (var db = new ShopDongHoDataContext())
                {
                    var kh = db.KHACHHANGs.FirstOrDefault(t => t.TENKH.Trim() == FullName.Trim() && t.SOCMND.Trim() == CMND.Trim());
                    if (kh != null)
                    {
                        var id = kh.IDKhachHang;
                        return Json(new { success = true, idKH = id, msg = "Khách hàng đã có trong hệ thống" });
                    }
                    else
                    {
                        int count = db.KHACHHANGs.Count();
                        count++;
                        KHACHHANG newKH = new KHACHHANG
                        {
                            MAKH = string.Format("KH{0}", count),
                            TENDN = null,
                            TENKH = FullName,
                            GIOITINH = GioiTinh,
                            NGAYSINH = Convert.ToDateTime(NgaySinh),
                            SDT = SDT,
                            DIACHI = DiaChi,
                            SOCMND = CMND,
                            TRANGTHAI = false
                        };
                        db.KHACHHANGs.InsertOnSubmit(newKH);
                        db.SubmitChanges();
                        return Json(new { success = true, idKH = count, msg = "Thêm khách hàng thành công" });
                    }
                }
            }
            catch
            {
                return Json(new { success = false, msg = "Thêm khách hàng không thành công" });
            }


        }


        public ActionResult ChitietDH(int IDHoaDon = 0)
        {
            using (var db = new ShopDongHoDataContext())
            {
                customerLinkOrder khachOrder = new customerLinkOrder();
                var HoaDon = (from hd in db.HOADONs
                              where hd.IDHoaDon == IDHoaDon
                              select new customerLinkOrder
                              {
                                  IDHoaDon = hd.IDHoaDon,
                                  MAHD = hd.MAHD,
                                  IDKHACHHANG = hd.IDKhachHang,
                                  MANV = hd.MANV,
                                  MAHINHTHUCTHANHTOAN = hd.MAHINHTHUCTHANHTOAN,
                                  NGAYLAP = hd.NGAYLAP,
                                  TONGTIEN = hd.TONGTIEN,
                                  TINHTRANG = hd.TINHTRANG ?? 1,
                                  GHICHU = hd.GHICHU,
                                  chitiets = new List<ChitietOrder>(),
                              }).FirstOrDefault();
                var cts = (from cthd in db.CHITIETHDs
                           join sp in db.SANPHAMs on cthd.MASP equals sp.MASP
                           where cthd.IDHoaDon == HoaDon.IDHoaDon
                           select new ChitietOrder
                           {
                               IDHoaDon = HoaDon.IDHoaDon,
                               MASP = cthd.MASP,
                               TENSP = sp.TENSP,
                               HINHANH = sp.HINHANH,
                               SOLUONG = cthd.SOLUONG,
                               KHUYENMAI = cthd.KHUYENMAI ?? 0,
                               DONGIA = cthd.DONGIA,
                               THANHTIEN = cthd.THANHTIEN,
                           }).ToList();
                List<ChitietOrder> newchitiet = new List<ChitietOrder>(cts);
                HoaDon.chitiets = newchitiet;

                var kh = db.KHACHHANGs.FirstOrDefault(t => t.IDKhachHang == HoaDon.IDKHACHHANG);

                var tinhTrangDH = TrangThaiDonHang.Select(t => new { value = t.Key, text = t.Value }).ToList();

                Session["KH"] = kh;
                ViewBag.TrangThai = tinhTrangDH;
                ViewBag.TrangThaiDon = HoaDon.TINHTRANG;
                ViewBag.kh = kh;
                ViewBag.km = HoaDon.chitiets.Sum(t => t.SOLUONG * t.KHUYENMAI);
                ViewBag.tongTien = HoaDon.TONGTIEN;
                ViewBag.thanhTien = HoaDon.chitiets.Sum(t => t.THANHTIEN);
                return View(HoaDon);
            }
        }

        [HttpPost]
        public async Task<ActionResult> ChitietDH(int IDHoaDon = 0, int tt = 0)
        {
            using (var db = new ShopDongHoDataContext())
            {
                TAIKHOAN tk = Session["Admin"] as TAIKHOAN;
                if (tk == null)
                    return RedirectToAction("DangNhap", "KhachHang");
                var hd = db.HOADONs.FirstOrDefault(t => t.IDHoaDon == IDHoaDon);
                if(hd.TINHTRANG == 1 && tt != 4)
                {
                    return RedirectToAction("Index", "QuanLyDonHang", new { TENDN = tk.TENDN });
                }

                hd.TINHTRANG = tt;
                var user = (from h in db.HOADONs
                            join k in db.KHACHHANGs on h.IDKhachHang equals k.IDKhachHang
                            join t in db.TAIKHOANs on k.TENDN equals t.TENDN
                            select t).FirstOrDefault();
                switch (tt)
                {
                    case 2:
                        if (user != null)
                        {
                            var homeUrl = Url.Action("Home", "Home", null, protocol: Request.Url.Scheme);

                            MailContext mailCont = new MailContext();
                            mailCont.URL = homeUrl;
                            mailCont.MAHD = hd.MAHD;
                            mailCont.content = "đang được vận chuyển, shipper sẽ liên lạc với quý khách trong thời gian sớm nhất.";
                            mailCont.Reason = "";

                            string cont = JsonConvert.SerializeObject(mailCont);
                            await sendMailTrangThai(user, cont, "Thông báo đơn hàng");
                        }
                        break;
                    case 3:
                        if (user != null)
                        {
                            var homeUrl = Url.Action("Home", "Home", null, protocol: Request.Url.Scheme);

                            MailContext mailCont = new MailContext();
                            mailCont.URL = homeUrl;
                            mailCont.MAHD = hd.MAHD;
                            mailCont.content = "";
                            mailCont.Reason = "";

                            string cont = JsonConvert.SerializeObject(mailCont);
                            await sendMailThanhToan(user, cont, "Thanh toán thành công");
                        }
                        break;
                    case 4:
                        if (user != null)
                        {
                            var homeUrl = Url.Action("Home", "Home", null, protocol: Request.Url.Scheme);

                            MailContext mailCont = new MailContext();
                            mailCont.URL = homeUrl;
                            mailCont.MAHD = hd.MAHD;
                            mailCont.content = "Đã bị hủy";
                            mailCont.Reason = "Sẽ có nhân viên gọi điện thông báo cho quý khách trong vòng 24h.";

                            string cont = JsonConvert.SerializeObject(mailCont);
                            await sendMailTrangThai(user,  cont, "Đơn hàng bị hủy");
                        }
                        var cthd = db.CHITIETHDs.Where(t => t.IDHoaDon == hd.IDHoaDon);
                        foreach (var item in cthd)
                        {
                            var sp = db.SANPHAMs.FirstOrDefault(t => t.MASP == item.MASP);
                            sp.SOLUONG += item.SOLUONG;
                        }
                        break;
                }
                db.SubmitChanges();
                return RedirectToAction("Index", "QuanLyDonHang", new { TENDN = tk.TENDN });
            }
        }



        public async Task<bool> sendMailThanhToan(TAIKHOAN user, string cont, string subject)
        {
            try
            {
                MailContext noidung = JsonConvert.DeserializeObject<MailContext>(cont);

                string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/template/notiDonHangThanhToan.html"));
                content = content.Replace("{{MADH}}", noidung.MAHD);
                content = content.Replace("{{URL}}", noidung.URL);

                var usermail = user.Email;
                Mail email = new Mail();
                await email.sendMail(usermail, subject, content);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> sendMailTrangThai(TAIKHOAN user,string cont,string subject)
        {
            try
            {
                MailContext noidung = JsonConvert.DeserializeObject<MailContext>(cont);

                string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/template/notiDonHangTrangThai.html"));
                content = content.Replace("{{MADH}}", noidung.MAHD);
                content = content.Replace("{{content}}", noidung.content);
                content = content.Replace("{{Reason}}", noidung.Reason);
                content = content.Replace("{{URL}}", noidung.URL);

                var usermail = user.Email;
                Mail email = new Mail();
                await email.sendMail(usermail, subject, content);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        [HttpPost]
        public async Task<JsonResult> cancelOrder(int IDHoaDon = 0)
        {
            try
            {
                using (var db = new ShopDongHoDataContext())
                {
                    var hd = db.HOADONs.FirstOrDefault(t => t.IDHoaDon == IDHoaDon);
                    var user = (from h in db.HOADONs
                                join k in db.KHACHHANGs on h.IDKhachHang equals k.IDKhachHang
                                join tk in db.TAIKHOANs on k.TENDN equals tk.TENDN
                                select tk).FirstOrDefault();
                    if(user != null)
                    {
                        var homeUrl = Url.Action("Home", "Home", null, protocol: Request.Url.Scheme);

                        MailContext mailCont = new MailContext();
                        mailCont.URL = homeUrl;
                        mailCont.MAHD = hd.MAHD;
                        mailCont.content = "Đã bị hủy";
                        mailCont.Reason = "Sẽ có nhân viên gọi điện thông báo cho quý khách trong vòng 24h";

                        string cont = JsonConvert.SerializeObject(mailCont);
                        await sendMailTrangThai(user, "Đơn hàng bị hủy", cont);
                    }
                    hd.TINHTRANG = 4;
                    var cthd = db.CHITIETHDs.Where(t => t.IDHoaDon == hd.IDHoaDon);
                    foreach (var item in cthd)
                    {
                        var sp = db.SANPHAMs.FirstOrDefault(t => t.MASP == item.MASP);
                        sp.SOLUONG += item.SOLUONG;
                    }
                    db.SubmitChanges();
                    return Json(new { success = true, msg = "Hủy đơn hàng thành công" });
                }
            }
            catch
            {
                return Json(new { success = false, msg = "Hủy đơn không thành công" });
            }
        }
    }
    public class MailContext
    {
        public string MAHD { get; set; }

        public string content { get; set; }

        public string Reason { get; set; }

        public string URL { get; set; }

    }
}