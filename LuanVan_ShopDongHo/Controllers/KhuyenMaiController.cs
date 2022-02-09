using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuanVan_ShopDongHo.Models;
using PagedList;

namespace LuanVan_ShopDongHo.Controllers
{
    public class KhuyenMaiController : Controller
    {
        // GET: KhuyenMai
        ShopDongHoDataContext db = new ShopDongHoDataContext();
        QuanLyController ql = new QuanLyController();
        public static List<KhuyenMai> dsKM;

        public ActionResult Index()
        {
            return View();
        }

        public List<SelectListItem> taoListTinhTrang()
        {
            var dsTT = new List<SelectListItem>();
            dsTT.Add(new SelectListItem() { Text = "Sắp diễn ra", Value = "Sắp diễn ra" });
            dsTT.Add(new SelectListItem() { Text = "Đang diễn ra", Value = "Đang diễn ra" });
            dsTT.Add(new SelectListItem() { Text = "Kết thúc", Value = "Kết thúc" });

            return dsTT;
        }

        public ActionResult QuanLyKhuyenMai(int? page, DateTime? tgBD = null, DateTime? tgKT = null, string tinhTrang = "", bool? trangThai = null)
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

            int pagesize = 10; //Số sản phẩm hiển thị trên 1 trang
            int pageNumber = (page ?? 1);//Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
                                         // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            List<KhuyenMai> ds = (from k in db.KHUYENMAIs
                                  where k.TRANGTHAI == temp
                                  select new KhuyenMai
                                  {
                                      maKM = k.MAKM,
                                      thoiGianBatDau = k.THOIGIANBD,
                                      thoiGianKetThuc = k.THOIGIANKT,
                                      tinhTrang = k.TINHTRANG,
                                      hinhAnh = k.HINHANH,
                                      km = k.KHUYENMAI1,
                                      trangThai = k.TRANGTHAI,
                                  }).ToList();

            if (tgBD != null)
            {
                tgBD = new DateTime(tgBD.Value.Year, tgBD.Value.Month, tgBD.Value.Day, 0, 0, 0);
                ds = ds.Where(t => t.thoiGianBatDau >= tgBD).ToList();
                ViewBag.tgBD = tgBD;
            }
            if (tgKT != null)
            {
                tgKT = new DateTime(tgKT.Value.Year, tgKT.Value.Month, tgKT.Value.Day, 23, 59, 59);
                ds = ds.Where(t => t.thoiGianKetThuc <= tgKT).ToList();
                ViewBag.tgKT = tgKT;
            }
            if (tinhTrang != "")
            {
                ds = ds.Where(t => t.tinhTrang.Trim().Contains(tinhTrang.Trim())).ToList();
                ViewBag.tinhTrang = tinhTrang;
            }

            dsKM = ds;
            var lst = ds.ToPagedList(pageNumber, pagesize);
            var dsTT = ql.taoListTrangThai();
            var dsTinhTrang = taoListTinhTrang();

            List<SANPHAM> dsSP = db.SANPHAMs.Where(t => t.TRANGTHAI == false).ToList();

            ViewBag.dsKM = lst;
            ViewBag.dsTT = dsTT;
            ViewBag.dsTinhTrang = dsTinhTrang;
            ViewBag.dsSP = dsSP;
            ViewBag.pageNum = pageNumber;
            ViewBag.pageSize = pagesize;
            KhuyenMaiModel km = new KhuyenMaiModel();
            return View(km);
        }

        [HttpPost]
        public JsonResult ThemKhuyenMai(KhuyenMaiModel pKM)
        {
            KHUYENMAI km = db.KHUYENMAIs.Where(t => t.MAKM == pKM.km.maKM).FirstOrDefault();
            if (km != null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã khuyến mãi này đã tồn tại! Xin vui lòng thử lại!"
                }, JsonRequestBehavior.AllowGet);
            }

            DateTime tgkt = (DateTime)pKM.km.thoiGianKetThuc;
            int result = tgkt.CompareTo((DateTime)pKM.km.thoiGianBatDau);
            if (result == -1)
            {
                return Json(new
                {
                    kq = false,
                    message = "Thời gian kết thúc phải lớn hơn hoặc bằng thời gian bắt đầu!"
                }, JsonRequestBehavior.AllowGet);
            }

            //Xử lý sản phẩm đang trong chương trình khuyến mãi khác

            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            DateTime checktgbd = new DateTime(pKM.km.thoiGianBatDau.Value.Year, pKM.km.thoiGianBatDau.Value.Month, pKM.km.thoiGianBatDau.Value.Day, 0, 0, 0);
            DateTime checktgkt = new DateTime(pKM.km.thoiGianKetThuc.Value.Year, pKM.km.thoiGianKetThuc.Value.Month, pKM.km.thoiGianKetThuc.Value.Day, 23, 59, 59);
            DateTime ssNgayBD = new DateTime(year, month, day, 0, 0, 0);
            DateTime ssNgayKT = new DateTime(year, month, day, 23, 59, 59);

            KHUYENMAI khuyenmai = new KHUYENMAI();
            khuyenmai.MAKM = pKM.km.maKM;
            khuyenmai.THOIGIANBD = checktgbd;
            khuyenmai.THOIGIANKT = checktgkt;
            khuyenmai.KHUYENMAI1 = pKM.km.km;
            if (pKM.km.hinhAnh != null)
            {
                string[] mang = pKM.km.hinhAnh.Split('\\');
                string hinh = mang[mang.Length - 1];
                khuyenmai.HINHANH = hinh;
            }
            khuyenmai.TRANGTHAI = false;

            if (checktgkt < ssNgayBD)
            {
                khuyenmai.TINHTRANG = "Kết thúc";
            }

            else if (checktgbd > ssNgayKT)
            {
                khuyenmai.TINHTRANG = "Sắp diễn ra";
            }

            else if ((checktgbd >= ssNgayBD && checktgkt >= ssNgayKT) || (checktgbd <= ssNgayBD && checktgkt >= ssNgayKT))
            {
                khuyenmai.TINHTRANG = "Đang diễn ra";
            }

            db.KHUYENMAIs.InsertOnSubmit(khuyenmai);


            for (int i = 0; i < pKM.ctkm.maSP.Count; i++)
            {
                string maSP = pKM.ctkm.maSP[i].Trim();
                bool checkValid = checkValidSP(maSP, pKM.km.thoiGianBatDau, pKM.km.thoiGianKetThuc);
                SANPHAM sp = db.SANPHAMs.Where(t => t.MASP == maSP).FirstOrDefault();
                if (checkValid == false)
                {
                    return Json(new
                    {
                        kq = false,
                        message = "Thêm khuyến mãi mới không thành công! Vui lòng kiểm tra lại sản phẩm " + sp.TENSP,
                    }, JsonRequestBehavior.AllowGet);
                }
                CHITIETKHUYENMAI ctkm = new CHITIETKHUYENMAI();
                ctkm.MAKM = pKM.km.maKM;
                ctkm.MASP = pKM.ctkm.maSP[i];
                db.CHITIETKHUYENMAIs.InsertOnSubmit(ctkm);
            }
            db.SubmitChanges();
            return Json(new
            {
                kq = true,
                message = "Thêm khuyến mãi mới thành công!"
            }, JsonRequestBehavior.AllowGet);
        }

        public bool checkValidSP(string maSP, DateTime? startAdd, DateTime? endAdd)
        {
            try
            {
                using (var db = new ShopDongHoDataContext())
                {
                    startAdd = new DateTime(startAdd.Value.Year, startAdd.Value.Month, startAdd.Value.Day, 0, 0, 0);
                    endAdd = new DateTime(endAdd.Value.Year, endAdd.Value.Month, endAdd.Value.Day, 23, 59, 59);

                    var ctkm = (from ct in db.CHITIETKHUYENMAIs
                                join km in db.KHUYENMAIs on ct.MAKM equals km.MAKM
                                join sp in db.SANPHAMs on ct.MASP equals sp.MASP
                                where sp.MASP == maSP && km.TINHTRANG.Contains("Đang diễn ra") || km.TINHTRANG.Contains("Sắp diễn ra")
                                select km).ToList();
                    bool kq = true;
                    if (ctkm != null)
                    {

                        foreach (var item in ctkm)
                        {
                            if (endAdd <= item.THOIGIANKT && endAdd >= item.THOIGIANBD)
                            {
                                kq = false;
                                break;
                            }
                            if (startAdd <= item.THOIGIANKT && startAdd >= item.THOIGIANBD)
                            {
                                kq = false;
                                break;
                            }
                            if (endAdd <= item.THOIGIANBD && startAdd >= item.THOIGIANKT)
                            {
                                kq = false;
                                break;
                            }
                        }
                    }
                    return kq;
                }
            }
            catch
            {
                return false;
            }
        }



        public JsonResult GetKhuyenMai(string maKM)
        {
            var km = (from k in db.KHUYENMAIs
                      where k.MAKM.Equals(maKM.Trim())
                      select new
                      {
                          k.MAKM,
                          k.THOIGIANBD,
                          k.THOIGIANKT,
                          k.KHUYENMAI1,
                          k.HINHANH,
                      });
            if (km == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Khuyến mãi này không tồn tại! Xin vui lòng thử lại!"
                }, JsonRequestBehavior.AllowGet);

            }

            string chuoi = "";
            List<CHITIETKHUYENMAI> dsKM = db.CHITIETKHUYENMAIs.Where(t => t.MAKM == maKM).ToList();
            if (dsKM.Count > 0)
            {
                for (int i = 0; i < dsKM.Count; i++)
                {
                    chuoi += dsKM[i].MASP + ",";
                }
                chuoi = chuoi.Substring(0, chuoi.Length - 1);
            }

            return Json(new
            {
                kq = true,
                data = km,
                sp = chuoi,
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SuaKhuyenMai(KhuyenMaiModel pKM)
        {
            KHUYENMAI km = db.KHUYENMAIs.Where(t => t.MAKM == pKM.km.maKM).FirstOrDefault();
            if (km == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã khuyến mãi này không tồn tại! Xin vui lòng thử lại!"
                }, JsonRequestBehavior.AllowGet);
            }

            DateTime tgkt = (DateTime)pKM.km.thoiGianKetThuc;
            int result = tgkt.CompareTo((DateTime)pKM.km.thoiGianBatDau);
            if (result == -1)
            {
                return Json(new
                {
                    kq = false,
                    message = "Thời gian kết thúc phải lớn hơn hoặc bằng thời gian bắt đầu!"
                }, JsonRequestBehavior.AllowGet);
            }

            //Xử lý sản phẩm đang trong chương trình khuyến mãi khác

            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            DateTime checktgbd = new DateTime(pKM.km.thoiGianBatDau.Value.Year, pKM.km.thoiGianBatDau.Value.Month, pKM.km.thoiGianBatDau.Value.Day, 0, 0, 0);
            DateTime checktgkt = new DateTime(pKM.km.thoiGianKetThuc.Value.Year, pKM.km.thoiGianKetThuc.Value.Month, pKM.km.thoiGianKetThuc.Value.Day, 23, 59, 59);
            DateTime ssNgayBD = new DateTime(year, month, day, 0, 0, 0);
            DateTime ssNgayKT = new DateTime(year, month, day, 23, 59, 59);

            km.THOIGIANBD = checktgbd;
            km.THOIGIANKT = checktgkt;
            km.KHUYENMAI1 = pKM.km.km;
            if (pKM.km.hinhAnh != null)
            {
                string[] mang = pKM.km.hinhAnh.Split('\\');
                string hinh = mang[mang.Length - 1];
                km.HINHANH = hinh;
            }
            km.TRANGTHAI = false;

            if (checktgkt < ssNgayBD)
            {
                km.TINHTRANG = "Kết thúc";
            }

            else if (checktgbd > ssNgayKT)
            {
                km.TINHTRANG = "Sắp diễn ra";
            }

            else if ((checktgbd >= ssNgayBD && checktgkt >= ssNgayKT) || (checktgbd <= ssNgayBD && checktgkt >= ssNgayKT))
            {
                km.TINHTRANG = "Đang diễn ra";
            }

            db.SubmitChanges();

            List<CHITIETKHUYENMAI> dsKhuyenMai = db.CHITIETKHUYENMAIs.Where(t => t.MAKM == pKM.km.maKM).ToList();
            List<CHITIETKHUYENMAI> temp = new List<CHITIETKHUYENMAI>(dsKhuyenMai);

            for (int i = 0; i < dsKhuyenMai.Count; i++)
            {
                db.CHITIETKHUYENMAIs.DeleteOnSubmit(dsKhuyenMai[i]);
            }

            for (int i = 0; i < pKM.ctkm.maSP.Count; i++)
            {
                string maSP = pKM.ctkm.maSP[i].Trim();
                if (temp.Exists(t => t.MASP == maSP))
                {
                    continue;
                }
                bool checkValid = checkValidSP(maSP, pKM.km.thoiGianBatDau, pKM.km.thoiGianKetThuc);
                SANPHAM sp = db.SANPHAMs.Where(t => t.MASP == maSP).FirstOrDefault();
                if (checkValid == false)
                {
                    return Json(new
                    {
                        kq = false,
                        message = "Cập nhật khuyến mãi không thành công! Vui lòng kiểm tra lại sản phẩm " + sp.TENSP,
                    }, JsonRequestBehavior.AllowGet);
                }
                if (!temp.Exists(t => t.MASP == maSP))
                {
                    CHITIETKHUYENMAI newCT = new CHITIETKHUYENMAI
                    {
                        MAKM = temp[0].MAKM,
                        KHUYENMAI = temp[0].KHUYENMAI,
                        MASP = maSP
                    };
                    temp.Add(newCT);
                }
            }
            foreach (var item in temp)
            {
                db.CHITIETKHUYENMAIs.InsertOnSubmit(item);
            }
            db.SubmitChanges();
            return Json(new
            {
                kq = true,
                message = "Cập nhật khuyến mãi thành công!"
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult XoaKhuyenMai(string maKM)
        {
            KHUYENMAI khuyenMai = db.KHUYENMAIs.Where(t => t.MAKM == maKM).FirstOrDefault();
            if (khuyenMai == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã khuyến mãi này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            if (khuyenMai.TINHTRANG.Equals("Đang diễn ra"))
            {
                return Json(new
                {
                    kq = false,
                    message = "Sự kiện khuyến mãi này đang diễn ra nên không thể xóa!",
                }, JsonRequestBehavior.AllowGet);
            }

            khuyenMai.TRANGTHAI = true;
            khuyenMai.TINHTRANG = "Kết thúc";
            db.SubmitChanges();
            return Json(new
            {
                kq = true,
                message = "Xóa thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public void CapNhatTinhTrang()
        {
            List<KHUYENMAI> dsKM = db.KHUYENMAIs.Where(t => t.TRANGTHAI == false).ToList();
            for (int i = 0; i < dsKM.Count; i++)
            {
                int year = DateTime.Now.Year;
                int month = DateTime.Now.Month;
                int day = DateTime.Now.Day;
                DateTime checktgbd = new DateTime(dsKM[i].THOIGIANBD.Value.Year, dsKM[i].THOIGIANBD.Value.Month, dsKM[i].THOIGIANBD.Value.Day, 0, 0, 0);
                DateTime checktgkt = new DateTime(dsKM[i].THOIGIANKT.Value.Year, dsKM[i].THOIGIANKT.Value.Month, dsKM[i].THOIGIANKT.Value.Day, 23, 59, 59);
                DateTime ssNgayBD = new DateTime(year, month, day, 0, 0, 0);
                DateTime ssNgayKT = new DateTime(year, month, day, 23, 59, 59);

                if (checktgkt < ssNgayBD)
                {
                    dsKM[i].TINHTRANG = "Kết thúc";
                }

                else if (checktgbd > ssNgayKT)
                {
                    dsKM[i].TINHTRANG = "Sắp diễn ra";
                }

                else if (checktgbd >= ssNgayBD && checktgkt >= ssNgayKT)
                {
                    dsKM[i].TINHTRANG = "Đang diễn ra";
                }

                db.SubmitChanges();
            }
        }

        public ActionResult XemChiTietKhuyenMai(string maKM, int? page)
        {
            TAIKHOAN tk = (TAIKHOAN)Session["Admin"];
            if (tk == null)
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            List<CHITIETKHUYENMAI> dsCTKM = db.CHITIETKHUYENMAIs.Where(t => t.MAKM == maKM).ToList();
            KHUYENMAI km = db.KHUYENMAIs.Where(t => t.MAKM == maKM).FirstOrDefault();

            KhuyenMaiModel kmModel = new KhuyenMaiModel();
            kmModel.km = new KhuyenMai();
            kmModel.ctkm = new ChiTietKhuyenMai();
            kmModel.ctkm.maSP = new List<string>();
            kmModel.km.maKM = km.MAKM;
            kmModel.km.thoiGianBatDau = km.THOIGIANBD;
            kmModel.km.thoiGianKetThuc = km.THOIGIANKT;
            kmModel.km.km = km.KHUYENMAI1;
            kmModel.km.tinhTrang = km.TINHTRANG;

            int pagesize = 10;
            int pageNumber = (page ?? 1);

            List<SANPHAM> dsSP = new List<SANPHAM>();
            if (dsCTKM.Count > 0)
            {
                for (int i = 0; i < dsCTKM.Count; i++)
                {
                    SANPHAM sp = db.SANPHAMs.Where(t => t.MASP == dsCTKM[i].MASP).FirstOrDefault();
                    kmModel.ctkm.maSP.Add(sp.TENSP);
                    dsSP.Add(sp);
                }
            }

            var lst = dsSP.ToPagedList(pageNumber, pagesize);

            ViewBag.dsSP = lst;
            ViewBag.pageNum = pageNumber;
            ViewBag.pageSize = pagesize;
            return View(kmModel);
        }

        public JsonResult KhoiPhucKhuyenMai(string maKM)
        {
            KHUYENMAI khuyenMai = db.KHUYENMAIs.Where(t => t.MAKM == maKM).FirstOrDefault();
            if (khuyenMai == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã khuyến mãi này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            khuyenMai.TRANGTHAI = false;
            db.SubmitChanges();
            return Json(new
            {
                kq = true,
                message = "Khôi phục thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpLoadAnh()
        {
            if (Request.Files.Count != 0)
            {
                var file = Request.Files[0];
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Images/KhuyenMai/"), fileName);
                file.SaveAs(path);
                return Json(new
                {
                    kq = true,
                });
            }
            else
            {
                return Json(new
                {
                    kq = false,
                });
            }
        }

        public ActionResult ShowKhuyenMaiModal()
        {
            List<KHUYENMAI> ds = db.KHUYENMAIs.Where(t => t.TINHTRANG.Equals("Đang diễn ra")).OrderByDescending(t => t.KHUYENMAI1).ToList();
            KHUYENMAI km = new KHUYENMAI();
            for (int i = 0; i < ds.Count; i++)
            {
                if(ds[i].HINHANH != null)
                {
                    km.HINHANH = ds[i].HINHANH;
                    km.THOIGIANBD = ds[i].THOIGIANBD;
                    km.THOIGIANKT = ds[i].THOIGIANKT;
                    km.TINHTRANG = ds[i].TINHTRANG;
                    km.TRANGTHAI = ds[i].TRANGTHAI;
                    km.MAKM = ds[i].MAKM;
                    km.KHUYENMAI1 = ds[i].KHUYENMAI1;
                    break;
                }
            }
            if(km.HINHANH == null)
            {
                ViewBag.kq = false;
            }
            else
            {
                ViewBag.kq = true;
            }
            return PartialView(km);
        }

        public ActionResult LoadKhuyenMaiCarousel()
        {
            List<KHUYENMAI> ds = db.KHUYENMAIs.Where(t => t.TINHTRANG.Equals("Đang diễn ra")).OrderByDescending(t => t.KHUYENMAI1).ToList();
            List<KHUYENMAI> km = new List<KHUYENMAI>();
            for (int i = 0; i < ds.Count; i++)
            {
                if (ds[i].HINHANH != null)
                {
                    km.Add(ds[i]);
                }
            }
            if (km.Count > 0)
            {
                ViewBag.kq = true;
            }
            else
            {
                ViewBag.kq = false;
            }
            return PartialView(km);
        }
    }
}