using TestWebApi.Helpers;
using TestWebApi.Services.Logging.ExceptionLogService;

namespace TestWebApi.Services.Logging.SchedulerLogService;

/// <summary>
/// 스케줄러 잡 로그 서비스
/// </summary>
public class SchedulerLogService : ISchedulerLogService
{
    private readonly TestDbContext _dbContext;

    private readonly IExceptionLogService _exceptionLogService;
    private readonly SystemInfoExtensions _systemInfoExtensions;

    public SchedulerLogService(
        TestDbContext dbContext,
        IExceptionLogService exceptionLogService,
        SystemInfoExtensions systemInfoExtensions
    )
    {
        _dbContext = dbContext;
        _exceptionLogService = exceptionLogService;
        _systemInfoExtensions = systemInfoExtensions;
    }

    /// <summary>
    /// 로그 저장
    /// </summary>
    /// <param name="log"></param>
    /// <returns></returns>
    public async Task<int> InsertLogAsync(SchedulerLog log)
    {
        try
        {
            _systemInfoExtensions.SetModelLogInfo(log);
            await _dbContext.SchedulerLogs.AddAsync(log);
            await _dbContext.SaveChangesAsync();
            var id = log.jobNo;
            return id;
        }
        catch (Exception e)
        {
            _ = _exceptionLogService.InsertExceptionLogAsync(e);
            throw;
        }
    }

    /// <summary>
    /// 로그 작업 종료 이후 업데이트
    /// </summary>
    /// <param name="log"></param>
    /// <returns></returns>
    public async Task UpdateLogAsync(SchedulerLog log)
    {
        try
        {
            var data = await _dbContext.SchedulerLogs.SingleAsync(g => g.jobNo == log.jobNo);
            data.resultType = log.resultType;
            data.remarks = log.remarks;
            _systemInfoExtensions.SetModelLogInfo(data);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _ = _exceptionLogService.InsertExceptionLogAsync(e);
            throw;
        }
    }
}
