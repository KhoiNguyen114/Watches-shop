using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan_ShopDongHo.Models
{
    public class BinhLuanSanPham
    {
        public int? idBinhLuan { get; set; }
        public string maSP { get; set; }
        public int? idTinTuc { get; set; }
        public string tenNguoiBL { get; set; }
        public string tenDN { get; set; }
        public DateTime? ngayTao { get; set; }
        public string noiDung { get; set; }
        public bool? isTinTuc { get; set; }
        public bool? trangThai { get; set; }
    }
}