using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuanVan_ShopDongHo.Models;
using PagedList;
using PagedList.Mvc;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using OfficeOpenXml;

namespace LuanVan_ShopDongHo.Controllers
{
    public class ThongKeController : Controller
    {
        Dictionary<int, string> selecChart = new Dictionary<int, string>
        {   {0,"-Biểu đồ-"},
            {1,"doughnut"},
            {2,"pie" },
            {3,"bar" },
            {4,"line" }
        };

        Dictionary<int, string> option = new Dictionary<int, string> { { 1, "Doanh số" }, { 2, "Số lượng bán ra" } };

        Dictionary<int, int> calendar_month = new Dictionary<int, int>();
        Dictionary<int, int> calendar_year = new Dictionary<int, int>();

        public void createCalendar()
        {
            // make select list month
            List<int> month = new List<int>();
            month.AddRange(Enumerable.Range(1, 12));
            foreach (var item in month)
            {
                calendar_month.Add(item, item);
            }
            // make select list year
            List<int> year = new List<int>();
            year.AddRange(Enumerable.Range(2015, (DateTime.Now.Year - 2015) + 1));
            foreach (var item in year)
            {
                calendar_year.Add(item, item);
            }
        }

        // GET: ThongKe
        public ActionResult Index()
        {
            var dsBieuDo = selecChart.Select(t => new { value = t.Key, text = t.Value }).ToList();

            ViewBag.dsBieuDo = dsBieuDo;

            return View();
        }
        public ActionResult DoanhSoCuaHang(string maHang = "", int? nam = null,int? option = null)
        {

            using (var db = new ShopDongHoDataContext())
            {
                createCalendar();

                var dsBieuDo = selecChart.Select(t => new { value = t.Key, text = t.Value }).ToList();

                ViewBag.dsBieuDo = dsBieuDo;

                var cal_month = calendar_month.ToList();

                var cal_year = calendar_year.Select(t => new { value = t.Key, text = t.Value.ToString() }).ToList();
                cal_year.Insert(0, new { value = 0, text = "-Chọn năm-" });

                var dsHang = db.HANGs.Select(t => new { value = t.MAHANG, text = t.TENHANG }).ToList();
                dsHang.Insert(0, new { value = "0", text = "-Chọn Hãng-" });
                List<int> soLuongThang = new List<int>();
                if (maHang != ""  && maHang != "0" && nam != null && nam != 0)
                {
                    var hang = db.HANGs.FirstOrDefault(t => t.MAHANG == maHang);
                    for (int i = 1; i <= 12; i++)
                    {
                        //var getHoadon = (from sp in db.SANPHAMs
                        //                 join cthd in db.CHITIETHDs on sp.MASP equals cthd.MASP
                        //                 join hd in db.HOADONs on cthd.IDHoaDon equals hd.IDHoaDon
                        //                 join kho in db.KHOs on cthd.MASP equals kho.MASP
                        //                 where hd.NGAYLAP.Value.Month == i && hd.NGAYLAP.Value.Year == nam && sp.MAHANG ==           maHang && kho.TRANGTHAI == 1
                        //                 select cthd).GroupBy(t=>t.MASP).ToList();

                        var getHoaDon = (from k in db.KHOs
                                         join sp in db.SANPHAMs on k.MASP equals sp.MASP
                                         where k.NGAYBAN.Value.Month == i && k.NGAYBAN.Value.Year == nam && k.TRANGTHAI == 1 && sp.MAHANG == maHang
                                         select k).ToList();

                        int count = getHoaDon.Count();
                        soLuongThang.Add(count);
                    }
                    ViewBag.repArray = soLuongThang;
                    ViewBag.brandName = hang.TENHANG;
                    ViewBag.selectedHang = maHang;
                    ViewBag.selectedYear = nam;
                }

                if(option != 0 && option != null)
                {
                    int choose = option ?? 1;
                    ViewBag.selectedOption = option;
                    ViewBag.typeOption = selecChart[choose];
                }

                ViewBag.dsHang = dsHang;
                ViewBag.calendar_year = cal_year;
                ViewBag.month = cal_month;
                return View();
            }
        }

        public ActionResult DoanhSoHang(int? page, string maHang = "", int? month = null, int? year = null, int? selectOption = null)
        {
            using (var db = new ShopDongHoDataContext())
            {
                createCalendar();

                var cal_month = calendar_month.Select(t => new { value = t.Key, text = t.Value.ToString() }).ToList();
                cal_month.Insert(0, new { value = 0, text = "-Chọn tháng-" });

                var cal_year = calendar_year.Select(t => new { value = t.Key, text = t.Value.ToString() }).ToList();
                cal_year.Insert(0, new { value = 0, text = "-Chọn năm" });

                var dsHang = db.HANGs.Select(t => new { value = t.MAHANG, text = t.TENHANG }).ToList();
                dsHang.Insert(0, new { value = "0", text = "-Chọn Hãng-" });

                var filterOption = option.Select(t => new { value = t.Key, text = t.Value }).ToList();
                filterOption.Insert(0, new { value = 0, text = "-Sắp xếp theo-" });

                var thongKe = (from h in db.HANGs
                               join sp in db.SANPHAMs on h.MAHANG equals sp.MAHANG
                               select new HangThongKe
                               {
                                   maHang = h.MAHANG,
                                   maSP = sp.MASP,
                                   tenSP = sp.TENSP,
                                   soLuongBanRa = 0,
                                   soLuongNhap = 0,
                                   doanhSo = 0,
                                   tonKho = 0,
                               }).ToList();

                foreach(var item in thongKe)
                {
                    var k = db.KHOs.Where(t => t.MASP == item.maSP && t.TRANGTHAI ==0 && t.NGAYBAN == null).ToList();
                    var count = k.Count();
                    item.tonKho = count;
                }

                if (maHang != "" && maHang != "0")
                {
                    thongKe = thongKe.Where(t => t.maHang == maHang).ToList();
                    ViewBag.selectedHang = maHang;
                }


                if ((month == null || month == 0) && (year == null || year == 0))
                {
                    foreach (var item in thongKe)
                    {
                        var soLuongNhap = (from pn in db.PHIEUNHAPs
                                           join ct in db.CHITIETPNs on pn.IDPhieuNhap equals ct.IDPhieuNhap
                                           where pn.TINHTRANG == true && ct.MASP == item.maSP
                                           select ct
                                           ).ToList().Sum(t => t.SOLUONG);
                        var tienNhap = (from pn in db.PHIEUNHAPs
                                        join ct in db.CHITIETPNs on pn.IDPhieuNhap equals ct.IDPhieuNhap
                                        where pn.TINHTRANG == true && ct.MASP == item.maSP
                                        select ct).ToList().Sum(t=> t.THANHTIEN);
                        long tienBan = 0;
                        //var soLuongNhap = db.CHITIETPNs.Where(t => t.MASP == item.maSP).Sum(t => t.SOLUONG);
                        item.soLuongNhap = soLuongNhap ?? 0;
                        //var cthd = db.CHITIETHDs.Where(t => t.MASP == item.maSP).ToList();


                        var k = db.KHOs.Where(t => t.MASP == item.maSP && t.TRANGTHAI == 1).ToList();
                        var count = k.Count();
                        
                        item.soLuongBanRa = count;

                        var cthd = (from ct in db.CHITIETHDs
                                    where ct.MASP == item.maSP
                                    select ct).GroupBy(t => t.QRCODE).ToList();

                        foreach(var i in cthd)
                        {
                            foreach(var t in i)
                            {
                                tienBan += t.THANHTIEN ?? 0;
                            }
                        }
                        item.doanhSo = tienBan - tienNhap;
                                          
                    }
                }
                else if (month != null && (year == null || year == 0))
                {
                    
                    foreach (var item in thongKe)
                    {
                        var soLuongNhap = (from pn in db.PHIEUNHAPs
                                           join ct in db.CHITIETPNs on pn.IDPhieuNhap equals ct.IDPhieuNhap
                                           where pn.TINHTRANG == true && ct.MASP == item.maSP && pn.NGAYNHAPHANG.Value.Month == month
                                           select ct
                                          ).ToList().Sum(t => t.SOLUONG);
                        var tienNhap = (from pn in db.PHIEUNHAPs
                                        join ct in db.CHITIETPNs on pn.IDPhieuNhap equals ct.IDPhieuNhap
                                        where pn.TINHTRANG == true && ct.MASP == item.maSP && pn.NGAYNHAPHANG.Value.Month == month
                                        select ct).ToList().Sum(t => t.THANHTIEN);
                        long tienBan = 0;
                        //var soLuongNhap = db.CHITIETPNs.Where(t => t.MASP == item.maSP).Sum(t => t.SOLUONG);
                        item.soLuongNhap = soLuongNhap ?? 0;
                        //var cthd = db.CHITIETHDs.Where(t => t.MASP == item.maSP).ToList();


                        var k = db.KHOs.Where(t => t.MASP == item.maSP && t.TRANGTHAI == 1 && t.NGAYBAN.Value.Month == month).ToList();
                        var count = k.Count();

                        item.soLuongBanRa = count;

                        var cthd = (from ct in db.CHITIETHDs
                                    join h in db.HOADONs on ct.IDHoaDon equals h.IDHoaDon
                                    where ct.MASP == item.maSP && h.NGAYLAP.Value.Month == month
                                    select ct).GroupBy(t => t.QRCODE).ToList();

                        foreach (var i in cthd)
                        {
                            foreach (var t in i)
                            {
                                tienBan += t.THANHTIEN ?? 0;
                            }
                        }
                        item.doanhSo = tienBan - tienNhap;
                    }
                    ViewBag.selectedMonth = month;

                }
                else if (year != null && (month == null || month == 0))
                {
                    //var pn = (from p in db.PHIEUNHAPs
                    //          join ct in db.CHITIETPNs on p.IDPhieuNhap equals ct.IDPhieuNhap
                    //          where p.NGAYLAP.Value.Year == year
                    //          select new
                    //          {
                    //              ct.SOLUONG,
                    //              ct.MASP,
                    //              p.TONGTIEN
                    //          }).ToList();
                    //var hd = (from h in db.HOADONs
                    //          join ct in db.CHITIETHDs on h.IDHoaDon equals ct.IDHoaDon
                    //          where h.NGAYLAP.Value.Year == year
                    //          select new
                    //          {
                    //              h.IDHoaDon,
                    //              ct.THANHTIEN,
                    //              ct.MASP,
                    //              ct.SOLUONG
                    //          }).ToList();
                    //foreach (var item in thongKe)
                    //{
                    //    var soLuongNhap = (from pn in db.PHIEUNHAPs
                    //                       join ct in db.CHITIETPNs on pn.IDPhieuNhap equals ct.IDPhieuNhap
                    //                       where pn.TINHTRANG == true && ct.MASP == item.maSP
                    //                       select ct
                    //                       ).ToList().Sum(t => t.SOLUONG);
                    //    //var soLuongNhap = db.CHITIETPNs.Where(t => t.MASP == item.maSP).Sum(t => t.SOLUONG);
                    //    item.soLuongNhap = soLuongNhap ?? 0;
                    //    //var cthd = db.CHITIETHDs.Where(t => t.MASP == item.maSP).ToList();


                    //    var k = db.KHOs.Where(t => t.MASP == item.maSP && t.TRANGTHAI == 1).ToList();
                    //    var count = k.Count();

                    //    item.soLuongBanRa = count;

                    //    var cthd = (from ct in db.CHITIETHDs
                    //                where ct.MASP == item.maSP
                    //                select ct).GroupBy(t => t.QRCODE).ToList();

                    //    foreach (var i in cthd)
                    //    {
                    //        foreach (var t in i)
                    //        {
                    //            item.doanhSo += t.THANHTIEN;
                    //        }
                    //    }

                    //}
                    foreach (var item in thongKe)
                    {
                        var soLuongNhap = (from pn in db.PHIEUNHAPs
                                           join ct in db.CHITIETPNs on pn.IDPhieuNhap equals ct.IDPhieuNhap
                                           where pn.TINHTRANG == true && ct.MASP == item.maSP && pn.NGAYNHAPHANG.Value.Year == year
                                           select ct
                  ).ToList().Sum(t => t.SOLUONG);
                        var tienNhap = (from pn in db.PHIEUNHAPs
                                        join ct in db.CHITIETPNs on pn.IDPhieuNhap equals ct.IDPhieuNhap
                                        where pn.TINHTRANG == true && ct.MASP == item.maSP && pn.NGAYNHAPHANG.Value.Year == year
                                        select ct).ToList().Sum(t => t.THANHTIEN);
                        long tienBan = 0;


                        //var soLuongNhap = db.CHITIETPNs.Where(t => t.MASP == item.maSP).Sum(t => t.SOLUONG);
                        item.soLuongNhap = soLuongNhap ?? 0;
                        //var cthd = db.CHITIETHDs.Where(t => t.MASP == item.maSP).ToList();


                        var k = db.KHOs.Where(t => t.MASP == item.maSP && t.TRANGTHAI == 1 && t.NGAYBAN.Value.Year == year).ToList();
                        var count = k.Count();

                        item.soLuongBanRa = count;

                        var cthd = (from ct in db.CHITIETHDs
                                    join h in db.HOADONs on ct.IDHoaDon equals h.IDHoaDon
                                    where ct.MASP == item.maSP && h.NGAYLAP.Value.Year == year
                                    select ct).GroupBy(t => t.QRCODE).ToList();

                        foreach (var i in cthd)
                        {
                            foreach (var t in i)
                            {
                                tienBan += t.THANHTIEN ?? 0;
                            }
                        }
                        item.doanhSo = tienBan - tienNhap;
                    }

                    ViewBag.selectedYear = year;
                }
                else
                {
                    //var pn = (from p in db.PHIEUNHAPs
                    //          join ct in db.CHITIETPNs on p.IDPhieuNhap equals ct.IDPhieuNhap
                    //          where p.NGAYLAP.Value.Year == year && p.NGAYLAP.Value.Month == month
                    //          select new
                    //          {
                    //              ct.SOLUONG,
                    //              ct.MASP,
                    //              p.TONGTIEN
                    //          }).ToList();
                    //var hd = (from h in db.HOADONs
                    //          join ct in db.CHITIETHDs on h.IDHoaDon equals ct.IDHoaDon
                    //          where h.NGAYLAP.Value.Year == year && h.NGAYLAP.Value.Month == month
                    //          select new
                    //          {
                    //              h.IDHoaDon,
                    //              ct.THANHTIEN,
                    //              ct.MASP,
                    //              ct.SOLUONG
                    //          }).ToList();
                    //foreach (var item in thongKe)
                    //{
                    //    var soLuongNhap = (from pn in db.PHIEUNHAPs
                    //                       join ct in db.CHITIETPNs on pn.IDPhieuNhap equals ct.IDPhieuNhap
                    //                       where pn.TINHTRANG == true && ct.MASP == item.maSP
                    //                       select ct
                    //                       ).ToList().Sum(t => t.SOLUONG);
                    //    //var soLuongNhap = db.CHITIETPNs.Where(t => t.MASP == item.maSP).Sum(t => t.SOLUONG);
                    //    item.soLuongNhap = soLuongNhap ?? 0;
                    //    //var cthd = db.CHITIETHDs.Where(t => t.MASP == item.maSP).ToList();


                    //    var k = db.KHOs.Where(t => t.MASP == item.maSP && t.TRANGTHAI == 1).ToList();
                    //    var count = k.Count();

                    //    item.soLuongBanRa = count;

                    //    var cthd = (from ct in db.CHITIETHDs
                    //                where ct.MASP == item.maSP
                    //                select ct).GroupBy(t => t.QRCODE).ToList();

                    //    foreach (var i in cthd)
                    //    {
                    //        foreach (var t in i)
                    //        {
                    //            item.doanhSo += t.THANHTIEN;
                    //        }
                    //    }

                    //}

                    foreach (var item in thongKe)
                    {
                        var soLuongNhap = (from pn in db.PHIEUNHAPs
                                           join ct in db.CHITIETPNs on pn.IDPhieuNhap equals ct.IDPhieuNhap
                                           where pn.TINHTRANG == true && ct.MASP == item.maSP && pn.NGAYNHAPHANG.Value.Year == year && pn.NGAYNHAPHANG.Value.Month == month
                                           select ct
                  ).ToList().Sum(t => t.SOLUONG);
                        var tienNhap = (from pn in db.PHIEUNHAPs
                                        join ct in db.CHITIETPNs on pn.IDPhieuNhap equals ct.IDPhieuNhap
                                        where pn.TINHTRANG == true && ct.MASP == item.maSP && pn.NGAYNHAPHANG.Value.Year == year && pn.NGAYNHAPHANG.Value.Month == month
                                        select ct).ToList().Sum(t => t.THANHTIEN);
                        long tienBan = 0;

                        //var soLuongNhap = db.CHITIETPNs.Where(t => t.MASP == item.maSP).Sum(t => t.SOLUONG);
                        item.soLuongNhap = soLuongNhap ?? 0;
                        //var cthd = db.CHITIETHDs.Where(t => t.MASP == item.maSP).ToList();


                        var k = db.KHOs.Where(t => t.MASP == item.maSP && t.TRANGTHAI == 1 && t.NGAYBAN.Value.Year == year).ToList();
                        var count = k.Count();

                        item.soLuongBanRa = count;

                        var cthd = (from ct in db.CHITIETHDs
                                    join h in db.HOADONs on ct.IDHoaDon equals h.IDHoaDon
                                    where ct.MASP == item.maSP && h.NGAYLAP.Value.Year == year && h.NGAYLAP.Value.Month == month
                                    select ct).GroupBy(t => t.QRCODE).ToList();

                        foreach (var i in cthd)
                        {
                            foreach (var t in i)
                            {
                                tienBan += t.THANHTIEN ?? 0;
                            }
                        }
                        item.doanhSo = tienBan - tienNhap;

                    }

                    ViewBag.selectedMonth = month;
                    ViewBag.selectedYear = year;
                }
                thongKe = thongKe.OrderByDescending(t => t.doanhSo).ToList();

                if (selectOption == 1)
                {
                    thongKe = thongKe.OrderByDescending(t => t.doanhSo).ToList();
                    ViewBag.selectedOption = selectOption;
                }

                if (selectOption == 2)
                {
                    thongKe = thongKe.OrderByDescending(t => t.soLuongBanRa).ToList();
                    ViewBag.selectedOption = selectOption;
                }

                int pageNumber = page ?? 1;
                int pageSize = 20;
                var lst = thongKe.ToPagedList(pageNumber, pageSize);

                ViewBag.dsHang = dsHang;
                ViewBag.filterOption = filterOption;
                ViewBag.calendar_month = cal_month;
                ViewBag.calendar_year = cal_year;
                ViewBag.pageNum = pageNumber;
                ViewBag.pageSize = pageSize;
                return View(lst);
            }

        }

        public ActionResult Export(int? page, string maHang = "", int? month = null, int? year = null, int? selectOption = null)
        {
            try
            {
                using (var db = new ShopDongHoDataContext())
                {
                    var thongKe = (from h in db.HANGs
                                   join sp in db.SANPHAMs on h.MAHANG equals sp.MAHANG
                                   select new HangThongKe
                                   {
                                       maHang = h.MAHANG,
                                       maSP = sp.MASP,
                                       tenSP = sp.TENSP,
                                       soLuongBanRa = 0,
                                       soLuongNhap = 0,
                                       doanhSo = 0
                                   }).ToList();

                    if (maHang != "" && maHang != "0")
                    {
                        thongKe = thongKe.Where(t => t.maHang == maHang).ToList();
                    }



                    if ((month == null || month == 0) && (year == null || year == 0))
                    {
                        foreach (var item in thongKe)
                        {
                            var soLuongNhap = db.CHITIETPNs.Where(t => t.MASP == item.maSP).Sum(t => t.SOLUONG);
                            item.soLuongNhap = soLuongNhap ?? 0;
                            var cthd = db.CHITIETHDs.Where(t => t.MASP == item.maSP).ToList();
                            item.soLuongBanRa = cthd.Sum(t => t.SOLUONG) ?? 0;
                            item.doanhSo = cthd.Sum(t => t.THANHTIEN) ?? 0;
                        }
                    }
                    else if (month != null && (year == null || year == 0))
                    {
                        var pn = (from p in db.PHIEUNHAPs
                                  join ct in db.CHITIETPNs on p.IDPhieuNhap equals ct.IDPhieuNhap
                                  where p.NGAYLAP.Value.Month == month
                                  select new
                                  {
                                      ct.SOLUONG,
                                      ct.MASP,
                                      p.TONGTIEN
                                  }).ToList();
                        var hd = (from h in db.HOADONs
                                  join ct in db.CHITIETHDs on h.IDHoaDon equals ct.IDHoaDon
                                  where h.NGAYLAP.Value.Month == month
                                  select new
                                  {
                                      h.IDHoaDon,
                                      ct.THANHTIEN,
                                      ct.MASP,
                                      ct.SOLUONG
                                  }).ToList();
                        foreach (var item in thongKe)
                        {
                            var soLuongNhap = pn.Where(t => t.MASP == item.maSP).Sum(t => t.SOLUONG);
                            item.soLuongNhap = soLuongNhap ?? 0;
                            item.soLuongBanRa = hd.Where(t => t.MASP == item.maSP).Sum(t => t.SOLUONG) ?? 0;
                            item.doanhSo = hd.Where(t => t.MASP == item.maSP).Sum(t => t.THANHTIEN) ?? 0;

                        }
                    }
                    else if (year != null && (month == null || month == 0))
                    {
                        var pn = (from p in db.PHIEUNHAPs
                                  join ct in db.CHITIETPNs on p.IDPhieuNhap equals ct.IDPhieuNhap
                                  where p.NGAYLAP.Value.Year == year
                                  select new
                                  {
                                      ct.SOLUONG,
                                      ct.MASP,
                                      p.TONGTIEN
                                  }).ToList();
                        var hd = (from h in db.HOADONs
                                  join ct in db.CHITIETHDs on h.IDHoaDon equals ct.IDHoaDon
                                  where h.NGAYLAP.Value.Year == year
                                  select new
                                  {
                                      h.IDHoaDon,
                                      ct.THANHTIEN,
                                      ct.MASP,
                                      ct.SOLUONG
                                  }).ToList();
                        foreach (var item in thongKe)
                        {
                            var soLuongNhap = pn.Where(t => t.MASP == item.maSP).Sum(t => t.SOLUONG);
                            item.soLuongNhap = soLuongNhap ?? 0;
                            item.soLuongBanRa = hd.Where(t => t.MASP == item.maSP).Sum(t => t.SOLUONG) ?? 0;
                            item.doanhSo = hd.Where(t => t.MASP == item.maSP).Sum(t => t.THANHTIEN) ?? 0;
                        }
                    }
                    else
                    {
                        var pn = (from p in db.PHIEUNHAPs
                                  join ct in db.CHITIETPNs on p.IDPhieuNhap equals ct.IDPhieuNhap
                                  where p.NGAYLAP.Value.Year == year && p.NGAYLAP.Value.Month == month
                                  select new
                                  {
                                      ct.SOLUONG,
                                      ct.MASP,
                                      p.TONGTIEN
                                  }).ToList();
                        var hd = (from h in db.HOADONs
                                  join ct in db.CHITIETHDs on h.IDHoaDon equals ct.IDHoaDon
                                  where h.NGAYLAP.Value.Year == year && h.NGAYLAP.Value.Month == month
                                  select new
                                  {
                                      h.IDHoaDon,
                                      ct.THANHTIEN,
                                      ct.MASP,
                                      ct.SOLUONG
                                  }).ToList();
                        foreach (var item in thongKe)
                        {
                            var soLuongNhap = pn.Where(t => t.MASP == item.maSP).Sum(t => t.SOLUONG);
                            item.soLuongNhap = soLuongNhap ?? 0;
                            item.soLuongBanRa = hd.Where(t => t.MASP == item.maSP).Sum(t => t.SOLUONG) ?? 0;
                            item.doanhSo = hd.Where(t => t.MASP == item.maSP).Sum(t => t.THANHTIEN) ?? 0;

                        }
                    }
                    thongKe = thongKe.OrderByDescending(t => t.doanhSo).ToList();
                    if (selectOption == 1)
                    {
                        thongKe = thongKe.OrderByDescending(t => t.doanhSo).ToList();
                    }

                    if (selectOption == 2)
                    {
                        thongKe = thongKe.OrderByDescending(t => t.soLuongBanRa).ToList();
                    }

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    ExcelPackage ep = new ExcelPackage();
                    ExcelWorksheet Sheet = ep.Workbook.Worksheets.Add("Tồn kho");
                    Sheet.Cells["A1"].Value = "STT";
                    Sheet.Cells["B1"].Value = "Mã sản phẩm";
                    Sheet.Cells["C1"].Value = "Tên sản phẩm";
                    Sheet.Cells["D1"].Value = "Số lượng nhập";
                    Sheet.Cells["E1"].Value = "Số lượng bán ra";
                    Sheet.Cells["F1"].Value = "Doanh số";
                    Sheet.Cells["G1"].Value = "Tồn kho";
                    int row = 2;
                    int stt = 1;
                    foreach (var item in thongKe)
                    {
                        Sheet.Cells[string.Format("A{0}", row)].Value = stt;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.maSP;
                        Sheet.Cells[string.Format("C{0}", row)].Value = item.tenSP;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.soLuongNhap;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.soLuongBanRa;
                        Sheet.Cells[string.Format("F{0}", row)].Value = item.doanhSo;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.tonKho;
                        row++;
                        stt++;
                    }
                    Sheet.Cells["A:AZ"].AutoFitColumns();
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment: filename=" + "ThongKe.xlsx");
                    Response.BinaryWrite(ep.GetAsByteArray());
                    Response.Flush();
                    Response.End();
                    return RedirectToAction("DoanhSoHang", "ThongKe");
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return RedirectToAction("DoanhSoHang", "ThongKe");
            }
        }


    }

}