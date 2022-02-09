using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LuanVan_ShopDongHo.Models
{
    public class TaiKhoan
    {
        [Required(ErrorMessage = ("Vui lòng không để trống mã sản phẩm")), StringLength(50, ErrorMessage = "Tên đăng nhập không vượt quá 50 ký tự")]
        [DisplayName("Tên đăng nhập")]
        public string TENDN { get; set; }

        [Required(ErrorMessage = ("Vui lòng không để trống mật khẩu")), StringLength(20, ErrorMessage = "Mật khẩu không vượt quá 20 ký tự")]
        [DisplayName("Mật khẩu")]
        public string MATKHAU { get; set; }

        public string MALOAI { get; set; }

        public string token { get; set; }

        public string key_tfa { get; set; }

        public DateTime date_send { get; set; }

    }

    public class TaiKhoanModel
    {
        public string tenDN { get; set; }
        public string matKhau { get; set; }
        public string tenLoai { get; set; }
        public string email { get; set; }

        public bool? trangThai { get; set; }

    }
}