using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuanVan_ShopDongHo.Models;

namespace LuanVan_ShopDongHo.Controllers
{
    public class BinhLuanController : Controller
    {
        // GET: BinhLuan
        ShopDongHoDataContext db = new ShopDongHoDataContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadBinhLuan(string maSP)
        {
            var dsBL = (from k in db.BINHLUANs
                        where k.MASP == maSP && k.TRANGTHAI == false  && k.ISTINTUC == false
                        select new BinhLuanSanPham
                        {
                            idBinhLuan = k.IDBINHLUAN,
                            noiDung = k.NOIDUNG,
                            ngayTao = k.NGAYTAO,
                            tenDN = k.TENDN,
                            trangThai = k.TRANGTHAI,
                            maSP = k.MASP,
                            idTinTuc = k.IDTINTUC,
                            isTinTuc = k.ISTINTUC,
                        }).OrderByDescending(t => t.ngayTao).ToList();

            for (int i = 0; i < dsBL.Count; i++)
            {
                KHACHHANG kh = db.KHACHHANGs.FirstOrDefault(t => t.TENDN == dsBL[i].tenDN);
                if (kh != null)
                {
                    dsBL[i].tenNguoiBL = kh.TENKH;
                }
            }
            return PartialView(dsBL);
        }

        public JsonResult GetBinhLuan(int IDBINHLUAN)
        {
            var blsp = (from k in db.BINHLUANs
                        where k.IDBINHLUAN == IDBINHLUAN
                        select new
                        {
                            k.IDBINHLUAN,
                            k.IDTINTUC,
                            k.TENDN,
                            k.TRANGTHAI,
                            k.NGAYTAO,
                            k.NOIDUNG,
                            k.ISTINTUC,
                            k.MASP,
                        });
            return Json(blsp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult XoaBinhLuan(int IDBINHLUAN)
        {
            BINHLUAN binhLuan = db.BINHLUANs.Where(t => t.IDBINHLUAN == IDBINHLUAN).FirstOrDefault();
            if (binhLuan == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Bình luận không tồn tại! Xin vui lòng thử lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            binhLuan.TRANGTHAI = true;

            db.SubmitChanges();

            return Json(new
            {
                kq = true,
                message = "Xóa bình luận thành công!",
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DangBinhLuan(BINHLUAN bl)
        {
            TAIKHOAN tk = (TAIKHOAN)Session["User"];
            if (tk == null)
            {
                return Json(new
                {
                    kq = false,
                    message = "Phiên đã hết hạn! Xin vui lòng đăng nhập lại!",
                }, JsonRequestBehavior.AllowGet);
            }

            BINHLUAN binhLuan = db.BINHLUANs.Where(t => t.IDBINHLUAN == bl.IDBINHLUAN).FirstOrDefault();
            if (binhLuan == null)
            {
                BINHLUAN blsp = new BINHLUAN();
                blsp.MASP = bl.MASP;
                blsp.IDTINTUC = bl.IDTINTUC;
                blsp.NOIDUNG = bl.NOIDUNG;
                if(bl.IDTINTUC != null)
                {
                    blsp.ISTINTUC = true;
                }
                else
                {
                    blsp.ISTINTUC = false;
                }               
                blsp.TRANGTHAI = false;
                blsp.NGAYTAO = DateTime.Now;
                blsp.TENDN = tk.TENDN;
                db.BINHLUANs.InsertOnSubmit(blsp);
                db.SubmitChanges();
                return Json(new
                {
                    kq = true,
                    message = "Đăng bình luận thành công!",
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                binhLuan.NOIDUNG = bl.NOIDUNG;
                if (binhLuan.IDTINTUC != null)
                {
                    binhLuan.ISTINTUC = true;
                }
                else
                {
                    binhLuan.ISTINTUC = false;
                }
                binhLuan.TRANGTHAI = false;
                binhLuan.NGAYTAO = DateTime.Now;
                db.SubmitChanges();
                return Json(new
                {
                    kq = true,
                    message = "Bình luận của bạn đã được cập nhật!",
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult LoadBinhLuanTinTuc(int idTinTuc)
        {
            var dsBL = (from k in db.BINHLUANs
                        where k.IDTINTUC == idTinTuc && k.TRANGTHAI == false && k.ISTINTUC == true
                        select new BinhLuanSanPham
                        {
                            idBinhLuan = k.IDBINHLUAN,
                            noiDung = k.NOIDUNG,
                            ngayTao = k.NGAYTAO,
                            tenDN = k.TENDN,
                            trangThai = k.TRANGTHAI,
                            maSP = k.MASP,
                            idTinTuc = k.IDTINTUC,
                            isTinTuc = k.ISTINTUC,
                        }).OrderByDescending(t => t.ngayTao).ToList();

            for (int i = 0; i < dsBL.Count; i++)
            {
                KHACHHANG kh = db.KHACHHANGs.FirstOrDefault(t => t.TENDN == dsBL[i].tenDN);
                if (kh != null)
                {
                    dsBL[i].tenNguoiBL = kh.TENKH;
                }
            }
            return PartialView(dsBL);
        }

    }
}