using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using LuanVan_ShopDongHo.Models;

namespace LuanVan_ShopDongHo.Controllers
{
    public class SanPhamAPIController : ApiController
    {
        ShopDongHoDataContext db = new ShopDongHoDataContext();

        [System.Web.Http.Route("api/GetAllSanPham")]
        public IHttpActionResult GetAllSanPham()
        {
            var sp = from k in db.SANPHAMs
                     select new
                     {
                         k.MASP,
                         k.TENSP,
                         k.CHITIETSP,
                         k.HINHANH,
                         k.SOLUONG,
                         k.DONGIA,
                         k.MAHANG,
                         k.MABAOHANH,
                         k.ThoiHanBH,
                         k.MADM,
                         k.DONGSP,
                         k.KICHTHUOC,
                         k.NANGLUONG,
                         k.LOAIDAY,
                         k.XUATSU,
                         k.MOTASP,
                         k.TRANGTHAI,
                     };
            return Json(new
            {
                data = sp,
            });
        }

        [System.Web.Http.Route("api/GetAllTaiKhoan")]
        public IHttpActionResult GetAllTaiKhoan()
        {
            var ds = from k in db.TAIKHOANs
                     select new
                     {
                         k.TENDN,
                         k.MATKHAU,
                         k.MALOAI,
                         k.Email,
                         k.token,
                         k.key_tfa,
                         k.date_send,
                         k.userConfirm,
                         k.TRANGTHAI,
                     };
            return Json(new
            {
                data = ds,
            });
        }


    }
}
