using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuanVan_ShopDongHo.Models;

namespace LuanVan_ShopDongHo.Controllers
{
    public class SanPhamController : Controller
    {
        ShopDongHoDataContext db = new ShopDongHoDataContext();
        // GET: SanPham
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ChiTietSanPham(string MASP)
        {
            var sp = db.SANPHAMs.Where(t => t.MASP == MASP).FirstOrDefault();
            var hang =  db.HANGs.Where(t => t.MAHANG == sp.MAHANG).FirstOrDefault();
            var bh = db.NOIDUNGBHs.Where(t => t.MANOIDUNG == sp.MABAOHANH).FirstOrDefault();
            List<KHUYENMAI> dsKM = db.KHUYENMAIs.Where(t => t.TINHTRANG.Equals("Đang diễn ra")).ToList();
            List<HienThiKhuyenMai> dsSPKM = new List<HienThiKhuyenMai>();
            for (int i = 0; i < dsKM.Count; i++)
            {
                List<CHITIETKHUYENMAI> dsCTKM = db.CHITIETKHUYENMAIs.Where(t => t.MAKM == dsKM[i].MAKM).ToList();
                if (dsCTKM.Count > 0)
                {
                    for (int j = 0; j < dsCTKM.Count; j++)
                    {
                        SANPHAM sanpham = db.SANPHAMs.Where(t => t.MASP == dsCTKM[j].MASP).FirstOrDefault();
                        if (sanpham != null)
                        {
                            HienThiKhuyenMai htkm = new HienThiKhuyenMai();
                            htkm.maSP = sanpham.MASP;
                            htkm.donGia = sanpham.DONGIA;
                            htkm.ptKhuyenMai = dsKM[i].KHUYENMAI1;
                            dsSPKM.Add(htkm);
                        }
                    }
                }
            }
            ViewBag.bh = bh;
            ViewBag.hang = hang.TENHANG;
            ViewBag.logo = hang.LOGO;
            ViewBag.dsKM = dsSPKM;
            return View(sp);
        }

    }
}