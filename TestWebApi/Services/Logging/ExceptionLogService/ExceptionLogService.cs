using System.Diagnostics;
using TestWebApi.Helpers;

namespace TestWebApi.Services.Logging.ExceptionLogService;

public class ExceptionLogService : IExceptionLogService
{
    private readonly TestDbContext _dbContext;
    private readonly ExceptionLogHelper _exceptionLogHelper;
    private readonly SystemInfoExtensions _systemInfoExtensions;

    public ExceptionLogService(
        TestDbContext dbContext,
        ExceptionLogHelper exceptionLogHelper,
        SystemInfoExtensions systemInfoExtensions
    )
    {
        _dbContext = dbContext;
        _exceptionLogHelper = exceptionLogHelper;
        _systemInfoExtensions = systemInfoExtensions;
    }

    public async Task InsertControllerExceptionLogAsync(ExceptionLog model)
    {
        try
        {
            _systemInfoExtensions.SetModelLogInfo(model);
            await _dbContext.ExceptionLogs.AddAsync(model);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _exceptionLogHelper.Log(e);
            throw;
        }
    }

    public async Task InsertExceptionLogAsync(Exception inner, string callerName = null)
    {
        try
        {
            var methodInfo = new StackTrace().GetFrame(1)!.GetMethod();

            var model = new ExceptionLog()
            {
                methodName = callerName,
                stackTrace = inner.StackTrace,
                message = inner.Message
            };
            _systemInfoExtensions.SetModelLogInfo(model);
            await _dbContext.ExceptionLogs.AddAsync(model);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _exceptionLogHelper.Log(e);
        }
    }

    public async Task<int> DeleteExpiredExceptionLogAsync()
    {
        var data = await _dbContext
            .ExceptionLogs.Where(g => g.createdAt <= DateTime.Now.AddDays(-14))
            .ToListAsync();
        var count = data.Count;
        _dbContext.RemoveRange(data);
        await _dbContext.SaveChangesAsync();
        return count;
    }
}
