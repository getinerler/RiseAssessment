using Hangfire;

namespace ReportMicroservice.Hangfire
{
    public class HangfireJobScheduler
    {
        public static void ScheduleJobs()
        {
            ScheduleGetReportRequestsJob();
        }

        public static void ScheduleGetReportRequestsJob()
        {
            RecurringJob.RemoveIfExists(nameof(GetReportRequestsJob));
            RecurringJob.AddOrUpdate<GetReportRequestsJob>(nameof(GetReportRequestsJob),
              job => job.Run(JobCancellationToken.Null, null), Cron.MinuteInterval(1));
        }
    }
}
