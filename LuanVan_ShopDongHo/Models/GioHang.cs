using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan_ShopDongHo.Models
{
    public class GioHang
    {
        ShopDongHoDataContext db = new ShopDongHoDataContext();
        public List<CartItem> dsSP;

        public GioHang()
        {
            dsSP = new List<CartItem>();
        }

        //public GioHang(int idKH)
        //{
        //    dsSP = (from k in db.GIOHANGs
        //            join sp in db.SANPHAMs on k.MASP equals sp.MASP
        //            where k.IDKhachHang == idKH
        //            select new CartItem
        //            {
        //                maSP = k.MASP,
        //                tenSP = sp.TENSP,
        //                loaiDay = sp.LOAIDAY,
        //                kichThuoc = sp.KICHTHUOC,
        //                donGia = sp.DONGIA,
        //                hinh = sp.HINHANH,
        //                soLuong = k.SOLUONG,
        //                soLuongHienCo = sp.SOLUONG,
        //                khuyenMai = k.GIAKM,
        //            }).ToList();
        //}

        public GioHang(List<CartItem> ds)
        {
            dsSP = ds;
        }

        //Thêm sản phẩm vào giỏ hàng
        public bool themMatHang(string ma)
        {
            CartItem sp = dsSP.Find(t => t.maSP == ma);
            if(sp == null)
            {
                CartItem a = new CartItem(ma);
                if (a == null)
                    return false;
                dsSP.Add(a);
            }
            else
            {
                sp.soLuong++;
            }
            return true;
        }

        //Tổng số lượng hàng đã thêm vào giỏ
        public int? tongSoLuongHangTrongGio()
        {
            if (dsSP.Count == 0)
                return 0;
            return dsSP.Sum(t => t.soLuong);
        }

        //Tổng tiền toàn bộ giỏ hàng
        public long? tongThanhTien()
        {
            if (dsSP == null)
                return 0;
            return dsSP.Sum(t => t.thanhTien);
        }
        public long? SumALL()
        {
            if (dsSP == null)
                return 0;
            return dsSP.Sum(t => t.tongTien);
        }
    }
}