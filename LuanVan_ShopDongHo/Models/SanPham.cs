using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LuanVan_ShopDongHo.Models
{
    public class SanPham
    {
        public SanPham()
        {

        }

        //[Required(ErrorMessage = ("Vui lòng không để trống mã sản phẩm")), StringLength(100, ErrorMessage = "Mã sản phẩm không được vượt quá 100 kí tự")]
        [DisplayName("Mã sản phẩm")]
        public string maSP { get; set; }

        //[Required(ErrorMessage = ("Tên sản phẩm không được bỏ trống"))]
        [DisplayName("Tên Sản phẩm")]
        public string tenSP { get; set; }

        [DisplayName("Chi tiết sản phẩm")]
        public string chiTietSP { get; set; }


        //[Required(ErrorMessage = ("Đơn giá không được bỏ trống"))]
        [DisplayName("Đơn giá")]
        public long? donGia { get; set; }

        [DisplayName("Hình Ảnh")]
        public string hinhAnh { get; set; }

        //[Required(ErrorMessage = ("Chi tiết sản phẩm không được bỏ trống"))]
        [DisplayName("Số lượng")]
        public int? soLuong { get; set; }

        
        [DisplayName("Mã hãng")]
        public string maHang { get; set; }

       
        [DisplayName("Mã bảo hành")]
        public string maBaoHanh { get; set; }

        //[Required(ErrorMessage = ("Vui lòng không để trống thời gian bảo hành"))]
        [DisplayName("Thời gian bảo hành")]
        public int? thoiHanBaoHanh { get; set; }

       
        [DisplayName("Mã danh mục")]
        public string maDanhmuc { get; set; }

        //[Required(ErrorMessage = ("Vui lòng không để trống phiên bản sản phẩm"))]
        [DisplayName("Phiên bản sản phẩm")]
        public string dongSP { get; set; }

        //[Required(ErrorMessage = ("Vui lòng không để trống kích thước"))]
        [DisplayName("Kích thước")]
        public string kichThuoc { get; set; }

        //[Required(ErrorMessage = ("Vui lòng không để trống năng lượng"))]
        [DisplayName("Năng lượng")]
        public string nangLuong { get; set; }

        //[Required(ErrorMessage = ("Vui lòng không để trống loại dây"))]
        [DisplayName("Loại dây")]
        public string loaiDay { get; set; }

        //[Required(ErrorMessage = ("Vui lòng không để trống xuất sứ"))]
        [DisplayName("Xuất sứ")]
        public string xuatSu { get; set; }

        public bool? isDelete { get; set; }

        public string moTaSP { get; set; }
    }

    public class SanPhamThem : SanPham
    {

    }

    public class SanPhamSua : SanPham
    {

    }

    public class SanPhamModel
    {
        public SanPhamThem spThem { get; set; }
        public SanPhamSua spSua { get; set; }
    }
}