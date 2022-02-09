using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using LuanVan_ShopDongHo.Models;
using PagedList;

namespace LuanVan_ShopDongHo.Controllers
{
    public class QuanLyController : Controller
    {
        // GET: QuanLy
        ShopDongHoDataContext db = new ShopDongHoDataContext();
        public static List<SANPHAM> dsSP;
        public static List<HANG> dsH;
        public static List<DANHMUCSP> dsDM;

        public ActionResult Index()
        {
            return View();
        }

        public List<SelectListItem> taoListPhienBan()
        {
            List<SelectListItem> ds = new List<SelectListItem>();
            ds.Add(new SelectListItem() { Text = "Phiên bản đặc biệt", Value = "Phiên bản đặc biệt" });
            ds.Add(new SelectListItem() { Text = "Phiên bản đặc thường", Value = "Phiên bản thường" });
            ds.Add(new SelectListItem() { Text = "Phiên bản đặc biệt_bluetooth", Value = "Phiên bản đặc biệt_bluetooth" });
            ds.Add(new SelectListItem() { Text = "Phiên bản đặc biệt_chrono", Value = "Phiên bản đặc biệt_chrono" });
            ds.Add(new SelectListItem() { Text = "Phiên bản đặc biệt_kim cương", Value = "Phiên bản đặc biệt_kim cương" });
            ds.Add(new SelectListItem() { Text = "Phiên bản đặc biệt_light", Value = "Phiên bản đặc biệt_light" });
            ds.Add(new SelectListItem() { Text = "Phiên bản đặc biệt_limited", Value = "Phiên bản đặc biệt_limited" });
            ds.Add(new SelectListItem() { Text = "Phiên bản đặc biệt_siêu mỏng", Value = "Phiên bản đặc biệt_siêu mỏng" });
            return ds;
        }

        public List<SelectListItem> taoListNangLuong()
        {
            var dsNL = new List<SelectListItem>();
            dsNL.Add(new SelectListItem() { Text = "Automatic (Tự động)", Value = "Automatic (Tự động)" });
            dsNL.Add(new SelectListItem() { Text = "Eco-Drive (Năng lượng ánh sáng)", Value = "Eco-Drive (Năng lượng ánh sáng)" });
            dsNL.Add(new SelectListItem() { Text = "Quartz (PIN)", Value = "Quartz (PIN)" });
            dsNL.Add(new SelectListItem() { Text = "Solar (Năng lượng ánh sáng)", Value = "Solar (Năng lượng ánh sáng)" });

            return dsNL;
        }

        public List<SelectListItem> taoListLoaiDay()
        {
            List<SelectListItem> ds = new List<SelectListItem>();
            ds.Add(new SelectListItem() { Text = "Dây cao su", Value = "Dây cao su" });
            ds.Add(new SelectListItem() { Text = "Dây hợp kim", Value = "Dây hợp kim" });
            ds.Add(new SelectListItem() { Text = "Dây da", Value = "Dây da" });
            ds.Add(new SelectListItem() { Text = "Dây kim loại", Value = "Dây kim loại" });
            ds.Add(new SelectListItem() { Text = "Dây silicone", Value = "Dây silicone" });
            ds.Add(new SelectListItem() { Text = "Dây vải", Value = "Dây vải" });
            return ds;
        }

        public List<SelectListItem> taoListThoiHanBaoHanh()
        {
            List<SelectListItem> ds = new List<SelectListItem>();
            ds.Add(new SelectListItem() { Text = "1 năm", Value = "1" });
            ds.Add(new SelectListItem() { Text = "2 năm", Value = "2" });
            ds.Add(new SelectListItem() { Text = "3 năm", Value = "3" });
            ds.Add(new SelectListItem() { Text = "4 năm", Value = "4" });
            return ds;
        }

        public List<SelectListItem> taoListQuocGia()
        {
            List<SelectListItem> ds = new List<SelectListItem>();
            ds.Add(new SelectListItem() { Text = "Chọn quốc gia", Value = "" });
            ds.Add(new SelectListItem() { Text = "Afganistan", Value = "Afganistan" });
            ds.Add(new SelectListItem() { Text = "Albania", Value = "Albania" });
            ds.Add(new SelectListItem() { Text = "Algeria", Value = "Algeria" });
            ds.Add(new SelectListItem() { Text = "American Samoa", Value = "American Samoa" });
            ds.Add(new SelectListItem() { Text = "Andorra", Value = "Andorra" });
            ds.Add(new SelectListItem() { Text = "Angola", Value = "Angola" });
            ds.Add(new SelectListItem() { Text = "Anguilla", Value = "Anguilla" });
            ds.Add(new SelectListItem() { Text = "Antigua & Barbuda", Value = "Antigua & Barbuda" });
            ds.Add(new SelectListItem() { Text = "Argentina", Value = "Argentina" });
            ds.Add(new SelectListItem() { Text = "Armenia", Value = "Armenia" });
            ds.Add(new SelectListItem() { Text = "Aruba", Value = "Aruba" });
            ds.Add(new SelectListItem() { Text = "Úc", Value = "Úc" });
            ds.Add(new SelectListItem() { Text = "Austria", Value = "Austria" });
            ds.Add(new SelectListItem() { Text = "Azerbaijan", Value = "Azerbaijan" });
            ds.Add(new SelectListItem() { Text = "Bahamas", Value = "Bahamas" });
            ds.Add(new SelectListItem() { Text = "Bahrain", Value = "Bahrain" });
            ds.Add(new SelectListItem() { Text = "Bangladesh", Value = "Bangladesh" });
            ds.Add(new SelectListItem() { Text = "Barbados", Value = "Barbados" });
            ds.Add(new SelectListItem() { Text = "Belarus", Value = "Belarus" });
            ds.Add(new SelectListItem() { Text = "Belgium", Value = "Belgium" });
            ds.Add(new SelectListItem() { Text = "Belize", Value = "Belize" });
            ds.Add(new SelectListItem() { Text = "Benin", Value = "Benin" });
            ds.Add(new SelectListItem() { Text = "Bermuda", Value = "Bermuda" });
            ds.Add(new SelectListItem() { Text = "Bhutan", Value = "Bhutan" });
            ds.Add(new SelectListItem() { Text = "Bolivia", Value = "Bolivia" });
            ds.Add(new SelectListItem() { Text = "Bonaire", Value = "Bonaire" });
            ds.Add(new SelectListItem() { Text = "Bosnia & Herzegovina", Value = "Bosnia & Herzegovina" });
            ds.Add(new SelectListItem() { Text = "Botswana", Value = "Botswana" });
            ds.Add(new SelectListItem() { Text = "Brazil", Value = "Brazil" });
            ds.Add(new SelectListItem() { Text = "Ấn Độ", Value = "Ấn Độ" });
            ds.Add(new SelectListItem() { Text = "Brunei", Value = "Brunei" });
            ds.Add(new SelectListItem() { Text = "Bulgaria", Value = "Bulgaria" });
            ds.Add(new SelectListItem() { Text = "Burkina Faso", Value = "Burkina Faso" });
            ds.Add(new SelectListItem() { Text = "Burundi", Value = "Burundi" });
            ds.Add(new SelectListItem() { Text = "Cambodia", Value = "Cambodia" });
            ds.Add(new SelectListItem() { Text = "Cameroon", Value = "Cameroon" });
            ds.Add(new SelectListItem() { Text = "Canada", Value = "Canada" });
            ds.Add(new SelectListItem() { Text = "Canary Islands", Value = "Canary Islands" });
            ds.Add(new SelectListItem() { Text = "Cape Verde", Value = "Cape Verde" });
            ds.Add(new SelectListItem() { Text = "Central African Republic", Value = "Central African Republic" });
            ds.Add(new SelectListItem() { Text = "Chad", Value = "Chad" });
            ds.Add(new SelectListItem() { Text = "Channel Islands", Value = "Channel Islands" });
            ds.Add(new SelectListItem() { Text = "Chile", Value = "Chile" });
            ds.Add(new SelectListItem() { Text = "Trung Quốc", Value = "Trung Quốc" });
            ds.Add(new SelectListItem() { Text = "Christmas Island", Value = "Christmas Island" });
            ds.Add(new SelectListItem() { Text = "Cocos Island", Value = "Cocos Island" });
            ds.Add(new SelectListItem() { Text = "Colombia", Value = "Colombia" });
            ds.Add(new SelectListItem() { Text = "Comoros", Value = "Comoros" });
            ds.Add(new SelectListItem() { Text = "Congo", Value = "Congo" });
            ds.Add(new SelectListItem() { Text = "Cook Islands", Value = "Cook Islands" });
            ds.Add(new SelectListItem() { Text = "Costa Rica", Value = "Costa Rica" });
            ds.Add(new SelectListItem() { Text = "Cote DIvoire", Value = "Cote DIvoire" });
            ds.Add(new SelectListItem() { Text = "Croatia", Value = "Croatia" });
            ds.Add(new SelectListItem() { Text = "Cuba", Value = "Cuba" });
            ds.Add(new SelectListItem() { Text = "Curaco", Value = "Curaco" });
            ds.Add(new SelectListItem() { Text = "Cyprus", Value = "Cyprus" });
            ds.Add(new SelectListItem() { Text = "Czech Republic", Value = "Czech Republic" });
            ds.Add(new SelectListItem() { Text = "Đan Mạch", Value = "Đan Mạch" });
            ds.Add(new SelectListItem() { Text = "Djibouti", Value = "Djibouti" });
            ds.Add(new SelectListItem() { Text = "Dominica", Value = "Dominica" });
            ds.Add(new SelectListItem() { Text = "Dominican Republic", Value = "Dominican Republic" });
            ds.Add(new SelectListItem() { Text = "East Timor", Value = "East Timor" });
            ds.Add(new SelectListItem() { Text = "Ecuador", Value = "Ecuador" });
            ds.Add(new SelectListItem() { Text = "Egypt", Value = "Egypt" });
            ds.Add(new SelectListItem() { Text = "El Salvador", Value = "El Salvador" });
            ds.Add(new SelectListItem() { Text = "Equatorial Guinea", Value = "Equatorial Guinea" });
            ds.Add(new SelectListItem() { Text = "Eritrea", Value = "Eritrea" });
            ds.Add(new SelectListItem() { Text = "Estonia", Value = "Estonia" });
            ds.Add(new SelectListItem() { Text = "Ethiopia", Value = "Ethiopia" });
            ds.Add(new SelectListItem() { Text = "Falkland Islands", Value = "Falkland Islands" });
            ds.Add(new SelectListItem() { Text = "Faroe Islands", Value = "Faroe Islands" });
            ds.Add(new SelectListItem() { Text = "Fiji", Value = "Fiji" });
            ds.Add(new SelectListItem() { Text = "Finland", Value = "Finland" });
            ds.Add(new SelectListItem() { Text = "France", Value = "France" });
            ds.Add(new SelectListItem() { Text = "French Guiana", Value = "French Guiana" });
            ds.Add(new SelectListItem() { Text = "French Polynesia", Value = "French Polynesia" });
            ds.Add(new SelectListItem() { Text = "French Southern Ter", Value = "French Southern Ter" });
            ds.Add(new SelectListItem() { Text = "Gabon", Value = "Gabon" });
            ds.Add(new SelectListItem() { Text = "Gambia", Value = "Gambia" });
            ds.Add(new SelectListItem() { Text = "Georgia", Value = "Georgia" });
            ds.Add(new SelectListItem() { Text = "Germany", Value = "Germany" });
            ds.Add(new SelectListItem() { Text = "Ghana", Value = "Ghana" });
            ds.Add(new SelectListItem() { Text = "Gibraltar", Value = "Gibraltar" });
            ds.Add(new SelectListItem() { Text = "Great Britain", Value = "Great Britain" });
            ds.Add(new SelectListItem() { Text = "Greece", Value = "Greece" });
            ds.Add(new SelectListItem() { Text = "Greenland", Value = "Greenland" });
            ds.Add(new SelectListItem() { Text = "Grenada", Value = "Grenada" });
            ds.Add(new SelectListItem() { Text = "Guadeloupe", Value = "Guadeloupe" });
            ds.Add(new SelectListItem() { Text = "Guam", Value = "Guam" });
            ds.Add(new SelectListItem() { Text = "Guatemala", Value = "Guatemala" });
            ds.Add(new SelectListItem() { Text = "Guinea", Value = "Guinea" });
            ds.Add(new SelectListItem() { Text = "Guyana", Value = "Guyana" });
            ds.Add(new SelectListItem() { Text = "Haiti", Value = "Haiti" });
            ds.Add(new SelectListItem() { Text = "Hawaii", Value = "Hawaii" });
            ds.Add(new SelectListItem() { Text = "Honduras", Value = "Honduras" });
            ds.Add(new SelectListItem() { Text = "Hồng Kông", Value = "Hồng Kông" });
            ds.Add(new SelectListItem() { Text = "Hungary", Value = "Hungary" });
            ds.Add(new SelectListItem() { Text = "Iceland", Value = "Iceland" });
            ds.Add(new SelectListItem() { Text = "Indonesia", Value = "Indonesia" });
            ds.Add(new SelectListItem() { Text = "Iran", Value = "Iran" });
            ds.Add(new SelectListItem() { Text = "Iraq", Value = "Iraq" });
            ds.Add(new SelectListItem() { Text = "Ireland", Value = "Ireland" });
            ds.Add(new SelectListItem() { Text = "Isle of Man", Value = "Isle of Man" });
            ds.Add(new SelectListItem() { Text = "Israel", Value = "Israel" });
            ds.Add(new SelectListItem() { Text = "Italy", Value = "Italy" });
            ds.Add(new SelectListItem() { Text = "Jamaica", Value = "Jamaica" });
            ds.Add(new SelectListItem() { Text = "Nhật Bản", Value = "Nhật Bản" });
            ds.Add(new SelectListItem() { Text = "Jordan", Value = "Jordan" });
            ds.Add(new SelectListItem() { Text = "Kazakhstan", Value = "Kazakhstan" });
            ds.Add(new SelectListItem() { Text = "Kenya", Value = "Kenya" });
            ds.Add(new SelectListItem() { Text = "Kiribati", Value = "Kiribati" });
            ds.Add(new SelectListItem() { Text = "Korea North", Value = "Korea North" });
            ds.Add(new SelectListItem() { Text = "Korea Sout", Value = "Korea Sout" });
            ds.Add(new SelectListItem() { Text = "Kuwait", Value = "Kuwait" });
            ds.Add(new SelectListItem() { Text = "Kyrgyzstan", Value = "Kyrgyzstan" });
            ds.Add(new SelectListItem() { Text = "Laos", Value = "Laos" });
            ds.Add(new SelectListItem() { Text = "Latvia", Value = "Latvia" });
            ds.Add(new SelectListItem() { Text = "Lebanon", Value = "Lebanon" });
            ds.Add(new SelectListItem() { Text = "Lesotho", Value = "Lesotho" });
            ds.Add(new SelectListItem() { Text = "Liberia", Value = "Liberia" });
            ds.Add(new SelectListItem() { Text = "Libya", Value = "Libya" });
            ds.Add(new SelectListItem() { Text = "Liechtenstein", Value = "Liechtenstein" });
            ds.Add(new SelectListItem() { Text = "Lithuania", Value = "Lithuania" });
            ds.Add(new SelectListItem() { Text = "Luxembourg", Value = "Luxembourg" });
            ds.Add(new SelectListItem() { Text = "Macau", Value = "Macau" });
            ds.Add(new SelectListItem() { Text = "Macedonia", Value = "Macedonia" });
            ds.Add(new SelectListItem() { Text = "Madagascar", Value = "Madagascar" });
            ds.Add(new SelectListItem() { Text = "Malaysia", Value = "Malaysia" });
            ds.Add(new SelectListItem() { Text = "Malawi", Value = "Malawi" });
            ds.Add(new SelectListItem() { Text = "Maldives", Value = "Maldives" });
            ds.Add(new SelectListItem() { Text = "Mali", Value = "Mali" });
            ds.Add(new SelectListItem() { Text = "Malta", Value = "Malta" });
            ds.Add(new SelectListItem() { Text = "Marshall Islands", Value = "Marshall Islands" });
            ds.Add(new SelectListItem() { Text = "Martinique", Value = "Martinique" });
            ds.Add(new SelectListItem() { Text = "Mauritania", Value = "Mauritania" });
            ds.Add(new SelectListItem() { Text = "Mauritius", Value = "Mauritius" });
            ds.Add(new SelectListItem() { Text = "Mayotte", Value = "Mayotte" });
            ds.Add(new SelectListItem() { Text = "Mexico", Value = "Mexico" });
            ds.Add(new SelectListItem() { Text = "Midway Islands", Value = "Midway Islands" });
            ds.Add(new SelectListItem() { Text = "Moldova", Value = "Moldova" });
            ds.Add(new SelectListItem() { Text = "Monaco", Value = "Monaco" });
            ds.Add(new SelectListItem() { Text = "Mongolia", Value = "Mongolia" });
            ds.Add(new SelectListItem() { Text = "Montserrat", Value = "Montserrat" });
            ds.Add(new SelectListItem() { Text = "Morocco", Value = "Morocco" });
            ds.Add(new SelectListItem() { Text = "Mozambique", Value = "Mozambique" });
            ds.Add(new SelectListItem() { Text = "Myanmar", Value = "Myanmar" });
            ds.Add(new SelectListItem() { Text = "Nambia", Value = "Nambia" });
            ds.Add(new SelectListItem() { Text = "Nauru", Value = "Nauru" });
            ds.Add(new SelectListItem() { Text = "Nepal", Value = "Nepal" });
            ds.Add(new SelectListItem() { Text = "Netherland Antilles", Value = "Netherland Antilles" });
            ds.Add(new SelectListItem() { Text = "Netherlands", Value = "Netherlands" });
            ds.Add(new SelectListItem() { Text = "Nevis", Value = "Nevis" });
            ds.Add(new SelectListItem() { Text = "New Caledonia", Value = "New Caledonia" });
            ds.Add(new SelectListItem() { Text = "New Zealand", Value = "New Zealand" });
            ds.Add(new SelectListItem() { Text = "Nicaragua", Value = "Nicaragua" });
            ds.Add(new SelectListItem() { Text = "Niger", Value = "Niger" });
            ds.Add(new SelectListItem() { Text = "Nigeria", Value = "Nigeria" });
            ds.Add(new SelectListItem() { Text = "Niue", Value = "Niue" });
            ds.Add(new SelectListItem() { Text = "Norfolk Island", Value = "Norfolk Island" });
            ds.Add(new SelectListItem() { Text = "Norway", Value = "Norway" });
            ds.Add(new SelectListItem() { Text = "Oman", Value = "Oman" });
            ds.Add(new SelectListItem() { Text = "Pakistan", Value = "Pakistan" });
            ds.Add(new SelectListItem() { Text = "Palau Island", Value = "Palau Island" });
            ds.Add(new SelectListItem() { Text = "Palestine", Value = "Palestine" });
            ds.Add(new SelectListItem() { Text = "Panama", Value = "Panama" });
            ds.Add(new SelectListItem() { Text = "Papua New Guinea", Value = "Papua New Guinea" });
            ds.Add(new SelectListItem() { Text = "Paraguay", Value = "Paraguay" });
            ds.Add(new SelectListItem() { Text = "Peru", Value = "Peru" });
            ds.Add(new SelectListItem() { Text = "Phillipines", Value = "Phillipines" });
            ds.Add(new SelectListItem() { Text = "Pitcairn Island", Value = "Pitcairn Island" });
            ds.Add(new SelectListItem() { Text = "Poland", Value = "Poland" });
            ds.Add(new SelectListItem() { Text = "Portugal", Value = "Portugal" });
            ds.Add(new SelectListItem() { Text = "Puerto Rico", Value = "Puerto Rico" });
            ds.Add(new SelectListItem() { Text = "Qatar", Value = "Qatar" });
            ds.Add(new SelectListItem() { Text = "Republic of Montenegro", Value = "Republic of Montenegro" });
            ds.Add(new SelectListItem() { Text = "Republic of Serbia", Value = "Republic of Serbia" });
            ds.Add(new SelectListItem() { Text = "Reunion", Value = "Reunion" });
            ds.Add(new SelectListItem() { Text = "Romania", Value = "Romania" });
            ds.Add(new SelectListItem() { Text = "Russia", Value = "Russia" });
            ds.Add(new SelectListItem() { Text = "Rwanda", Value = "Rwanda" });
            ds.Add(new SelectListItem() { Text = "St Barthelemy", Value = "St Barthelemy" });
            ds.Add(new SelectListItem() { Text = "St Eustatius", Value = "St Eustatius" });
            ds.Add(new SelectListItem() { Text = "St Helena", Value = "St Helena" });
            ds.Add(new SelectListItem() { Text = "St Kitts-Nevis", Value = "St Kitts-Nevis" });
            ds.Add(new SelectListItem() { Text = "St Lucia", Value = "St Lucia" });
            ds.Add(new SelectListItem() { Text = "St Maarten", Value = "St Maarten" });
            ds.Add(new SelectListItem() { Text = "St Pierre & Miquelon", Value = "St Pierre & Miquelon" });
            ds.Add(new SelectListItem() { Text = "St Vincent & Grenadines", Value = "St Vincent & Grenadines" });
            ds.Add(new SelectListItem() { Text = "Saipan", Value = "Saipan" });
            ds.Add(new SelectListItem() { Text = "Samoa", Value = "Samoa" });
            ds.Add(new SelectListItem() { Text = "Samoa American", Value = "Samoa American" });
            ds.Add(new SelectListItem() { Text = "San Marino", Value = "San Marino" });
            ds.Add(new SelectListItem() { Text = "Sao Tome & Principe", Value = "Sao Tome & Principe" });
            ds.Add(new SelectListItem() { Text = "Saudi Arabia", Value = "Saudi Arabia" });
            ds.Add(new SelectListItem() { Text = "Senegal", Value = "Senegal" });
            ds.Add(new SelectListItem() { Text = "Seychelles", Value = "Seychelles" });
            ds.Add(new SelectListItem() { Text = "Sierra Leone", Value = "Sierra Leone" });
            ds.Add(new SelectListItem() { Text = "Singapore", Value = "Singapore" });
            ds.Add(new SelectListItem() { Text = "Slovakia", Value = "Slovakia" });
            ds.Add(new SelectListItem() { Text = "Slovenia", Value = "Slovenia" });
            ds.Add(new SelectListItem() { Text = "Solomon Islands", Value = "Solomon Islands" });
            ds.Add(new SelectListItem() { Text = "Somalia", Value = "Somalia" });
            ds.Add(new SelectListItem() { Text = "South Africa", Value = "South Africa" });
            ds.Add(new SelectListItem() { Text = "Spain", Value = "Spain" });
            ds.Add(new SelectListItem() { Text = "Sri Lanka", Value = "Sri Lanka" });
            ds.Add(new SelectListItem() { Text = "Sudan", Value = "Sudan" });
            ds.Add(new SelectListItem() { Text = "Suriname", Value = "Suriname" });
            ds.Add(new SelectListItem() { Text = "Swaziland", Value = "Swaziland" });
            ds.Add(new SelectListItem() { Text = "Sweden", Value = "Sweden" });
            ds.Add(new SelectListItem() { Text = "Thụy Sĩ", Value = "Thụy Sĩ" });
            ds.Add(new SelectListItem() { Text = "Syria", Value = "Syria" });
            ds.Add(new SelectListItem() { Text = "Tahiti", Value = "Tahiti" });
            ds.Add(new SelectListItem() { Text = "Taiwan", Value = "Taiwan" });
            ds.Add(new SelectListItem() { Text = "Tajikistan", Value = "Tajikistan" });
            ds.Add(new SelectListItem() { Text = "Tanzania", Value = "Tanzania" });
            ds.Add(new SelectListItem() { Text = "Thái Lan", Value = "Thái Lan" });
            ds.Add(new SelectListItem() { Text = "Togo", Value = "Togo" });
            ds.Add(new SelectListItem() { Text = "Tokelau", Value = "Tokelau" });
            ds.Add(new SelectListItem() { Text = "Tonga", Value = "Tonga" });
            ds.Add(new SelectListItem() { Text = "Trinidad & Tobago", Value = "Trinidad & Tobago" });
            ds.Add(new SelectListItem() { Text = "Tunisia", Value = "Tunisia" });
            ds.Add(new SelectListItem() { Text = "Turkey", Value = "Turkey" });
            ds.Add(new SelectListItem() { Text = "Turkmenistan", Value = "Turkmenistan" });
            ds.Add(new SelectListItem() { Text = "Turks & Caicos Is", Value = "Turks & Caicos Is" });
            ds.Add(new SelectListItem() { Text = "Tuvalu", Value = "Tuvalu" });
            ds.Add(new SelectListItem() { Text = "Uganda", Value = "Uganda" });
            ds.Add(new SelectListItem() { Text = "United Kingdom", Value = "United Kingdom" });
            ds.Add(new SelectListItem() { Text = "Ukraine", Value = "Ukraine" });
            ds.Add(new SelectListItem() { Text = "United Arab Erimates", Value = "United Arab Erimates" });
            ds.Add(new SelectListItem() { Text = "United States of America", Value = "United States of America" });
            ds.Add(new SelectListItem() { Text = "Uraguay", Value = "Uraguay" });
            ds.Add(new SelectListItem() { Text = "Uzbekistan", Value = "Uzbekistan" });
            ds.Add(new SelectListItem() { Text = "Vanuatu", Value = "Vanuatu" });
            ds.Add(new SelectListItem() { Text = "Vatican City State", Value = "Vatican City State" });
            ds.Add(new SelectListItem() { Text = "Venezuela", Value = "Venezuela" });
            ds.Add(new SelectListItem() { Text = "Việt Nam", Value = "Việt Nam" });
            ds.Add(new SelectListItem() { Text = "Virgin Islands (Brit)", Value = "Virgin Islands (Brit)" });
            ds.Add(new SelectListItem() { Text = "Virgin Islands (USA)", Value = "Virgin Islands (USA)" });
            ds.Add(new SelectListItem() { Text = "Wake Island", Value = "Wake Island" });
            ds.Add(new SelectListItem() { Text = "Wallis & Futana Is", Value = "Wallis & Futana Is" });
            ds.Add(new SelectListItem() { Text = "Yemen", Value = "Yemen" });
            ds.Add(new SelectListItem() { Text = "Zaire", Value = "Zaire" });
            ds.Add(new SelectListItem() { Text = "Zambia", Value = "Zambia" });
            ds.Add(new SelectListItem() { Text = "Zimbabwe", Value = "Zimbabwe" });
            return ds;
        }

        public List<SelectListItem> taoListTrangThai()
        {
            var dsTT = new List<SelectListItem>();
            dsTT.Add(new SelectListItem() { Text = "Có hiệu lực", Value = "False" });
            dsTT.Add(new SelectListItem() { Text = "Đã xóa", Value = "True" });

            return dsTT;
        }

        public ActionResult QuanLySanPham(int? page,string maHang = "", string maBH = "",int? thoigianBH = null,string maDM ="",
            string pb ="",
            string nl ="",
            string ld = "",
            string xs = "",
            bool? trangThai = null)
        {
            TAIKHOAN tk = (TAIKHOAN)Session["Admin"];
            if (tk == null)
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            if (page == null)
            {
                page = 1;
            }
            int pagesize = 10; //Số sản phẩm hiển thị trên 1 trang
            int pageNumber = (page ?? 1);//Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
                                         // nếu page = null thì lấy giá trị 1 cho biến pageNumber.

            bool temp = trangThai ?? false;

            List<SANPHAM> ds = db.SANPHAMs.Where(t=> t.TRANGTHAI == temp).ToList();

            if(maHang != "")
            {
                ds = ds.Where(t => t.MAHANG == maHang).ToList();
                ViewBag.maHang = maHang;
            }   
            if(maBH != "")
            {
                ds = ds.Where(t => t.MABAOHANH == maBH).ToList();
                ViewBag.maBH = maBH;
            }
            if(thoigianBH != null)
            {
                ds = ds.Where(t => t.ThoiHanBH == thoigianBH).ToList();
                ViewBag.thoigianBH = thoigianBH;
            }
            if(maDM != "")
            {
                ds = ds.Where(t => t.MADM == maDM).ToList();
                ViewBag.maDM = maDM;
            }
            if(pb != "")
            {
                ds = ds.Where(t => t.DONGSP.Contains(pb)).ToList();
                ViewBag.pb = pb;
            }
            if (nl != "")
            {
                ds = ds.Where(t => t.NANGLUONG.Contains(nl)).ToList();
                ViewBag.nl = nl;
            }
            if (ld != "")
            {
                ds = ds.Where(t => t.LOAIDAY.Contains(ld)).ToList();
                ViewBag.ld = ld;
            }

            if (xs != "")
            {
                ds = ds.Where(t => t.XUATSU.Contains(xs)).ToList();
                ViewBag.xs = xs;
            }





            dsSP = ds;
            var lst = ds.ToPagedList(pageNumber, pagesize);
            List<HANG> dsHang = db.HANGs.Where(t => t.TRANGTHAI == false).ToList();
            List<NOIDUNGBH> dsBaoHanh = db.NOIDUNGBHs.Where(t => t.TRANGTHAI == false).ToList();
            List<DANHMUCSP> dsDMSP = db.DANHMUCSPs.Where(t => t.TRANGTHAI == false).ToList();
            var dsPhienBan = taoListPhienBan();
            var dsNangLuong = taoListNangLuong();
            var dsLoaiDay = taoListLoaiDay();
            var dsThoiHanBaoHanh = taoListThoiHanBaoHanh();
            var dsQuocGia = taoListQuocGia();
            var dsTT = taoListTrangThai();
            
            ViewBag.dsSPLoad = lst;
            ViewBag.dsHang = dsHang;
            ViewBag.dsBaoHanh = dsBaoHanh;
            ViewBag.dsDMSP = dsDMSP;
            ViewBag.dsPhienBan = dsPhienBan;
            ViewBag.dsNangLuong = dsNangLuong;
            ViewBag.dsLoaiDay = dsLoaiDay;
            ViewBag.dsThoiHanBaoHanh = dsThoiHanBaoHanh;
            ViewBag.dsQuocGia = dsQuocGia;
            ViewBag.dsTT = dsTT;
            ViewBag.pageNum = pageNumber;
            ViewBag.pageSize = pagesize;
            SANPHAM sp = new SANPHAM();
            return View(sp);
        }

        [HttpPost]
        public JsonResult ThemSanPham(SANPHAM pSP, string maPN, int chietKhau)
        {
            SANPHAM sp = db.SANPHAMs.Where(t => t.MASP == pSP.MASP).FirstOrDefault();
            if (sp != null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã sản phẩm phải là duy nhất!",
                }, JsonRequestBehavior.AllowGet);
            }

            string[] mang = pSP.HINHANH.Split('\\');
            string hinh = mang[mang.Length - 1];

            SANPHAM a = new SANPHAM();
            a.MASP = pSP.MASP;
            a.TENSP = pSP.TENSP;
            a.DONGIA = pSP.DONGIA;
            a.CHITIETSP = pSP.CHITIETSP;
            a.SOLUONG = 0;
            a.MAHANG = pSP.MAHANG;
            a.MABAOHANH = pSP.MABAOHANH;
            a.MADM = pSP.MADM;
            a.DONGSP = pSP.DONGSP;
            a.KICHTHUOC = pSP.KICHTHUOC.Trim() + "mm";
            a.NANGLUONG = pSP.NANGLUONG;
            a.LOAIDAY = pSP.LOAIDAY;
            a.ThoiHanBH = pSP.ThoiHanBH;
            a.XUATSU = pSP.XUATSU;
            a.TRANGTHAI = false;
            a.MOTASP = pSP.MOTASP;
            a.HINHANH = mang[mang.Length - 1];
            db.SANPHAMs.InsertOnSubmit(a);
            db.SubmitChanges();

            PHIEUNHAP pn = db.PHIEUNHAPs.Where(t => t.MAPN == maPN).FirstOrDefault();
            if (pn == null)
            {
                return Json(new
                {
                    kq = true,
                    message = "Mã phiếu nhập này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            CHITIETPN ctpn = new CHITIETPN();
            ctpn.IDPhieuNhap = pn.IDPhieuNhap;
            ctpn.MASP = pSP.MASP;
            ctpn.SOLUONG = pSP.SOLUONG;
            ctpn.DONGIA = pSP.DONGIA;
            ctpn.CHIETKHAU = chietKhau;
            long? temp = pSP.SOLUONG * pSP.DONGIA;
            long? tt = temp * chietKhau / 100;
            ctpn.THANHTIEN = temp - tt;

            pn.TONGTIEN += ctpn.THANHTIEN;

            db.CHITIETPNs.InsertOnSubmit(ctpn);
            db.SubmitChanges();

            //db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, db.SANPHAMs);

            return Json(new
            {
                kq = true,
                tt = string.Format("{0:#,##}", pn.TONGTIEN),
                message = "Thêm thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpLoadAnh()
        {
            if (Request.Files.Count != 0)
            {
                var file = Request.Files[0];
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Images/SanPham/"), fileName);
                file.SaveAs(path);
                return Json(new
                {
                    kq = true,
                });
            }
            else
            {
                return Json(new
                {
                    kq = false,
                });
            }
        }

        public JsonResult GetSanPham(string maSP)
        {
            var sp = (from k in db.SANPHAMs
                      where k.MASP.Equals(maSP.Trim())
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
                      });
            return Json(sp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SuaSanPham(SANPHAM pSP)
        {
            SANPHAM sp = db.SANPHAMs.Where(t => t.MASP == pSP.MASP).FirstOrDefault();
            if (sp == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Sản phẩm này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            sp.TENSP = pSP.TENSP;
            sp.DONGIA = pSP.DONGIA;
            sp.CHITIETSP = pSP.CHITIETSP;
            sp.SOLUONG = pSP.SOLUONG;
            sp.MAHANG = pSP.MAHANG;
            sp.MABAOHANH = pSP.MABAOHANH;
            sp.MADM = pSP.MADM;
            sp.DONGSP = pSP.DONGSP;
            sp.KICHTHUOC = pSP.KICHTHUOC.Trim() + "mm";
            sp.NANGLUONG = pSP.NANGLUONG;
            sp.LOAIDAY = pSP.LOAIDAY;
            sp.ThoiHanBH = pSP.ThoiHanBH;
            sp.XUATSU = pSP.XUATSU;
            sp.MOTASP = pSP.MOTASP;
            sp.TRANGTHAI = false;
            if (pSP.HINHANH != null)
            {
                string[] mang = pSP.HINHANH.Split('\\');
                sp.HINHANH = mang[mang.Length - 1];

            }
            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Cập nhật sản phẩm thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult XoaSanPham(string MASP)
        {
            SANPHAM sp = db.SANPHAMs.Where(t => t.MASP == MASP).FirstOrDefault();
            if (sp == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Sản phẩm này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            sp.TRANGTHAI = true;
            db.SubmitChanges();
            return Json(new { 
                kq = true,
                message = "Xóa thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult KhoiPhucSanPham(string MASP)
        {
            SANPHAM sp = db.SANPHAMs.Where(t => t.MASP == MASP).FirstOrDefault();
            if (sp == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Sản phẩm này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            sp.TRANGTHAI = false;
            db.SubmitChanges();
            return Json(new
            {
                kq = true,
                message = "Khôi phục thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult QuanLyHang(int? page,string quocGia  = "",bool? trangThai = null)
        {
            TAIKHOAN tk = (TAIKHOAN)Session["Admin"];
            if (tk == null)
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            if (page == null)
            {
                page = 1;
            }
            int pagesize = 10; //Số sản phẩm hiển thị trên 1 trang
            int pageNumber = (page ?? 1);//Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
                                         // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            List<HANG> dsHang = db.HANGs.Where(t => t.TRANGTHAI == false).ToList(); 

            if(quocGia != "")
            {
                dsHang = dsHang.Where(t => t.QUOCGIA == quocGia).ToList();
                ViewBag.quocGia = quocGia;
            }

            if(trangThai != null)
            {
                dsHang = dsHang.Where(t => t.TRANGTHAI == trangThai).ToList();
                ViewBag.trangThai = trangThai;
            }

            dsH = dsHang;
            var lst = dsHang.ToPagedList(pageNumber, pagesize);
            var dsTT = taoListTrangThai();

            ViewBag.dsHang = lst;
            ViewBag.dsTT = dsTT;
            ViewBag.pageNum = pageNumber;
            ViewBag.pageSize = pagesize;
            ViewBag.dsQuocGia = taoListQuocGia();
            HANG a = new HANG();
            return View(a);
        }


        [HttpPost]
        public JsonResult ThemHangMoi(HANG hang)
        {
            HANG h = db.HANGs.Where(t => t.MAHANG == hang.MAHANG).FirstOrDefault();
            if (h != null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã hãng này đã tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            //Lấy hình logo
            string[] mang = hang.LOGO.Split('\\');
            string hinh = mang[mang.Length - 1];

            //Lấy hình banner
            string[] mang1 = hang.BANNER.Split('\\');
            string hinh1 = mang1[mang1.Length - 1];

            HANG a = new HANG();
            a.MAHANG = hang.MAHANG;
            a.TENHANG = hang.TENHANG;
            a.THONGTIN = hang.THONGTIN;
            a.LOGO = hinh;
            a.BANNER = hinh1;
            a.QUOCGIA = hang.QUOCGIA;
            a.URL = hang.URL;
            a.TRANGTHAI = false;
            db.HANGs.InsertOnSubmit(a);
            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Thêm hãng mới thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpLoadAnhLogoHang()
        {
            if (Request.Files.Count != 0)
            {
                var file = Request.Files[0];
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Images/Brand_Logo/"), fileName);
                file.SaveAs(path);
                return Json(new
                {
                    kq = true,
                });
            }
            else
            {
                return Json(new
                {
                    kq = false,
                });
            }
        }

        [HttpPost]
        public JsonResult UpLoadAnhBannerHang()
        {
            if (Request.Files.Count != 0)
            {
                var file = Request.Files[0];
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Images/BannerHang/"), fileName);
                file.SaveAs(path);
                return Json(new
                {
                    kq = true,
                });
            }
            else
            {
                return Json(new
                {
                    kq = false,
                });
            }
        }

        public JsonResult GetHang(string maHang)
        {
            var hang = (from k in db.HANGs
                        where k.MAHANG.Equals(maHang.Trim())
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
                        });
            return Json(hang, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SuaHang(HANG hang)
        {
            HANG h = db.HANGs.Where(t => t.MAHANG == hang.MAHANG).FirstOrDefault();
            if (h == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã hãng này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            h.MAHANG = hang.MAHANG;
            h.TENHANG = hang.TENHANG;
            h.THONGTIN = hang.THONGTIN;
            h.QUOCGIA = hang.QUOCGIA;
            h.URL = hang.URL;
            h.TRANGTHAI = false;
            if (hang.LOGO != null)
            {
                //Lấy hình logo
                string[] mang = hang.LOGO.Split('\\');
                string hinh = mang[mang.Length - 1];
                h.LOGO = hinh;
            }
            if (hang.BANNER != null)
            {
                //Lấy hình banner
                string[] mang1 = hang.BANNER.Split('\\');
                string hinh1 = mang1[mang1.Length - 1];
                h.BANNER = hinh1;
            }

            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Cập nhật hãng thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult XoaHang(string maHang)
        {
            HANG h = db.HANGs.Where(t => t.MAHANG == maHang).FirstOrDefault();
            if (h == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã hãng này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            h.TRANGTHAI = true;
            db.SubmitChanges();
            return Json(new
            {
                kq = true,
                message = "Xóa thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult KhoiPhucHang(string maHang)
        {
            HANG h = db.HANGs.Where(t => t.MAHANG == maHang).FirstOrDefault();
            if (h == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã hãng này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            h.TRANGTHAI = false;
            db.SubmitChanges();
            return Json(new
            {
                kq = true,
                message = "Khôi phục thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult QuanLyDanhMuc(int? page,bool? trangThai = null)
        {
            TAIKHOAN tk = (TAIKHOAN)Session["Admin"];
            if (tk == null)
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            if (page == null)
            {
                page = 1;
            }
            int pagesize = 10; //Số sản phẩm hiển thị trên 1 trang
            int pageNumber = (page ?? 1);//Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
                                         // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            List<DANHMUCSP> dsDanhMuc = db.DANHMUCSPs.Where(t => t.TRANGTHAI == false).ToList();

            if(trangThai!= null)
            {
                dsDanhMuc = dsDanhMuc.Where(t => t.TRANGTHAI == trangThai).ToList();
                ViewBag.trangThai = trangThai;
            }

            dsDM = dsDanhMuc;
            var lst = dsDanhMuc.ToPagedList(pageNumber, pagesize);
            var dsTT = taoListTrangThai();

            ViewBag.dsDM = lst;
            ViewBag.dsTT = dsTT;
            ViewBag.pageNum = pageNumber;
            ViewBag.pageSize = pagesize;
            DANHMUCSP a = new DANHMUCSP();
            return View(a);
        }

        [HttpPost]
        public JsonResult ThemDanhMucMoi(DANHMUCSP dmSP)
        {
            DANHMUCSP h = db.DANHMUCSPs.Where(t => t.MADM == dmSP.MADM).FirstOrDefault();
            if (h != null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã danh mục này đã tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            DANHMUCSP a = new DANHMUCSP();
            a.MADM = dmSP.MADM;
            a.TENDM = dmSP.TENDM;
            a.TRANGTHAI = false;

            db.DANHMUCSPs.InsertOnSubmit(a);
            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Thêm danh mục sản phẩm mới thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDanhMuc(string maDM)
        {
            var danhMuc = (from k in db.DANHMUCSPs
                        where k.MADM.Equals(maDM.Trim())
                        select new
                        {
                            k.MADM,
                            k.TENDM,
                            k.TRANGTHAI,
                        });
            return Json(danhMuc, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SuaDanhMuc(DANHMUCSP dmSP)
        {
            DANHMUCSP dm = db.DANHMUCSPs.Where(t => t.MADM == dmSP.MADM).FirstOrDefault();
            if (dm == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã danh mục này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            dm.TENDM = dmSP.TENDM;
            dm.TRANGTHAI = false;

            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Cập nhật danh mục thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult XoaDanhMuc(string maDM)
        {
            DANHMUCSP danhMuc = db.DANHMUCSPs.Where(t => t.MADM == maDM).FirstOrDefault();
            if (danhMuc == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã danh mục này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            danhMuc.TRANGTHAI = true;
            db.SubmitChanges();
            return Json(new
            {
                kq = true,
                message = "Xóa thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult KhoiPhuDanhMuc(string maDM)
        {
            DANHMUCSP danhMuc = db.DANHMUCSPs.Where(t => t.MADM == maDM).FirstOrDefault();
            if (danhMuc == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Mã danh mục này không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            danhMuc.TRANGTHAI = false;
            db.SubmitChanges();
            return Json(new
            {
                kq = true,
                message = "Khôi phục thành công!",
            }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult loadAnhSummernoteSanPham(HttpPostedFileBase fupload)
        {
            if (Request.Files.Count != 0)
            {
                var file = Request.Files[0];
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Images/SanPham/MoTaSanPham/"), fileName);
                WebImage webImage = new WebImage(file.InputStream);
                webImage.Resize(803, 452);
                webImage.Save(path);
                //file.SaveAs(path);
                string duongDan = Convert.ToString(path);
                return Json(new
                {
                    kq = true,
                    data = fileName,
                }, JsonRequestBehavior.AllowGet);

            }
            return Json(new
            {
                kq = false,
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult XoaAnhSummernoteSanPham(string filePath)
        {
            string[] mang = filePath.Split('/');
            var fileName = mang[mang.Length - 1];
            var path = Path.Combine(Server.MapPath("~/Content/Images/SanPham/MoTaSanPham/"), fileName);
            FileInfo fileA = new FileInfo(path);
            if (fileA.Exists)
            {
                fileA.Delete();
            }
            return Json(new
            {
                kq = true,
            }, JsonRequestBehavior.AllowGet);
        }
    }
}