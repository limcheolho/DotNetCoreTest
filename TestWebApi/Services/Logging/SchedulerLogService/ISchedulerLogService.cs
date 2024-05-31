namespace TestWebApi.Services.Logging.SchedulerLogService;


/// <summary>
/// 스케줄러 잡 로그 서비스
/// </summary>
public interface ISchedulerLogService
{
    /// <summary>
    /// 스케줄러 로그 insert
    /// </summary>
    /// <param name="log"></param>
    /// <returns></returns>
    public Task<int> InsertLogAsync(SchedulerLog log);

    /// <summary>
    /// 스케줄러 로그 update
    /// </summary>
    /// <param name="log"></param>
    /// <returns></returns>
    public Task UpdateLogAsync(SchedulerLog log);
}