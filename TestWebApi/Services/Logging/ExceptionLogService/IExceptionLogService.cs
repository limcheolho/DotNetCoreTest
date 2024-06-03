using System.Runtime.CompilerServices;
using TestWebApi.DataContext.Models;

namespace TestWebApi.Services.Logging.ExceptionLogService;

/// <summary>
/// exception 로그
/// </summary>
public interface IExceptionLogService
{
    /// <summary>
    /// 컨트롤러에서 발생한 Exception로그 저장
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public Task InsertControllerExceptionLogAsync(ExceptionLog model);

    /// <summary>
    /// Exception로그 저장
    /// </summary>
    /// <param name="inner"></param>
    /// <param name="callerName"></param>
    public Task InsertExceptionLogAsync(
        Exception inner,
        [CallerMemberName] string? callerName = null
    );

    /// <summary>
    /// 2주일이 지난 에러 로그 삭제
    /// </summary>
    /// <returns></returns>
    public Task<int> DeleteExpiredExceptionLogAsync();
}
