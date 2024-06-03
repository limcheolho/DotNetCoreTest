namespace TestWebApi.Services.Logging.ApiLogService;

public class ApiLogService : IApiLogService
{
    private readonly TestDbContext _dbContext;
    private readonly SystemInfoExtensions _systemInfoExtensions;

    public ApiLogService(TestDbContext dbContext, SystemInfoExtensions systemInfoExtensions)
    {
        _dbContext = dbContext;
        _systemInfoExtensions = systemInfoExtensions;
    }

    /// <summary>
    /// 로그 insert
    /// </summary>
    /// <param name="apiLog"></param>
    /// <returns></returns>
    public async Task<int> InsertLogAsync(ApiLog apiLog)
    {
        apiLog.createdAt = DateTime.Now;
        if (apiLog.reqContents != null)
            apiLog.reqContents = apiLog.reqContents.TruncateString(4000);
        _systemInfoExtensions.SetModelLogInfo(apiLog);

        await _dbContext.ApiLogs.AddAsync(apiLog);
        var result = await _dbContext.SaveChangesAsync();
        var id = apiLog.logNo;
        return id;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="apiLog"></param>
    /// <returns></returns>
    public async Task<bool> UpdateLogAsync(ApiLog apiLog)
    {
        var data = await _dbContext.ApiLogs.FirstOrDefaultAsync(g => g.logNo == apiLog.logNo);
        if (data == null)
            return false;

        if (apiLog.resContents != null)
            data.resContents = apiLog.resContents.TruncateString(4000);
        data.statusCode = apiLog.statusCode;
        data.elapsedSec = apiLog.elapsedSec;

        _systemInfoExtensions.SetSystemModelLogInfo(data);

        var result = await _dbContext.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// 로그 삭제 (스케줄러 잡에서 사용)
    /// </summary>
    /// <returns></returns>
    public async Task<int> DeleteLogAsync()
    {
        var data = await _dbContext
            .ApiLogs.Where(g => g.createdAt <= DateTime.Now.AddDays(-14))
            .ToListAsync();
        var count = data.Count;
        _dbContext.RemoveRange(data);
        await _dbContext.SaveChangesAsync();
        return count;
    }
}
