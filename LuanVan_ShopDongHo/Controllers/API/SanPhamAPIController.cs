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
                     where k.TRANGTHAI == false
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
            return Json(sp);
        }

        [System.Web.Http.Route("api/GetAllSanPham")]
        public IHttpActionResult GetAllSanPham(int page, int limit)
        {
            List<SANPHAM> ds = db.SANPHAMs.Where(t => t.TRANGTHAI == false).ToList();
            List<SANPHAM> dsSP = new List<SANPHAM>();
            int test = ds.Count / limit;
            if (limit >= ds.Count)
            {
                var dsHang = from k in db.SANPHAMs
                             where k.TRANGTHAI == false
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
                return Json(dsHang);
            }
            if (page > test)
            {
                return Json(new { message = "Dữ liệu không hợp lệ" });
            }
            if (page == 1)
            {
                for (int i = 0; i < limit; i++)
                {
                    dsSP.Add(ds[i]);
                }
            }
            else
            {
                int dem = 0;
                int start = limit * page;
                for (int i = start; i < ds.Count; i++)
                {
                    if (dem == limit)
                    {
                        break;
                    }
                    else
                    {
                        dsSP.Add(ds[i]);
                        dem++;
                    }
                }
            }

            var lst = dsSP.Select(k => new
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
            });

            return Json(lst);
        }

        [System.Web.Http.Route("api/GetAllSanPhamTimKiem")]
        public IHttpActionResult GetAllSanPhamTimKiem(int page, int limit, string tukhoa)
        {
            List<SANPHAM> ds = new List<SANPHAM>();
            if (tukhoa != "")
            {
                ds = db.SANPHAMs.Where(t => (t.MASP.ToLower().Equals(tukhoa.ToLower().Trim()) || t.TENSP.Contains(tukhoa.ToLower().Trim())) && t.TRANGTHAI == false).ToList();

            }
            else
            {
                ds = db.SANPHAMs.ToList();
            }
            List<SANPHAM> dsSP = new List<SANPHAM>();
            int test = ds.Count / limit;
            if (limit >= ds.Count)
            {
                var dsHang = from k in db.SANPHAMs
                             where k.TRANGTHAI == false
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
                return Json(dsHang);
            }
            if (page > test)
            {
                return Json(new { message = "Dữ liệu không hợp lệ" });
            }
            if (page == 1)
            {
                for (int i = 0; i < limit; i++)
                {
                    dsSP.Add(ds[i]);
                }
            }
            else
            {
                int dem = 0;
                int start = limit * page;
                for (int i = start; i < ds.Count; i++)
                {
                    if (dem == limit)
                    {
                        break;
                    }
                    else
                    {
                        dsSP.Add(ds[i]);
                        dem++;
                    }
                }
            }

            var lst = dsSP.Select(k => new
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
            });

            return Json(lst);
        }

        [System.Web.Http.Route("api/GetAllTaiKhoan")]
        public IHttpActionResult GetAllTaiKhoan()
        {
            var ds = from k in db.TAIKHOANs
                     where k.TRANGTHAI == false
                     select new
                     {
                         k.TENDN,
                         k.MATKHAU,
                         k.MALOAI,
                         //k.Email,
                         //k.token,
                         //k.key_tfa,
                         //k.date_send,
                         //k.userConfirm,
                         k.TRANGTHAI,
                     };
            return Json(ds);
        }

        [System.Web.Http.Route("api/GetAllHang")]
        public IHttpActionResult GetAllHang(int page, int limit)
        {
            List<HANG> ds = db.HANGs.Where(t => t.TRANGTHAI == false).ToList();
            List<HANG> dsSP = new List<HANG>();
            int test = ds.Count / limit;
            if (limit >= ds.Count)
            {
                var dsHang = from k in db.HANGs
                             where k.TRANGTHAI == false
                             select new
                             {
                                 k.MAHANG,
                                 k.TENHANG,
                                 k.THONGTIN,
                                 k.LOGO,
                                 k.URL,
                                 k.BANNER,
                                 k.QUOCGIA,
                                 k.TRANGTHAI,
                             };
                return Json(dsHang);
            }
            if (page > test)
            {
                return Json(new { message = "Dữ liệu không hợp lệ" });
            }
            if (page == 1)
            {
                for (int i = 0; i < limit; i++)
                {
                    dsSP.Add(ds[i]);
                }
            }
            else
            {
                int dem = 0;
                int start = limit * page;
                for (int i = start; i < ds.Count; i++)
                {
                    if (dem == limit)
                    {
                        break;
                    }
                    else
                    {
                        dsSP.Add(ds[i]);
                        dem++;
                    }
                }
            }

            var lst = dsSP.Select(k => new
            {
                k.MAHANG,
                k.TENHANG,
                k.THONGTIN,
                k.LOGO,
                k.URL,
                k.BANNER,
                k.QUOCGIA,
                k.TRANGTHAI,
            });

            return Json(lst);
        }

        [System.Web.Http.Route("api/GetAllHang")]
        public IHttpActionResult GetAllHang()
        {
            var ds = from k in db.HANGs
                     where k.TRANGTHAI == false
                     select new
                     {
                         k.MAHANG,
                         k.TENHANG,
                         k.THONGTIN,
                         k.LOGO,
                         k.URL,
                         k.BANNER,
                         k.QUOCGIA,
                         k.TRANGTHAI,
                     };
            return Json(ds);
        }

        [System.Web.Http.Route("api/GetAllDanhMucSP")]
        public IHttpActionResult GetAllDanhMucSP()
        {
            var ds = from k in db.DANHMUCSPs
                     where k.TRANGTHAI == false
                     select new
                     {
                         k.MADM,
                         k.TENDM,
                         k.TRANGTHAI,
                     };
            return Json(ds);
        }

        [System.Web.Http.Route("api/GetAllPhieuNhap")]
        public IHttpActionResult GetAllPhieuNhap(int page, int limit)
        {
            List<PhieuNhapHang> ds = (from k in db.PHIEUNHAPs
                                      join nv in db.NHANVIENs on k.MANV equals nv.MANV
                                      join h in db.HANGs on k.MAHANG equals h.MAHANG
                                      where k.TRANGTHAI == false
                                      select new PhieuNhapHang
                                      {
                                          IDPhieuNhap = k.IDPhieuNhap,
                                          maPN = k.MAPN,
                                          tenNV = nv.TENNV,
                                          tenHang = h.TENHANG,
                                          ngayLap = string.Format("{0:dd/MM/yyyy}", k.NGAYLAP),
                                          tongTien = k.TONGTIEN,
                                          tinhTrang = k.TINHTRANG,
                                          trangThai = k.TRANGTHAI,
                                      }).ToList();
            List<PhieuNhapHang> dsSP = new List<PhieuNhapHang>();
            int test = ds.Count / limit;
            if (limit >= ds.Count)
            {
                List<PhieuNhapHang> dsPN = (from k in db.PHIEUNHAPs
                                            join nv in db.NHANVIENs on k.MANV equals nv.MANV
                                            join h in db.HANGs on k.MAHANG equals h.MAHANG
                                            where k.TRANGTHAI == false
                                            select new PhieuNhapHang
                                            {
                                                IDPhieuNhap = k.IDPhieuNhap,
                                                maPN = k.MAPN,
                                                tenNV = nv.TENNV,
                                                tenHang = h.TENHANG,
                                                ngayLap = string.Format("{0:dd/MM/yyyy}", k.NGAYLAP),
                                                tongTien = k.TONGTIEN,
                                                tinhTrang = k.TINHTRANG,
                                                trangThai = k.TRANGTHAI,
                                            }).ToList();
                return Json(dsPN);
            }
            if (page > test)
            {
                return Json(new { message = "Dữ liệu không hợp lệ" });
            }
            if (page == 1)
            {
                for (int i = 0; i < limit; i++)
                {
                    dsSP.Add(ds[i]);
                }
            }
            else
            {
                int dem = 0;
                int start = limit * page;
                for (int i = start; i < ds.Count; i++)
                {
                    if (dem == limit)
                    {
                        break;
                    }
                    else
                    {
                        dsSP.Add(ds[i]);
                        dem++;
                    }
                }
            }

            var lst = dsSP;

            return Json(lst);
        }

        [System.Web.Http.Route("api/GetAllPhieuNhap")]
        public IHttpActionResult GetAllPhieuNhap()
        {
            List<PhieuNhapHang> ds = (from k in db.PHIEUNHAPs
                                      join nv in db.NHANVIENs on k.MANV equals nv.MANV
                                      join h in db.HANGs on k.MAHANG equals h.MAHANG
                                      where k.TRANGTHAI == false
                                      select new PhieuNhapHang
                                      {
                                          IDPhieuNhap = k.IDPhieuNhap,
                                          maPN = k.MAPN,
                                          tenNV = nv.TENNV,
                                          tenHang = h.TENHANG,
                                          ngayLap = string.Format("{0:dd/MM/yyyy}", k.NGAYLAP),
                                          tongTien = k.TONGTIEN,
                                          tinhTrang = k.TINHTRANG,
                                          trangThai = k.TRANGTHAI,
                                      }).ToList();
            return Json(ds);
        }

        [System.Web.Http.Route("api/GetChiTietPhieuNhap")]
        public IHttpActionResult GetChiTietPhieuNhap(int id)
        {
            List<ChiTietPNHang> ds = (from k in db.CHITIETPNs
                                      join sp in db.SANPHAMs on k.MASP equals sp.MASP
                                      where k.IDPhieuNhap == id
                                      select new ChiTietPNHang
                                      {
                                          tenSP = sp.TENSP,
                                          soLuong = k.SOLUONG,
                                          donGia = k.DONGIA,
                                          thanhTien = k.THANHTIEN,
                                          hinhAnh = sp.HINHANH,
                                      }).ToList();
            return Json(ds);
        }

        [System.Web.Http.Route("api/GetAllPhieuNhapChuaXuLy")]
        public IHttpActionResult GetAllPhieuNhapChuaXuLy(int page, int limit)
        {
            List<PhieuNhapHang> ds = (from k in db.PHIEUNHAPs
                                      join nv in db.NHANVIENs on k.MANV equals nv.MANV
                                      join h in db.HANGs on k.MAHANG equals h.MAHANG
                                      where k.TRANGTHAI == false && k.TINHTRANG == false
                                      select new PhieuNhapHang
                                      {
                                          IDPhieuNhap = k.IDPhieuNhap,
                                          maPN = k.MAPN,
                                          tenNV = nv.TENNV,
                                          tenHang = h.TENHANG,
                                          ngayLap = string.Format("{0:dd/MM/yyyy}", k.NGAYLAP),
                                          tongTien = k.TONGTIEN,
                                          tinhTrang = k.TINHTRANG,
                                          trangThai = k.TRANGTHAI,
                                      }).ToList();
            List<PhieuNhapHang> dsSP = new List<PhieuNhapHang>();
            int test = ds.Count / limit;
            if (limit >= ds.Count)
            {
                List<PhieuNhapHang> dsPN = (from k in db.PHIEUNHAPs
                                            join nv in db.NHANVIENs on k.MANV equals nv.MANV
                                            join h in db.HANGs on k.MAHANG equals h.MAHANG
                                            where k.TRANGTHAI == false && k.TINHTRANG == false
                                            select new PhieuNhapHang
                                            {
                                                IDPhieuNhap = k.IDPhieuNhap,
                                                maPN = k.MAPN,
                                                tenNV = nv.TENNV,
                                                tenHang = h.TENHANG,
                                                ngayLap = string.Format("{0:dd/MM/yyyy}", k.NGAYLAP),
                                                tongTien = k.TONGTIEN,
                                                tinhTrang = k.TINHTRANG,
                                                trangThai = k.TRANGTHAI,
                                            }).ToList();
                return Json(dsPN);
            }
            if (page > test)
            {
                return Json(new { message = "Dữ liệu không hợp lệ" });
            }
            if (page == 1)
            {
                for (int i = 0; i < limit; i++)
                {
                    dsSP.Add(ds[i]);
                }
            }
            else
            {
                int dem = 0;
                int start = limit * page;
                for (int i = start; i < ds.Count; i++)
                {
                    if (dem == limit)
                    {
                        break;
                    }
                    else
                    {
                        dsSP.Add(ds[i]);
                        dem++;
                    }
                }
            }

            var lst = dsSP;

            return Json(lst);
        }

        [System.Web.Http.Route("api/GetAllPhieuNhapDaXuLy")]
        public IHttpActionResult GetAllPhieuNhapDaXuLy(int page, int limit)
        {
            List<PhieuNhapHang> ds = (from k in db.PHIEUNHAPs
                                      join nv in db.NHANVIENs on k.MANV equals nv.MANV
                                      join h in db.HANGs on k.MAHANG equals h.MAHANG
                                      where k.TRANGTHAI == false && k.TINHTRANG == true
                                      select new PhieuNhapHang
                                      {
                                          IDPhieuNhap = k.IDPhieuNhap,
                                          maPN = k.MAPN,
                                          tenNV = nv.TENNV,
                                          tenHang = h.TENHANG,
                                          ngayLap = string.Format("{0:dd/MM/yyyy}", k.NGAYLAP),
                                          tongTien = k.TONGTIEN,
                                          tinhTrang = k.TINHTRANG,
                                          trangThai = k.TRANGTHAI,
                                      }).ToList();
            List<PhieuNhapHang> dsSP = new List<PhieuNhapHang>();
            int test = ds.Count / limit;
            if (limit >= ds.Count)
            {
                List<PhieuNhapHang> dsPN = (from k in db.PHIEUNHAPs
                                            join nv in db.NHANVIENs on k.MANV equals nv.MANV
                                            join h in db.HANGs on k.MAHANG equals h.MAHANG
                                            where k.TRANGTHAI == false && k.TINHTRANG == true
                                            select new PhieuNhapHang
                                            {
                                                IDPhieuNhap = k.IDPhieuNhap,
                                                maPN = k.MAPN,
                                                tenNV = nv.TENNV,
                                                tenHang = h.TENHANG,
                                                ngayLap = string.Format("{0:dd/MM/yyyy}", k.NGAYLAP),
                                                tongTien = k.TONGTIEN,
                                                tinhTrang = k.TINHTRANG,
                                                trangThai = k.TRANGTHAI,
                                            }).ToList();
                return Json(dsPN);
            }
            if (page > test)
            {
                return Json(new { message = "Dữ liệu không hợp lệ" });
            }
            if (page == 1)
            {
                for (int i = 0; i < limit; i++)
                {
                    dsSP.Add(ds[i]);
                }
            }
            else
            {
                int dem = 0;
                int start = limit * page;
                for (int i = start; i < ds.Count; i++)
                {
                    if (dem == limit)
                    {
                        break;
                    }
                    else
                    {
                        dsSP.Add(ds[i]);
                        dem++;
                    }
                }
            }

            var lst = dsSP;

            return Json(lst);
        }

        [System.Web.Http.Route("api/GetThongTinNhanVien")]
        public IHttpActionResult GetThongTinNhanVien(string tenDN)
        {
            var nhanVien = (from k in db.NHANVIENs
                            where k.TENDN == tenDN
                            select new ThongTinTaiKhoan
                            {
                                maNV = k.MANV,
                                tenNV = k.TENNV,
                                diaChi = k.DIACHI,
                                gioiTinh = k.GIOITINH,
                                dienThoai = k.DIENTHOAI,
                                tenDN = k.TENDN,
                                ngaySinh = string.Format("{0:dd/MM/yyyy}", k.NGAYSINH),
                            }).FirstOrDefault();
            return Json(nhanVien);
        }
    }

    public class PhieuNhapHang
    {
        public int? IDPhieuNhap { get; set; }
        public string maPN { get; set; }

        public string tenNV { get; set; }
        public string tenHang { get; set; }
        public string ngayLap { get; set; }
        public long? tongTien { get; set; }
        public bool? tinhTrang { get; set; }
        public bool? trangThai { get; set; }
    }

    public class ChiTietPNHang
    {
        public string tenSP { get; set; }
        public int? soLuong { get; set; }
        public long? donGia { get; set; }
        public long? thanhTien { get; set; }
        public string hinhAnh { get; set; }
    }

    public class ThongTinTaiKhoan
    {
        public string maNV { get; set; }
        public string tenNV { get; set; }
        public string dienThoai { get; set; }
        public string ngaySinh { get; set; }
        public string gioiTinh { get; set; }
        public string diaChi { get; set; }
        public string tenDN { get; set; }
    }
}
