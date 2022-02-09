using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LuanVan_ShopDongHo.Models;
using PagedList;

namespace LuanVan_ShopDongHo.Controllers
{
    public class VoucherController : Controller
    {
        ShopDongHoDataContext db = new ShopDongHoDataContext();
        QuanLyController ql = new QuanLyController();

        const string chuoiGoc = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        // GET: Voucher
        public ActionResult Index()
        {
            return View();
        }

        public List<SelectListItem> taoListTinhTrang()
        {
            var dsTT = new List<SelectListItem>();
            dsTT.Add(new SelectListItem() { Text = "Sắp diễn ra", Value = "1" });
            dsTT.Add(new SelectListItem() { Text = "Đang diễn ra", Value = "2" });
            dsTT.Add(new SelectListItem() { Text = "Kết thúc", Value = "3" });

            return dsTT;
        }

        public ActionResult QuanLyChuongTrinhVoucher(int? page, bool? trangThai = null)
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
            CapNhatTinhTrang();

            bool temp = trangThai ?? false;
            ViewBag.trangThai = temp;

            int pagesize = 10;
            int pageNumber = (page ?? 1);

            List<CHUONGTRINHVOUCHER> ds = db.CHUONGTRINHVOUCHERs.Where(t => t.TRANGTHAI == temp).ToList();

            var lst = ds.ToPagedList(pageNumber, pagesize);
            var dsTT = ql.taoListTrangThai();
            var dsTinhTrang = taoListTinhTrang();

            ViewBag.dsCTVoucher = lst;
            ViewBag.dsTT = dsTT;
            ViewBag.dsTinhTrang = dsTinhTrang;
            ViewBag.pageNum = pageNumber;
            ViewBag.pageSize = pagesize;
            CHUONGTRINHVOUCHER ctvou = new CHUONGTRINHVOUCHER();
            return View(ctvou);
        }

        public string TaoChuoiNgauNhien(int length)
        {
            const string chuoiGoc = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rd = new Random();
            while (0 < length--)
            {
                res.Append(chuoiGoc[rd.Next(chuoiGoc.Length)]);
            }
            return res.ToString();
        }

        [HttpPost]
        public JsonResult ThemCTVoucher(CHUONGTRINHVOUCHER ctv)
        {
            CHUONGTRINHVOUCHER ctvoucher = db.CHUONGTRINHVOUCHERs.Where(t => t.MACHUONGTRINH == ctv.MACHUONGTRINH).FirstOrDefault();
            if (ctvoucher != null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã chương trình này đã tồn tại! Xin vui lòng thử lại!"
                }, JsonRequestBehavior.AllowGet);
            }

            DateTime tgkt = (DateTime)ctv.NGAYKETTHUC;
            int result = tgkt.CompareTo((DateTime)ctv.NGAYTAO);
            if (result == -1)
            {
                return Json(new
                {
                    kq = false,
                    message = "Ngày kết thúc phải lớn hơn hoặc bằng ngày tạo!"
                }, JsonRequestBehavior.AllowGet);
            }

            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            DateTime checktgbd = new DateTime(ctv.NGAYTAO.Value.Year, ctv.NGAYTAO.Value.Month, ctv.NGAYTAO.Value.Day, 0, 0, 0);
            DateTime checktgkt = new DateTime(ctv.NGAYKETTHUC.Value.Year, ctv.NGAYKETTHUC.Value.Month, ctv.NGAYKETTHUC.Value.Day, 23, 59, 59);
            DateTime ssNgayBD = new DateTime(year, month, day, 0, 0, 0);
            DateTime ssNgayKT = new DateTime(year, month, day, 23, 59, 59);

            CHUONGTRINHVOUCHER ctvou = new CHUONGTRINHVOUCHER();
            ctvou.MACHUONGTRINH = ctv.MACHUONGTRINH;
            ctvou.NGAYTAO = checktgbd;
            ctvou.NGAYKETTHUC = checktgkt;
            ctvou.GIAMGIA = ctv.GIAMGIA;
            ctvou.GHICHU = ctv.GHICHU;
            ctvou.SOLUONG = ctv.SOLUONG;
            ctvou.TRANGTHAI = false;

            if (checktgkt < ssNgayBD)
            {
                ctvou.TINHTRANG = 3;
            }

            else if (checktgbd > ssNgayKT)
            {
                ctvou.TINHTRANG = 1;
            }

            else if ((checktgbd >= ssNgayBD && checktgkt >= ssNgayKT) || (checktgbd <= ssNgayBD && checktgkt >= ssNgayKT))
            {
                ctvou.TINHTRANG = 2;
            }

            db.CHUONGTRINHVOUCHERs.InsertOnSubmit(ctvou);
            db.SubmitChanges();
            int length = 8;

            for (int i = 0; i < ctvou.SOLUONG; i++)
            {
                string chuoi = "";
                try
                {
                    StringBuilder res;
                    Random rd = new Random();
                    bool kiemTra = false;
                    string chuoiTam = "";

                    do
                    {
                        res = new StringBuilder();
                        int temp = length;
                        while (0 < temp--)
                        {
                            res.Append(chuoiGoc[rd.Next(chuoiGoc.Length)]);
                        }
                        VOUCHER vou = db.VOUCHERs.Where(t => t.MACHUONGTRINH == ctv.MACHUONGTRINH && t.MAVOUCHER == res.ToString()).FirstOrDefault();
                        if (vou != null)
                        {
                            kiemTra = true;
                        }
                        else
                        {
                            kiemTra = false;
                        }
                    } while (kiemTra == true);
                    chuoiTam = res.ToString();
                    chuoi = ctvou.MACHUONGTRINH + chuoiTam;
                }
                catch(Exception)
                {

                }                

                VOUCHER v = new VOUCHER();
                v.MACHUONGTRINH = ctvou.MACHUONGTRINH;
                v.TRANGTHAI = false;
                v.MAVOUCHER = chuoi;
                db.VOUCHERs.InsertOnSubmit(v);
                db.SubmitChanges();
            }

            return Json(new
            {
                kq = true,
                message = "Thêm chương trình voucher mới thành công!"
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult XemChiTietVoucher(string maCT, int? page)
        {
            TAIKHOAN tk = (TAIKHOAN)Session["Admin"];
            if (tk == null)
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }

            CHUONGTRINHVOUCHER ctv = db.CHUONGTRINHVOUCHERs.Where(t => t.MACHUONGTRINH == maCT).FirstOrDefault();
            List<KHACHHANG> dsKH = db.KHACHHANGs.Where(t => t.TRANGTHAI == false).ToList();
            var ds = (from k in db.VOUCHERs
                      where k.MACHUONGTRINH == maCT
                      select new Voucher
                      {
                          maChuongTrinh = k.MACHUONGTRINH,
                          maVoucher = k.MAVOUCHER,
                          tenKH = db.KHACHHANGs.FirstOrDefault(t => t.IDKhachHang == k.IDKhachHang).TENKH,
                          ngaySuDung = k.NGAYSUDUNG,
                          trangThai = k.TRANGTHAI,
                      }).ToList();

            int pagesize = 10;
            int pageNumber = (page ?? 1);

            var lst = ds.ToPagedList(pageNumber, pagesize);

            ViewBag.dsVoucher = lst;
            ViewBag.pageNum = pageNumber;
            ViewBag.pageSize = pagesize;
            ViewBag.dsKH = dsKH;
            return View(ctv);
        }

        public JsonResult GetCTVoucher(string maCT)
        {
            var voucher = (from k in db.CHUONGTRINHVOUCHERs
                           select new
                           {
                               k.MACHUONGTRINH,
                               k.GHICHU,
                               k.GIAMGIA,
                               k.NGAYTAO,
                               k.NGAYKETTHUC,
                               k.TINHTRANG,
                               k.TRANGTHAI,
                               k.SOLUONG,
                           });

            return Json(new
            {
                kq = true,
                data = voucher,
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SuaCTVoucher(CHUONGTRINHVOUCHER ctv)
        {
            CHUONGTRINHVOUCHER ctvoucher = db.CHUONGTRINHVOUCHERs.Where(t => t.MACHUONGTRINH == ctv.MACHUONGTRINH).FirstOrDefault();
            if (ctvoucher == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã chương trình này không tồn tại! Xin vui lòng thử lại!"
                }, JsonRequestBehavior.AllowGet);
            }

            DateTime tgkt = (DateTime)ctv.NGAYKETTHUC;
            int result = tgkt.CompareTo((DateTime)ctv.NGAYTAO);
            if (result == -1)
            {
                return Json(new
                {
                    kq = false,
                    message = "Ngày kết thúc phải lớn hơn hoặc bằng ngày tạo!"
                }, JsonRequestBehavior.AllowGet);
            }

            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            DateTime checktgbd = new DateTime(ctv.NGAYTAO.Value.Year, ctv.NGAYTAO.Value.Month, ctv.NGAYTAO.Value.Day, 0, 0, 0);
            DateTime checktgkt = new DateTime(ctv.NGAYKETTHUC.Value.Year, ctv.NGAYKETTHUC.Value.Month, ctv.NGAYKETTHUC.Value.Day, 23, 59, 59);
            DateTime ssNgayBD = new DateTime(year, month, day, 0, 0, 0);
            DateTime ssNgayKT = new DateTime(year, month, day, 23, 59, 59);

            ctvoucher.NGAYTAO = checktgbd;
            ctvoucher.NGAYKETTHUC = checktgkt;
            ctvoucher.GIAMGIA = ctv.GIAMGIA;
            ctvoucher.GHICHU = ctv.GHICHU;
            ctvoucher.TRANGTHAI = false;

            if (checktgkt < ssNgayBD)
            {
                ctvoucher.TINHTRANG = 3;
            }

            else if (checktgbd > ssNgayKT)
            {
                ctvoucher.TINHTRANG = 1;
            }

            else if ((checktgbd >= ssNgayBD && checktgkt >= ssNgayKT) || (checktgbd <= ssNgayBD && checktgkt >= ssNgayKT))
            {
                ctvoucher.TINHTRANG = 2;
            }

            db.SubmitChanges();
            return Json(new
            {
                kq = true,
                message = "Cập nhật chương trình voucher thành công!"
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult XoaCTVoucher(string maCT)
        {
            CHUONGTRINHVOUCHER ctvoucher = db.CHUONGTRINHVOUCHERs.Where(t => t.MACHUONGTRINH == maCT).FirstOrDefault();
            if (ctvoucher == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã chương trình voucher này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            if (ctvoucher.TINHTRANG == 2)
            {
                return Json(new
                {
                    kq = false,
                    message = "Sự kiện khuyến mãi này đang diễn ra nên không thể xóa!",
                }, JsonRequestBehavior.AllowGet);
            }

            ctvoucher.TRANGTHAI = true;
            ctvoucher.TINHTRANG = 3;
            db.SubmitChanges();
            return Json(new
            {
                kq = true,
                message = "Xóa thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult KhoiPhucCTVoucher(string maCT)
        {
            CHUONGTRINHVOUCHER ctvoucher = db.CHUONGTRINHVOUCHERs.Where(t => t.MACHUONGTRINH == maCT).FirstOrDefault();
            if (ctvoucher == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã chương trình voucher này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            ctvoucher.TRANGTHAI = false;
            db.SubmitChanges();
            return Json(new
            {
                kq = true,
                message = "Khôi phục thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public void CapNhatTinhTrang()
        {
            List<CHUONGTRINHVOUCHER> dsCTVoucher = db.CHUONGTRINHVOUCHERs.Where(t => t.TRANGTHAI == false).ToList();
            for (int i = 0; i < dsCTVoucher.Count; i++)
            {
                int year = DateTime.Now.Year;
                int month = DateTime.Now.Month;
                int day = DateTime.Now.Day;
                DateTime checktgbd = new DateTime(dsCTVoucher[i].NGAYTAO.Value.Year, dsCTVoucher[i].NGAYTAO.Value.Month, dsCTVoucher[i].NGAYTAO.Value.Day, 0, 0, 0);
                DateTime checktgkt = new DateTime(dsCTVoucher[i].NGAYKETTHUC.Value.Year, dsCTVoucher[i].NGAYKETTHUC.Value.Month, dsCTVoucher[i].NGAYKETTHUC.Value.Day, 23, 59, 59);
                DateTime ssNgayBD = new DateTime(year, month, day, 0, 0, 0);
                DateTime ssNgayKT = new DateTime(year, month, day, 23, 59, 59);

                if (checktgkt < ssNgayBD)
                {
                    dsCTVoucher[i].TINHTRANG = 3;
                }

                else if (checktgbd > ssNgayKT)
                {
                    dsCTVoucher[i].TINHTRANG = 1;
                }

                else if (checktgbd >= ssNgayBD && checktgkt >= ssNgayKT)
                {
                    dsCTVoucher[i].TINHTRANG = 2;
                }

                db.SubmitChanges();
            }
        }

        public async Task<JsonResult> SendMailVoucher(int idKH, string maCT, string maVoucher)
        {
            try
            {
                KHACHHANG kh = db.KHACHHANGs.Where(t => t.IDKhachHang == idKH).FirstOrDefault();
                TAIKHOAN user = db.TAIKHOANs.Where(t => t.TENDN == kh.TENDN).FirstOrDefault();
                if(user == null || user.TRANGTHAI == true)
                {
                    return Json(new
                    {
                        kq = false,
                        message = "Tài khoản này không tồn tại hoặc đã bị khóa!",
                    }, JsonRequestBehavior.AllowGet);
                }

                VOUCHER vou = db.VOUCHERs.Where(t => t.MACHUONGTRINH == maCT && t.MAVOUCHER == maVoucher).FirstOrDefault();
                CHUONGTRINHVOUCHER ctvou = db.CHUONGTRINHVOUCHERs.Where(t => t.MACHUONGTRINH == maCT).FirstOrDefault();
                if(ctvou.TINHTRANG != 2)
                {
                    return Json(new
                    {
                        kq = false,
                        message = "Chương trình này hiện không diễn ra nên không thể gửi!",
                    }, JsonRequestBehavior.AllowGet);
                }

                string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/template/mailVoucher.html"));
                content = content.Replace("{{voucher}}", vou.MAVOUCHER);
                content = content.Replace("{{giamgia}}", ctvou.GIAMGIA.ToString());

                var usermail = user.Email;
                Mail email = new Mail();
                await email.sendMail(usermail, "Voucher khuyến mãi", content);

                vou.IDKhachHang = kh.IDKhachHang;
                db.SubmitChanges();

                return Json(new
                {
                    kq = true,
                    message = "Gửi voucher thành công!",
                }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception)
            {
                return Json(new
                {
                    kq = false,
                    message = "Gửi voucher thất bại!",
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}