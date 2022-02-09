using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan_ShopDongHo.Models
{
    public class BaoHanh
    {
        public int? idBaoHanh { get; set; }
        public string maHoaDon { get; set; }
        public int? idChitiet { get; set; }
        public string tenSP { get; set; }
        public DateTime? ngayBD { get; set; }
        public DateTime? ngayKT { get; set; }
        public string ghiChu { get; set; }
        public bool? tinhTrang { get; set; }
        public string qrcode { get; set; }
    }

    public class BaoHanhXuatFile
    {
        public int? idBaoHanh { get; set; }
        public string maHoaDon { get; set; }
        public string tenKH { get; set; }
        public int? idChitiet { get; set; }
        public int? thoiHanBaoHanh { get; set; }
        public DateTime? ngayLapHD { get; set; }
        public string tenSP { get; set; }
        public long? donGia { get; set; }
        public string xuatSu { get; set; }
        public DateTime? ngayBD { get; set; }
        public DateTime? ngayKT { get; set; }
        public string ghiChu { get; set; }
        public bool? tinhTrang { get; set; }
        public string qrcode { get; set; }
    }
}