using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan_ShopDongHo.Models
{
    public class PhanQuyen
    {
        public string tenDN { get; set; }

        public string tenNhomNguoiDung { get; set; }
    }

    public class PhanQuyenModel
    {
        public string tenNND { get; set; }
        public string quyen { get; set; }
    }

    public class PhanQuyenLoad
    {
        public int? idNND { get; set; }
        public string tenNND { get; set; }
        public bool? quyenBanHang { get; set; }
        public bool? quyenChinhSuaNguoiDung { get; set; }
        public bool? quyenBaoCao { get; set; }
        public bool? quyenQuanLyKho { get; set; }
        public bool? quyenQuanLyKhuyenMai { get; set; }

    }
    
}