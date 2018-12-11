using Quartz;
using Quartz.Impl;
using System;

namespace IssueManagementSystem
{
    public class JobScheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<SendNotificationToLevel2>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s => s.WithIntervalInMinutes(30).OnEveryDay()
                   
                  )
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}