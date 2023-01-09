using System;

namespace ECommerce.API.Schedulers
{
    public class JobSchedule
    {
        public JobSchedule(Type jobType,  int intervalInSecond)
        {
            JobType = jobType;
            IntervalInSecond = intervalInSecond; 
        }
        public Type JobType { get; } 
        public int IntervalInSecond { get; set; }

    }
}
