namespace TestWebApi.Schedulers.Jobs;
public class DeleteExceptionLogsJob : IJob
{
    private readonly IExceptionLogService _exceptionLogService;
    private readonly SchedulerBase _schedulerBase;

    public DeleteExceptionLogsJob(
        IExceptionLogService exceptionLogService,
        SchedulerBase schedulerBase
    )
    {
        _exceptionLogService = exceptionLogService;
        _schedulerBase = schedulerBase;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        async Task<string> Do()
        {
            var count = await _exceptionLogService.DeleteExpiredExceptionLogAsync();
            return $"2주가 지난 Exception Log 삭제 총{count}건 ";
        }

        await _schedulerBase.RunSchedulerAsync(SchedulerJobType.deleteExpiredExceptionLogs, Do);
    }
}
