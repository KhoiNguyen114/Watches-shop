using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan_ShopDongHo.Models
{
    public class brand
    {
        public string tenHang { get; set; }

        public int soluong { get; set; }
    }
    public class ThongKeCuaHangNam
    {
        public int month { get; set; }

        public string[] thongkeThang { get; set; }
    }
}