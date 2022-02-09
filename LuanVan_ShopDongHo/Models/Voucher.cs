using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan_ShopDongHo.Models
{
    public class Voucher
    {
        public string maChuongTrinh { get; set; }
        public string maVoucher { get; set; }
        public string tenKH { get; set; }
        public DateTime? ngaySuDung { get; set; }
        public bool? trangThai { get; set; }

    }
}