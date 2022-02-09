using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LuanVan_ShopDongHo.Models
{
    public class TinTuc
    {
        public int? maTinTuc { get; set; }
        public string tieuDe { get; set; }
        public string tomTat { get; set; }
        public string noiDung { get; set; }
        public string hinhDaiDien { get; set; }

        public DateTime? ngayDang { get; set; }
        public string tenNV { get; set; }
        public bool? trangThai { get; set; }
    }
}