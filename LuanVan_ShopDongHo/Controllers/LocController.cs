using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuanVan_ShopDongHo.Models;
using PagedList;
using PagedList.Mvc;
using Newtonsoft.Json;


namespace LuanVan_ShopDongHo.Controllers
{
    public class LocController : Controller
    {
        ShopDongHoDataContext db = new ShopDongHoDataContext();
        // GET: Loc

        public static List<SANPHAM> dsSP;

        private static IQueryable<Product> baseProducts;

        public static IPagedList<Product> sanphams;

        private static List<Product> products;

        public static Dictionary<string,string> queryDicts = new Dictionary<string, string>();

        private static bool isNew = true;

        private static bool isFiltering = false;

        public static Dictionary<string, string> dsTien = new Dictionary<string, string>();

        public static Dictionary<string, string> dsDongSP = new Dictionary<string, string>();

        public static Dictionary<string, string> dsLoaiDay = new Dictionary<string, string>();
            

        public static query_type queriesDM = new query_type();

        public static query_type queriesHANG = new query_type();

        public static query_type queriesGIA = new query_type();

        public static query_type queriesDONGSP = new query_type();

        public static query_type queriesLOAIDAY = new query_type();

        public static query_type queriesKM = new query_type();

        public static List<query_condition> conditionDM = new List<query_condition>();

        public static List<query_condition> conditionHANG = new List<query_condition>();

        public static List<query_condition> conditionGIA = new List<query_condition>();

        public static List<query_condition> conditionDONGSP = new List<query_condition>();

        public static List<query_condition> conditionLOAIDAY = new List<query_condition>();

        public static List<query_condition> conditionKM = new List<query_condition>();

        public static List<Product> filter_products = new List<Product>();

        public static Dictionary<int, query_type> conditionList = new Dictionary<int, query_type>();

        public const int pagesize = 9;

        public ActionResult FilterMenu()
        {
            List<HANG> ds = db.HANGs.ToList();
            List<DANHMUCSP> dsGT = db.DANHMUCSPs.ToList();
            dsTien = TaoListGiaCa();
            dsDongSP = TaoDongSP();
            dsLoaiDay = TaoDSLoaiDay();


            ViewBag.dsHang = ds;
            ViewBag.dsGT = dsGT;
            ViewBag.dsTien = dsTien;
            ViewBag.dsDongSP = dsDongSP;
            ViewBag.dsLoaiDay = dsLoaiDay;
            LocSanPham a = new LocSanPham();

            return PartialView(a);
        }


        public Dictionary<string,string> TaoListGiaCa()
        {
            Dictionary<string, string> ds = new Dictionary<string, string>();
            ds.Add("First","0 - 2,000,000");
            ds.Add("Second","2,000,000 - 4,000,000");
            ds.Add("Third","4,000,000 - 6,000,000");
            ds.Add("Forth","6,000,000 - 8,000,000");
            ds.Add("Fifth","8,000,000 - 20,000,000");
            ds.Add("Sixth","Trên 20,000,000");
            return ds;
        }

        public Dictionary<string, string> TaoDongSP()
        {
            Dictionary<string, string> ds = new Dictionary<string, string>();
            ds.Add("LI","Limited");
            ds.Add("DIA","Kim Cương");
            ds.Add("FLAT","Siêu mỏng");
            ds.Add("CRO","Chrono");
            return ds;
        }

        public Dictionary<string, string> TaoDSLoaiDay()
        {
            Dictionary<string, string> ds = new Dictionary<string, string>();
            ds.Add("CS","Dây cao su");
            ds.Add("KL","Dây kim loại");
            ds.Add("LEA","Dây da");
            return ds;
        }

        public List<string> TaoDSKichThuoc()
        {
            List<string> ds = new List<string>();
            return ds;

        }

        public ActionResult MainFilter(int? page,string maHang = "", string maDM ="",string dongSP ="",string loaiDay ="",string donGIA ="", bool? newFilter = null)
        {
            if(conditionList == null || conditionList.Count <= 0)
                conditionList = createDictinaryFilter();
            int pageNumber = (page ?? 1);
            
            if(newFilter != null)
            {
                isNew = newFilter ?? false;
            }
            
            if(isFiltering == false)
            {
                if (isNew == true)
                {
                    baseProducts = getDefaultProducts();
                    products = baseProducts.ToList();
                    isNew = false;
                     if (maHang != "")
                     {
                         products = products.Where(t => t.MAHANG == maHang).ToList();
                     }

                     if (maDM != "")
                     {
                         products = products.Where(t => t.MADM == maDM).ToList();
                     }

                     if (dongSP != "")
                     {
                         products = products.Where(t => t.DONGSP.Contains(dongSP)).ToList();
                     }

                     if (loaiDay != "")
                     {
                         products = products.Where(t => t.LOAIDAY.Contains(loaiDay)).ToList();
                     }

                     if (donGIA != "")
                     {
                         var values = donGIA.Trim().Split(new char[] { '-', ' ' });
                         int count = values.Count();
                         string d1 = moneyToString(values[0]);
                         string d2 = moneyToString(values[count - 1]);
                         bool parse1 = long.TryParse(d1, out long o_dg1);
                         bool parse2 = long.TryParse(d2, out long o_dg2);

                         long? dg1, dg2;


                         if (parse1 == true)
                         {
                             dg1 = o_dg1;
                         }
                         else
                         {
                             dg1 = null;
                         }


                         if (parse2 == true)
                         {
                             dg2 = o_dg2;
                         }

                         else
                             dg2 = null;

                         long? mini = dg1, maxi = dg2;

                         switch (d1)
                         {
                             case "Trên":
                                 mini = dg2;
                                 maxi = null;
                                 break;
                             case "Dưới":
                                 maxi = dg2;
                                 mini = null;
                                 break;
                         }

                         long g1 = mini ?? -1;
                         long g2 = maxi ?? -1;


                         if (g2 == -1)
                         {
                             products = products.Where(t => t.DONGIA > g1).ToList();
                         }
                         else if (g1 == -1)
                         {
                             products = products.Where(t => t.DONGIA < g2).ToList();
                         }
                         else
                         {
                             products = products.Where(t => t.DONGIA > g1 && t.DONGIA < g2).ToList();
                         }
                     }
                     
                }
            }         
            else if (isFiltering == true)
            {
                products = Session["products"] as List<Product>;
                isFiltering = false;
                pageNumber = 1;
            }
            sanphams = products.ToPagedList(pageNumber, pagesize);

            //Số sản phẩm hiển thị trên 1 trang

            //Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber. 

            List<KHUYENMAI> dsKM = db.KHUYENMAIs.Where(t => t.TINHTRANG.Equals("Đang diễn ra")).ToList();
            List<HienThiKhuyenMai> dsSPKM = new List<HienThiKhuyenMai>();
            for (int i = 0; i < dsKM.Count; i++)
            {
                List<CHITIETKHUYENMAI> dsCTKM = db.CHITIETKHUYENMAIs.Where(t => t.MAKM == dsKM[i].MAKM).ToList();
                if (dsCTKM.Count > 0)
                {
                    for (int j = 0; j < dsCTKM.Count; j++)
                    {
                        SANPHAM sanpham = db.SANPHAMs.Where(t => t.MASP == dsCTKM[j].MASP).FirstOrDefault();
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

            ViewBag.sanphams = sanphams;
            ViewBag.dsKM = dsSPKM;
            Session["baseProducts"] = baseProducts;
            Session["products"] = products;
            Session["sanphams"] = sanphams;
            Session["query_coditions"] = queryDicts;
            return View(sanphams);
        }

        private Dictionary<int, query_type> createDictinaryFilter()
        {
            try
            {
                Dictionary<int, query_type> lst = new Dictionary<int, query_type>();
                queriesHANG.keyWord = "MAHANG";
                queriesHANG.type = conditionHANG;
                queriesHANG.count = conditionHANG.Count();
                lst.Add(1, queriesHANG);

                queriesDM.keyWord = "MADM";
                queriesDM.type = conditionDM;
                queriesDM.count = conditionDM.Count();
                lst.Add(2, queriesDM);

                queriesGIA.keyWord = "DONGIA";
                queriesGIA.type = conditionGIA;
                queriesGIA.count = conditionGIA.Count();
                lst.Add(3, queriesGIA);

                queriesLOAIDAY.keyWord = "LOAIDAY";
                queriesLOAIDAY.type = conditionLOAIDAY;
                queriesLOAIDAY.count = conditionLOAIDAY.Count();
                lst.Add(4, queriesLOAIDAY);

                queriesDONGSP.keyWord = "DONGSP";
                queriesDONGSP.type = conditionDONGSP;
                queriesDONGSP.count = conditionDONGSP.Count();
                lst.Add(5, queriesDONGSP);

                queriesKM.keyWord = "KHUYENMAI";
                queriesKM.type = conditionKM;
                queriesKM.count = conditionKM.Count();
                lst.Add(6, queriesKM);
                return lst;
            }
            catch
            {
                return null;
            }
        }

        private IQueryable<Product> getDefaultProducts()
        {
            IQueryable<Product> ds = baseProducts = (from sp in db.SANPHAMs
                                                     where sp.TRANGTHAI == false
                                                 select new Product
                                                 {
                                                     MASP = sp.MASP,
                                                     TENSP = sp.TENSP,
                                                     DONGIA = sp.DONGIA,
                                                     HINHANH = sp.HINHANH,
                                                     KICHTHUOC = sp.KICHTHUOC,
                                                     LOAIDAY = sp.LOAIDAY,
                                                     NANGLUONG = sp.NANGLUONG,
                                                     MADM = sp.MADM,
                                                     MAHANG = sp.MAHANG,
                                                     MABAOHANH = sp.MABAOHANH,
                                                     THOIGIANBH = sp.ThoiHanBH,
                                                     DONGSP = sp.DONGSP
                                                 });
            return ds;
        }

        [HttpGet]
        public JsonResult Filter(string condition ="",string condition_element="")
        {
            try
            {
                isFiltering = true;
                using (var db = new ShopDongHoDataContext())
                {
                    string s = condition_element;
                    var elements = s.Split(':');
                    var ele_field = elements[0];
                    var ele_value = elements[1];

                    //Lọc điều kiện
                    string query_condition = condition;
                    query_condition.Trim();
                    var cons = query_condition.Split(':');

                    string field = cons[0].ToString();
                    string value = cons[1].ToString();
                    var content = new query_condition();
                    content.filter_content = field;
                    content.value = value;

                    // Thêm điều kiện vào danh sách điuề kiện
                    int key = 0;
                    foreach (var item in conditionList)
                    {
                       if(item.Value.keyWord.Trim().ToString().Equals(content.filter_content.Trim().ToString()))
                        {
                            key = item.Key;
                            break;
                        }
                    }
                       
                    if(key > 0)
                    {
                        conditionList[key].count++;
                        conditionList[key].type.Add(content);
                    }

                    conditionList =  conditionList.OrderByDescending(t => t.Value.count).ToDictionary(x=> x.Key, x=> x.Value);

                    filter_products = getFilterProducts();
                    if (filter_products == null)
                        return Json(new { success = false, messeage = "Lỗi"},JsonRequestBehavior.AllowGet);


                    queryDicts = Session["query_coditions"] as Dictionary<string,string>;
                    if(!queryDicts.ContainsKey(ele_field.Trim()))
                    {
                        queryDicts.Add(ele_field, ele_value);
                    }   
                    
                    isFiltering = true;
                    Session["products"] = filter_products;
                    Session["query_coditions"] = queryDicts;
                    return Json(new { success = true, data = sanphams}, JsonRequestBehavior.AllowGet);
                } 
            }
             catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Json(new { success = false });
            }
        }

        private List<Product> getFilterProducts()
        {
            try
            {
             
                List<Product> temp = new List<Product>();
                if (conditionList == null || conditionList.Count <= 0)
                    return null;
                int j = 0;
                foreach (var item in conditionList)
                {
                    if(item.Value.count > 0)
                    {
                        string content = item.Value.keyWord;
                        if (j == 0 && content == "DONGIA")
                        {
                            temp = baseProducts.ToList();
                            filter_products.Clear();
                            foreach (var condition in item.Value.type)
                            {
                                var values = condition.value.Trim().Split(new char[] {'-',' '});
                                int count = values.Count();
                                string d1 = moneyToString(values[0]);
                                string d2 = moneyToString(values[count-1]);
                                bool parse1 = long.TryParse(d1,out long o_dg1);
                                bool parse2 = long.TryParse(d2, out long o_dg2);

                                long? dg1, dg2;
 

                                if (parse1 == true)
                                {
                                    dg1 = o_dg1;
                                }
                                else
                                {
                                    dg1 = null;
                                }    


                                if (parse2 == true)
                                {
                                    dg2 = o_dg2;
                                }    

                                else
                                    dg2 = null;

                                long? mini = dg1, maxi = dg2;

                                switch (d1)
                                {
                                    case "Trên":
                                        mini = dg2;
                                        maxi = null;
                                        break;
                                    case "Dưới":    
                                        maxi = dg2;
                                        mini = null;
                                        break;
                                }    

                                List<Product> addFilter = addFilterProducts(content,"", mini, maxi, temp);
                                filter_products.AddRange(addFilter);
                            }
                        }
                        else if (j == 0)
                        {
                            temp = baseProducts.ToList();
                            filter_products.Clear();
                            foreach (var condition in item.Value.type)
                            {
                                List<Product> addFilter = addFilterProducts(content, condition.value, null, null, temp);
                                filter_products.AddRange(addFilter);
                            }
                        }
                        else if (content == "DONGIA")
                        {
                            temp = new List<Product>(filter_products);
                            filter_products.Clear();

                            foreach (var condition in item.Value.type)
                            {
                                var values = condition.value.Trim().Split('-');
                                string d1 = moneyToString(values[0]);
                                string d2 = moneyToString(values[1]);

                                bool parse1 = long.TryParse(d1, out long o_dg1);
                                bool parse2 = long.TryParse(d1, out long o_dg2);
                                long? dg1, dg2;
                                long? mini = null, maxi = null;

                                if (parse1 == true)
                                    dg1 = o_dg1;
                                else
                                {
                                    dg1 = null;
                                }


                                if (parse2 == true)
                                    dg2 = o_dg2;
                                else
                                    dg2 = null;

                                switch (d1)
                                {
                                    case "Trên":
                                        mini = dg2;
                                        maxi = null;
                                        break;
                                    case "Dưới":
                                        maxi = dg2;
                                        mini = null;
                                        break;
                                }

                                List<Product> addFilter = addFilterProducts(content, null, mini, maxi, temp);
                                filter_products.AddRange(addFilter);
                            }
                        }
                        else
                        {
                            temp = new List<Product>(filter_products);
                            filter_products.Clear();
                            foreach (var condition in item.Value.type)
                            {
                                List<Product> addFilter = addFilterProducts(content, condition.value, null, null, temp);
                                filter_products.AddRange(addFilter);
                            }
                        }    
                        j++;
                    }    
                }

                return filter_products;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        private string moneyToString(string money)
        {
            var values = money.Split(',');
            string s = "";
            foreach (var item in values)
                s += item;
            return s;
        }

        private List<Product> addFilterProducts(string content, string value, long? min, long? max,List<Product> db)
        {
            using (var dtb = new ShopDongHoDataContext())
            {
                List<Product> filters = new List<Product>();
                switch (content)
                {
                    case "MAHANG":
                        filters = db.Where(t => t.MAHANG.Trim().Equals(value.Trim())).ToList();
                        break;
                    case "MADM":
                        filters = db.Where(t => t.MADM.Trim().Equals(value.Trim())).ToList();
                        break;
                    case "LOAIDAY":
                        filters = db.Where(t => t.LOAIDAY.Contains(value)).ToList();
                        break;
                    case "DONGIA":
                        long dg1 = min ?? -1;
                        long dg2 = max ?? -1;


                        if (dg2 == -1)
                        {
                            filters = db.Where(t => t.DONGIA > dg1).ToList();
                        }
                        else if (dg1 == -1)
                        {
                            filters = db.Where(t => t.DONGIA < dg2).ToList();
                        }
                        else
                        {
                            filters = db.Where(t => t.DONGIA > dg1 && t.DONGIA < dg2).ToList();
                        }
                        break;
                    case "DONGSP":
                        filters = db.Where(t => t.DONGSP.Contains(value.Trim().ToLower())).ToList();
                        break;
                    case "KHUYENMAI":
                        var spKM = (from k in dtb.KHUYENMAIs
                                    join ct in dtb.CHITIETKHUYENMAIs on k.MAKM equals ct.MAKM
                                    join sp in dtb.SANPHAMs on ct.MASP equals sp.MASP
                                    where k.TINHTRANG == "Đang diễn ra"
                                    select sp).ToList();
                        filters = db.Where(t => spKM.Exists(x => x.MASP == t.MASP) == true).ToList();
                        break;
                }
                return filters;

            }
        }

        [HttpGet]
        public JsonResult EraseCondition(string condition = "", string condition_element = "")
        {
            try
            {

                queryDicts = Session["query_coditions"] as Dictionary<string, string>;

                string s = condition_element;
                var elements = s.Split(':');
                var ele_field = elements[0];
                var ele_value = elements[1];

                foreach (var item in queryDicts)
                {
                    if (item.Key.Contains(ele_field.Trim()))
                    {
                        queryDicts.Remove(ele_field);
                        break;
                    }    
                }


                if (conditionList == null || conditionList.Count <= 0)
                    return Json(new { success = false });
                var cons = condition.Split(':');

                string field = cons[0].ToString();
                string value = cons[1].ToString();

                var content = new query_condition();
                content.filter_content = field;
                content.value = value;


                query_type temp = conditionList.FirstOrDefault(t => t.Value.keyWord.Trim().Equals(content.filter_content.Trim())).Value;
                int key = conditionList.FirstOrDefault(t => t.Value.keyWord.Trim().Equals(content.filter_content.Trim())).Key;
                if (temp == null)
                    return Json(new { success = false });
                query_condition qtype = new Controllers.query_condition();
                qtype = temp.type.FirstOrDefault(t => t.value.Trim().Equals(content.value.Trim()));
                temp.type.Remove(qtype);
                temp.count--;
                conditionList[key] = temp;

   

                queryDicts.Remove(content.value.Trim());

                Session["query_coditions"] = queryDicts;
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Json(new { success = false });
            }
        }

        public ActionResult TimKiemSanPham(string tuKhoa)
        {
            Session["loc"] = null;
            //if (String.IsNullOrEmpty(tuKhoa))
            //{
            //    return RedirectToAction("TrangChuLocSP", "Loc");
            //}
            var ds = (from sp in db.SANPHAMs where sp.TRANGTHAI == false && (sp.TENSP.Contains(tuKhoa) || sp.MASP == tuKhoa) select new Product {
                MASP = sp.MASP,
                TENSP = sp.TENSP,
                DONGIA = sp.DONGIA,
                HINHANH = sp.HINHANH,
                KICHTHUOC = sp.KICHTHUOC,
                LOAIDAY = sp.LOAIDAY,
                NANGLUONG = sp.NANGLUONG,
                MADM = sp.MADM,
                MAHANG = sp.MAHANG,
                MABAOHANH = sp.MABAOHANH,
                THOIGIANBH = sp.ThoiHanBH,
                DONGSP = sp.DONGSP
            }).ToList();

            products = ds;

            int? page = 1;
            int pagesize = 9; //Số sản phẩm hiển thị trên 1 trang
            int pageNumber = (page ?? 1);//Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
                                         // nếu page = null thì lấy giá trị 1 cho biến pageNumber.

            if (ds.Count == 0)
            {
                Session["loc"] = "Không có kết quả tìm kiếm phù hợp";
            }
            else
            {
                var lsts = ds.ToPagedList(pageNumber, pagesize);
                isNew = false;
                return View("MainFilter", lsts);
            }
            var lst = ds.ToPagedList(pageNumber, pagesize);
            return View("MainFilter", lst);
        }
    }

    public class query_condition
    {
        public string filter_content { get; set; }

        public string value { get; set; }        

    }

    public class query_type
    {
        public List<query_condition> type { get; set; }

        public int count { get; set; }

        public string keyWord { get; set; }
    }
}