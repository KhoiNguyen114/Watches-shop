using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using LuanVan_ShopDongHo.Models;
using Rotativa;

namespace LuanVan_ShopDongHo.Controllers
{
    public class XuatFileController : Controller
    {
        ShopDongHoDataContext db = new ShopDongHoDataContext();


        // GET: XuatFile
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult XuatFilePhieuNhap(int idPN)
        {
            GridView gv = new GridView();
            gv.DataSource = (from k in db.CHITIETPNs
                             join h in db.SANPHAMs on k.MASP equals h.MASP
                             where k.IDPhieuNhap == idPN
                             select new ChiTietPhieuNhap
                             {
                                 IDPhieuNhap = k.IDPhieuNhap,
                                 tenSP = h.TENSP,
                                 donGia = k.DONGIA,
                                 soLuong = k.SOLUONG,
                                 thanhTien = k.THANHTIEN,
                             }).ToList();
            gv.DataBind();
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ExportPhieuNhap.xls");
            //Mã hóa chữa sang UTF8
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                //Apply text style to each Row
                gv.Rows[i].Attributes.Add("class", "textmode");
            }
            //Add màu nền cho header của file excel
            gv.HeaderRow.BackColor = System.Drawing.Color.DarkBlue;
            //Màu chữ cho header của file excel
            gv.HeaderStyle.ForeColor = System.Drawing.Color.White;
                       
            gv.HeaderRow.Cells[0].Text = "ID Phiếu nhập";
            gv.HeaderRow.Cells[1].Text = "Tên sản phẩm";
            gv.HeaderRow.Cells[2].Text = "Số lượng";
            gv.HeaderRow.Cells[3].Text = "Đơn giá";
            gv.HeaderRow.Cells[4].Text = "Thành tiền";
            gv.RenderControl(hw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View();
        }


    }
}