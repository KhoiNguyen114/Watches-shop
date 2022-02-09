using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan_ShopDongHo.Models
{
    public class customerLinkOrder
    {
        public int IDHoaDon { get; set; }

        public string MAHD { get; set; }

        public int? IDKHACHHANG { get; set; }

        public string MANV { get; set; }

        public int? MAHINHTHUCTHANHTOAN { get; set; }

        public DateTime? NGAYLAP { get; set; }

        public long? TONGTIEN { get; set; }

        public int TINHTRANG { get; set; }

        public string GHICHU { get; set; }
        
        public List<ChitietOrder> chitiets { get; set; }
    }
    public class ChitietOrder
    {
        public int IDHoaDon { get; set; }

        public string MASP { get; set; }

        public string TENSP { get; set; }

        public string HINHANH { get; set; }

        public int? SOLUONG { get; set; }

        public long? DONGIA { get; set; }

        public long? KHUYENMAI { get; set; }

        public long? THANHTIEN { get; set; }
    }
}