using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Text;
using Newtonsoft.Json;
using LuanVan_ShopDongHo.Models;

namespace LuanVan_ShopDongHo.Controllers.Api
{
    public class ThongKeAPIController : ApiController
    {
        // GET: ThongKeAPI
        [HttpGet]
        public IHttpActionResult getThongKe(int? year)
        {
            using (var db = new ShopDongHoDataContext())
            {
                var thongKe = (from h in db.HANGs
                               join sp in db.SANPHAMs on h.MAHANG equals sp.MAHANG
                               select new HangThongKe
                               {
                                   maHang = h.MAHANG,
                                   maSP = sp.MASP,
                                   tenSP = sp.TENSP,
                                   soLuongBanRa = 0,
                                   soLuongNhap = 0,
                                   doanhSo = 0,
                                   tonKho = 0,
                               }).ToList();

                foreach (var item in thongKe)
                {
                    var k = db.KHOs.Where(t => t.MASP == item.maSP && t.TRANGTHAI == 0 && t.NGAYBAN == null).ToList();
                    var count = k.Count();
                    item.tonKho = count;
                }

                foreach (var item in thongKe)
                {
                    var soLuongNhap = (from pn in db.PHIEUNHAPs
                                       join ct in db.CHITIETPNs on pn.IDPhieuNhap equals ct.IDPhieuNhap
                                       where pn.TINHTRANG == true && ct.MASP == item.maSP && pn.NGAYNHAPHANG.Value.Year == year
                                       select ct
              ).ToList().Sum(t => t.SOLUONG);
                    var tienNhap = (from pn in db.PHIEUNHAPs
                                    join ct in db.CHITIETPNs on pn.IDPhieuNhap equals ct.IDPhieuNhap
                                    where pn.TINHTRANG == true && ct.MASP == item.maSP && pn.NGAYNHAPHANG.Value.Year == year
                                    select ct).ToList().Sum(t => t.THANHTIEN);
                    long tienBan = 0;


                    //var soLuongNhap = db.CHITIETPNs.Where(t => t.MASP == item.maSP).Sum(t => t.SOLUONG);
                    item.soLuongNhap = soLuongNhap ?? 0;
                    //var cthd = db.CHITIETHDs.Where(t => t.MASP == item.maSP).ToList();


                    var k = db.KHOs.Where(t => t.MASP == item.maSP && t.TRANGTHAI == 1 && t.NGAYBAN.Value.Year == year).ToList();
                    var count = k.Count();

                    item.soLuongBanRa = count;

                    var cthd = (from ct in db.CHITIETHDs
                                join h in db.HOADONs on ct.IDHoaDon equals h.IDHoaDon
                                where ct.MASP == item.maSP && h.NGAYLAP.Value.Year == year
                                select ct).GroupBy(t => t.QRCODE).ToList();

                    foreach (var i in cthd)
                    {
                        foreach (var t in i)
                        {
                            tienBan += t.THANHTIEN ?? 0;
                        }
                    }
                    item.doanhSo = tienBan - tienNhap;
                }

                return Json(thongKe);
            }
        }
    }
}