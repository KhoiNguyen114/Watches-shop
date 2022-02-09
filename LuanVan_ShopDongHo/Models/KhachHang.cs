using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace LuanVan_ShopDongHo.Models
{
    public class KhachHang
    {
        [Required(ErrorMessage = ("Vui lòng không để trống họ và tên"))]
        [DisplayName("Họ tên")]
        public string TENKH { get; set; }

        public DateTime  NGAYSINH { get; set; }

        public string GIOITINH { get; set; }

        [Required(ErrorMessage = ("Vui lòng không để trống số điện thoại"))]
        [DisplayName("Số điện thoại")]
        public string SDT { get; set; }

        [Required(ErrorMessage = ("Vui lòng không để trống địa chỉ"))]
        [DisplayName("Địa chỉ")]
        public string DIACHI { get; set; }

        [Required(ErrorMessage = ("Vui lòng không để trống số cmnd"))]
        [DisplayName("CMND")]
        public string SOCMND { get; set; }
    }
}