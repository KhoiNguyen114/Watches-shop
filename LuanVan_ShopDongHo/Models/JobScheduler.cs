using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Quartz;
using Quartz.Impl;

namespace LuanVan_ShopDongHo.Models
{
    public class JobScheduler
    {
        public static async Task StartAsync()
        {
            //Task update KM
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            IScheduler mScheduler = await schedulerFactory.GetScheduler();

            IJobDetail updateKM = JobBuilder.Create<UpdateKM>().WithIdentity("UpdateKMJob").Build();
            

            ITrigger triggerKM = (ITrigger)TriggerBuilder.Create()
                .WithIdentity("UpdateKMTrigger")
                .WithDailyTimeIntervalSchedule
                (s => s.WithIntervalInHours(24).OnEveryDay().StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0)))
                .ForJob("UpdateKMJob")
                .Build();

            if (await mScheduler.CheckExists(updateKM.Key) == true)
            {
                await mScheduler.DeleteJob(updateKM.Key);
            }
            await mScheduler.ScheduleJob(updateKM, triggerKM);
            await mScheduler.Start();

            // Task update BH
            IJobDetail updateBH = JobBuilder.Create<UpdateBH>().WithIdentity("UpdateBHJob").Build();

            ITrigger triggerBH = (ITrigger)TriggerBuilder.Create()
            .WithIdentity("UpdateBHTrigger")
            .WithDailyTimeIntervalSchedule
            (s => s.WithIntervalInHours(24).OnEveryDay().StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0)))
            .ForJob("UpdateBHJob")
            .Build();

            if (await mScheduler.CheckExists(updateBH.Key) == true)
            {
                await mScheduler.DeleteJob(updateBH.Key);
            }

            await mScheduler.ScheduleJob(updateBH, triggerBH);

            await mScheduler.Start();

            // Task update vourcher
            IJobDetail updateVourcher = JobBuilder.Create<updateVourcher>().WithIdentity("UpdateVourcherJob").Build();
            ITrigger triggerVourcher = (ITrigger)TriggerBuilder.Create()
            .WithIdentity("UpdateVourcherTrigger")
            .WithDailyTimeIntervalSchedule
            (s => s.WithIntervalInHours(24).OnEveryDay().StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0)))
            .ForJob("UpdateVourcherJob")
            .Build();
            if (await mScheduler.CheckExists(updateVourcher.Key) == true)
            {
                await mScheduler.DeleteJob(updateVourcher.Key);
            }
            await mScheduler.ScheduleJob(updateVourcher, triggerVourcher);

            await mScheduler.Start();

        }
    }
}