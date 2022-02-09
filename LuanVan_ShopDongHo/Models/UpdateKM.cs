using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Quartz;
using Quartz.Impl;

namespace LuanVan_ShopDongHo.Models
{
    public class UpdateKM : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            DateTime today = DateTime.Now;
            using (var db = new ShopDongHoDataContext())
            {
                var km = db.KHUYENMAIs.ToList();
                foreach (var item in km)
                {
                    if(item.THOIGIANKT.Value < today)
                    {
                        item.TINHTRANG = "Kết thúc";
                    }
                    else if(item.THOIGIANBD.Value > today)
                    {
                        item.TINHTRANG = "Sắp diễn ra";
                    }
                    else
                    {
                        item.TINHTRANG = "Đang diễn ra";
                    }
                }
                db.SubmitChanges();
            }
            throw new NotImplementedException();
        }

    }
}