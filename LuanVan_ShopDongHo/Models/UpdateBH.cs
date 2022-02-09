using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Quartz;
using Quartz.Impl;

namespace LuanVan_ShopDongHo.Models
{
    public class UpdateBH : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            DateTime today = DateTime.Now;
            using (var db = new ShopDongHoDataContext())
            {
                var bh = db.BAOHANHs.ToList();
                foreach(var item in bh)
                {
                    if(item.THOIGIANKT.Value < today)
                    {
                        item.TINHTRANG = false;
                    }
                    else
                    {
                        item.TINHTRANG = true;
                    }
                }
                db.SubmitChanges();
            }
            throw new NotImplementedException();
        }
    }
}