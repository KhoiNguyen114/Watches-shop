using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Text;
using Newtonsoft.Json;
using LuanVan_ShopDongHo.Models;


namespace LuanVan_ShopDongHo.Controllers.API
{
    public class HoaDonAPIController : ApiController
    {
        [HttpGet]
        public IHttpActionResult getHoaDons(int? type)
        {
            using (var db = new ShopDongHoDataContext())
            {

                List<hd> hds = new List<hd>();
                int t = type ?? 0;
                if(t == 0 )
                {
                    var hdss = (from hd in db.HOADONs
                                join ht in db.HINHTHUCs on hd.MAHINHTHUCTHANHTOAN equals ht.MAHINHTHUC
                                orderby hd.NGAYLAP descending
                                select new hd
                                {
                                    idHoaDon = hd.IDHoaDon,
                                    maHoaDon = hd.MAHD,
                                    ngayLap = string.Format("{0:dd/MM/yyyy}", hd.NGAYLAP.Value),
                                    tt = hd.TINHTRANG ?? 0,
                                    hinhThuc = ht.TENHINHTHUC,
                                    tongTien = hd.TONGTIEN,
                                }).ToList();
                    hds = hdss;
                }        
                else
                {
                    var hdss = (from hd in db.HOADONs
                                join ht in db.HINHTHUCs on hd.MAHINHTHUCTHANHTOAN equals ht.MAHINHTHUC
                                join kh in db.KHACHHANGs on hd.IDKhachHang equals kh.IDKhachHang
                                where hd.TINHTRANG == t
                                select new hd
                                {
                                    idHoaDon = hd.IDHoaDon,
                                    maHoaDon = hd.MAHD,
                                    ngayLap = string.Format("{0:dd/MM/yyyy}", hd.NGAYLAP.Value),
                                    tt = hd.TINHTRANG ?? 0,
                                    hinhThuc = ht.TENHINHTHUC,
                                    tongTien = hd.TONGTIEN,
                                }).ToList();
                    hds = hdss;
                }

                foreach(var item in hds)
                {
                    switch(item.tt)
                    {
                        case 1:
                            item.tinhTrang = "Đang chờ xử lý";
                            break;
                        case 2:
                            item.tinhTrang = "Đang vận chuyển";
                            break;
                        case 3:
                            item.tinhTrang = "Đơn hàng đã thanh toán";
                            break;
                        case 4:
                            item.tinhTrang = "Đơn hàng đã hủy";
                            break;
                    }
                }

                return Json(hds);
            }
        }

        [HttpGet]
        public IHttpActionResult getChitietHD(int idHD)
        {
            using (var db = new ShopDongHoDataContext())
            {
                var cthoadon = (from h in db.HOADONs
                              join ct in db.CHITIETHDs on h.IDHoaDon equals ct.IDHoaDon
                              join kh in db.KHACHHANGs on h.IDKhachHang equals kh.IDKhachHang
                              join ht in db.HINHTHUCs on h.MAHINHTHUCTHANHTOAN equals ht.MAHINHTHUC
                              where h.IDHoaDon == idHD
                              select new cthd
                              {
                                  idHoaDon = h.IDHoaDon,
                                  maHoaDon = h.MAHD,
                                  ngayLap = string.Format("{0:dd/MM/yyyy}", h.NGAYLAP.Value),
                                  tt = h.TINHTRANG ?? 0,
                                  hinhThuc = ht.TENHINHTHUC,
                                  tongTien = h.TONGTIEN,
                                  dsSP = new List<Sp>(),
                                  tenKH = kh.TENKH,
                                  diaChi = kh.DIACHI,
                                  gioiTinh = kh.GIOITINH,
                                  soCMND = kh.SOCMND,
                                  soDT = kh.SDT,
                              }).FirstOrDefault();
                if(cthoadon != null)
                {
                    switch (cthoadon.tt)
                    {
                        case 1:
                            cthoadon.tinhTrang = "Đang chờ xử lý";
                            break;
                        case 2:
                            cthoadon.tinhTrang = "Đang vận chuyển";
                            break;
                        case 3:
                            cthoadon.tinhTrang = "Đơn hàng đã thanh toán";
                            break;
                        case 4:
                            cthoadon.tinhTrang = "Đơn hàng đã hủy";
                            break;
                    }

                    var chitiets = (from ct in db.CHITIETHDs
                                    join sp in db.SANPHAMs on ct.MASP equals sp.MASP
                                    where ct.IDHoaDon == cthoadon.idHoaDon
                                    select new Sp
                                    {
                                        tenSP = sp.TENSP,
                                        hinhAnh = sp.HINHANH,
                                        quant = ct.SOLUONG ?? 0,
                                    }).ToList();
                    cthoadon.dsSP.AddRange(chitiets);
                }
                return Json(cthoadon);
            }
        }
        public class hd
        {
            public int idHoaDon { get; set; }
            public string maHoaDon { get; set; }
            public string ngayLap { get; set; }
            public int? tt { get; set; }
            public string tinhTrang { get; set; }
            public string hinhThuc { get; set; }
            public long? tongTien { get; set; }

        }

        public class cthd
        {
            public int idHoaDon { get; set; }
            public string maHoaDon { get; set; }
            public string ngayLap { get; set; }
            public int? tt { get; set; }
            public string tinhTrang { get; set; }

            public string hinhThuc { get; set; }
            public long? tongTien { get; set; }

            public List<Sp> dsSP { get; set; }

            public string tenKH { get; set; }

            public string diaChi { get; set; }

            public string soDT { get; set; }

            public string gioiTinh { get; set; }

            public string soCMND { get; set; }

        }
        public class Sp
        {
            public string hinhAnh { get; set; }

            public string tenSP { get; set; }

            public int quant { get; set; }
        }
    }
}