using Quartz;
using TestWebApi.Schedulers.Jobs;

namespace TestWebApi.Startup;

public static class QuartzConfiguration
{
    public static void QuartzStartup(this IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            //로그 삭제
            var deleteApiLogsJobKey = new JobKey("DeleteApiLogsJob");
            q.AddJob<DeleteApiLogsJob>(opts => opts.WithIdentity(deleteApiLogsJobKey));
            q.AddTrigger(opts =>
                opts.ForJob(deleteApiLogsJobKey)
                    .WithIdentity("DeleteApiLogsJob-trigger")
                    //오전 3시에 작업
                    // .StartNow()
                    .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(03, 00))
            );

            //Exception로그 삭제
            var deleteExceptionLogsJobKey = new JobKey("DeleteExceptionLogsJob");
            q.AddJob<DeleteExceptionLogsJob>(opts => opts.WithIdentity(deleteExceptionLogsJobKey));
            q.AddTrigger(opts =>
                opts.ForJob(deleteExceptionLogsJobKey)
                    .WithIdentity("DeleteExceptionLogsJob-trigger")
                    .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(03, 30))
            );

            //유효기간 지난 RefreshToken 삭제
            var deleteUnusedRefreshTokensJobKey = new JobKey("DeleteUnusedRefreshTokensJob");
            q.AddJob<DeleteUnusedRefreshTokensJob>(opts =>
                opts.WithIdentity(deleteUnusedRefreshTokensJobKey)
            );
            q.AddTrigger(opts =>
                opts.ForJob(deleteUnusedRefreshTokensJobKey)
                    .WithIdentity("DeleteUnusedRefreshTokensJob-trigger")
                    .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(03, 30))
            );
        });
    }
}
