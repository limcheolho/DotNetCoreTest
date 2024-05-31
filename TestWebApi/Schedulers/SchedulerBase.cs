using Quartz;
using TestWebApi.Services.Logging.ExceptionLogService;
using TestWebApi.Services.Logging.SchedulerLogService;

namespace TestWebApi.Schedulers;

/// <summary>
/// 스케줄러 베이스
/// </summary>
public class SchedulerBase
{
    private readonly ISchedulerLogService _schedulerLogService;
    private readonly IExceptionLogService _exceptionLogService;

    public SchedulerBase(
        ISchedulerLogService schedulerLogService,
        IExceptionLogService exceptionLogService
    )
    {
        _schedulerLogService = schedulerLogService;
        _exceptionLogService = exceptionLogService;
    }

    /// <summary>
    /// 배치 로직
    /// </summary>
    /// <param name="jobType"></param>
    /// <param name="func"></param>
    /// <exception cref="JobExecutionException"></exception>
    public async Task RunSchedulerAsync(SchedulerJobType jobType, Func<Task<string>> func)
    {
        var jobNo = 0;
        try
        {
            //로그 insert
            jobNo = await _schedulerLogService.InsertLogAsync(
                new SchedulerLog() { jobType = jobType.ToString() }
            );

            //배치 실행
            var result = await func();

            //로그에 완료 여부 체크
            await _schedulerLogService.UpdateLogAsync(
                new SchedulerLog()
                {
                    jobNo = jobNo,
                    resultType = SchedulerJobResultType.success.ToString(),
                    remarks = result,
                }
            );
        }
        catch (Exception e)
        {
            //exception 로그에 기록
            await _exceptionLogService.InsertExceptionLogAsync(e);

            //스케줄러 로그에 기록
            await _schedulerLogService.UpdateLogAsync(
                new SchedulerLog()
                {
                    jobNo = jobNo,
                    resultType = SchedulerJobResultType.failed.ToString(),
                    remarks = e.ToString()
                }
            );

            throw new JobExecutionException(
                msg: "errrorrrrorrr",
                refireImmediately: true,
                cause: e
            );
        }
    }
}
