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
    public class CommonApiController : Controller
    {
        // GET: CommonApi
        public ActionResult Index()
        {
            return View();
        }

        public bool isAdmin(string tenDN)
        {
            try
            {
                using (var db = new ShopDongHoDataContext())
                {
                    var tk = db.TAIKHOANs.FirstOrDefault(t => t.TENDN == tenDN);
                    var gp = (from gr in db.GROUPPERMISSIONs
                              join gn in db.GROUPNAMEs on gr.IDGROUP equals gn.IDGROUP
                              where gr.TENDN == tk.TENDN
                              select gr).FirstOrDefault();
                    if (gp != null && gp.IDGROUP == 1)
                        return true;
                    Session["Admin"] = tk;
                    Session["User"] = tk;
                    return false;
                }    
            }
            catch
            {
                return false;
            }
        }
    }
}