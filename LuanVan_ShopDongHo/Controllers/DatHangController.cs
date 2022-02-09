using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuanVan_ShopDongHo.Models;
using PagedList;
using PagedList.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace LuanVan_ShopDongHo.Controllers
{
    public class DatHangController : Controller
    {
        // GET: DatHang
        ShopDongHoDataContext db = new ShopDongHoDataContext();
        public ActionResult Index()
        {
            return View();
        }

        Dictionary<int, string> TrangThaiDonHang = new Dictionary<int, string>
        {
            {0,"Tất cả trạng thái"},
            {1,"Đang chờ xử lý"},
            {2,"Đang vận chuyển"},
            {3,"Đơn hàng đã thanh toán"},
            {4,"Đơn hàng đã hủy"}
        };

        //Xem thông tin giỏ hàng
        public ActionResult XemGioHang()
        {
            TAIKHOAN tk = (TAIKHOAN)Session["user"];
            if (tk == null)
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            GioHang gio = (GioHang)Session["GH"];
            if (gio == null)
            {
                gio = new GioHang();
            }
            return View(gio);
        }

        //Hiển thị số lượng hiện có trong giỏ hàng dưới dạng Partial View
        public ActionResult HienThiSLMH()
        {
            GioHang gio = (GioHang)Session["GH"];
            if (gio == null)
            {
                gio = new GioHang();
            }
            return PartialView(gio);
        }


        public JsonResult ThemVaoGio(string maSP)
        {
            SANPHAM sp = db.SANPHAMs.Where(t => t.MASP == maSP).FirstOrDefault();
            if (sp == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Sản phẩm này không tồn tại",
                }, JsonRequestBehavior.AllowGet);
            }
            GioHang gio = (GioHang)Session["GH"];
            if (gio == null)
            {
                gio = new GioHang();
            }
            CartItem item = new CartItem(maSP);
            bool kq = gio.themMatHang(maSP);
            Session["GH"] = gio;
            return Json(new
            {
                kq = true,
                message = "Thêm thành công",
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult XoaMatHang(string maSP)
        {
            bool kq = true;
            GioHang gio = (GioHang)Session["GH"];
            CartItem a = gio.dsSP.Where(t => t.maSP == maSP).FirstOrDefault();
            if (a == null)
            {
                kq = false;
            }
            gio.dsSP.Remove(a);
            Session["GH"] = gio;
            return Json(new
            {
                data = gio.dsSP,
                status = kq
            }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult XoaMatHangOrder(string maSP)
        {
            bool kq = true;
            GioHang gio = (GioHang)Session["GH"];
            CartItem a = gio.dsSP.Where(t => t.maSP == maSP).FirstOrDefault();
            if (a == null)
            {
                kq = false;
            }
            gio.dsSP.Remove(a);

            Session["GH"] = gio;

            return Json(new
            {
                data = gio.dsSP,
                status = kq,
                tongTien = gio.SumALL(),
                thanhTien = gio.tongThanhTien(),
                km = gio.dsSP.Sum(t => t.khuyenMai * t.soLuong)

            }, JsonRequestBehavior.AllowGet); ;

        }

        public JsonResult reloadTongTien(string maSP, string soLuong)
        {
            bool kq = true;
            GioHang gio = (GioHang)Session["GH"];
            if (gio == null)
            {
                gio = new GioHang();
            }
            CartItem a = gio.dsSP.Where(t => t.maSP == maSP).FirstOrDefault();
            var ctkm = (from ctk in db.CHITIETKHUYENMAIs
                        join k in db.KHUYENMAIs on ctk.MAKM equals k.MAKM
                        where ctk.MASP == maSP && k.TINHTRANG == "Đang Diễn Ra"
                        select k).FirstOrDefault();
            var km = ctkm == null ? null : ctkm.KHUYENMAI1;
            int temp;
            if (!int.TryParse(soLuong, out temp))
            {
                kq = false;
                return Json(new
                {
                    kq = kq,
                }, JsonRequestBehavior.AllowGet);
            }
            long? tienKM = a.donGia * (km ?? 0) / 100;
            a.soLuong = int.Parse(soLuong);
            a.khuyenMai = tienKM;
            Session["GH"] = gio;
            long? tongTien = gio.SumALL();
            long? thanhTien = a.thanhTien;
            return Json(new
            {
                kq = kq,
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult loadGioHang()
        {
            GioHang gio = (GioHang)Session["GH"];
            if (gio == null)
            {
                gio = new GioHang();
            }
            return Json(new
            {
                data = gio.dsSP,
                tongTien = gio.SumALL(),
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Order()
        {
            if (Session["GH"] == null)
            {
                ViewBag.errorMessage = "Vui lòng chọn sản phẩm";
                return RedirectToAction("Home", "Home");
            }
            if (Session["KH"] == null)
            {
                ViewBag.errorMessage = "Vui lòng đăng nhập";
                return RedirectToAction("DangNhap", "KhachHang");
            }

            using (var db = new ShopDongHoDataContext())
            {
                var hinhThucs = db.HINHTHUCs.Select(x => new { value = x.MAHINHTHUC, text = x.TENHINHTHUC }).ToList();
                hinhThucs.Insert(0, new { value = 0, text = "- Loại Hình Thức -" });

                order orderKH = new order();
                orderKH.gh = Session["GH"] as GioHang;
                orderKH.kh = Session["KH"] as KHACHHANG;
                var kh = db.KHACHHANGs.FirstOrDefault(t => t.IDKhachHang == orderKH.kh.IDKhachHang);
                ViewBag.hinhThucTT = hinhThucs;
                ViewBag.order = orderKH;
                ViewBag.kh = kh;
                ViewBag.km = orderKH.gh.dsSP.Sum(t => t.khuyenMai * t.soLuong);
                ViewBag.tongTien = orderKH.gh.SumALL();
                ViewBag.thanhTien = orderKH.gh.tongThanhTien();
                return View(orderKH.gh);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Order(string idKH = "", string hinhthuc = "", string total = "", string noteDH = "")
        {
            try
            {
                if (Session["GH"] == null)
                {
                    ViewBag.errorMessage = "Vui lòng chọn sản phẩm";
                    return RedirectToAction("Home", "Home");
                }
                if (Session["KH"] == null)
                {
                    ViewBag.errorMessage = "Vui lòng đăng nhập";
                    return RedirectToAction("DangNhap", "KhachHang");
                }
                total = total.Replace(",", "");
                using (var db = new ShopDongHoDataContext())
                {
                    KHACHHANG kh = Session["KH"] as KHACHHANG;
                    var hinhThucs = db.HINHTHUCs.Select(x => new { value = x.MAHINHTHUC, text = x.TENHINHTHUC }).ToList();
                    TAIKHOAN user = Session["User"] as TAIKHOAN;
                    if (user == null)
                    {
                        return RedirectToAction("DangNhap", "KhachHang");
                    }
                    hinhThucs.Insert(0, new { value = 0, text = "- Loại Hình Thức -" });

                    DateTime now = DateTime.Now;
                    var count = db.HOADONs.Count();
                    count++;
                    string MAHD = "HD" + now.Day + now.Month + now.Year + count;

                    try
                    {
                        var homeUrl = Url.Action("Home", "Home", null, protocol: Request.Url.Scheme);

                        string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/template/notiDonHangDat.html"));
                        content = content.Replace("{{MADH}}", MAHD);
                        content = content.Replace("{{URL}}", homeUrl);

                        var usermail = user.Email;
                        Mail email = new Mail();
                        await email.sendMail(usermail, "Đặt hàng thành công", content);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        order orderKH = new order();
                        orderKH.gh = Session["GH"] as GioHang;
                        orderKH.kh = Session["KH"] as KHACHHANG;
                        ViewBag.hinhThucTT = hinhThucs;
                        ViewBag.order = orderKH;
                        ViewBag.kh = kh;
                        ViewBag.km = orderKH.gh.dsSP.Sum(t => t.khuyenMai * t.soLuong);
                        ViewBag.tongTien = orderKH.gh.SumALL();
                        ViewBag.thanhTien = orderKH.gh.tongThanhTien();
                        return View(orderKH.gh);
                    }

                    HOADON hd = new HOADON
                    {
                        MAHD = MAHD,
                        IDKhachHang = int.Parse(idKH),
                        MAHINHTHUCTHANHTOAN = int.Parse(hinhthuc),
                        NGAYLAP = DateTime.Now,
                        TONGTIEN = int.Parse(total),
                        GHICHU = noteDH,
                        TRANGTHAI = true,
                        TINHTRANG = 1,
                    };
                    db.HOADONs.InsertOnSubmit(hd);
                    db.SubmitChanges();

                    var idHD = db.HOADONs.Where(t => t.MAHD.Equals(MAHD)).FirstOrDefault().IDHoaDon;

                    if (idHD > 0)
                    {
                        GioHang gh = Session["GH"] as GioHang;
                        foreach (var item in gh.dsSP)
                        {
                            for (int i = 0; i < item.soLuong; i++)
                            {
                                var chitiet = new CHITIETHD
                                {
                                    IDHoaDon = idHD,
                                    MASP = item.maSP,
                                    SOLUONG = 1,
                                    DONGIA = item.donGia,
                                    THANHTIEN = item.thanhTien,
                                    KHUYENMAI = item.khuyenMai
                                };
                                db.CHITIETHDs.InsertOnSubmit(chitiet);
                            }
                        }
                    }
                    db.SubmitChanges();
                    GioHang gh_new = new GioHang();
                    Session["GH"] = gh_new;
                    return RedirectToAction("Home", "Home", new { msg = "Đặt hàng thành công" });
                }
            }
            catch (Exception)
            {
                using (var db = new ShopDongHoDataContext())
                {
                    var hinhThucs = db.HINHTHUCs.Select(x => new { value = x.MAHINHTHUC, text = x.TENHINHTHUC }).ToList();
                    hinhThucs.Insert(0, new { value = 0, text = "- Loại Hình Thức -" });

                    order orderKH = new order();
                    orderKH.gh = Session["GH"] as GioHang;
                    orderKH.kh = Session["KH"] as KHACHHANG;
                    ViewBag.hinhThucTT = hinhThucs;
                    ViewBag.order = orderKH;
                    ViewBag.kh = orderKH.kh;
                    ViewBag.tongTien = orderKH.gh.SumALL();
                    ViewBag.thanhTien = orderKH.gh.tongThanhTien();
                    ViewBag.error = "Đặt hàng không thành công";
                    return View(orderKH.gh);
                }
            }
        }
        public ActionResult ShowCustomOrder()
        {
            if (Session["KH"] == null)
                return RedirectToAction("Home", "Home");
            return View();
        }
        public ActionResult ShowOrders(int? page, int? trangthai = -1)
        {
            if (Session["KH"] == null)
                return RedirectToAction("Home", "Home");
            KHACHHANG kh = Session["KH"] as KHACHHANG;
            using (var db = new ShopDongHoDataContext())
            {

                customerLinkOrder khachOrder = new customerLinkOrder();
                var HoaDons = (from hd in db.HOADONs
                               where hd.IDKhachHang == kh.IDKhachHang
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
                               }).OrderByDescending(t => t.NGAYLAP).ToList();
                if (trangthai >= 0)
                {
                    HoaDons = HoaDons.Where(t => t.TINHTRANG == trangthai).ToList();
                }

                foreach (var hd in HoaDons)
                {

                    var cts = (from cthd in db.CHITIETHDs
                               join sp in db.SANPHAMs on cthd.MASP equals sp.MASP
                               where cthd.IDHoaDon == hd.IDHoaDon
                               select new
                               {
                                   cthd.IDHoaDon,
                                   cthd.MASP,
                                   cthd.SOLUONG,
                                   cthd.KHUYENMAI,
                                   cthd.DONGIA,
                                   cthd.THANHTIEN,
                                   sp.TENSP,
                                   sp.HINHANH,

                               }).ToList();
                    var ls = cts.GroupBy(t => t.MASP);
                    List<ChitietOrder> newchitiet = new List<ChitietOrder>();
                    foreach (var item in ls)
                    {
                        foreach (var i in item)
                        {
                            ChitietOrder ct = new ChitietOrder();
                            ct.SOLUONG = i.SOLUONG;
                            ct.MASP = i.MASP;
                            ct.THANHTIEN = i.THANHTIEN;
                            ct.DONGIA = i.DONGIA;
                            ct.HINHANH = i.HINHANH;
                            ct.TENSP = i.TENSP;
                            ct.IDHoaDon = i.IDHoaDon ?? 0;
                            newchitiet.Add(ct);
                        } 
                    }
                    hd.chitiets = newchitiet;
                }
                ViewBag.TrangThai = TrangThaiDonHang;
                int pageNumber = page ?? 1;
                int pageSize = 20;
                var results = HoaDons.ToPagedList(pageNumber, pageSize);
                return View(results);
            }
        }

        public ActionResult OrderDetail(int IDHoaDon = 0)
        {
            if (Session["KH"] == null)
                return RedirectToAction("DangNhap", "KhachHang");

            using (var db = new ShopDongHoDataContext())
            {
                var kh = Session["KH"] as KHACHHANG;
                customerLinkOrder khachOrder = new customerLinkOrder();
                var HoaDon = (from hd in db.HOADONs
                              where hd.IDKhachHang == kh.IDKhachHang && hd.IDHoaDon == IDHoaDon
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
                           select new
                           {
                               cthd.IDHoaDon,
                               cthd.MASP,
                               cthd.SOLUONG,
                               cthd.KHUYENMAI,
                               cthd.DONGIA,
                               cthd.THANHTIEN,
                               sp.TENSP,
                               sp.HINHANH,

                           }).ToList();
                var ls = cts.GroupBy(t => t.MASP).Select(grp => grp.ToList()).ToList();
                List<ChitietOrder> newchitiet = new List<ChitietOrder>();
                foreach (var item in ls)
                {

                    ChitietOrder ct = new ChitietOrder();

                    for (int i = 0; i < item.Count(); i++)
                    {
                        if (i == 0)
                        {
                            ct.SOLUONG = item[i].SOLUONG;
                            ct.MASP = item[i].MASP;
                            ct.THANHTIEN = item[i].THANHTIEN;
                            ct.DONGIA = item[i].DONGIA;
                            ct.HINHANH = item[i].HINHANH;
                            ct.TENSP = item[i].TENSP;
                            ct.IDHoaDon = item[i].IDHoaDon ?? 0;
                            ct.KHUYENMAI = item[i].KHUYENMAI;
                        }
                        else
                        {
                            ct.SOLUONG++;
                            ct.THANHTIEN += item[i].THANHTIEN;
                            ct.KHUYENMAI += item[i].KHUYENMAI;
                        }
                    }
                }
                HoaDon.chitiets = newchitiet;
                Session["KH"] = kh;
                ViewBag.TrangThai = TrangThaiDonHang;
                ViewBag.TrangThaiDon = HoaDon.TINHTRANG;
                ViewBag.kh = kh;
                ViewBag.km = HoaDon.chitiets.Sum(t => t.SOLUONG * t.KHUYENMAI);
                ViewBag.tongTien = HoaDon.TONGTIEN;
                ViewBag.thanhTien = HoaDon.chitiets.Sum(t => t.THANHTIEN);
                return View(HoaDon);
            }
        }


        public async Task<JsonResult> cancelOrder(int IDHoaDon = 0)
        {
            try
            {
                using (var db = new ShopDongHoDataContext())
                {
                    var hd = db.HOADONs.FirstOrDefault(t => t.IDHoaDon == IDHoaDon);
                    if (hd.TINHTRANG != 1)
                        return Json(new { success = false, msg = "Bạn không thể hủy đơn hàng này" });
                    hd.TRANGTHAI = false;
                    hd.TINHTRANG = 4;
                    TAIKHOAN user = Session["User"] as TAIKHOAN;
                    try
                    {
                        var homeUrl = Url.Action("Home", "Home", null, protocol: Request.Url.Scheme);

                        string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/template/notiDonHangTrangThai.html"));
                        content = content.Replace("{{MADH}}", hd.MAHD);
                        content = content.Replace("{{content}}", "đã được hủy thành công");
                        content = content.Replace("{{URL}}", homeUrl);

                        var usermail = user.Email;
                        Mail email = new Mail();
                        await email.sendMail(usermail, "Hủy đơn thành công", content);
                    }
                    catch (Exception ex)
                    {
                        return Json(new { success = false, msg = "Hủy đơn không thành công" });
                    }
                    var cthd = db.CHITIETHDs.Where(t => t.IDHoaDon == hd.IDHoaDon);
                    foreach (var item in cthd)
                    {
                        var sp = db.SANPHAMs.FirstOrDefault(t => t.MASP == item.MASP);
                        sp.SOLUONG += item.SOLUONG;
                    }
                    db.SubmitChanges();
                    return Json(new { success = true, msg = "Hủy thành công", data = hd.TINHTRANG });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Json(new { sucess = false, msg = "Hủy đơn không thành công" });
            }
        }

        [HttpPost]
        public JsonResult getVourcher(string maVourcher = "",string money ="")
        {
            try
            {
                using (var db = new ShopDongHoDataContext())
                {
                    double total = 0;
                    bool kq = double.TryParse(money, out total);
                    if(kq == false)
                    {
                        return Json(new { succes = false });
                    }
                    var sales = (from ctv in db.CHUONGTRINHVOUCHERs
                                 join v in db.VOUCHERs on ctv.MACHUONGTRINH equals v.MACHUONGTRINH
                                 where v.MAVOUCHER == maVourcher
                                 select ctv).FirstOrDefault();
                    if(sales == null)
                    {
                        return Json(new { succes = false });
                    }


                    total = total - (total * ((float)(sales.GIAMGIA ?? 0)/100));

                    return Json(new { success = true , data = total });
                }
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public JsonResult GetPhiVanChuyen(string param1, string param2, string param3)
        {
            try
            {
                float distance = 0;
                long orderCost = 0;
                string[] d = param1.Split(new char[] { ',', '.' });
                bool kq1 = float.TryParse(d[0], out distance);
                bool kq2 = long.TryParse(param2, out orderCost);
                long cost = 0;
                string msg = "";
                if (kq1 == true && kq2 == true)
                {
                    if (distance < 10 || orderCost > 20000000)
                    {
                        cost = 0;
                        msg = "Đơn hàng của quý khách " + param3 + " được freeship";
                    }
                    else if (distance < 20)
                    {
                        cost = 15000;
                        msg = "Đơn hàng của quý khách " + param3 + " có phí vận chuyển : " + string.Format("{0:#,###}", cost);
                    }
                    else
                    {
                        cost = 15000;
                        distance = distance - 20;
                        int multi = (int)distance / 5;
                        cost = cost + multi * 500;
                        msg = "Đơn hàng của quý khách " + param3 + " có phí vận chuyển : " + string.Format("{0:#,###}", cost);
                    }
                    return Json(new { success = true, data = cost, msg = msg });
                }

                return Json(new { success = false, msg = "Cannot calculate distance between two points" });
            }
            catch
            {
                return Json(new { success = false, msg = "Order fail" });
            }
        }
    }
}