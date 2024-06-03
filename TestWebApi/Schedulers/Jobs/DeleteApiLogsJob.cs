namespace TestWebApi.Schedulers.Jobs;

public class DeleteApiLogsJob : IJob
{
    private readonly IApiLogService _apiLogService;
    private readonly SchedulerBase _schedulerBase;

    public DeleteApiLogsJob(IApiLogService apiLogService, SchedulerBase schedulerBase)
    {
        _apiLogService = apiLogService;
        _schedulerBase = schedulerBase;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await _schedulerBase.RunSchedulerAsync(jobType: SchedulerJobType.deleteApiLogs, func: Do);

        async Task<string> Do()
        {
            var count = await _apiLogService.DeleteLogAsync();
            return $"apiLog 삭제 성공 총 {count}건";
        }
    }
}
