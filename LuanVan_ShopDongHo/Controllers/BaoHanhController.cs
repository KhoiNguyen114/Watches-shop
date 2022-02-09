using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using LuanVan_ShopDongHo.Models;
using PagedList;
using Rotativa;

namespace LuanVan_ShopDongHo.Controllers
{
    public class BaoHanhController : Controller
    {
        ShopDongHoDataContext db = new ShopDongHoDataContext();

        // GET: BaoHanh
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

        public List<SelectListItem> taoListTinhTrang()
        {
            var dsTT = new List<SelectListItem>();
            dsTT.Add(new SelectListItem() { Text = "Đang bảo hành", Value = "false" });
            dsTT.Add(new SelectListItem() { Text = "Hoàn thành", Value = "true" });

            return dsTT;
        }

        public List<SelectListItem> taoListFilterTheoLuaChon()
        {
            var dsTT = new List<SelectListItem>();
            dsTT.Add(new SelectListItem() { Text = "Theo hóa đơn", Value = "0" });
            dsTT.Add(new SelectListItem() { Text = "Theo thông tin khách hàng", Value = "1" });

            return dsTT;
        }

        public ActionResult QuanLyCheDoBaoHanh(int? page, bool? trangThai = null)
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

            List<NOIDUNGBH> dsNDBH = db.NOIDUNGBHs.Where(t => t.TRANGTHAI == false).ToList();
            var lst = dsNDBH.ToPagedList(pageNumber, pagesize);
            if (trangThai != null)
            {
                dsNDBH = dsNDBH.Where(t => t.TRANGTHAI == trangThai).ToList();
                ViewBag.trangThai = trangThai;
            }
            var dsTrangThai = taoListTrangThai();

            ViewBag.dsTrangThai = dsTrangThai;
            ViewBag.dsNDBHLoad = lst;
            ViewBag.pageNum = pageNumber;
            ViewBag.pageSize = pagesize;
            NOIDUNGBH nd = new NOIDUNGBH();
            return View(nd);
        }

        public ActionResult QuanLyThongTinBaoHanh(int? page, int? type, string maHD = "", bool? tinhTrang = null, DateTime? ngayBD = null, DateTime? ngayKT = null)
        {
            CapNhatTinhTrangThongTinBaoHanh();
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

            List<BaoHanh> dsTTBH = (from k in db.BAOHANHs
                                    join ct in db.CHITIETHDs on k.IDCHITIET equals ct.IDCHITIET
                                    join hd in db.HOADONs on ct.IDHoaDon equals hd.IDHoaDon
                                    join sp in db.SANPHAMs on k.MASP equals sp.MASP
                                    select new BaoHanh
                                    {
                                        idBaoHanh = k.IDBAOHANH,
                                        idChitiet = k.IDCHITIET,
                                        maHoaDon = hd.MAHD,
                                        ngayBD = k.THOIGIANBD,
                                        ngayKT = k.THOIGIANKT,
                                        tinhTrang = k.TINHTRANG,
                                        tenSP = sp.TENSP,
                                        ghiChu = k.GHICHU,
                                    }).ToList();

            if (maHD != "" && type == 0)
            {
                dsTTBH = dsTTBH.Where(t => t.maHoaDon.Contains(maHD.Trim())).ToList();
                ViewBag.maHD = maHD;
                ViewBag.phuongThucFilter = 0;
            }

            if (maHD != "" && type == 1)
            {
                dsTTBH = (from k in db.BAOHANHs
                          join ct in db.CHITIETHDs on k.IDCHITIET equals ct.IDCHITIET
                          join hd in db.HOADONs on ct.IDHoaDon equals hd.IDHoaDon
                          join sp in db.SANPHAMs on k.MASP equals sp.MASP
                          join kh in db.KHACHHANGs on hd.IDKhachHang equals kh.IDKhachHang
                          where kh.TENKH.Trim().Contains(maHD) || kh.SDT == maHD || kh.SOCMND == maHD
                          select new BaoHanh
                          {
                              idBaoHanh = k.IDBAOHANH,
                              idChitiet = k.IDCHITIET,
                              maHoaDon = hd.MAHD,
                              ngayBD = k.THOIGIANBD,
                              ngayKT = k.THOIGIANKT,
                              tinhTrang = k.TINHTRANG,
                              tenSP = sp.TENSP,
                              ghiChu = k.GHICHU,
                          }).ToList();
                ViewBag.maHD = maHD;
                ViewBag.phuongThucFilter = 1;
            }

            if (tinhTrang != null)
            {
                dsTTBH = dsTTBH.Where(t => t.tinhTrang == tinhTrang).ToList();
                ViewBag.tinhTrang = tinhTrang;
            }

            

            if (ngayBD != null)
            {
                dsTTBH = dsTTBH.Where(t => t.ngayBD.Value == ngayBD).ToList();
                ViewBag.ngayBD = ngayBD;
            }


            if (ngayKT != null)
            {
                dsTTBH = dsTTBH.Where(t => t.ngayKT.Value == ngayKT).ToList();
                ViewBag.ngayKT = ngayKT;
            }

            var lst = dsTTBH.ToPagedList(pageNumber, pagesize);
            var dsTinhTrang = taoListTinhTrang();
            var dsFilterLuaChon = taoListFilterTheoLuaChon();
            var dsHD = db.HOADONs.Where(t => t.TINHTRANG == 3).ToList();


            ViewBag.dsTinhTrang = dsTinhTrang;
            ViewBag.dsFilterLuaChon = dsFilterLuaChon;
            ViewBag.dsTTBH = lst;
            ViewBag.pageNum = pageNumber;
            ViewBag.pageSize = pagesize;
            ViewBag.dsHD = dsHD;
            BAOHANH bh = new BAOHANH();
            return View(bh);
        }

        public ActionResult ChiTietCheDoBaoHanh(string maCDBH)
        {
            TAIKHOAN tk = (TAIKHOAN)Session["Admin"];
            if (tk == null)
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }

            NOIDUNGBH ndBaoHanh = db.NOIDUNGBHs.Where(t => t.MANOIDUNG == maCDBH).FirstOrDefault();
            if (ndBaoHanh == null)
            {
                return RedirectToAction("QuanLyCheDoBaoHanh", "BaoHanh");
            }

            return View(ndBaoHanh);
        }

        public JsonResult loadAnhSummernoteCheDoBaoHanh(HttpPostedFileBase fupload)
        {
            if (Request.Files.Count != 0)
            {
                var file = Request.Files[0];
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Images/CheDoBaoHanh/"), fileName);
                //WebImage webImage = new WebImage(file.InputStream);
                //webImage.Resize(1110, 624);
                //webImage.Save(path);
                file.SaveAs(path);
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

        public JsonResult XoaAnhSummernoteCheDoBaoHanh(string filePath)
        {
            string[] mang = filePath.Split('/');
            var fileName = mang[mang.Length - 1];
            var path = Path.Combine(Server.MapPath("~/Content/Images/CheDoBaoHanh/"), fileName);
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
        public JsonResult ThemCheDoBaoHanhMoi(NOIDUNGBH ndbh)
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

            NOIDUNGBH ndBaoHanh = db.NOIDUNGBHs.Where(t => t.MANOIDUNG == ndbh.MANOIDUNG).FirstOrDefault();
            if (ndBaoHanh != null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã chế độ bảo hành này đã tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            NOIDUNGBH noiDungBH = new NOIDUNGBH();
            noiDungBH.MANOIDUNG = ndbh.MANOIDUNG;
            noiDungBH.NOIDUNG = ndbh.NOIDUNG;
            noiDungBH.SONAMBH = ndbh.SONAMBH;
            noiDungBH.TRANGTHAI = false;
            db.NOIDUNGBHs.InsertOnSubmit(noiDungBH);
            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Thêm chế độ bảo hành mới thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SuaCheDoBaoHanh(NOIDUNGBH ndbh)
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

            NOIDUNGBH ndBaoHanh = db.NOIDUNGBHs.Where(t => t.MANOIDUNG == ndbh.MANOIDUNG).FirstOrDefault();
            if (ndBaoHanh == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã chế độ bảo hành này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            ndBaoHanh.NOIDUNG = ndbh.NOIDUNG;
            ndBaoHanh.SONAMBH = ndbh.SONAMBH;
            ndBaoHanh.TRANGTHAI = false;
            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Cập nhật chế độ bảo hành thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCheDoBaoHanh(string maCDBH)
        {
            var cdbh = (from k in db.NOIDUNGBHs
                        where k.MANOIDUNG.Equals(maCDBH.Trim())
                        select new
                        {

                            k.MANOIDUNG,
                            k.TRANGTHAI,
                            k.NOIDUNG,
                            k.SONAMBH,
                        });
            return Json(cdbh, JsonRequestBehavior.AllowGet);
        }

        public JsonResult XoaCheDoBaoHanh(string maCDBH)
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

            NOIDUNGBH ndBaoHanh = db.NOIDUNGBHs.Where(t => t.MANOIDUNG == maCDBH).FirstOrDefault();
            if (ndBaoHanh == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã chế độ bảo hành này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            ndBaoHanh.TRANGTHAI = true;
            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Xóa chế độ bảo hành thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult KhoiPhucCheDoBaoHanh(string maCDBH)
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

            NOIDUNGBH ndBaoHanh = db.NOIDUNGBHs.Where(t => t.MANOIDUNG == maCDBH).FirstOrDefault();
            if (ndBaoHanh == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã chế độ bảo hành này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            ndBaoHanh.TRANGTHAI = false;
            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Khôi phục chế độ bảo hành thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSanPhamTheoHD(int idHoaDon)
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

            List<CHITIETHD> dsCTHD = db.CHITIETHDs.Where(t => t.IDHoaDon == idHoaDon).ToList();
            var dsSP = new List<SelectListItem>();

            if (dsCTHD.Count > 0)
            {
                for (int i = 0; i < dsCTHD.Count; i++)
                {
                    SANPHAM sp = db.SANPHAMs.Where(t => t.MASP == dsCTHD[i].MASP).FirstOrDefault();
                    if (sp != null)
                    {
                        dsSP.Add(new SelectListItem() { Text = sp.TENSP, Value = sp.MASP });
                    }
                }
            }
            return Json(new
            {
                kq = true,
                message = "Load dữ liệu sản phẩm thành công",
                data = dsSP,
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ThemThongTinBaoHanhMoi(BAOHANH ttbh)
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

            List<BAOHANH> bh = db.BAOHANHs.Where(t => t.MASP == ttbh.MASP && t.IDCHITIET == ttbh.IDCHITIET).ToList();
            for (int i = 0; i < bh.Count; i++)
            {
                if (bh[i].TINHTRANG == false)
                {
                    return Json(new
                    {
                        kq = false,
                        message = "Sản phẩm này đang được bảo hành! Xin vui lòng kiểm tra lại hoặc cập nhật tình trạng!",
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            DateTime tgkt = (DateTime)ttbh.THOIGIANKT;
            int result = tgkt.CompareTo((DateTime)DateTime.Now);
            if (result == -1)
            {
                return Json(new
                {
                    kq = false,
                    message = "Thời gian kết thúc phải lớn hơn hoặc bằng thời gian bắt đầu!"
                }, JsonRequestBehavior.AllowGet);
            }

            SANPHAM sp = db.SANPHAMs.Where(t => t.MASP == ttbh.MASP).FirstOrDefault();
            if (sp == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Sản phẩm này không tồn tại! Xin vui lòng kiểm tra lại!"
                }, JsonRequestBehavior.AllowGet);
            }
            int mahd = 0;
            var cthd = db.CHITIETHDs.Where(t => t.IDCHITIET == ttbh.IDCHITIET).FirstOrDefault();
            if(cthd != null)
            {
                mahd = cthd.IDHoaDon ?? 0;
            }
            HOADON hd = db.HOADONs.Where(t => t.IDHoaDon == mahd).FirstOrDefault();
            if (hd == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Hóa đơn này không tồn tại! Xin vui lòng kiểm tra lại!"
                }, JsonRequestBehavior.AllowGet);
            }

            //Kiểm tra thời hạn bảo hành của sản phẩm còn bảo hành hay không
            NOIDUNGBH ndbh = db.NOIDUNGBHs.Where(t => t.MANOIDUNG == sp.MABAOHANH).FirstOrDefault();
            if (ndbh == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Sản phẩm này chưa được hỗ trợ bảo hành! Xin vui lòng kiểm tra lại!"
                }, JsonRequestBehavior.AllowGet);
            }

            DateTime ngayHetHan = hd.NGAYLAP.Value.AddYears((int)ndbh.SONAMBH);
            DateTime ngayHienTai = DateTime.Now;
            int resultTime = DateTime.Compare(ngayHetHan, ngayHienTai);
            if (resultTime == -1)
            {
                return Json(new
                {
                    kq = false,
                    message = "Sản phẩm này đã hết hạn bảo hành"
                }, JsonRequestBehavior.AllowGet);
            }

            BAOHANH baoHanh = new BAOHANH();
            baoHanh.IDCHITIET = ttbh.IDCHITIET;
            baoHanh.MASP = ttbh.MASP;
            baoHanh.THOIGIANBD = DateTime.Now;
            baoHanh.THOIGIANKT = ttbh.THOIGIANKT;
            baoHanh.GHICHU = ttbh.GHICHU;
            baoHanh.TINHTRANG = false;
            baoHanh.QRCODE = cthd.QRCODE;
            db.BAOHANHs.InsertOnSubmit(baoHanh);
            db.SubmitChanges();
            CapNhatTinhTrangThongTinBaoHanh();

            return Json(new
            {
                kq = true,
                message = "Thêm thông tin bảo hành mới thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetThongTinBaoHanh(int idTTBH)
        {
            var ttbh = (from k in db.BAOHANHs
                        join ct in db.CHITIETHDs on k.IDCHITIET equals ct.IDCHITIET
                        join hd in db.HOADONs on ct.IDHoaDon equals hd.IDHoaDon
                        join sp in db.SANPHAMs on k.MASP equals sp.MASP
                        where k.IDBAOHANH == idTTBH
                        select new BaoHanh
                        {
                            idBaoHanh = k.IDBAOHANH,
                            idChitiet = k.IDCHITIET,
                            maHoaDon = hd.MAHD,
                            ngayBD = k.THOIGIANBD,
                            ngayKT = k.THOIGIANKT,
                            tinhTrang = k.TINHTRANG,
                            tenSP = sp.TENSP,
                            ghiChu = k.GHICHU,
                            qrcode = k.QRCODE,
                        }).FirstOrDefault();
            return Json(ttbh, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SuaThongTinBaoHanh(BAOHANH ttbh)
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

            BAOHANH baoHanh = db.BAOHANHs.Where(t => t.IDBAOHANH == ttbh.IDBAOHANH).FirstOrDefault();
            if (baoHanh == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Thông tin bảo hành này không tồn tại! Xin vui lòng kiểm tra lại"
                }, JsonRequestBehavior.AllowGet);
            }

            DateTime tgkt = (DateTime)ttbh.THOIGIANKT;
            int result = tgkt.CompareTo((DateTime)baoHanh.THOIGIANBD);
            if (result == -1)
            {
                return Json(new
                {
                    kq = false,
                    message = "Thời gian kết thúc phải lớn hơn hoặc bằng thời gian bắt đầu!"
                }, JsonRequestBehavior.AllowGet);
            }

            

            SANPHAM sp = db.SANPHAMs.Where(t => t.MASP == baoHanh.MASP).FirstOrDefault();
            if (sp == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Sản phẩm này không tồn tại! Xin vui lòng kiểm tra lại!"
                }, JsonRequestBehavior.AllowGet);
            }
            CHITIETHD ct = db.CHITIETHDs.Where(t => t.IDCHITIET == baoHanh.IDCHITIET).FirstOrDefault();
            HOADON hd = db.HOADONs.Where(t => t.IDHoaDon == ct.IDHoaDon).FirstOrDefault();
            if (hd == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Hóa đơn này không tồn tại! Xin vui lòng kiểm tra lại!"
                }, JsonRequestBehavior.AllowGet);
            }

            //Kiểm tra thời hạn bảo hành của sản phẩm còn bảo hành hay không
            DateTime ngayHetHan = hd.NGAYLAP.Value.AddYears((int)sp.ThoiHanBH);
            DateTime ngayHienTai = DateTime.Now;
            int resultTime = DateTime.Compare(ngayHetHan, ngayHienTai);
            if (resultTime == -1)
            {
                return Json(new
                {
                    kq = false,
                    message = "Sản phẩm này đã hết hạn bảo hành"
                }, JsonRequestBehavior.AllowGet);
            }

            baoHanh.THOIGIANKT = ttbh.THOIGIANKT;
            baoHanh.GHICHU = ttbh.GHICHU;

            db.SubmitChanges();
            CapNhatTinhTrangThongTinBaoHanh();
            return Json(new
            {
                kq = true,
                message = "Cập nhật thông tin bảo hành thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public void CapNhatTinhTrangThongTinBaoHanh()
        {
            List<BAOHANH> ds = db.BAOHANHs.ToList();
            for (int i = 0; i < ds.Count; i++)
            {
                DateTime ngayHienTai = DateTime.Now;
                DateTime ngayKetThuc = (DateTime)ds[i].THOIGIANKT;
                int resultTime = DateTime.Compare(ngayKetThuc, ngayHienTai);
                if (resultTime == -1)
                {
                    ds[i].TINHTRANG = true;
                    db.SubmitChanges();
                }
                else
                {
                    ds[i].TINHTRANG = false;
                    db.SubmitChanges();
                }
            }
        }

        public ActionResult XuatFilePdfBaoHanh(int idCTHD)
        {
            var ttbh = (from ct  in db.CHITIETHDs
                        join hd in db.HOADONs on ct.IDHoaDon equals hd.IDHoaDon   
                        join kh in db.KHACHHANGs on hd.IDKhachHang equals kh.IDKhachHang
                        join sp in db.SANPHAMs on ct.MASP equals sp.MASP
                        join ndbh in db.NOIDUNGBHs on sp.MABAOHANH equals ndbh.MANOIDUNG
                        where ct.IDCHITIET == idCTHD
                        select new BaoHanhXuatFile
                        {
                            maHoaDon = hd.MAHD,
                            tenSP = sp.TENSP,
                            qrcode = ct.QRCODE,
                            ngayLapHD = hd.NGAYLAP,
                            thoiHanBaoHanh = ndbh.SONAMBH,
                            tenKH = kh.TENKH,
                            donGia = sp.DONGIA,
                            xuatSu = sp.XUATSU,
                        }).FirstOrDefault();

            var report = new PartialViewAsPdf("~/Views/BaoHanh/XuatFileBaoHanh.cshtml", ttbh);
            return report;
        }
    }
}