using Quartz;
using TestWebApi.Services.Logging.ExceptionLogService;
using TestWebApi.Services.Logging.SchedulerLogService;
using TestWebApi.Services.TelegramService;

namespace TestWebApi.Schedulers;

/// <summary>
/// 스케줄러 베이스
/// </summary>
public class SchedulerBase
{
    private readonly ISchedulerLogService _schedulerLogService;
    private readonly IExceptionLogService _exceptionLogService;
    private readonly ITelegramService _telegramService;

    public SchedulerBase(
        ISchedulerLogService schedulerLogService,
        IExceptionLogService exceptionLogService,
        ITelegramService telegramService

    )
    {
        _schedulerLogService = schedulerLogService;
        _exceptionLogService = exceptionLogService;
        _telegramService = telegramService;

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

            await _telegramService.SendSchedulerStartAsync(jobType, jobNo);

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
            
            //성공시 텔레그램 메시지 전송
            await _telegramService.SendSchedulerTerminatedAsync(jobType, jobNo, result);

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
