using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan_ShopDongHo.Models
{
    public class HangThongKe
    {
        public string maHang { get; set; }

        public string maSP { get; set; }

        public string tenSP { get; set; }
        public int soLuongBanRa { get; set; }

        public int soLuongNhap { get; set; }

        public long? doanhSo { get; set; }

        public int tonKho { get; set;  }
    }
}