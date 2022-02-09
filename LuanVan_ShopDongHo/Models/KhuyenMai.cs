using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan_ShopDongHo.Models
{
    public class KhuyenMai
    {
        public string maKM { get; set; }
        public DateTime? thoiGianBatDau { get; set; }
        public DateTime? thoiGianKetThuc { get; set; }
        public string tinhTrang { get; set; }
        public int? km { get; set; }
        public string hinhAnh { get; set; }
        public bool? trangThai { get; set; }
    }

    public class ChiTietKhuyenMai
    {
        public List<string> maSP { get; set; }
    }

    public class KhuyenMaiModel
    {
        public KhuyenMai km { get; set; }
        public ChiTietKhuyenMai ctkm { get; set; }
    }

    public class HienThiKhuyenMai
    {
        public string maSP { get; set; }
        public int? ptKhuyenMai { get; set; }
        public long? donGia { get; set; }
        public long? donGiaKM
        {
            get { return donGia - (donGia * ptKhuyenMai / 100); }
        }
    }
}