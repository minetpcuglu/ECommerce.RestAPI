using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;

namespace ECommerce.API.Schedulers
{
    public class QuartzHostedService : IHostedService
    {
        private readonly ISchedulerFactory schedulerFactory;
        private readonly IJobFactory jobFactory;
        private readonly IEnumerable<JobSchedule> jobSchedules;
        public QuartzHostedService(
            ISchedulerFactory schedulerFactory,
            IJobFactory jobFactory,
            IEnumerable<JobSchedule> jobSchedules)
        {
            this.schedulerFactory = schedulerFactory;
            this.jobSchedules = jobSchedules;
            this.jobFactory = jobFactory;
        }
        public IScheduler Scheduler { get; set; }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Debug.WriteLine("__________________________________________________________________________");
            Debug.WriteLine("__________________________________StartAsync______________________________");
            Debug.WriteLine("__________________________________________________________________________");
            Scheduler = await schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = jobFactory;
            await Scheduler.Start(cancellationToken);

            //Scheduler = await new StdSchedulerFactory().GetScheduler();
            //await Scheduler.Start();



            foreach (var jobSchedule in jobSchedules)
            {
                var job = CreateJob(jobSchedule);
                var trigger = CreateTrigger(jobSchedule);
                await Scheduler.ScheduleJob(job, trigger, cancellationToken);
            }
           
        }
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            Debug.WriteLine("__________________________________________________________________________");
            Debug.WriteLine("__________________________________StopAsync_______________________________");
            Debug.WriteLine("__________________________________________________________________________");
            await Scheduler.Shutdown(cancellationToken);
        }
        private static IJobDetail CreateJob(JobSchedule schedule)
        {
            Debug.WriteLine("__________________________________________________________________________");
            Debug.WriteLine("__________________________________CreateJob_______________________________");
            Debug.WriteLine("__________________________________________________________________________");
            var jobType = schedule.JobType;
            return JobBuilder
                .Create(jobType)
                .WithIdentity(jobType.FullName)
                .WithDescription(jobType.Name)
                .Build();
 
        }
        private static ITrigger CreateTrigger(JobSchedule schedule)
        {
            Debug.WriteLine("__________________________________________________________________________");
            Debug.WriteLine("__________________________________CreateTrigger___________________________");
            Debug.WriteLine("__________________________________________________________________________");
            return TriggerBuilder
                .Create()
                 .WithIdentity($"{schedule.JobType.FullName}.trigger")
               // .WithIdentity($"{schedule.JobName}")
                .StartNow() //https://enesaysan.com/software/quartz-net-core/
                .WithSimpleSchedule(builder => builder.WithIntervalInSeconds(schedule.IntervalInSecond).RepeatForever())
                //.StartAt(DateTime.Now.AddSeconds(5))
               // .WithCronSchedule(schedule.CronExpression)
               
                //.WithDescription(schedule.CronExpression)
                .Build();
        }
    }
}
