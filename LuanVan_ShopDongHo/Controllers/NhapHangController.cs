using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using LuanVan_ShopDongHo.Models;
using PagedList;
using Rotativa;

namespace LuanVan_ShopDongHo.Controllers
{
    public class NhapHangController : Controller
    {
        ShopDongHoDataContext db = new ShopDongHoDataContext();
        QuanLyController ql = new QuanLyController();
        public List<PhieuNhap> dsPN;

        // GET: NhapHang
        public ActionResult Index()
        {
            return View();
        }

        public List<SelectListItem> taoListPhienBan()
        {
            List<SelectListItem> ds = new List<SelectListItem>();
            ds.Add(new SelectListItem() { Text = "Phiên bản đặc biệt", Value = "Phiên bản đặc biệt" });
            ds.Add(new SelectListItem() { Text = "Phiên bản đặc thường", Value = "Phiên bản thường" });
            ds.Add(new SelectListItem() { Text = "Phiên bản đặc biệt_bluetooth", Value = "Phiên bản đặc biệt_bluetooth" });
            ds.Add(new SelectListItem() { Text = "Phiên bản đặc biệt_chrono", Value = "Phiên bản đặc biệt_chrono" });
            ds.Add(new SelectListItem() { Text = "Phiên bản đặc biệt_kim cương", Value = "Phiên bản đặc biệt_kim cương" });
            ds.Add(new SelectListItem() { Text = "Phiên bản đặc biệt_light", Value = "Phiên bản đặc biệt_light" });
            ds.Add(new SelectListItem() { Text = "Phiên bản đặc biệt_limited", Value = "Phiên bản đặc biệt_limited" });
            ds.Add(new SelectListItem() { Text = "Phiên bản đặc biệt_siêu mỏng", Value = "Phiên bản đặc biệt_siêu mỏng" });
            return ds;
        }

        public List<SelectListItem> taoListNangLuong()
        {
            var dsNL = new List<SelectListItem>();
            dsNL.Add(new SelectListItem() { Text = "Automatic (Tự động)", Value = "Automatic (Tự động)" });
            dsNL.Add(new SelectListItem() { Text = "Eco-Drive (Năng lượng ánh sáng)", Value = "Eco-Drive (Năng lượng ánh sáng)" });
            dsNL.Add(new SelectListItem() { Text = "Quartz (PIN)", Value = "Quartz (PIN)" });
            dsNL.Add(new SelectListItem() { Text = "Solar (Năng lượng ánh sáng)", Value = "Solar (Năng lượng ánh sáng)" });

            return dsNL;
        }

        public List<SelectListItem> taoListLoaiDay()
        {
            List<SelectListItem> ds = new List<SelectListItem>();
            ds.Add(new SelectListItem() { Text = "Dây cao su", Value = "Dây cao su" });
            ds.Add(new SelectListItem() { Text = "Dây hợp kim", Value = "Dây hợp kim" });
            ds.Add(new SelectListItem() { Text = "Dây da", Value = "Dây da" });
            ds.Add(new SelectListItem() { Text = "Dây kim loại", Value = "Dây kim loại" });
            ds.Add(new SelectListItem() { Text = "Dây silicone", Value = "Dây silicone" });
            ds.Add(new SelectListItem() { Text = "Dây vải", Value = "Dây vải" });
            return ds;
        }

        public List<SelectListItem> taoListThoiHanBaoHanh()
        {
            List<SelectListItem> ds = new List<SelectListItem>();
            ds.Add(new SelectListItem() { Text = "1 năm", Value = "1" });
            ds.Add(new SelectListItem() { Text = "2 năm", Value = "2" });
            ds.Add(new SelectListItem() { Text = "3 năm", Value = "3" });
            ds.Add(new SelectListItem() { Text = "4 năm", Value = "4" });
            return ds;
        }

        public List<SelectListItem> taoListTinhTrangPhieuNhap()
        {
            List<SelectListItem> ds = new List<SelectListItem>();
            ds.Add(new SelectListItem() { Text = "Đã xử lý", Value = "true" });
            ds.Add(new SelectListItem() { Text = "Chưa xử lý", Value = "false" });
            return ds;
        }

        public List<SelectListItem> taoListNV()
        {
            List<SelectListItem> ds = new List<SelectListItem>();
            List<NHANVIEN> dsNV = db.NHANVIENs.ToList();
            for (int i = 0; i < dsNV.Count; i++)
            {
                ds.Add(new SelectListItem() { Text = dsNV[i].TENNV, Value = dsNV[i].MANV });
            }
            return ds;
        }

        public List<SelectListItem> taoListHang()
        {
            List<SelectListItem> ds = new List<SelectListItem>();
            List<HANG> dsH = db.HANGs.ToList();
            for (int i = 0; i < dsH.Count; i++)
            {
                ds.Add(new SelectListItem() { Text = dsH[i].TENHANG, Value = dsH[i].MAHANG });
            }
            return ds;
        }


        public ActionResult QuanLyPhieuNhap(int? page, string idNV = "", DateTime? ngayTao = null, string idHang = "", bool? tinhTrang = null)
        {
            TAIKHOAN tk = (TAIKHOAN)Session["Admin"];
            if (tk == null)
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            bool temp = tinhTrang ?? false;
            List<PhieuNhap> ds = (from k in db.PHIEUNHAPs
                                  join nv in db.NHANVIENs on k.MANV equals nv.MANV
                                  join h in db.HANGs on k.MAHANG equals h.MAHANG
                                  where k.TRANGTHAI == temp
                                  select new PhieuNhap
                                  {
                                      IDPhieuNhap = k.IDPhieuNhap,
                                      maPN = k.MAPN,
                                      tenNV = nv.TENNV,
                                      tenHang = h.TENHANG,
                                      ngayLap = k.NGAYLAP,
                                      tongTien = k.TONGTIEN,
                                      tinhTrang = k.TINHTRANG,
                                  }).ToList();

            if (page == null)
            {
                page = 1;
            }
            int pagesize = 10; //Số sản phẩm hiển thị trên 1 trang
            int pageNumber = (page ?? 1);//Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
                                         // nếu page = null thì lấy giá trị 1 cho biến pageNumber.



            if (idNV != "")
            {
                var nv = db.NHANVIENs.FirstOrDefault(t => t.MANV == idNV);
                ds = ds.Where(t => t.tenNV == nv.TENNV).ToList();
                ViewBag.idNV = nv.MANV;
            }
            if (ngayTao != null)
            {
                ds = ds.Where(t => t.ngayLap.Value == ngayTao.Value).ToList();
                ViewBag.ngayTao = ngayTao;
            }
            if (idHang != "")
            {
                var hang = db.HANGs.FirstOrDefault(t => t.MAHANG == idHang);
                ds = ds.Where(t => t.tenHang == hang.TENHANG).ToList();
                ViewBag.nhaCC = idHang;
            }
            if(tinhTrang != null)
            {
                ds = ds.Where(t => t.tinhTrang == tinhTrang).ToList();
                ViewBag.tinhTrang = tinhTrang;
            }

            var lst = ds.ToPagedList(pageNumber, pagesize);
            dsPN = ds;
            var dsTinhTrang = taoListTinhTrangPhieuNhap();
            var dsNV = taoListNV();
            var dsHang = taoListHang();

            ViewBag.dsTinhTrang = dsTinhTrang;
            ViewBag.dsNV = dsNV;
            ViewBag.dsHang = dsHang;
            ViewBag.pageNum = pageNumber;
            ViewBag.pageSize = pagesize;
            ViewBag.dsPN = lst;


            PHIEUNHAP pn = new PHIEUNHAP();
            return View(pn);
        }

        public ActionResult TaoPhieuNhap()
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
            List<HANG> dsHang = db.HANGs.ToList();
            List<NOIDUNGBH> dsBaoHanh = db.NOIDUNGBHs.Where(t => t.TRANGTHAI == false).ToList();
            List<DANHMUCSP> dsDMSP = db.DANHMUCSPs.ToList();
            List<SANPHAM> dsSP = db.SANPHAMs.ToList();
            var dsPhienBan = taoListPhienBan();
            var dsNangLuong = taoListNangLuong();
            var dsLoaiDay = taoListLoaiDay();
            var dsThoiHanBaoHanh = taoListThoiHanBaoHanh();
            var dsQuocGia = ql.taoListQuocGia();

            ViewBag.nhanVien = nv.TENNV;
            ViewBag.dsHangDB = dsHang;
            ViewBag.dsBaoHanh = dsBaoHanh;
            ViewBag.dsDMSP = dsDMSP;
            ViewBag.dsSP = dsSP;
            ViewBag.dsPhienBan = dsPhienBan;
            ViewBag.dsNangLuong = dsNangLuong;
            ViewBag.dsLoaiDay = dsLoaiDay;
            ViewBag.dsThoiHanBaoHanh = dsThoiHanBaoHanh;
            ViewBag.dsQuocGia = dsQuocGia;
            ViewBag.dsHang = taoListHang();
            ViewBag.dsNV = taoListNV();

            NhapHangModel nhapHang = new NhapHangModel();
            return View(nhapHang);
        }

        [HttpPost]
        public JsonResult TaoPN(NhapHangModel nhap)
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
                    message = "Tên đăng nhập không đúng! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }
            bool kq = true;
            PHIEUNHAP pn = db.PHIEUNHAPs.Where(t => t.MAPN == nhap.pn.maPN).FirstOrDefault();
            if (pn != null)
            {
                kq = false;
                return Json(new
                {
                    kq = false,
                    message = "Mã phiếu nhập này đã tồn tại! Xin vui lòng thử lại!",
                }
                , JsonRequestBehavior.AllowGet);
            }

            DateTime now = DateTime.Now;
            var count = db.PHIEUNHAPs.Count();
            count++;
            string ma = "PN" + now.Day + now.Month + now.Year + count;

            PHIEUNHAP phieu = new PHIEUNHAP();
            phieu.MAPN = ma;
            phieu.MANV = nv.MANV;
            phieu.MAHANG = nhap.pn.tenHang;
            phieu.NGAYLAP = DateTime.Now;
            phieu.TINHTRANG = false;
            phieu.TONGTIEN = 0;
            phieu.TRANGTHAI = false;
            db.PHIEUNHAPs.InsertOnSubmit(phieu);
            db.SubmitChanges();

            return Json(new
            {
                kq = kq,
                message = "Tạo thành công",
                mapn = phieu.MAPN,
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSanPhamPN(string maSP)
        {
            var sp = (from k in db.SANPHAMs
                      where k.MASP.Equals(maSP.Trim())
                      select new
                      {
                          k.MASP,
                          k.TENSP,
                          k.DONGIA,
                      });
            return Json(sp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ThemCTPNSPDaCo(string maSP, int soLuong, string maPN, int chietKhau)
        {
            SANPHAM sp = db.SANPHAMs.Where(t => t.MASP == maSP).FirstOrDefault();
            if (sp == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã sản phẩm này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            PHIEUNHAP pn = db.PHIEUNHAPs.Where(t => t.MAPN == maPN).FirstOrDefault();
            if (pn == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã phiếu nhập này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            CHITIETPN chiTietPN = db.CHITIETPNs.Where(t => t.MASP == maSP && t.IDPhieuNhap == pn.IDPhieuNhap).FirstOrDefault();
            if(chiTietPN != null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Sản phẩm này đã tồn tại trong phiếu nhập! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            //sp.SOLUONG += soLuong;

            CHITIETPN ctpn = new CHITIETPN();
            ctpn.IDPhieuNhap = pn.IDPhieuNhap;
            ctpn.MASP = sp.MASP;
            ctpn.SOLUONG = soLuong;
            ctpn.DONGIA = sp.DONGIA;
            ctpn.CHIETKHAU = chietKhau;
            long? temp = soLuong * sp.DONGIA;
            long? tt = temp * chietKhau / 100;
            ctpn.THANHTIEN = temp - tt;

            pn.TONGTIEN += ctpn.THANHTIEN;

            db.CHITIETPNs.InsertOnSubmit(ctpn);
            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Thêm chi tiết phiếu nhập thành công!",
                tt = string.Format("{0:#,##}", pn.TONGTIEN),
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult XoaCTPN(string maPN, string maSP)
        {
            PHIEUNHAP pn = db.PHIEUNHAPs.Where(t => t.MAPN == maPN).FirstOrDefault();
            if (pn == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã phiếu nhập này không tồn tại",
                }, JsonRequestBehavior.AllowGet);
            }
            CHITIETPN ctpn = db.CHITIETPNs.Where(t => t.MASP == maSP && t.IDPhieuNhap == pn.IDPhieuNhap).FirstOrDefault();
            if (ctpn == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Chi tiết phiếu nhập này không tồn tại",
                }, JsonRequestBehavior.AllowGet);
            }

            db.CHITIETPNs.DeleteOnSubmit(ctpn);

            SANPHAM sp = db.SANPHAMs.Where(t => t.MASP == maSP).FirstOrDefault();
            //sp.SOLUONG -= ctpn.SOLUONG;

            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Xóa thành công",
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult XemChiTietPhieuNhap(int idPN, int? page)
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

            List<CHITIETPN> ctpn = db.CHITIETPNs.Where(t => t.IDPhieuNhap == idPN).ToList();
            PHIEUNHAP pn = db.PHIEUNHAPs.Where(t => t.IDPhieuNhap == idPN).FirstOrDefault();
            HANG hang = db.HANGs.Where(t => t.MAHANG == pn.MAHANG).FirstOrDefault();
            NHANVIEN nv = db.NHANVIENs.Where(t => t.MANV == pn.MANV).FirstOrDefault();

            NhapHangModel nhapHang = new NhapHangModel();
            nhapHang.pn = new PhieuNhap();
            nhapHang.ctpn = new List<ChiTietPhieuNhap>();

            nhapHang.pn.IDPhieuNhap = pn.IDPhieuNhap;
            nhapHang.pn.maPN = pn.MAPN;
            nhapHang.pn.tenHang = hang.TENHANG;
            nhapHang.pn.tenNV = nv.TENNV;
            nhapHang.pn.ngayLap = pn.NGAYLAP;
            nhapHang.pn.tongTien = pn.TONGTIEN;
            nhapHang.pn.tinhTrang = pn.TINHTRANG;

            for (int i = 0; i < ctpn.Count; i++)
            {
                ChiTietPhieuNhap ctPhieuNhap = new ChiTietPhieuNhap();
                SANPHAM sp = db.SANPHAMs.Where(t => t.MASP == ctpn[i].MASP).FirstOrDefault();
                ctPhieuNhap.IDPhieuNhap = ctpn[i].IDPhieuNhap;
                ctPhieuNhap.tenSP = sp.TENSP;
                ctPhieuNhap.donGia = ctpn[i].DONGIA;
                ctPhieuNhap.soLuong = ctpn[i].SOLUONG;
                ctPhieuNhap.thanhTien = ctpn[i].THANHTIEN;
                nhapHang.ctpn.Add(ctPhieuNhap);
            }

            var dsTinhTrang = taoListTinhTrangPhieuNhap();
            var lst = nhapHang.ctpn.ToPagedList(pageNumber, pagesize);

            ViewBag.dsPN = lst;
            ViewBag.dsTinhTrang = dsTinhTrang;
            ViewBag.pageNum = pageNumber;
            ViewBag.pageSize = pagesize;

            return View(nhapHang);
        }

        const string validCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        Random ran = new Random();
        int length = 12;
        public JsonResult CapNhatTinhTrang(int maPN, string tinhTrang)
        {
            PHIEUNHAP pn = db.PHIEUNHAPs.Where(t => t.IDPhieuNhap == maPN).FirstOrDefault();
            if (pn == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã phiếu nhập này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);

            }

            if (tinhTrang == "true")
            {
                pn.TINHTRANG = true;
                pn.NGAYNHAPHANG = DateTime.Now;
                List<CHITIETPN> dsCTPN = db.CHITIETPNs.Where(t => t.IDPhieuNhap == maPN).ToList();
                for (int i = 0; i < dsCTPN.Count; i++)
                {
                    SANPHAM sp = db.SANPHAMs.Where(t => t.MASP == dsCTPN[i].MASP).FirstOrDefault();
                    if (sp != null)
                    {
                        for (int j = 0; j < dsCTPN[i].SOLUONG; j++)
                        {
                            try
                            {
                                StringBuilder resultCode;
                                string cd = "";
                                bool duplicated = false;
                                do
                                {
                                    resultCode = new StringBuilder();
                                    int temp = length;
                                    while (0 < temp--)
                                    {
                                        resultCode.Append(validCharacters[ran.Next(validCharacters.Length)]);
                                    }
                                    cd = resultCode.ToString();
                                    var existedK = db.KHOs.FirstOrDefault(t => t.MASP == sp.MASP && t.QRCODE == cd);
                                    if (existedK != null)
                                        duplicated = true;
                                    else
                                        duplicated = false;
                                } while (duplicated == true);
                                KHO k = new KHO
                                {
                                    MASP = sp.MASP,
                                    QRCODE = cd,
                                    NGAYNHAPHANG = pn.NGAYNHAPHANG,
                                    SOLUONG = 1,
                                    TRANGTHAI = 0
                                };
                                db.KHOs.InsertOnSubmit(k);
                                db.SubmitChanges();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                                int newLength = length++;
                                StringBuilder resultCode = new StringBuilder();
                                while (0 < length--)
                                {
                                    resultCode.Append(validCharacters[ran.Next(validCharacters.Length)]);
                                }
                                KHO k = new KHO
                                {
                                    MASP = sp.MASP,
                                    QRCODE = resultCode.ToString(),
                                    NGAYNHAPHANG = pn.NGAYNHAPHANG,
                                    SOLUONG = 1,
                                    TRANGTHAI = 0
                                };
                                db.KHOs.InsertOnSubmit(k);
                                db.SubmitChanges();
                            }
                        }
                    }
                }
            }
            else
            {
                pn.TINHTRANG = false;
                DateTime ngayNhap = pn.NGAYNHAPHANG ?? DateTime.Now;
                List<CHITIETPN> dsCTPN = db.CHITIETPNs.Where(t => t.IDPhieuNhap == maPN).ToList();
                for (int i = 0; i < dsCTPN.Count; i++)
                {
                    SANPHAM sp = db.SANPHAMs.Where(t => t.MASP == dsCTPN[i].MASP).FirstOrDefault();
                    var sanPhamkho = (from k in db.KHOs where k.MASP == dsCTPN[i].MASP && k.NGAYNHAPHANG == ngayNhap select k).ToList();
                    if (sanPhamkho != null && sanPhamkho.Count() > 0)
                    {
                        db.KHOs.DeleteAllOnSubmit(sanPhamkho);
                        db.SubmitChanges();
                    }
                }
            }
            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Cập nhật thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult XuatFilePdfPhieuNhap(int idPN)
        {
            var phieuNhap = (from k in db.PHIEUNHAPs
                             join nv in db.NHANVIENs on k.MANV equals nv.MANV
                             join h in db.HANGs on k.MAHANG equals h.MAHANG
                             where k.IDPhieuNhap == idPN
                             select new PhieuNhap
                             {
                                 IDPhieuNhap = k.IDPhieuNhap,
                                 maPN = k.MAPN,
                                 tenNV = nv.TENNV,
                                 tenHang = h.TENHANG,
                                 ngayLap = k.NGAYLAP,
                                 tongTien = k.TONGTIEN,
                                 tinhTrang = k.TINHTRANG,
                             }).FirstOrDefault();
            var ctpn = (from k in db.CHITIETPNs
                        join sp in db.SANPHAMs on k.MASP equals sp.MASP
                        where k.IDPhieuNhap == idPN
                        select new ChiTietPhieuNhap
                        {
                            IDPhieuNhap = k.IDPhieuNhap,
                            tenSP = sp.TENSP,
                            donGia = k.DONGIA,
                            thanhTien = k.THANHTIEN,
                            soLuong = k.SOLUONG,
                        }).ToList();
            NhapHangXuatFile a = new NhapHangXuatFile();
            a.pn = new PhieuNhap();
            a.ctpn = new List<ChiTietPhieuNhap>();
            a.pn = phieuNhap;
            a.ctpn = ctpn;

            var report = new PartialViewAsPdf("~/Views/NhapHang/XuatFilePhieuNhap.cshtml", a);
            return report;
        }
    }
}