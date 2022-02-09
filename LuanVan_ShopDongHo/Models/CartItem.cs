using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan_ShopDongHo.Models
{
    public class CartItem
    {
        public string maSP { get; set; }
        public string tenSP { get; set; }
        public string kichThuoc { get; set; }
        public string loaiDay { get; set; }
        public string hinh { get; set; }
        public long? donGia { get; set; }
        public int? soLuong { get; set; }

        public long? khuyenMai { get; set; }
        public int? soLuongHienCo { get; set; }
        public long? thanhTien { get { return (soLuong * donGia); } }

        public long? tongTien { get { return (soLuong * donGia) - ((khuyenMai ?? 0) * soLuong); } }

        ShopDongHoDataContext db = new ShopDongHoDataContext();

        public CartItem(string ma)
        {
            SANPHAM sp = db.SANPHAMs.Where(t => t.MASP == ma).FirstOrDefault();
            var km = (from k in db.KHUYENMAIs
                      join ctk in db.CHITIETKHUYENMAIs on k.MAKM equals ctk.MAKM
                      where sp.MASP == ctk.MASP && k.TINHTRANG.Contains("Đang Diễn Ra")
                      select k.KHUYENMAI1).FirstOrDefault();
            if (sp != null)
            {
                this.maSP = sp.MASP;
                this.tenSP = sp.TENSP;
                this.kichThuoc = sp.KICHTHUOC;
                this.loaiDay = sp.LOAIDAY;
                this.hinh = sp.HINHANH;
                this.donGia = sp.DONGIA;
                this.soLuongHienCo = sp.SOLUONG;
                this.soLuong = 1;
                this.khuyenMai = (long)((this.donGia) * km ?? 0) / 100;
            }
        }

        public CartItem()
        {

        }
    }
}