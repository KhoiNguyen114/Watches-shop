using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan_ShopDongHo.Models
{
    public class PhieuNhap
    {
        public int? IDPhieuNhap { get; set; }
        public string maPN { get; set; }

        public string tenNV { get; set; }
        public string tenHang { get; set; }
        public DateTime? ngayLap { get; set; }
        public long? tongTien { get; set; }
        public bool? tinhTrang { get; set; }
    }

    public class ChiTietPhieuNhap
    {
        public int? IDPhieuNhap { get; set; }
        public string tenSP { get; set; }
        public int? soLuong { get; set; }
        public long? donGia { get; set; }
        public long? thanhTien { get; set; }
    }

    public class NhapHangModel
    {
        public PhieuNhap pn { get; set; }
        public List<ChiTietPhieuNhap> ctpn { get; set; }

        public SanPham sp { get; set; }
    }

    public class NhapHangXuatFile
    {
        public PhieuNhap pn { get; set; }
        public List<ChiTietPhieuNhap> ctpn { get; set; }
    }
}