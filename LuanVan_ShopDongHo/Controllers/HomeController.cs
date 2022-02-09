using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LuanVan_ShopDongHo.Models;

namespace LuanVan_ShopDongHo.Controllers
{
    public class HomeController : Controller
    {
    
        [ChildActionOnly]
        public ActionResult RenderMenu()
        {
            using (var db = new ShopDongHoDataContext())
            {
                var brands = (from brand in db.HANGs
                              select brand);

                var brands_swiss = brands.Where(t => t.QUOCGIA.Trim().ToLower().Equals("thụy sĩ")).ToList();
                var brands_notSwiss = brands.Where(t => t.QUOCGIA.Trim().ToLower() != "thụy sĩ").ToList();

                int brands_swiss_count = brands_swiss.Count();
                int brands_notSwiss_count = brands.Count() - brands_swiss_count;

                int brands_notSwiss1_count = (brands_notSwiss_count % 2 == 0) ?brands_notSwiss_count/2 : brands_notSwiss_count%2;
                int brands_left = brands_notSwiss_count - brands_notSwiss1_count;
    

                var brands_notSwiss1 = brands_notSwiss.OrderBy(t => t.TENHANG).Take(brands_notSwiss1_count);


                var brands_notSwiss2 = brands_notSwiss.OrderByDescending(t => t.TENHANG).Take(brands_left);


                var idBrands_male = db.SANPHAMs.Where(sp => sp.DANHMUCSP.TENDM.Trim().ToLower().Equals("NAM")).OrderByDescending(sp => sp.DONGIA).Select(sp=> sp.MAHANG).Distinct().ToList();

                var idBrands_male_swiss = new List<HANG>();
                var idBrands_male_another = new List<HANG>();
                
                foreach(var item in idBrands_male)
                {
                    var brand = db.HANGs.FirstOrDefault(b => b.MAHANG == item && b.QUOCGIA.Trim().ToLower().Equals("thụy sĩ"));
                    if (brand != null)
                        idBrands_male_swiss.Add(brand);
                }

                foreach (var item in idBrands_male)
                {
                    var brand = db.HANGs.FirstOrDefault(b => b.MAHANG == item && b.QUOCGIA.Trim().ToLower() !="thụy sĩ");
                    if (brand != null)
                        idBrands_male_another.Add(brand);
                }

                var idBrands_female = db.SANPHAMs.Where(sp => sp.DANHMUCSP.TENDM.Trim().ToLower().Equals("Nữ")).OrderByDescending(sp => sp.DONGIA).Select(sp => sp.MAHANG).Distinct().ToList();

                var idBrands_female_swiss = new List<HANG>();
                var idBrands_female_another = new List<HANG>();

                foreach (var item in idBrands_female)
                {
                    var brand = db.HANGs.FirstOrDefault(b => b.MAHANG == item && b.QUOCGIA.Trim().Equals("thụy sĩ"));
                    if (brand != null)
                        idBrands_female_swiss.Add(brand);
                }

                foreach (var item in idBrands_female)
                {
                    var brand = db.HANGs.FirstOrDefault(b => b.MAHANG == item && b.QUOCGIA.Trim() != "thụy sĩ");
                    if (brand != null)
                        idBrands_female_another.Add(brand);
                }



                var brands_male_hot = new List<HANG>();
                var brands_female_hot = new List<HANG>();

                foreach(var item in idBrands_male_another)
                {
                    brands_male_hot.Add(item);
                    if (brands_male_hot.Count() > 4)
                        break;
                }

                foreach (var item in idBrands_female_another)
                {
                    brands_female_hot.Add(item);
                    if (brands_female_hot.Count() > 4)
                        break;
                }


                ViewBag.brands_notSwiss1 = brands_notSwiss1;
                ViewBag.brands_notSwiss2 = brands_notSwiss2;
                ViewBag.brands_swiss = brands_swiss;
                ViewBag.brands_male_swiss = idBrands_male_swiss;
                ViewBag.brands_female_swiss = idBrands_female_swiss;
                ViewBag.brands_male_hot = brands_male_hot;
                ViewBag.brands_female_hot = brands_female_hot;

                return PartialView("Menu");
            }
        }

        public ActionResult getMenu()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        [HttpPost]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        public ActionResult Home(string msg = "")
        {

            using (var dtb = new ShopDongHoDataContext())
            {
                var dsSP_Nam = (from sanpham in dtb.SANPHAMs
                                join danhmuc in dtb.DANHMUCSPs on sanpham.MADM equals danhmuc.MADM
                                where danhmuc.TENDM.Equals("Nam")
                                select sanpham).OrderByDescending(s => s.DONGIA).Take(8).ToList();

                var dsSP_Nu = (from sanpham in dtb.SANPHAMs
                               join danhmuc in dtb.DANHMUCSPs on sanpham.MADM equals danhmuc.MADM
                               where danhmuc.TENDM.Equals("Nữ")
                               select sanpham).OrderByDescending(s => s.DONGIA).Take(8).ToList();

                var dsSP_Recom = (from sanpham in dtb.SANPHAMs
                               join danhmuc in dtb.DANHMUCSPs on sanpham.MADM equals danhmuc.MADM
                               select sanpham).OrderByDescending(s => s.DONGIA).Take(8).ToList();

                var DanhMucSP = (from dm in dtb.DANHMUCSPs select dm);

                List<KHUYENMAI> dsKM = dtb.KHUYENMAIs.Where(t => t.TINHTRANG.Equals("Đang diễn ra")).ToList();
                List<HienThiKhuyenMai> dsSPKM = new List<HienThiKhuyenMai>();
                for (int i = 0; i < dsKM.Count; i++)
                {
                    List<CHITIETKHUYENMAI> dsCTKM = dtb.CHITIETKHUYENMAIs.Where(t => t.MAKM == dsKM[i].MAKM).ToList();
                    if (dsCTKM.Count > 0)
                    {
                        for (int j = 0; j < dsCTKM.Count; j++)
                        {
                            SANPHAM sanpham = dtb.SANPHAMs.Where(t => t.MASP == dsCTKM[j].MASP).FirstOrDefault();
                            if (sanpham != null)
                            {
                                HienThiKhuyenMai htkm = new HienThiKhuyenMai();
                                htkm.maSP = sanpham.MASP;
                                htkm.donGia = sanpham.DONGIA;
                                htkm.ptKhuyenMai = dsKM[i].KHUYENMAI1;
                                dsSPKM.Add(htkm);
                            }
                        }
                    }
                }
                ViewBag.msg = msg;
                ViewBag.dsKM = dsSPKM;
                ViewBag.dsDM = DanhMucSP;
                ViewBag.dsSP_Nam = dsSP_Nam;
                ViewBag.dsSP_Nu = dsSP_Nu;
                ViewBag.dsSP_Rec = dsSP_Recom;

                ViewBag.cardName = "";
                return View();
            }    


        }

        public ActionResult MenuKhachHang()
        {
            TAIKHOAN tk = Session["User"] as TAIKHOAN;
            return PartialView("MenuKhachHang");
        }
    }  
    
}