using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LuanVan_ShopDongHo.Models
{
    public class HangSP
    {
        [Required(ErrorMessage ="Mã hãng không được bỏ trống"), StringLength(10,ErrorMessage ="Mã hãng không được vượt quá 10 kí tự")]
        public string maHang { get; set; }
        [Required(ErrorMessage = "Tên hãng không được bỏ trống")]
        public string tenHang { get; set; }
        [Required(ErrorMessage = "Thông tin hãng không được bỏ trống")]
        public string thongTin { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn đường dẫn hãng")]
        public string url { get; set; }
        public string logo { get; set; }
        public string banner { get; set; }
        [Required(ErrorMessage = "Thông tin quốc gia của hãng không được bỏ trống")]
        public string quocGia { get; set; }
    }
}