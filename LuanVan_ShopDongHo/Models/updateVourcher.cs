using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Quartz;
using Quartz.Impl;
namespace LuanVan_ShopDongHo.Models
{
    public class updateVourcher : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            DateTime today = DateTime.Now;
            using (var db = new ShopDongHoDataContext())
            {
                var ctvourcher = db.CHUONGTRINHVOUCHERs.ToList();
                foreach (var item in ctvourcher)
                {
                    if (item.NGAYKETTHUC.Value < today)
                    {
                        item.TINHTRANG = 3;
                    }
                    else if (item.NGAYTAO.Value > today)
                    {
                        item.TINHTRANG = 1;
                    }
                    else
                    {
                        item.TINHTRANG = 2;
                    }
                }
                db.SubmitChanges();
            }
            throw new NotImplementedException();
        }
    }
}